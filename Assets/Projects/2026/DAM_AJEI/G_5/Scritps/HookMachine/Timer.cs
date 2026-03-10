using TMPro;
using UnityEditorInternal.VersionControl;
using UnityEngine;

namespace Autohand.Demo
{

    public class Timer : MonoBehaviour
    {
        public GameLoopManager gameLoopManager;

        [SerializeField] private float duration = 31f;
        private float timeRemaining = 0f;
        private TMPro.TextMeshPro textMeshPro;
        public bool bIsFinished = true;
        public bool RestartGame =true;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            timeRemaining = duration;
            textMeshPro = GetComponent<TextMeshPro>();
        }

        // Update is called once per frame
        void Update()
        {
            if (bIsFinished) return;

            timeRemaining -= Time.deltaTime;

            if (timeRemaining < 0f)
            {
                TimeOut();
                bIsFinished = true;
            }

            if (!bIsFinished) UpdateText();
        }

        public void StartTimer()
        {
            if (!RestartGame) return;
            RestartGame = false;
            bIsFinished = false;

            timeRemaining = duration;

            int timeToShow = (int)duration;
            textMeshPro.text = timeToShow.ToString();
            gameLoopManager.StartMovement();
        }

        private void TimeOut()
        {
            Debug.Log("TIME OUT!");
            textMeshPro.text = "XXX";
            gameLoopManager.EndGame();
        }

        private void UpdateText()
        {
            int timeToShow = (int)timeRemaining;
            textMeshPro.text = timeToShow.ToString();
        }
    }
}