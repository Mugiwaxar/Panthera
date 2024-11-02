using RoR2;
using System;
using System.Linq;
using UnityEngine;

namespace Panthera.BodyComponents
{
    public class PantheraTracker
    {

        public static void SearchForTarget(Action<HuntressTracker, Ray> orig, HuntressTracker self, Ray aimRay)
        {

            // Get the Panthera Object //
            PantheraObj ptraObj = self.gameObject.GetComponent<PantheraObj>();
            if (ptraObj == null)
            {
                orig(self, aimRay);
                return;
            }

            // Create the Team Mask //
            TeamMask teamMask = new TeamMask();

            // Check the Save My Friend Ability //
            //if (ptraObj.activePreset != null && ptraObj.activePreset.getAbilityLevel(PantheraConfig.SaveMyFriendAbilityID) > 0 && ptraObj.interactPressed == true)
            //    teamMask.AddTeam(TeamIndex.Player);
            //else
            //    teamMask.AddTeam(TeamIndex.Monster);

            // Search a Target //
            self.search.teamMaskFilter = teamMask;
            self.search.filterByLoS = true;
            self.search.searchOrigin = aimRay.origin;
            self.search.searchDirection = aimRay.direction;
            self.search.sortMode = BullseyeSearch.SortMode.Distance;
            self.search.maxDistanceFilter = self.maxTrackingDistance;
            self.search.maxAngleFilter = self.maxTrackingAngle;
            self.search.RefreshCandidates();
            self.search.FilterOutGameObject(self.gameObject);
            self.trackingTarget = self.search.GetResults().FirstOrDefault();

        }

    }
}
