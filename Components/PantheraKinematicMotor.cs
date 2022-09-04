using KinematicCharacterController;
using RoR2;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace Panthera.Components
{

	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]

	class PantheraKinematicMotor : KinematicCharacterMotor
	{

		public PantheraMotor pantheraMotor;

		public void DoInit()
		{
			this.pantheraMotor = base.GetComponent<PantheraMotor>();
		}

		private void Awake()
		{
			Transform = base.transform;
			ValidateData();
			InitialTickPosition = Transform.position;
			InitialTickRotation = Transform.rotation;
			TransientPosition = Transform.position;
			TransientRotation = Transform.rotation;
			RebuildCollidableLayers();
			if ((bool)CharacterController)
			{
				CharacterController.SetupCharacterMotor(this);
			}
			SetCapsuleDimensions(CapsuleRadius, CapsuleHeight, CapsuleYOffset);
		}

		#region Update Phase 1
		// ----------------------------------------------------------------------------------------------------------------------------- //
		// ----------------------------------------------------------- UPDATE PHASE 1 -------------------------------------------------- //
		// ----------------------------------------------------------------------------------------------------------------------------- //
		public new void UpdatePhase1(float deltaTime)
		{

            #region Checks
            // Check if the velocity isn't null //
            if (float.IsNaN(_baseVelocity.x) || float.IsNaN(_baseVelocity.y) || float.IsNaN(_baseVelocity.z))
			{
				_baseVelocity = Vector3.zero;
			}


			// Check if the RigidBody velocity isn't null //
			if (float.IsNaN(_attachedRigidbodyVelocity.x) || float.IsNaN(_attachedRigidbodyVelocity.y) || float.IsNaN(_attachedRigidbodyVelocity.z))
			{
				_attachedRigidbodyVelocity = Vector3.zero;
			}
            #endregion

            #region Before Update
			// Before the simulation update //
            CharacterController.BeforeCharacterUpdate(deltaTime);
            #endregion

            #region Init Values
            // The position/rotation applied at the end of the simulation //
            TransientPosition = Transform.position;
			TransientRotation = Transform.rotation;
			// The position/rotation at the beginning of the simulation //
			InitialSimulationPosition = TransientPosition;
			InitialSimulationRotation = TransientRotation;
			// Used to know how many time the Rigid body will be projected this frame //
			_rigidbodyProjectionHitCount = 0;
			// ????????????????????? //
			OverlapsCount = 0;
			// ????????????????????? //
			_lastSolvedOverlapNormalDirty = false;
			// Grouding Statue //
			LastGroundingStatus.CopyFrom(GroundingStatus);
			GroundingStatus = default(CharacterGroundingReport);
			GroundingStatus.GroundNormal = CharacterUp;
			#endregion

			#region Root Motion move
			// Used to know if the character was moved by Root Motion //
			if (_movePositionDirty)
			{
				// _solveMovementCollisions -> This seems to be always true //
				if (_solveMovementCollisions)
				{
					if (InternalCharacterMove(_movePositionTarget - TransientPosition, deltaTime, out _internalResultingMovementMagnitude, out _internalResultingMovementDirection) && InteractiveRigidbodyHandling)
					{
						Vector3 processedVelocity = Vector3.zero;
						ProcessVelocityForRigidbodyHits(ref processedVelocity, deltaTime);
					}
				}
				else
				{
					TransientPosition = _movePositionTarget;
				}
				_movePositionDirty = false;
			}
            #endregion

            #region Overlap
            // Check if the character must be shifted because of overlaping another character //
            if (_solveMovementCollisions)
			{
				Vector3 direction = _cachedWorldUp;
				float distance = 0f;
				int i = 0;
				bool flag = false;
				for (; i < 3; i++)
				{
					if (flag)
					{
						break;
					}
					int num = CharacterCollisionsOverlap(TransientPosition, TransientRotation, _internalProbedColliders);
					if (num > 0)
					{
						for (int j = 0; j < num; j++)
						{
							Rigidbody attachedRigidbody = _internalProbedColliders[j].attachedRigidbody;
							if ((bool)attachedRigidbody && (!attachedRigidbody.isKinematic || (bool)attachedRigidbody.GetComponent<PhysicsMover>()))
							{
								continue;
							}
							Transform component = _internalProbedColliders[j].GetComponent<Transform>();
							if (Physics.ComputePenetration(Capsule, TransientPosition, TransientRotation, _internalProbedColliders[j], component.position, component.rotation, out direction, out distance))
							{
								HitStabilityReport hitStabilityReport = default(HitStabilityReport);
								hitStabilityReport.IsStable = IsStableOnNormal(direction);
								direction = GetObstructionNormal(direction, hitStabilityReport);
								Vector3 vector = direction * (distance + 0.001f);
								TransientPosition += vector;
								if (OverlapsCount < _overlaps.Length)
								{
									_overlaps[OverlapsCount] = new OverlapResult(direction, _internalProbedColliders[j]);
									OverlapsCount++;
								}
								break;
							}
						}
					}
					else
					{
						flag = true;
					}
				}
			}
            #endregion

            #region Ground
            // Check everything about the ground //
            if (_solveGrounding)
			{
				if (MustUnground)
				{
					TransientPosition += CharacterUp * 0.0075f;
				}
				else
				{
					float probingDistance = 0.005f;
					if (!LastGroundingStatus.SnappingPrevented && (LastGroundingStatus.IsStableOnGround || LastMovementIterationFoundAnyGround))
					{
						probingDistance = ((StepHandling == StepHandlingMethod.None) ? CapsuleRadius : Mathf.Max(CapsuleRadius, MaxStepHeight));
						probingDistance += GroundDetectionExtraDistance;
					}
					ProbeGround(ref _internalTransientPosition, TransientRotation, probingDistance, ref GroundingStatus);
				}
			}
            #endregion

            #region Character Ground
            // Inform the Caracter object that ground thinks are done //
            LastMovementIterationFoundAnyGround = false;
			MustUnground = false;
			if (_solveGrounding)
			{
				CharacterController.PostGroundingUpdate(deltaTime);
			}
            #endregion

            #region RigidBody check
			// Check if the RigidBody is interactive //
            if (!InteractiveRigidbodyHandling)
			{
				return;
			}
            #endregion

            #region RigidBody
            // Find the right attached RigidBody //
            _lastAttachedRigidbody = AttachedRigidbody;
			if ((bool)AttachedRigidbodyOverride)
			{
				AttachedRigidbody = AttachedRigidbodyOverride;
			}
			else if (GroundingStatus.IsStableOnGround && (bool)GroundingStatus.GroundCollider.attachedRigidbody)
			{
				Rigidbody interactiveRigidbody = GetInteractiveRigidbody(GroundingStatus.GroundCollider);
				if ((bool)interactiveRigidbody)
				{
					AttachedRigidbody = interactiveRigidbody;
				}
			}
			else
			{
				AttachedRigidbody = null;
			}
            #endregion

            #region RigidBody Velocity
            // Calcul the RigidBody Velocity //
            Vector3 vector2 = Vector3.zero;
			if ((bool)AttachedRigidbody)
			{
				vector2 = GetVelocityFromRigidbodyMovement(AttachedRigidbody, TransientPosition, deltaTime);
			}
            #endregion

            #region Last RigidBody
            // Add the last RigidBody velocity //
            if (PreserveAttachedRigidbodyMomentum && _lastAttachedRigidbody != null && AttachedRigidbody != _lastAttachedRigidbody)
			{
				_baseVelocity += _attachedRigidbodyVelocity;
				_baseVelocity -= vector2;
			}
            #endregion

            #region Rigidbody placement
            // Set the RigidBody position and rotation //
            _attachedRigidbodyVelocity = _cachedZeroVector;
			if ((bool)AttachedRigidbody)
			{
				_attachedRigidbodyVelocity = vector2;
				Vector3 normalized = Vector3.ProjectOnPlane(Quaternion.Euler(57.29578f * AttachedRigidbody.angularVelocity * deltaTime) * CharacterForward, CharacterUp).normalized;
				TransientRotation = Quaternion.LookRotation(normalized, CharacterUp);
			}
            #endregion

            #region Gravity
			// Maybe some gravity things, I don't really know //
            if ((bool)GroundingStatus.GroundCollider && (bool)GroundingStatus.GroundCollider.attachedRigidbody && GroundingStatus.GroundCollider.attachedRigidbody == AttachedRigidbody && AttachedRigidbody != null && _lastAttachedRigidbody == null)
			{
				_baseVelocity -= Vector3.ProjectOnPlane(_attachedRigidbodyVelocity, CharacterUp);
			}
            #endregion

            #region Velocity check
			// Check if they are velocity //
            if (!(_attachedRigidbodyVelocity.sqrMagnitude > 0f))
			{
				return;
			}
            #endregion

            #region Final move
			// Calcul the final Position and Rotation //
            _isMovingFromAttachedRigidbody = true;
			if (_solveMovementCollisions)
			{
				if (InternalCharacterMove(_attachedRigidbodyVelocity * deltaTime, deltaTime, out _internalResultingMovementMagnitude, out _internalResultingMovementDirection))
				{
					_attachedRigidbodyVelocity = _internalResultingMovementDirection * _internalResultingMovementMagnitude / deltaTime;
				}
				else
				{
					_attachedRigidbodyVelocity = Vector3.zero;
				}
			}
			else
			{
				TransientPosition += _attachedRigidbodyVelocity * deltaTime;
			}
			_isMovingFromAttachedRigidbody = false;
            #endregion
        }

        #endregion

        #region Update Phase 2
        // ----------------------------------------------------------------------------------------------------------------------------- //
        // ----------------------------------------------------------- UPDATE PHASE 2 -------------------------------------------------- //
        // ----------------------------------------------------------------------------------------------------------------------------- //
        public void UpdatePhase2(float deltaTime)
		{

            #region Init Values
            // Set the internal rotation to zero //
            CharacterController.UpdateRotation(ref _internalTransientRotation, deltaTime);
			// Set the Transient Rotation //
			TransientRotation = _internalTransientRotation;
            #endregion

            #region Dirty Rotation
			// If a direct rotation was done //
            if (_moveRotationDirty)
			{
				TransientRotation = _moveRotationTarget;
				_moveRotationDirty = false;
			}
            #endregion

            #region Collide
            if (_solveMovementCollisions && InteractiveRigidbodyHandling)
			{

				// Check if the Character is on something that can collide //
				if (InteractiveRigidbodyHandling && (bool)AttachedRigidbody)
				{
					float radius = Capsule.radius;
					if (CharacterGroundSweep(TransientPosition + CharacterUp * radius, TransientRotation, -CharacterUp, radius, out var closestHit) && closestHit.collider.attachedRigidbody == AttachedRigidbody && IsStableOnNormal(closestHit.normal))
					{
						float num = radius - closestHit.distance;
						TransientPosition = TransientPosition + CharacterUp * num + CharacterUp * 0.001f;
					}
				}

				// Do collider stuff //
				if (SafeMovement || InteractiveRigidbodyHandling)
				{
					Vector3 direction = _cachedWorldUp;
					float distance = 0f;
					int i = 0;
					bool flag = false;
					for (; i < 3; i++)
					{
						if (flag)
						{
							break;
						}
						int num2 = CharacterCollisionsOverlap(TransientPosition, TransientRotation, _internalProbedColliders);
						if (num2 > 0)
						{
							for (int j = 0; j < num2; j++)
							{
								Transform component = _internalProbedColliders[j].GetComponent<Transform>();
								if (!Physics.ComputePenetration(Capsule, TransientPosition, TransientRotation, _internalProbedColliders[j], component.position, component.rotation, out direction, out distance))
								{
									continue;
								}
								HitStabilityReport hitStabilityReport = default(HitStabilityReport);
								hitStabilityReport.IsStable = IsStableOnNormal(direction);
								direction = GetObstructionNormal(direction, hitStabilityReport);
								Vector3 vector = direction * (distance + 0.001f);
								TransientPosition += vector;
								if (InteractiveRigidbodyHandling)
								{
									Rigidbody attachedRigidbody = _internalProbedColliders[j].attachedRigidbody;
									if ((bool)attachedRigidbody)
									{
										PhysicsMover component2 = attachedRigidbody.GetComponent<PhysicsMover>();
										if ((bool)component2 && (bool)attachedRigidbody && (!attachedRigidbody.isKinematic || (bool)component2))
										{
											HitStabilityReport hitStabilityReport2 = default(HitStabilityReport);
											hitStabilityReport2.IsStable = IsStableOnNormal(direction);
											if (hitStabilityReport2.IsStable)
											{
												LastMovementIterationFoundAnyGround = hitStabilityReport2.IsStable;
											}
											if ((bool)component2.Rigidbody && component2.Rigidbody != AttachedRigidbody)
											{
												Vector3 point = TransientPosition + TransientRotation * CharacterTransformToCapsuleCenter;
												Vector3 transientPosition = TransientPosition;
												MeshCollider meshCollider = _internalProbedColliders[j] as MeshCollider;
												if (!meshCollider || meshCollider.convex)
												{
													Physics.ClosestPoint(point, _internalProbedColliders[j], component.position, component.rotation);
												}
												StoreRigidbodyHit(component2.Rigidbody, Velocity, transientPosition, direction, hitStabilityReport2);
											}
										}
									}
								}
								if (OverlapsCount < _overlaps.Length)
								{
									_overlaps[OverlapsCount] = new OverlapResult(direction, _internalProbedColliders[j]);
									OverlapsCount++;
								}
								break;
							}
						}
						else
						{
							flag = true;
						}
					}
				}
			}
            #endregion

            #region Velocity
            // Update and check the Velocity //
            CharacterController.UpdateVelocity(ref _baseVelocity, deltaTime);
			if (_baseVelocity.magnitude < 0.01f)
			{
				_baseVelocity = Vector3.zero;
			}

			// Add the Velocity to the character position //
			if (_baseVelocity.sqrMagnitude > 0f)
			{
				if (_solveMovementCollisions)
				{
					if (InternalCharacterMove(_baseVelocity * deltaTime, deltaTime, out _internalResultingMovementMagnitude, out _internalResultingMovementDirection))
					{
						_baseVelocity = _internalResultingMovementDirection * _internalResultingMovementMagnitude / deltaTime;
					}
					else
					{
						_baseVelocity = Vector3.zero;
					}
				}
				else
				{
					TransientPosition += _baseVelocity * deltaTime;
				}
			}
            #endregion

            #region RigidBody hit
            // Add all hit the character took //
            if (InteractiveRigidbodyHandling)
			{
				ProcessVelocityForRigidbodyHits(ref _baseVelocity, deltaTime);
			}
            #endregion

            #region Planar Constrain
			// Planar constrain ??? //
            if (HasPlanarConstraint)
			{
				TransientPosition = InitialSimulationPosition + Vector3.ProjectOnPlane(TransientPosition - InitialSimulationPosition, PlanarConstraintAxis.normalized);
			}
            #endregion

            #region Discrete Collisions
			// I don't know what is discrete collisions //
            if (DetectDiscreteCollisions)
			{
				int num3 = CharacterCollisionsOverlap(TransientPosition, TransientRotation, _internalProbedColliders, 0.002f);
				for (int k = 0; k < num3; k++)
				{
					CharacterController.OnDiscreteCollisionDetected(_internalProbedColliders[k]);
				}
			}
            #endregion

            #region After Character Update
            // And of the simulation character event
            CharacterController.AfterCharacterUpdate(deltaTime);
            #endregion

        }

        #endregion

    }
}
