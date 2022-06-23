using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using flanne.Core;
using HarmonyLib;

namespace StarlessNightMod
{
    public class NightPatch
    {
        [HarmonyPatch(typeof(InitState), "Enter")]
        [HarmonyPostfix]
        static void InitStateExit_postfix()
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
