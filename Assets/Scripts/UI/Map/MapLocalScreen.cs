using Data;
using System.Collections;
using System.Collections.Generic;
using UI.Map;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MapLocalScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup root;

        [SerializeField] private LocalMapVisualizator[] maps;
        [SerializeField] private Image rocketImg;
        [SerializeField] private RectTransform rocketTransform;

        [SerializeField] private Button playGameBtn;

        private LocalMapVisualizator currentMap => maps[PlayerStatsData.GlobalStage];

        // Start is called before the first frame update
        void Start()
        {
            RocketSkinsData.OnSkinChanged += (spr) => rocketImg.sprite = spr;
            playGameBtn.onClick.AddListener(LaunchGame);

            SetLocalMapPosition();
        }

        bool animateMap = false;

        void Update()
        {
            if (!animateMap) return;

            if(PlayerStatsData.Lives == 0)
            {
                PlayerStatsData.LocalStage = 0;
                GalaxyResultScreen.Instance.Show(false, () =>
                {
                    Hide();
                    PlayerStatsData.Lives = 3;
                });

                animateMap = false;
                return;
            }

            if (PlayerStatsData.LocalStage >= currentMap.Points.Length)
            {
                PlayerStatsData.LocalStage = 0;
                PlayerStatsData.GlobalStage++;
                GalaxyResultScreen.Instance.Show(true, () =>
                {
                    Hide();
                    MapGlobalScreen.Instance.ShowAnim();
                });

                animateMap = false;
                return;
            }

            if (rocketTransform.anchoredPosition != currentMap.Points[PlayerStatsData.LocalStage].anchoredPosition)
            {
                playGameBtn.interactable = false;

                rocketTransform.anchoredPosition = Vector2.MoveTowards(
                    rocketTransform.anchoredPosition,
                    currentMap.Points[PlayerStatsData.LocalStage].anchoredPosition,
                    Time.deltaTime * 320f);

                currentMap.Root.anchoredPosition = Vector2.MoveTowards(
                    currentMap.Root.anchoredPosition,
                    new Vector2(0f, Mathf.Clamp(-currentMap.Points[PlayerStatsData.LocalStage].anchoredPosition.y, -247, 247)),
                    Time.deltaTime * 320f);
            }
            else
            {
                animateMap = false;

                playGameBtn.interactable = true;
            }
        }

        public void Show()
        {
            root.gameObject.SetActive(true);
            rocketTransform.SetParent(currentMap.Root);

            foreach (var map in maps)
                map.gameObject.SetActive(map == currentMap);

            animateMap = true;
            rocketTransform.localScale =
                new Vector2(Mathf.Sign(currentMap.Points[PlayerStatsData.LocalStage].anchoredPosition.x), 1f);
        }

        public void Hide()
        {
            root.gameObject.SetActive(false);
        }

        public void ShowAnim()
        {
            animateMap = true;
        }

        private void SetLocalMapPosition()
        {
            RectTransform curPoint = currentMap.Points[PlayerStatsData.LocalStage];
            currentMap.Root.anchoredPosition = new Vector2(
                0f, Mathf.Clamp(-currentMap.Points[PlayerStatsData.LocalStage].anchoredPosition.y, -247, 247));

            rocketImg.sprite = RocketSkinsData.RocketSkin;
            rocketTransform.SetParent(currentMap.Root);
            rocketTransform.anchoredPosition = curPoint.anchoredPosition;
            rocketTransform.localScale = new Vector2(Mathf.Sign(curPoint.anchoredPosition.x), 1f);
        }

        private void LaunchGame()
        {
            AudioControl.Instance.Click();
            RouletteScreen.Instance.Show();
        }

        [ContextMenu("UP Stage")]
        private void UpStage()
        {
            PlayerStatsData.LocalStage++;
            animateMap = true;
        }
    }
}
