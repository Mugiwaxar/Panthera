using Panthera;
using Panthera.Base;
using Panthera.BodyComponents;
using Panthera.Components;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Panthera.BodyComponents
{
    public class PantheraItemChange : MonoBehaviour
    {

        public PantheraObj ptraObj;

        public void Update()
        {

            // Check the Inventory //
            if (ptraObj == null || ptraObj.characterBody == null
                || ptraObj.characterBody.master == null || ptraObj.characterBody.master.inventory == null)
                return;

            // Get the Inventory //
            Inventory inventory = ptraObj.characterBody.master.inventory;

            // Get the Panthera Skill Locator //
            PantheraSkillLocator skillLocator = ptraObj.GetComponent<PantheraSkillLocator>();
            if (skillLocator == null) return;

            // Essence of Heresy //
            if (inventory.GetItemCount(PantheraConfig.ItemChange_heresyEssenceIndex) > 0)
            {
                inventory.RemoveItem(PantheraConfig.ItemChange_heresyEssenceIndex, 1);
                inventory.GiveItem(PantheraConfig.ItemChange_brittleCrownIndex, 1);
            }

            // Hooks of Heresy //
            if (inventory.GetItemCount(PantheraConfig.ItemChange_heresyHooksIndex) > 0)
            {
                inventory.RemoveItem(PantheraConfig.ItemChange_heresyHooksIndex, 1);
                inventory.GiveItem(PantheraConfig.ItemChange_brittleCrownIndex, 1);
            }

            // Strides of Heresy //
            if (inventory.GetItemCount(PantheraConfig.ItemChange_heresyStridesIndex) > 0)
            {
                inventory.RemoveItem(PantheraConfig.ItemChange_heresyStridesIndex, 1);
                inventory.GiveItem(PantheraConfig.ItemChange_brittleCrownIndex, 1);
            }

            // Visions of Heresy //
            if (inventory.GetItemCount(PantheraConfig.ItemChange_heresyVisionsIndex) > 0)
            {
                inventory.RemoveItem(PantheraConfig.ItemChange_heresyVisionsIndex, 1);
                inventory.GiveItem(PantheraConfig.ItemChange_brittleCrownIndex, 1);
            }

            // NoCooldowns Buff //
            if (ptraObj.characterBody.HasBuff(RoR2Content.Buffs.NoCooldowns))
            {
                foreach (int key in skillLocator.rechargeSkillList.Keys.ToList())
                {
                    skillLocator.rechargeSkillList[key].cooldown -= PantheraConfig.ItemChange_noCooldownTimeRemoved;
                }
            }

        }

    }
}
