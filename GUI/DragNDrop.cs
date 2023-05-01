using Panthera.Base;
using Panthera.Components;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Panthera.GUI
{
    internal class DragNDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        public PantheraSkill attachedSkill;
        public Transform dragArea;
        public ConfigPanel configPanel;

        public GameObject dragObj;
        public int startingDragSlotID;

        public void OnBeginDrag(PointerEventData eventData)
        {

            // Check the Click //
            if (eventData.button != PointerEventData.InputButton.Left) return;

            // Find the attached Skill //
            if (this.startingDragSlotID != 0)
            {
                this.attachedSkill = Preset.SelectedPreset.getSkillBySlotID(this.startingDragSlotID);
                if (this.attachedSkill == null) return;
            }

            // Create the Dragging Ability //
            this.dragObj = UnityEngine.Object.Instantiate<GameObject>(this.attachedSkill.iconPrefab, this.dragArea);
            this.dragObj.name = "DragAbility";
            Image icon = this.dragObj.transform.Find("Icon").GetComponent<Image>();
            icon.sprite = this.attachedSkill.icon;
            icon.raycastTarget = false;
            this.dragObj.SetActive(true);

            // Play the Sound //
            Utils.Sound.playSound(Utils.Sound.Page1, this.configPanel.gameObject);

            // Remove the Skill from the actual slot if exist //
            if (startingDragSlotID != 0)
            {
                GameObject slotObj = this.configPanel.skillsSlotList[startingDragSlotID];
                Image image = slotObj.transform.Find("AbilityContainer").Find("AbilityIcon").GetComponent<Image>();
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
                TextMeshProUGUI CDText = slotObj.transform.Find("CooldownText").GetComponent<TextMeshProUGUI>();
                CDText.gameObject.SetActive(false);
            }

        }

        public void OnDrag(PointerEventData eventData)
        {
            // Set the Dragging Ability position //
            var screenPoint = new Vector3(eventData.position.x, eventData.position.y, 100);
            this.dragObj.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // Remove the Skill from the actual slot if exist //
            if (startingDragSlotID != null)
            {
                Preset.SelectedPreset.removeSkillFromSlot(startingDragSlotID);
            }

            // Play the Sound //
            Utils.Sound.playSound(Utils.Sound.Page2, this.configPanel.gameObject);

            // Check if there is a Skill Slot //
            foreach (GameObject obj in eventData.hovered)
            {
                if (obj.name.Contains("BarSkill"))
                {
                    // Get the Destination Skill Slot //
                    int destSlotID = this.configPanel.getSlotIdFromObject(obj);

                    // Check if this is a Skill Slot //
                    if (destSlotID != 0)
                    {
                        // Check if there is a Skill //
                        PantheraSkill destSkill = Preset.SelectedPreset.getSkillBySlotID(destSlotID);
                        if (this.startingDragSlotID != null && destSkill != null)
                        {
                            // Add the Skill to the Slot //
                            Preset.SelectedPreset.addSkillToSlot(this.startingDragSlotID, destSkill.skillID);
                        }
                        // Change the assigned Skill //
                        Preset.SelectedPreset.addSkillToSlot(destSlotID, this.attachedSkill.skillID);
                    }

                }
            }

            // Destroy the Dragging Ability //
            GameObject.Destroy(this.dragObj);

            // Update the Skill Bars //
            configPanel.updateSkillBars();

        }
    }
}