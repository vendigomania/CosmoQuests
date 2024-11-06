using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class GalaxyResultScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup root;

        [SerializeField] private GameObject winPart;
        [SerializeField] private GameObject losePart;

        public static GalaxyResultScreen Instance { get; private set; }
        private UnityAction confirmAction;

        // Start is called before the first frame update
        void Start()
        {
            Instance = this;
        }

        public void Show(bool _isWin, UnityAction _confirmAaction)
        {
            root.gameObject.SetActive(true);

            winPart.SetActive(_isWin);
            losePart.SetActive(!_isWin);

            confirmAction = _confirmAaction;
        }

        public void Hide()
        {
            root.gameObject.SetActive(false);
        }

        public void OnClickConfirm()
        {
            confirmAction?.Invoke();
            Hide();
        }
    }
}
