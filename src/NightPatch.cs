using UnityEngine;
using flanne.Core;
using flanne;
using HarmonyLib;

namespace StarlessNightMod
{
    public class NightPatch
    {
        [HarmonyPatch(typeof(InitState), "Enter")]
        [HarmonyPrefix]
        static void InitStateEnter_prefix()
        {
            GameObject im = GameObject.Find("FogOfWarImage");

            for (int i = 0; i < 2; i++)
            {
                GameObject clone = GameObject.Instantiate(im);
                clone.transform.SetParent(im.GetComponentInParent<Transform>());
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
                else if (objectPoolItem.tag == "Boomer" ||
                         objectPoolItem.tag == "BrainMonster" ||
                         objectPoolItem.tag == "Lamprey" ||
                         objectPoolItem.tag == "EyeMonster")
                {
                    GameObject enemy = objectPoolItem.objectToPool;
                    GameObject visibleInFog = enemy.transform.Find("VisibleInFog").gameObject;
                    visibleInFog.SetActive(false);
                }
                else if (objectPoolItem.tag == "EyeMonsterProjectile")
                {
                    GameObject projectile = objectPoolItem.objectToPool;
                    GameObject sprite = projectile.transform.Find("Sprite").gameObject;
                    sprite.layer = 0;
                }
            }
        }

        [HarmonyPatch(typeof(ObjectPooler), "AddObject")]
        [HarmonyPrefix]
        static void ObjectPoolerAddObject_prefix(string tag, GameObject GO)
        {
            if (tag == "PF_HeartPickup")
            {
                GameObject bounce = GO.transform.Find("Bounce").gameObject;

                GameObject blue = bounce.transform.Find("RenderCircleBlue").gameObject;
                blue.SetActive(false);
                GameObject red = bounce.transform.Find("RenderCircleRed").gameObject;
                red.SetActive(false);
            }
            else if (tag == "PF_SpawnedBug")
            {
                GameObject visibleInFog = GO.transform.Find("VisibleInFog").gameObject;
                visibleInFog.SetActive(false);
            }
        }
    }
}
