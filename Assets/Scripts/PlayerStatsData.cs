using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Data
{
    public class PlayerStatsData : MonoBehaviour
    {
        public static UnityAction OnCoinsChanged;
        public static int Coins
        {
            get => PlayerPrefs.GetInt("Coins", 0);
            set
            {
                PlayerPrefs.SetInt("Coins", value);
                OnCoinsChanged?.Invoke();
            }
        }

        //map data
        public static int GlobalStage
        {
            get => PlayerPrefs.GetInt("GlobalStage", 0);
            set => PlayerPrefs.SetInt("GlobalStage", value);
        }

        public static int LocalStage
        {
            get => PlayerPrefs.GetInt("LocalStage", 0);
            set => PlayerPrefs.SetInt("LocalStage", value);
        }

        //lives
        public static UnityAction<int> OnLivesChanged;

        public static int Lives
        {
            get => PlayerPrefs.GetInt("Lives", 3);
            set
            {
                PlayerPrefs.SetInt("Lives", value);
                OnLivesChanged?.Invoke(value);
            }
        }

        [ContextMenu("Clear")]
        private void ClearAll()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
