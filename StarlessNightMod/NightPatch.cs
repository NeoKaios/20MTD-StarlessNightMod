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
            GameObject fogofwar = FindInSceneByName("FogOfWarCanvas");

            if (fogofwar)   
            {
                GameObject im = FindChildByName(fogofwar, "FogOfWarImage");

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

                    GameObject blue = FindChildByName(xpObject, "RenderCircleBlue");
                    blue.SetActive(false);
                    GameObject red = FindChildByName(xpObject, "RenderCircleRed");
                    red.SetActive(false);
                }
            }
        }
        static public GameObject FindChildByName(GameObject go, string name)
        {

            Transform[] transform = go.GetComponentsInChildren<Transform>();
            foreach (Transform child in transform)
            {
                if (child.gameObject.name == name)
                {
                    return child.gameObject;
                }
            }
            return null;
        }
        static public GameObject FindInSceneByName(string name)
        {

            GameObject[] gos = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject go in gos)
            {
                GameObject res = FindChildByName(go, name);
                if (res)
                {
                    return res;
                }
            }
            return null;
        }
    }
}
