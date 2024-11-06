using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Minigames.Cards
{
    public class CardsController : BaseMinigame
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private TMP_Text triesLable;
        [SerializeField] private Sprite[] typeSprites;
        [SerializeField] private List<CardVisualizator> visualizators = new List<CardVisualizator>();

        int remain = 6;
        int tries = 9;
        CardVisualizator first;
        CardVisualizator second;

        // Start is called before the first frame update
        void Start()
        {
            canvas.worldCamera = Camera.main;

            for(int index = 0; index < 11; index++)
            {
                visualizators.Add(Instantiate(visualizators[0], visualizators[0].transform.parent));
            }

            CardVisualizator.OnChoice += OnCoice;

            int setTypeIndex = Random.Range(0, typeSprites.Length);
            for(int i = 0; i < visualizators.Count; i++)
            {
                visualizators[i].SetType(typeSprites[(setTypeIndex + i / 2) % typeSprites.Length]);
                visualizators[i].transform.SetSiblingIndex(Random.Range(0, visualizators.Count));
            }
        }

        private void Update()
        {
            triesLable.transform.localScale = Vector3.Lerp(triesLable.transform.localScale, Vector3.one, 3f * Time.deltaTime);
        } 

        protected override void Confirm()
        {
            CardVisualizator.OnChoice -= OnCoice;

            base.Confirm();
        }

        private void OnCoice(CardVisualizator _vis)
        {
            AudioControl.Instance.Click();

            if(first == null)
            {
                first = _vis;
                first.Open();
            }
            else if(second == null)
            {
                second = _vis;
                second.Open();

                StartCoroutine(Check());
            }
        }  

        IEnumerator Check()
        {
            yield return new WaitForSeconds(1f);

            if(first.Icon == second.Icon)
            {
                remain--;

                if (remain == 0) Win();
            }
            else
            {
                first.Close();
                second.Close();

                tries--;
                triesLable.text = tries.ToString();
                triesLable.transform.localScale = Vector3.one * 1.5f;

                if (tries == 0) Lose();
            }

            first = null;
            second = null;
        }
    }
}
