using BepInEx;
using HarmonyLib;

namespace StarlessNightMod
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    class StarlessNightPlugin : BaseUnityPlugin
    {
        public const string PLUGIN_GUID = "kaios.mod.starless";
        public const string PLUGIN_NAME = "Starless Night";
        public const string PLUGIN_VERSION = "0.1";

        public void Awake()
        {
            try
            {
                Harmony.CreateAndPatchAll(typeof(NightPatch));
            }
            catch
            {
                Logger.LogError($"{PLUGIN_GUID} failed to patch methods (NightPatch).");
            }
        }
    }
}
