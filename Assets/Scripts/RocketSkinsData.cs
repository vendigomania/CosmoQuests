using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Data
{
    public class RocketSkinsData : MonoBehaviour
    {
        public Sprite[] sprites;

        public static Sprite[] Sprites => Instance.sprites;

        private static RocketSkinsData Instance;

        //skin
        public static UnityAction<Sprite> OnSkinChanged;

        public static Sprite RocketSkin
        {
            get => Sprites[PlayerPrefs.GetInt("SelectedSpriteId", 0)];
            set
            {
                PlayerPrefs.SetInt("SelectedSpriteId", int.Parse(value.name) - 1);
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
