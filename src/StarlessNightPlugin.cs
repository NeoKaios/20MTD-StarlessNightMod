using BepInEx;
using HarmonyLib;

namespace StarlessNightMod
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class StarlessNightPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            try
            {
                Harmony.CreateAndPatchAll(typeof(NightPatch));
            }
            catch
            {
                Logger.LogError($"{PluginInfo.PLUGIN_GUID} failed to patch methods (NightPatch).");
            }
        }
    }
}
