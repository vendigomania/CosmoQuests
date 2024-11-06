using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Flask
{
    public class FlaskFillController : BaseMinigame
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Image fillImg;

        [SerializeField] private TMP_Text bonusLable;
        [SerializeField] private Animator bonusAnimator;

        float fillProgress = 0.3f;

        float bonusTimer;

        bool isPlaying = false;

        private void Start()
        {
            canvas.worldCamera = Camera.main;
        }

        private void Update()
        {
            if (!isPlaying) return;

            if (fillImg.fillAmount == 0f)
            {
                isPlaying = false;
                Lose();
            }
            else if(fillImg.fillAmount == 1f)
            {
                isPlaying = false;
                Win();
            }

            fillImg.fillAmount = Mathf.Lerp(fillImg.fillAmount, fillProgress, Time.deltaTime * 4f);

            fillProgress -= Time.deltaTime / 10f;

            //bonus
            if (bonusTimer > 0f)
            {
                bonusTimer -= Time.deltaTime;

                if(bonusTimer <= 0f)
                {
                    int bonus = Random.Range(-20, 10);

                    fillProgress += bonus / 100f;

                    bonusLable.text = $"{bonus}%";
                    if (bonus > 0)
                    {
                        bonusAnimator.Play("Profit");
                    }
                    else if (bonus < 0)
                    {
                        bonusAnimator.Play("Deficit");
                    }

                    bonusTimer = Random.Range(3, 8);
                }
            }
        }

        public void Fill()
        {
            isPlaying = true;

            fillProgress += 3f * Time.deltaTime;

            if(bonusTimer == 0)
            {
                bonusTimer = Random.Range(3, 8);
            }

            AudioControl.Instance.Click();
        }
    }
}
