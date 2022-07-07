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
    class DarknessCore
    {
        static DifficultyModList activeModList;
        static DifficultyModifier[] defaultModifiers = null; // Default game ModList
        static DarknessCore CurrentActiveInstance; // Current darkness core instance that is represented in game (in the diff controller menu)
        //Instance attributes
        int maxDiff; // Max difficulty of this instance
        bool useDefaultDarkness;
        int autoIndexer = 0;
        private DifficultyModifier[] moddedModifiers; //Actual modified ModList
        public DarknessCore(int newDarknessCount, bool useDefaultDarkness)
        {
            Init();
            if (useDefaultDarkness && newDarknessCount < 15)
            {
                throw new InvalidOperationException("Cannot set darkness count < 15 if useDefaultDarkness is active");
            }
            this.useDefaultDarkness = useDefaultDarkness;
            maxDiff = newDarknessCount;

            if (useDefaultDarkness) autoIndexer = 16;

            CurrentActiveInstance = this;
            //create new bigger array
            moddedModifiers = new DifficultyModifier[newDarknessCount + 1]; // Starts at darkness 0
            defaultModifiers.CopyTo(moddedModifiers, 0);
        }

        public void SetDifficultyMod(DifficultyModifier difficultyModifier, int index)
        {
            if (index >= 0 && index <= maxDiff)
            {
                Debug.Log(index + " " + maxDiff + " " + moddedModifiers.Length);
                moddedModifiers[index] = difficultyModifier;
            }
            else throw new IndexOutOfRangeException();
        }
        public void AutoSetNextDifficultyMod(DifficultyModifier difficultyModifier)
        {
            if (autoIndexer <= maxDiff)
            {
                moddedModifiers[autoIndexer] = difficultyModifier;
                autoIndexer++;
            }
            else throw new InvalidOperationException("Already filled the difficulty array");
        }

        // Called by DifficultyController Init prefix, everything should be setup by now
        public static void Init()
        {
            if (defaultModifiers != null) return;
            //get DifficultyModifiers
            DifficultyModList[] modLists = Resources.FindObjectsOfTypeAll<DifficultyModList>();

            Debug.Log("Finding default darkness levels...");
            defaultModifiers = new DifficultyModifier[modLists[0].mods.Length];
            modLists[0].mods.CopyTo(defaultModifiers, 0);
            activeModList = modLists[0];
        }

        public static int GetActiveMaxDiff()
        {
            return CurrentActiveInstance.maxDiff;
        }
        public static void SetToActiveModifiers()
        {
            activeModList.mods = CurrentActiveInstance.moddedModifiers;
        }
        public static DifficultyModifier[] GetActiveModifiers()
        {
            return CurrentActiveInstance.moddedModifiers;
        }
    }
}