using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]private float duration = 31f;
    private float timeRemaining = 0f;
    private TMPro.TextMeshPro textMeshPro;
    private bool bIsFinished = true;

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
        if (!bIsFinished) return;

        bIsFinished = false;

        timeRemaining = duration;

        int timeToShow = (int)duration;
        textMeshPro.text = timeToShow.ToString();
    }

    private void TimeOut()
    {
        Debug.Log("TIME OUT!");
        textMeshPro.text = "XXX";
    }

    private void UpdateText()
    {
        int timeToShow = (int)timeRemaining;
        textMeshPro.text = timeToShow.ToString();
    }
}
