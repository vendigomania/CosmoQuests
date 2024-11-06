using Data;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Minigames
{
    public class BaseMinigame : MonoBehaviour
    {
        protected void Win()
        {
            PlayerStatsData.LocalStage++;
            MinigamesController.Instance.ReusltScreen.Show(true, Confirm);

            AudioControl.Instance.Right();
        }

        protected void Lose()
        {
            PlayerStatsData.Lives--;
            MinigamesController.Instance.ReusltScreen.Show(false, Confirm);

            AudioControl.Instance.Fail();
        }

        protected virtual void Confirm()
        {
            LoadingScreen.Show();
            MinigamesController.Instance.ReusltScreen.Hide();
            AudioControl.Instance.Click();

            Destroy(gameObject);
        }
    }
}
