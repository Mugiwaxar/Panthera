using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.EventSystems;
using UnityEngine;
using Panthera.Utils;

namespace Panthera.GUI.Tooltips
{
    public class SimpleTooltipComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {

            if (this.name == "EnduranceButtonImage" || this.name == "ForceButtonImage" || this.name == "AgilityButtonImage" || this.name == "SwiftnessButtonImage" || this.name == "DexterityButtonImage")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("TOOLTIP_ATTRIBUTE_UP_BUTTON"));
                return;
            }

            if (name == "EnduranceText")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("TOOLTIP_ATTRIBUTE_ENDURENCE"));
                return;
            }

            if (name == "ForceText")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("TOOLTIP_ATTRIBUTE_FORCE"));
                return;
            }

            if (name == "AgilityText")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("TOOLTIP_ATTRIBUTE_AGILITY"));
                return;
            }

            if (name == "SwiftnessText")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("TOOLTIP_ATTRIBUTE_SWIFTNESS"));
                return;
            }

            if (name == "DexterityText")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("TOOLTIP_ATTRIBUTE_DEXTERITY"));
                return;
            }

            if (name == "HealthText")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("TOOLTIP_ATTRIBUTE_MAXHEALTH"));
                return;
            }

            if (name == "HealthRegenText")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("TOOLTIP_ATTRIBUTE_HEALTHREGEN"));
                return;
            }

            if (name == "MoveSpeedText")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("TOOLTIP_ATTRIBUTE_MOVESPEED"));
                return;
            }

            if (name == "DamageText")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("TOOLTIP_ATTRIBUTE_DAMAGE"));
                return;
            }

            if (name == "AttackSpeedText")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("TOOLTIP_ATTRIBUTE_ATTACKSPEED"));
                return;
            }

            if (name == "CriticText")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("TOOLTIP_ATTRIBUTE_CRITIC"));
                return;
            }

            if (name == "DefenseText")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("TOOLTIP_ATTRIBUTE_DEFENSE"));
                return;
            }

            if (name == "SliderLayout")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("TOOLTIP_OVERVIEW_LEVELSLIDER"));
                return;
            }

            if (name == "LunarCoinImage" || name == "LunarCoinText")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("TOOLTIP_OVERVIEW_LUNARCOIN"));
                return;
            }

            if (name == "OverviewResetButton")
            {
                SimpleTooltip.ShowTooltip(PantheraTokens.Get("TOOLTIP_OVERVIEW_RESET"));
                return;
            }

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (SimpleTooltip.showCounter > 0)
                SimpleTooltip.HideTooltip();
        }
    }
}
