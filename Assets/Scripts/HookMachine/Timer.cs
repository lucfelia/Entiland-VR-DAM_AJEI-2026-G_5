using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]private float duration = 30f;
    private float timeRemaining = 0f;
    private TMPro.TextMeshPro textMeshPro;
    private bool bIsFinished = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeRemaining = duration;
        textMeshPro = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bIsFinished == true) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining < 0f)
        {
            TimeOut();
            bIsFinished = true;
        }

        UpdateText();
    }

    private void TimeOut()
    {
        Debug.Log("TIME OUT!");
    }

    private void UpdateText()
    {
        int timeToShow = (int)timeRemaining;
        textMeshPro.text = timeToShow.ToString();
    }
}
