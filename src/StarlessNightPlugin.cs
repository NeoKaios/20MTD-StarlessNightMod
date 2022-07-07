using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using DarknessLib;

namespace StarlessNightMod
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class StarlessNightPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            try
            {
                DarknessCore core = new DarknessCore(16, true);
                core.SetDifficultyMod(
                    ScriptableObject.CreateInstance<NightPatch>().Init("StarlessNight", 16, "The night is darker and the enemies sneakier")
                    , 16);
                Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            }
            catch
            {
                Logger.LogError($"{PluginInfo.PLUGIN_GUID} failed to patch methods (NightPatch).");
            }
        }
    }
}
