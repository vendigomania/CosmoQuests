using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup root;
        [SerializeField] private Image progress;
        
        private static LoadingScreen Instance;

        // Start is called before the first frame update
        void Start()
        {
            Instance = this;
            enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(progress.fillAmount < 1f)
            {
                progress.fillAmount += Time.deltaTime * 2f;
            }
            else if(root.alpha > 0f)
            {
                root.alpha -= Time.deltaTime * 2f;
            }
            else
            {
                enabled = false;
                root.gameObject.SetActive(false);
            }
        }

        public static void Show()
        {
            Instance.ShowAnimation();
        }

        public void ShowAnimation()
        {
            root.gameObject.SetActive(true);
            root.alpha = 1f;
            progress.fillAmount = 0f;

            enabled = true;
        }
    }
}
