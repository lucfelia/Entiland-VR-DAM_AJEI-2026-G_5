using TMPro;
using UnityEngine;

namespace Autohand.Demo
{

    public class Timer : MonoBehaviour
    {
        [SerializeField] private float duration = 31f;
        private float timeRemaining = 0f;
        private TMPro.TextMeshPro textMeshPro;
        public bool bIsFinished = true;

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
            }

            if (!bIsFinished) UpdateText();
        }

        public void StartTimer()
        {
            if (!bIsFinished) return;

            bIsFinished = false;

            timeRemaining = duration;

            int timeToShow = (int)duration;
            textMeshPro.text = timeToShow.ToString();
            GameLoopManager.Instance.StartMovement();
        }

        private void TimeOut()
        {
            Debug.Log("TIME OUT!");
            textMeshPro.text = "XXX";
            GameLoopManager.Instance.EndGame();
        }

        private void UpdateText()
        {
            int timeToShow = (int)timeRemaining;
            textMeshPro.text = timeToShow.ToString();
        }
    }
}