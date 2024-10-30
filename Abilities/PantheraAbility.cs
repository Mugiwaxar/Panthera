using Panthera;
using Panthera.Abilities;
using Panthera.Base;
using Panthera.Components;
using Panthera.NetworkMessages;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Panthera.Abilities
{
    public class PantheraAbility
    {

        public enum AbilityType
        {
            primary,
            passive,
            active,
            special
        }

        public string name;
        public int abilityID;
        public AbilityType type;
        public string desc1;
        public string desc2;
        public Sprite icon;
        public int maxLevel;
        public float cooldown;
        public int requiredAbility;
        public GameObject associatedGUIObj;
        public bool hasMastery = false;

        public virtual void updateDesc()
        {

        }

    }
}
