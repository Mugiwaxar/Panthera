using Panthera.Abilities;
using Panthera.Abilities.Actives;
using Panthera.Abilities.Passives;
using Panthera.Abilities.Primaries;
using Panthera.MachineScripts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UnityEngine;

namespace Panthera.Base
{
    public class CharacterAbilities
    {

        // (int AbilityID, MachineScript skill) Represent the Abilities List //
        public Dictionary<int, PantheraAbility> AbilityList = new Dictionary<int, PantheraAbility>();

        public CharacterAbilities()
        {
            this.addAbilityToList(PantheraConfig.FelineSkills_AbilityID, new FelineSkills());
            this.addAbilityToList(PantheraConfig.SharpenedFangs_AbilityID, new SharpenedFangs());
            this.addAbilityToList(PantheraConfig.Fury_AbilityID, new Fury());
            this.addAbilityToList(PantheraConfig.EnchantedFur_AbilityID, new EnchantedFur());
            this.addAbilityToList(PantheraConfig.Guardian_AbilityID, new Guardian());
            this.addAbilityToList(PantheraConfig.WindWalker_AbilityID, new WindWalker());
            this.addAbilityToList(PantheraConfig.Predator_AbilityID, new Predator());
            this.addAbilityToList(PantheraConfig.Prowl_AbilityID, new Prowl());
            this.addAbilityToList(PantheraConfig.Detection_AbilityID, new Detection());
            this.addAbilityToList(PantheraConfig.Ambition_AbilityID, new Ambition());
            this.addAbilityToList(PantheraConfig.AirSlash_AbilityID, new AirSlash());
            this.addAbilityToList(PantheraConfig.FrontShield_AbilityID, new FrontShield());
            this.addAbilityToList(PantheraConfig.ClawsStorm_AbilityID, new ClawsStorm());
            this.addAbilityToList(PantheraConfig.SwiftMoves_AbilityID, new SwiftMoves());
            this.addAbilityToList(PantheraConfig.GhostRip_AbilityID, new GhostRip());
            this.addAbilityToList(PantheraConfig.ImprovedShield_AbilityID, new ImprovedShield());
            this.addAbilityToList(PantheraConfig.ShieldBash_AbilityID, new ShieldBash());
            this.addAbilityToList(PantheraConfig.GoldenRip_AbilityID, new GoldenRip());
            this.addAbilityToList(PantheraConfig.Tornado_AbilityID, new Tornado());
            this.addAbilityToList(PantheraConfig.ArcaneAnchor_AbilityID, new ArcaneAnchor());
            this.addAbilityToList(PantheraConfig.ConvergenceHook_AbilityID, new ConvergenceHook());
            this.addAbilityToList(PantheraConfig.MassiveHook_AbilityID, new MassiveHook());
            this.addAbilityToList(PantheraConfig.SixthSense_AbilityID, new SixthSense());
            this.addAbilityToList(PantheraConfig.RelentlessStalker_AbilityID, new RelentlessStalker());
            this.addAbilityToList(PantheraConfig.RoarOfResilience_AbilityID, new RoarOfResilience());
            this.addAbilityToList(PantheraConfig.ClawsSharpening_AbilityID, new ClawsSharpening());
            this.addAbilityToList(PantheraConfig.GoldenStart_AbilityID, new GoldenStart());
            this.addAbilityToList(PantheraConfig.StealthStrike_AbilityID, new StealthStrike());
            this.addAbilityToList(PantheraConfig.CryoLeap_AbilityID, new CryoLeap());
            this.addAbilityToList(PantheraConfig.ShadowStalker_AbilityID, new ShadowStalker());
            this.addAbilityToList(PantheraConfig.WardensVitality_AbilityID, new WardensVitality());
            this.addAbilityToList(PantheraConfig.SavageRevitalization_AbilityID, new SavageRevitalization());
            this.addAbilityToList(PantheraConfig.InnateProtection_AbilityID, new InnateProtection());
            this.addAbilityToList(PantheraConfig.Furrify_AbilityID, new Furrify());
            this.addAbilityToList(PantheraConfig.Concentration_AbilityID, new Concentration());
            this.addAbilityToList(PantheraConfig.SuperiorFlair_AbilityID, new SuperiorFlair());
            this.addAbilityToList(PantheraConfig.UntamedSpirit_AbilityID, new UntamedSpirit());
            this.addAbilityToList(PantheraConfig.GodPower_AbilityID, new GodPower());
            this.addAbilityToList(PantheraConfig.PortalSurge_AbilityID, new PortalSurge());
            this.addAbilityToList(PantheraConfig.EternalFury_AbilityID, new EternalFury());
            this.addAbilityToList(PantheraConfig.InnerRage_AbilityID, new InnerRage());
            this.addAbilityToList(PantheraConfig.InfernalSwipe_AbilityID, new InfernalSwipe());
            this.addAbilityToList(PantheraConfig.HeatWave_AbilityID, new HeatWave());
            this.addAbilityToList(PantheraConfig.KineticResorption_AbilityID, new KineticResorption());
            this.addAbilityToList(PantheraConfig.ExtendedProtection_AbilityID, new ExtendedProtection());
        }

        public void addAbilityToList(int ID, PantheraAbility ability)
        {
            if (this.AbilityList.ContainsKey(ID))
            {
                Debug.LogWarning("[Panthera -> Character.addAbilityToList] The Abilities List already contain an Ability with the ID: " + ID);
                return;
            }
            this.AbilityList.Add(ID, ability);
        }

        public PantheraAbility getAbilityByID(int ID)
        {
            if (this.AbilityList.ContainsKey(ID))
                return this.AbilityList[ID];
            return null;
        }

    }
}
