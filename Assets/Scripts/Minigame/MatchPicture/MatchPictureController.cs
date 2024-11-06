using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.MatchPicture
{
    public class MatchPictureController : BaseMinigame
    {
        [SerializeField] private Canvas canvas;

        [SerializeField] private Sprite[] sprites;

        [SerializeField] private MatchSlider slider;
        [SerializeField] private Image backImg;
        [SerializeField] private RectTransform blackQuad;

        [SerializeField] private Image matchImg;
        [SerializeField] private RectTransform matchMoveQuad;

        // Start is called before the first frame update
        void Start()
        {
            canvas.worldCamera = Camera.main;

            blackQuad.sizeDelta = new Vector2(Random.Range(160f, 280f), Random.Range(160f, 280f));
            blackQuad.anchoredPosition = new Vector2(Random.Range(160f, 260f) * (Random.Range(0, 2) == 0? -1 : 1), Random.Range(-240f, 240f));

            matchMoveQuad.sizeDelta = blackQuad.sizeDelta;
            matchMoveQuad.anchoredPosition = new Vector2(-blackQuad.anchoredPosition.x, blackQuad.anchoredPosition.y);

            matchImg.rectTransform.anchoredPosition = -blackQuad.anchoredPosition;

            int randomSpriteIndex = Random.Range(0, sprites.Length);
            backImg.sprite = sprites[randomSpriteIndex];
            matchImg.sprite = sprites[randomSpriteIndex];

            slider.value = (matchMoveQuad.anchoredPosition.x + 300f) / 600f;

            ///
            slider.onValueChanged.AddListener(OnValueChanged);
            slider.OnEndDragEvent.AddListener(OnEndDrag);
        }

        private void OnValueChanged(float _value)
        {
            matchMoveQuad.anchoredPosition = new Vector2(-300f + 600f * _value, blackQuad.anchoredPosition.y);
        }

        private void OnEndDrag(float _value)
        {
            if(Vector2.Distance(matchMoveQuad.anchoredPosition, blackQuad.anchoredPosition) < 10f)
            {
                Win();

                AudioControl.Instance.Right();
            }
            else
            {
                Lose();

                AudioControl.Instance.Fail();
            }
        }
    }
}
