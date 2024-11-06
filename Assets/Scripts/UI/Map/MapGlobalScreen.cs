using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MapGlobalScreen : MonoBehaviour
    {
        [SerializeField] private GameObject[] lives;

        [SerializeField] private RectTransform globalMap;
        [SerializeField] private RectTransform[] points;
        
        [SerializeField] private Image rocketImg;
        [SerializeField] private RectTransform rocketTransform;

        [SerializeField] private Button playMapBtn;
        [SerializeField] private TMP_Text playBtnLable;

        [SerializeField] private Button shopBtn;

        [SerializeField] private TMP_Text coinsCountLable;

        [Space, SerializeField] private MapLocalScreen mapLocalScreen;


        public static MapGlobalScreen Instance { get; private set; }


        // Start is called before the first frame update
        void Start()
        {
            Instance = this;
            randomOffset = new Vector2(Random.Range(-200, 200), Random.Range(0, 2) == 0 ? -140 : 140);

            SetMapPosition();

            ShowCoins();

            PlayerStatsData.OnCoinsChanged += ShowCoins;
            RocketSkinsData.OnSkinChanged += (spr) => rocketImg.sprite = spr;
            PlayerStatsData.OnLivesChanged += SetLives;

            playMapBtn.onClick.AddListener(OpenLocalMap);
            playBtnLable.text = $"Play: Map {PlayerStatsData.GlobalStage + 1}";

            shopBtn.onClick.AddListener(ShowShop);
        }

        bool animateMap = false;
        Vector2 randomOffset;

        void Update()
        {
            if (!animateMap) return;

            if (PlayerStatsData.GlobalStage >= points.Length)
            {
                PlayerStatsData.GlobalStage = 0;
                PlayerStatsData.LocalStage = 0;
            }

            if (rocketTransform.anchoredPosition != points[PlayerStatsData.GlobalStage].anchoredPosition + randomOffset)
            {
                playMapBtn.interactable = false;
                Vector2 curPoint = points[PlayerStatsData.GlobalStage].anchoredPosition;

                rocketTransform.anchoredPosition = Vector2.MoveTowards(
                    rocketTransform.anchoredPosition,
                    curPoint + randomOffset,
                    Time.deltaTime * 320f);


                globalMap.anchoredPosition = Vector2.MoveTowards(
                    globalMap.anchoredPosition,
                    new Vector2( -curPoint.x, Mathf.Clamp(-curPoint.y, -299, 299)),
                    Time.deltaTime * 320f);
            }
            else
            {
                animateMap = false;

                playBtnLable.text = $"Play: Map {PlayerStatsData.GlobalStage + 1}";
                playMapBtn.interactable = true;
            }
        }

        public void ShowAnim()
        {
            animateMap = true;
            rocketTransform.localScale =
                        new Vector2(Mathf.Sign(points[PlayerStatsData.GlobalStage].anchoredPosition.x), 1f);
        }

        public void UpdateLocalMap()
        {
            mapLocalScreen.ShowAnim();
        }


        private void SetMapPosition()
        {
            Vector2 curPoint = points[PlayerStatsData.GlobalStage].anchoredPosition;
            globalMap.anchoredPosition = new Vector2(
                -curPoint.x,
                Mathf.Clamp(-curPoint.y, -299, 299));

            rocketImg.sprite = RocketSkinsData.RocketSkin;

            rocketTransform.anchoredPosition = curPoint + randomOffset;
            rocketTransform.localScale = new Vector2(-Mathf.Sign(randomOffset.x), 1f);
        }

        private void SetLives(int count)
        {
            for(var i = 0; i < lives.Length; i++)
            {
                lives[i].SetActive(i < count);
            }
        }

        private void OpenLocalMap()
        {
            AudioControl.Instance.Click();
            mapLocalScreen.Show();
        }

        private void ShowShop() => ShopScreen.Instance.Show();

        private void ShowCoins() => coinsCountLable.text = PlayerStatsData.Coins.ToString();

        [ContextMenu("UP Stage")]
        private void UpStage()
        {
            PlayerStatsData.GlobalStage++;
            animateMap = true;
        }
    }
}