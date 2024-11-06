using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Spin
{
    public class SpinController : BaseMinigame
    {
        [SerializeField] private Canvas canvas;

        [SerializeField] private Sprite[] spinSprites;

        [SerializeField] private Image targetImg;
        [SerializeField] private SpinLine[] lines;
        [SerializeField] private Button launchBtn;


        // Start is called before the first frame update
        void Start()
        {
            canvas.worldCamera = Camera.main;

            targetImg.sprite = spinSprites[Random.Range(0, spinSprites.Length)];

            foreach(var line in lines)
            {
                line.Init(spinSprites);
                line.OnStop += OnRoultteStop;
            }

            launchBtn.onClick.AddListener(Launch);
        }

        private void Launch()
        {
            foreach(var line in lines) line.Launch();

            launchBtn.interactable = false;
            AudioControl.Instance.Click();

            AudioControl.Instance.RouletteLaunch();
        }

        int remain = 3;
        private void OnRoultteStop(Sprite _sprite)
        {


            if (_sprite == targetImg.sprite)
            {
                Win();
                AudioControl.Instance.Right();
                AudioControl.Instance.StopRoulette();
            }
            else
            {
                remain--;

                if (remain == 0)
                {
                    AudioControl.Instance.Fail();
                    AudioControl.Instance.StopRoulette();

                    Lose();
                }
            }
        }
    }
}
