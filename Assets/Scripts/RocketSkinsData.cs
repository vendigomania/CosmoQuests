using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Data
{
    [System.Serializable]
    public struct SkinData
    {
        public int Id;
        public string Name;
        public string Description;
        public Sprite Icon;
    }

    public class RocketSkinsData : MonoBehaviour
    {
        [SerializeField] SkinData[] skins;

        public static SkinData[] Skins => Instance.skins;

        private static RocketSkinsData Instance;

        //skin
        public static UnityAction<SkinData> OnSkinChanged;

        public static SkinData RocketSkin
        {
            get => Skins[PlayerPrefs.GetInt("SelectedSkinId", 0)];
            set
            {
                PlayerPrefs.SetInt("SelectedSkinId", value.Id);
                OnSkinChanged?.Invoke(value);
            }
        }

        // Start is called before the first frame update
        void Awake()
        {
            Instance = this;
        }
    }
}
