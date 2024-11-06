using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Figures
{
    public class FiguresController : BaseMinigame
    {
        [SerializeField] private Canvas canvas;

        [SerializeField] private Sprite[] sprites;

        [SerializeField] private Image[] images;
        [SerializeField] private Button[] buttons;

        private int currentQuery = 0;

        private void Start()
        {
            canvas.worldCamera = Camera.main;

            for(int index = 0; index < buttons.Length; index++)
            {
                int tempInd = index;
                buttons[index].onClick.AddListener(() => OnClickFigure(tempInd));
            }

            List<int> randomQuery = new List<int>();
            for(var i = 0; i < 4; i++)
            {
                randomQuery.Add(i);
            }

            int spriteIndex = Random.Range(0, sprites.Length);
            for(int index = 0; index < images.Length; index++)
            {
                int randomBtnInd = Random.Range(0, randomQuery.Count);
                buttons[randomQuery[randomBtnInd]].image.sprite = sprites[spriteIndex];
                buttons[randomQuery[randomBtnInd]].image.rectTransform.anchoredPosition = new Vector2(
                    -200 + (index % 2 * 400) + Random.Range(-100, 100),
                    -200 + (index / 2 * 400) + Random.Range(-100, 100));
                randomQuery.RemoveAt(randomBtnInd);

                images[index].sprite = sprites[spriteIndex];
                spriteIndex += Random.Range(1, 3);
                spriteIndex %= sprites.Length;
            }
        }

        private void OnClickFigure(int _id)
        {
            if(buttons[_id].image.sprite == images[currentQuery].sprite)
            {
                currentQuery++;

                buttons[_id].GetComponentInChildren<TMP_Text>().text = currentQuery.ToString();

                if (currentQuery == 4)
                {
                    Win();
                }
                else
                {
                    AudioControl.Instance.Click();
                }
            }
            else
            {
                Lose();
            }
        }
    }
}
