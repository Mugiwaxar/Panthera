using Panthera.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Panthera.GUI.Tooltips
{
    public class SimpleTooltipComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {

            if (name == "EnduranceText")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("attribute_Endurance"));
                return;
            }

            if (name == "ForceText")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("attribute_Force"));
                return;
            }

            if (name == "AgilityText")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("attribute_Agility"));
                return;
            }

            if (name == "SwiftnessText")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("attribute_Swiftness"));
                return;
            }

            if (name == "DexterityText")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("attribute_Dexterity"));
                return;
            }

            if (name == "SpiritText")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("attribute_Spirit"));
                return;
            }

            if (name == "MasteryHelp")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("help_Mastery"));
                return;
            }

            if (name == "RuseHelp")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("help_Ruse"));
                return;
            }

            if (name == "FuryHelp")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("help_Fury"));
                return;
            }

            if (name == "ClassicHelp")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("help_Classic"));
                return;
            }

            if (name == "GuardianHelp")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("help_Gardian"));
                return;
            }

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (SimpleTooltip.ShowCounter > 0)
                SimpleTooltip.HideTooltip();
        }
    }
}
