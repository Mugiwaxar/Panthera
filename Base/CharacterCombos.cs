using EntityStates.LunarGolem;
using Panthera.BodyComponents;
using Panthera.Combos;
using Panthera.GUI;
using Panthera.MachineScripts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UnityEngine;
using static Panthera.GUI.KeysBinder;

namespace Panthera.Base
{
    public class CharacterCombos
    {

        // (int ComboID, PantheraCombo Combo) Represent the list of all Combos //
        public Dictionary<int, PantheraCombo> CombosList = new Dictionary<int, PantheraCombo>();

        public CharacterCombos()
        {
            this.addComboToList(PantheraConfig.Rip_CombosID, new Combos.List.Rip());
            this.addComboToList(PantheraConfig.Slash_CombosID, new Combos.List.Slash());
            this.addComboToList(PantheraConfig.Leap_CombosID, new Combos.List.Leap());
            this.addComboToList(PantheraConfig.MightyRoar_CombosID, new Combos.List.MightyRoar());
            this.addComboToList(PantheraConfig.Fury_CombosID, new Combos.List.Fury());
            this.addComboToList(PantheraConfig.Guardian_CombosID, new Combos.List.Guardian());
            this.addComboToList(PantheraConfig.Prowl_CombosID, new Combos.List.Prowl());
            this.addComboToList(PantheraConfig.Detection_CombosID, new Combos.List.Detection());
            this.addComboToList(PantheraConfig.AirSlash_CombosID, new Combos.List.AirSlash());
            this.addComboToList(PantheraConfig.FrontShield_CombosID, new Combos.List.FrontShield());
            this.addComboToList(PantheraConfig.ClawsStorm_CombosID, new Combos.List.ClawsStorm());
            this.addComboToList(PantheraConfig.ShieldBash_CombosID, new Combos.List.ShieldBash());
            this.addComboToList(PantheraConfig.ArcaneAnchor_CombosID, new Combos.List.ArcaneAnchor());
            this.addComboToList(PantheraConfig.ConvergenceHook_CombosID, new Combos.List.ConvergenceHook());
            this.addComboToList(PantheraConfig.MassiveHook_CombosID, new Combos.List.MassiveHook());
            this.addComboToList(PantheraConfig.Ambition_CombosID, new Combos.List.Ambition());
        }

        public void addComboToList(int ID, PantheraCombo combo)
        {
            if (this.CombosList.ContainsKey(ID))
            {
                Debug.LogWarning("[Panthera -> Character.addComboToList] The Combos List already contain a Combo with the ID: " + ID);
                return;
            }
            this.CombosList.Add(ID, combo);
        }

    }
}
