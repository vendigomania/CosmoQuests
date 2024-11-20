using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Shop
{
    public class ShopRocketItem : MonoBehaviour
    {
        [SerializeField] private Image icon;

        [SerializeField] private Button buySelectBtn;
        [SerializeField] private GameObject coinIcon;
        [SerializeField] private TMP_Text statusIcon;

        public static UnityAction OnSelectAction;

        private SkinData item;
        private int cost;

        #region select

        public bool IsSelected => RocketSkinsData.RocketSkin.Id == item.Id;

        public void SetSelected()
        {
            RocketSkinsData.RocketSkin = item;
            OnSelectAction?.Invoke();
        }

        #endregion

        #region buy

        public bool IsBought => PlayerPrefs.GetInt($"{icon.sprite.name}IsBought", 0) == 1 || icon.sprite.name == "1";

        public void SetBought() => PlayerPrefs.SetInt($"{icon.sprite.name}IsBought", 1);

        #endregion

        private void Start()
        {
            buySelectBtn.onClick.AddListener(OnClick);

            OnSelectAction += UpdateInfo;
            PlayerStatsData.OnCoinsChanged += UpdateInfo;
        }

        public void Initialize(SkinData _skin)
        {
            item = _skin;
            icon.sprite = _skin.Icon;

            cost = (50 * (1 + int.Parse(icon.sprite.name)) / 2);
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            coinIcon.SetActive(!IsBought);

            if(IsBought)
            {
                if(IsSelected)
                {
                    statusIcon.text = "Equiped";
                }
                else
                {
                    statusIcon.text = "Unequiped";
                }
            }
            else
            {
                statusIcon.text = cost.ToString();
            }
        }

        private void OnClick()
        {
            if(IsBought)
            {
                AudioControl.Instance.Click();
                SetSelected();
            }
            else if(PlayerStatsData.Coins >= cost)
            {
                AudioControl.Instance.Click();
                SetBought();
                PlayerStatsData.Coins -= cost;
            }
        }
    }
}
