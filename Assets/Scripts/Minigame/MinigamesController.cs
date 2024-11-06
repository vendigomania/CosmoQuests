using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames
{
    public class MinigamesController : MonoBehaviour
    {
        [SerializeField] private BaseMinigame[] minigames;
        [SerializeField] private ResultScreen resultScreen;

        public ResultScreen ReusltScreen => resultScreen;
        public static MinigamesController Instance { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            Instance = this;
        }

        public void OpenGame(int _variant)
        {
            Debug.Log($"Game start {_variant}");
            Instantiate(minigames[_variant], transform);
        }
    }
}
