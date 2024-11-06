using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class RouletteScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private RectTransform rouletteRoot;

        public static RouletteScreen Instance { get; private set; }

        private float rollRemainTime;

        // Start is called before the first frame update
        void Start()
        {
            Instance = this;
        }

        public void Show()
        {
            canvasGroup.gameObject.SetActive(true);

            AudioControl.Instance.RouletteLaunch();
            rollRemainTime = Random.Range(3f, 5f);
        }

        public void Hide()
        {
            canvasGroup.gameObject.SetActive(false);
            AudioControl.Instance.Click();
        }

        private void Update()
        {
            if(rollRemainTime > 0)
            {
                rouletteRoot.Rotate(Vector3.forward * rollRemainTime * 100f * Time.deltaTime);

                rollRemainTime -= Time.deltaTime;

                if (rollRemainTime <= 0f)
                {
                    AudioControl.Instance.StopRoulette();
                    float angle = rouletteRoot.rotation.eulerAngles.z;
                    int variant = Mathf.FloorToInt(angle + 22.5f) / 45 % 8;
                    Debug.Log("Wheel angle " + angle);

                    StartCoroutine(OpenGame(variant));
                }
            }
        }

       IEnumerator OpenGame(int _variant)
        {
            yield return new WaitForSeconds(2f);

            LoadingScreen.Show();
            Minigames.MinigamesController.Instance.OpenGame(_variant);
            Hide();
        }
    }
}
