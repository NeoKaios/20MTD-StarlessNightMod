using UnityEngine;
using flanne.Core;
using flanne;
using HarmonyLib;
using DarknessLib;

namespace StarlessNightMod
{
    [HarmonyPatch]
    public class NightPatch : DifficultyModifierBetter
    {
        static NightPatch Instance;

        public override DifficultyModifierBetter Init(string name, int rank, string description = "a nighter darkness")
        {
            if (Instance != null) return Instance;

            base.Init(name, rank, description);
            Instance = this;
            return Instance;
        }

        public override void ModifyGame(GameController gameController)
        {
            GameObject im = GameObject.Find("FogOfWarImage");

            for (int i = 0; i < 2; i++)
            {
                GameObject clone = GameObject.Instantiate(im);
                clone.transform.SetParent(im.GetComponentInParent<Transform>());
            }
        }

        public override void UnModifiyGame(GameController gameController)
        {
            foreach (ObjectPoolItem objectPoolItem in ObjectPooler.SharedInstance.itemsToPool)
            {
                ChangeGameObject(true, objectPoolItem.tag, objectPoolItem.objectToPool);
            }
        }

        [HarmonyPatch(typeof(ObjectPooler), "Awake")]
        [HarmonyPrefix]
        static void ObjectPoolerAwake_prefix(ref ObjectPooler __instance)
        {
            if (!Instance.IsModActive()) return;
            foreach (ObjectPoolItem objectPoolItem in __instance.itemsToPool)
            {
                ChangeGameObject(false, objectPoolItem.tag, objectPoolItem.objectToPool);
            }
        }

        [HarmonyPatch(typeof(ObjectPooler), "AddObject")]
        [HarmonyPrefix]
        static void ObjectPoolerAddObject_prefix(string tag, GameObject GO)
        {
            if (!Instance.IsModActive()) return;
            ChangeGameObject(false, tag, GO);
        }

        private static void ChangeGameObject(bool showThroughFog, string tag, GameObject GO)
        {
            if (tag == "SmallXP" || tag == "LargeXP" || tag == "PF_HeartPickup")
            {
                GameObject bounce = GO.transform.Find("Bounce").gameObject;

                GameObject blue = bounce.transform.Find("RenderCircleBlue").gameObject;
                blue.SetActive(showThroughFog);
                GameObject red = bounce.transform.Find("RenderCircleRed").gameObject;
                red.SetActive(showThroughFog);
            }
            else if (tag == "Boomer" ||
                     tag == "BrainMonster" ||
                     tag == "Lamprey" ||
                     tag == "EyeMonster" ||
                     tag == "PF_SpawnedBug")
            {
                GameObject visibleInFog = GO.transform.Find("VisibleInFog").gameObject;
                visibleInFog.SetActive(showThroughFog);
            }
            else if (tag == "EyeMonsterProjectile")
            {
                GameObject sprite = GO.transform.Find("Sprite").gameObject;
                sprite.layer = showThroughFog ? 9 : 0; // 9 is VisibleInFog layer, 0 is default
            }
        }
    }
}
