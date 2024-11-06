using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Minigames.Cards
{
    public class CardVisualizator : MonoBehaviour
    {
        [SerializeField] private GameObject cover;
        [SerializeField] private Image iconImg;

        public bool IsOpen { get; private set; }

        public static UnityAction<CardVisualizator> OnChoice; 
        public Sprite Icon { get; private set; }

        private void Update()
        {
            if(IsOpen)
            {
                if(transform.localScale.x < 1f)
                {
                    transform.localScale = new Vector2(Mathf.Min(1f, transform.localScale.x + Time.deltaTime * 10), 1f);
                    if(transform.localScale.x > 0f && cover.activeSelf)
                    {
                        cover.SetActive(false);
                    }
                }
            }
            else
            {
                if (transform.localScale.x > -1f)
                {
                    transform.localScale = new Vector2(Mathf.Max(-1f, transform.localScale.x - Time.deltaTime * 10), 1f);
                    if (transform.localScale.x < 0f && !cover.activeSelf)
                    {
                        cover.SetActive(true);
                        iconImg.sprite = Icon;
                    }
                }
            }
        }

        public void SetType(Sprite _sprite)
        {
            Icon = _sprite;

            if(IsOpen) Close();
            else
            {
                iconImg.sprite = Icon;
            }
        }

        public void Choice()
        {
            if (IsOpen) return;

            OnChoice?.Invoke(this);
        }

        public void Open()
        {
            IsOpen = true;
        }

        public void Close()
        {
            IsOpen = false;
            cover.SetActive(true);
        }
    }
}
