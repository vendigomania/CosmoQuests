using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Minigames
{
    public class ResultScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup root;

        [SerializeField] private GameObject winPart;
        [SerializeField] private TMP_Text coinsLable;

        [SerializeField] private GameObject losePart;

        private UnityAction confirmAction;

        public void Show(bool _isWin, UnityAction _confirmAaction)
        {
            root.gameObject.SetActive(true);

            winPart.SetActive(_isWin);
            if(_isWin)
            {
                PlayerStatsData.Coins += 20;
                coinsLable.text = PlayerStatsData.Coins.ToString();
            }

            losePart.SetActive(!_isWin);

            confirmAction = _confirmAaction;
        }

        public void Hide()
        {
            root.gameObject.SetActive(false);
        }

        public void OnClickConfirm()
        {
            UI.MapGlobalScreen.Instance.UpdateLocalMap();
            confirmAction?.Invoke();
            Hide();
        }
    }
}
