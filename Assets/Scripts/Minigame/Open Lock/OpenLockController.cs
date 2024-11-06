using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.OpenLock
{
    public class OpenLockController : BaseMinigame
    {
        [SerializeField] private Canvas canvas;

        [SerializeField] private Slider controlSlider;
        [SerializeField] private GameObject[] activeZones;

        [SerializeField] private Slider[] zoneSliders;

        int currentZoneIndex = 0;
        int direction = 1;

        // Start is called before the first frame update
        void Start()
        {
            canvas.worldCamera = Camera.main;

            for(var ind = 0; ind < zoneSliders.Length; ind++)
            {
                zoneSliders[ind].value = Random.Range(0.2f, 0.8f);
            }

            SetControlSlider();
        }

        // Update is called once per frame
        void Update()
        {
            if (currentZoneIndex >= 3) return;

            if(controlSlider.value == 1f)
            {
                direction = -1;
            }
            else if(controlSlider.value == 0f)
            {
                direction = 1;
            }

            controlSlider.value += Time.deltaTime * direction;
        }

        public void Pick()
        {
            if(Mathf.Abs(controlSlider.value - zoneSliders[currentZoneIndex].value) < (0.1f - currentZoneIndex * 0.03f))
            {
                currentZoneIndex++;

                AudioControl.Instance.Right();

                if (currentZoneIndex == 3)
                {
                    Win();
                }
                else
                {
                    SetControlSlider();
                }
            }
            else
            {
                AudioControl.Instance.Fail();

                if (currentZoneIndex == 0)
                {
                    Lose();
                }
                else
                {
                    currentZoneIndex--;

                    SetControlSlider();
                }
            }
        }

        private void SetControlSlider()
        {
            controlSlider.transform.SetParent(zoneSliders[currentZoneIndex].transform);
            controlSlider.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            for(int ind = 0; ind < zoneSliders.Length;ind++)
            {
                activeZones[ind].SetActive(ind == currentZoneIndex);
            }
        }
    }
}
