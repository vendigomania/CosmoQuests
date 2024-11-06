using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class ShopScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image current;

        [SerializeField] private List<ShopRocketItem> items = new List<ShopRocketItem>();
        [SerializeField] private TMP_Text coinsLable;

        public static ShopScreen Instance { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            SetCoins();
            PlayerStatsData.OnCoinsChanged += SetCoins;
            RocketSkinsData.OnSkinChanged += SetSkin;

            for(int num = 0; num < RocketSkinsData.Sprites.Length; num++)
            {
                if(items.Count - 1 < num)
                {
                    items.Add(Instantiate(items[0], items[0].transform.parent));
                }

                items[num].Initialize(RocketSkinsData.Sprites[num]);
            }

            Instance = this;
        }

        public void Show()
        {
            canvasGroup.gameObject.SetActive(true);

            foreach(var item in items) item.UpdateInfo();

            AudioControl.Instance.Click();

            SetSkin(RocketSkinsData.RocketSkin);
        }

        public void Hide()
        {
            canvasGroup.gameObject.SetActive(false);
            AudioControl.Instance.Click();
        }

        private void SetCoins()
        {
            coinsLable.text = PlayerStatsData.Coins.ToString();
        }

        private void SetSkin(Sprite _skin)
        {
            current.sprite = _skin;
        }
    }
}
