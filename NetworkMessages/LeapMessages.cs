using R2API.Networking.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;
using UnityEngine;
using RoR2;
using Panthera.Components;

namespace Panthera.NetworkMessages
{

	//public class ClientCreateLeapCercleFX : INetMessage
	//{

	//	GameObject player;
	//	Vector3 position;

	//	public ClientCreateLeapCercleFX()
	//	{

	//	}

	//	public ClientCreateLeapCercleFX(GameObject player, Vector3 position)
 //       {
	//		this.player = player;
	//		this.position = position;
 //       }

	//	public void OnReceived()
 //       {
	//		if (player == null) return;
	//		if (Util.HasEffectiveAuthority(this.player) == true) return;
	//		PantheraObj ptraObj = this.player.GetComponent<PantheraObj>();
	//		if (ptraObj == null) return;
	//		GameObject effect = Utils.Functions.SpawnEffect(player, PantheraAssets.LeapCercleFX, position, PantheraConfig.Model_generalScale, null, Util.QuaternionSafeLookRotation(player.transform.localRotation.eulerAngles));
	//		ptraObj.actualLeapCerle = effect.GetComponent<LeapCercleComponent>();
	//		ptraObj.actualLeapCerle.ptraObj = ptraObj;
	//		ptraObj.actualLeapCerle.client = true;
	//	}

 //       public void Serialize(NetworkWriter writer)
 //       {
	//		writer.Write(this.player);
	//		writer.Write(this.position);
 //       }

	//	public void Deserialize(NetworkReader reader)
	//	{
	//		this.player = reader.ReadGameObject();
	//		this.position = reader.ReadVector3();
	//	}

	//}

	//public class ClientDestroyLeapCercleFX : INetMessage
 //   {

	//	public GameObject player;

	//	public ClientDestroyLeapCercleFX()
 //       {

 //       }

	//	public ClientDestroyLeapCercleFX(GameObject player)
 //       {
	//		this.player = player;
 //       }     

 //       public void OnReceived()
 //       {
	//		if (player == null) return;
	//		if (Util.HasEffectiveAuthority(this.player) == true) return;
	//		PantheraObj ptraObj = this.player.GetComponent<PantheraObj>();
	//		if (ptraObj == null) return;
	//		if (ptraObj.actualLeapCerle != null)
	//		{
	//			GameObject.Destroy(ptraObj.actualLeapCerle.gameObject, PantheraConfig.LeapCerle_delayBeforeDestroyed);
	//			ptraObj.actualLeapCerle.destroying = true;
	//			ptraObj.actualLeapCerle = null;
	//		}
	//	}

 //       public void Serialize(NetworkWriter writer)
 //       {
	//		writer.Write(this.player);
 //       }

	//	public void Deserialize(NetworkReader reader)
	//	{
	//		this.player = reader.ReadGameObject();
	//	}

	//}

}
