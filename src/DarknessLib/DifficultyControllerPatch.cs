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


        [HarmonyPatch(typeof(DifficultyController), "Init")]
        [HarmonyPrefix]
        public static void InitPrefix(ref int maxDiff, ref DifficultyModList ___modList)
        {
            DarknessCore.Init();
            maxDiff = DarknessCore.GetActiveMaxDiff();
            Debug.Log(maxDiff);

            Debug.Log(___modList.mods.Length);
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
            foreach (DifficultyModifier dm in DarknessCore.GetActiveModifiers())
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