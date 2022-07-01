using UnityEngine;
using flanne.Core;
using flanne;
using HarmonyLib;

namespace DarknessLib
{
    public class DifficultyModifierBetter : DifficultyModifier
    {
        public int darknessRank;
        public virtual DifficultyModifierBetter Init(string name, int darknessRank, string description = "Unspecified")
        {
            this.name = name;
            //TODO: Use Zeprus lib :p
            LocalizationSystem.GetDictionaryForEditor().Add(name, description);
            this.descriptionStringID = name;

            this.darknessRank = darknessRank;
            return this;
        }
        public virtual void UnModifiyGame(GameController gameController) { }
        public bool IsModActive()
        {
            return Loadout.difficultyLevel >= darknessRank;
        }
    }
}