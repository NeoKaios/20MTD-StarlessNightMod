using UnityEngine;
using UnityEngine.SceneManagement;
using flanne.Core;
using flanne;
using HarmonyLib;

namespace StarlessNightMod
{
    public class NightPatch
    {
        [HarmonyPatch(typeof(InitState), "Enter")]
        [HarmonyPrefix]
        static void InitStateExit_prefix()
        {
            GameObject fogofwar = GameObject.Find("FogOfWarCanvas");

            if (fogofwar)
            {
                GameObject im = fogofwar.transform.Find("FogOfWarImage").gameObject;

                for (int i = 0; i < 5; i++)
                {
                    GameObject clone = GameObject.Instantiate(im);
                    clone.transform.SetParent(fogofwar.transform);
                }
            }
        }

        [HarmonyPatch(typeof(ObjectPooler), "Awake")]
        [HarmonyPrefix]
        static void ObjectPoolerAwake_prefix(ref ObjectPooler __instance)
        {
            foreach (ObjectPoolItem objectPoolItem in __instance.itemsToPool)
            {
                if (objectPoolItem.tag == "SmallXP" || objectPoolItem.tag == "LargeXP")
                {
                    GameObject xpObject = objectPoolItem.objectToPool;
                    GameObject bounce = xpObject.transform.Find("Bounce").gameObject;

                    GameObject blue = bounce.transform.Find("RenderCircleBlue").gameObject;
                    blue.SetActive(false);
                    GameObject red = bounce.transform.Find("RenderCircleRed").gameObject;
                    red.SetActive(false);
                }
            }
        }
    }
}
