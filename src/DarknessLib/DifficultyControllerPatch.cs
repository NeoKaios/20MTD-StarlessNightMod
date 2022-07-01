using System;
using HarmonyLib;
using flanne;
using flanne.Core;
using flanne.UI;
using flanne.TitleScreen;
using UnityEngine;
using System.Collections.Generic;

namespace DarknessLib
{
    [HarmonyPatch]
    class DifficultyControllerPatch
    {
        private static List<DifficultyModifier> DarkerModList = new List<DifficultyModifier>();
        private static DifficultyModList moddedModList;

        public static void AddDifficultyMod(DifficultyModifier dm)
        {
            DarkerModList.Add(dm);
        }

        [HarmonyPatch(typeof(DifficultyController), "Init")]
        [HarmonyPrefix]
        public static void InitPrefix(ref int maxDiff)
        {
            if (moddedModList != null) { maxDiff = moddedModList.mods.Length - 1; return; }
            //get DifficultyModifiers
            DifficultyModList[] modLists = Resources.FindObjectsOfTypeAll<DifficultyModList>();
            //ScriptableObject.FindObjectFromInstanceID();
            foreach (DifficultyModList ml in modLists)
            {
                Debug.Log(ml.name + " " + ml.GetInstanceID());
            }
            Debug.Log("Patching darkness levels...");
            moddedModList = modLists[0];
            //create new bigger array
            DifficultyModifier[] patchedMods = new DifficultyModifier[moddedModList.mods.Length + DarkerModList.Count];
            //include base modifiers
            moddedModList.mods.CopyTo(patchedMods, 0);
            //add own modifiers
            for (int i = 0; i < (patchedMods.Length - moddedModList.mods.Length); i++)
            {
                Debug.Log("Placing mod in slot " + (moddedModList.mods.Length + i) + "/" + (patchedMods.Length - 1));
                patchedMods[moddedModList.mods.Length + i] = DarkerModList[i];
            }
            //overwrite original modlist
            moddedModList.mods = patchedMods;
            //adjust max Difficulty
            maxDiff = moddedModList.mods.Length - 1;
            Debug.Log("Patched successfully.");

            return;
        }

        [HarmonyPatch(typeof(TransitionToRetryState), "Enter")]
        [HarmonyPrefix]
        public static void RemoveDarknessOnRetry()
        {
            RemoveDarknessMod();
        }

        [HarmonyPatch(typeof(TransitionToTitleState), "Enter")]
        [HarmonyPrefix]
        public static void RemoveDarknessOnBattleExit()
        {
            RemoveDarknessMod();
        }

        public static void RemoveDarknessMod()
        {
            GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
            foreach (DifficultyModifier dm in moddedModList.mods)
            {
                try
                {
                    ((DifficultyModifierBetter)dm).UnModifiyGame(gameController);
                }
                catch (InvalidCastException) { }
            }
        }
    }
}