using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Minigames.Spin
{
    public class SpinLine : MonoBehaviour
    {
        [System.Serializable]
        public struct Cell
        {
            public RectTransform CellTransform;
            public Image CellImage;
        }

        [SerializeField] private Button stopBtn;
        [SerializeField] private Cell[] cells;

        public UnityAction<Sprite> OnStop;

        private Sprite[] spinSprites;

        int spriteIndex;

        // Start is called before the first frame update
        public void Init(Sprite[] _sprites)
        {
            spinSprites = _sprites;

            stopBtn.onClick.AddListener(Stop);

            spriteIndex = Random.Range(0, spinSprites.Length);
            foreach (var cell in cells)
            {
                cell.CellImage.sprite = spinSprites[spriteIndex];
                spriteIndex = (spriteIndex + 1) % spinSprites.Length;
            }

            stopBtn.interactable = false;
        }

        private float motion;

        // Update is called once per frame
        void Update()
        {
            if (motion > 0f)
            {
                for (int index = 0; index < cells.Length; index++)
                {
                    cells[index].CellTransform.anchoredPosition += Vector2.down * Time.deltaTime * motion * 10f;

                    if (cells[index].CellTransform.anchoredPosition.y < -416f)
                    {
                        cells[index].CellTransform.anchoredPosition += Vector2.up * 832f;
                        cells[index].CellImage.sprite = spinSprites[spriteIndex];
                        spriteIndex = (spriteIndex + 1) % spinSprites.Length;
                    }
                }

                if(motion < 100) motion -= Time.deltaTime * 50f;

                if(motion <= 0f)
                {
                    Cell best = cells[0];
                    float minDistance = Mathf.Abs(best.CellTransform.anchoredPosition.y);
                    foreach(var cell in cells)
                    {
                        if(Mathf.Abs(cell.CellTransform.anchoredPosition.y) < minDistance)
                        {
                            best = cell;
                        }
                    }

                    Vector2 move = Vector2.up * -best.CellTransform.anchoredPosition.y;
                    foreach (var cell in cells)
                    {
                        cell.CellTransform.anchoredPosition += move;
                    }

                    foreach(var cell in cells)
                        if (cell.CellTransform.anchoredPosition.y < -416f)
                        {
                            cell.CellTransform.anchoredPosition += Vector2.up * 832f;
                            cell.CellImage.sprite = spinSprites[spriteIndex];
                            spriteIndex = (spriteIndex + 1) % spinSprites.Length;
                        }

                    OnStop?.Invoke(best.CellImage.sprite);
                }
            }
        }

        public void Launch()
        {
            motion = 100f;

            stopBtn.interactable = true;
        }

        private void Stop()
        {
            AudioControl.Instance.Click();

            motion = 99f;

            stopBtn.interactable = false;
        }
    }
}
