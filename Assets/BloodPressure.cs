using System.Collections;
using TMPro;
using UnityEngine;

public class BloodPressure : MonoBehaviour
{
    public static BloodPressure Instance;

    public int pressureLevel = 50;
    public TextMeshProUGUI pressureText;

    public int lowPressureCadence;
    public int pressureDecreaseRate;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(DecreasePressureOverTime());
    }

    private void Update()
    {
        UpdatePressureTest();
        CheckBloodPressureStatus();
    }

    IEnumerator DecreasePressureOverTime()
    {
        while (GameManager.Instance.gameIsRunning)
        {
            pressureLevel -= pressureDecreaseRate;
            yield return new WaitForSeconds(lowPressureCadence);
        }
    }

    private void UpdatePressureTest()
    {
        pressureText.text = pressureLevel.ToString();
    }

    public void CheckBloodPressureStatus()
    {
        if (pressureLevel >= 100)
        {
            pressureLevel = 100;
            GameManager.Instance.gameIsRunning = false;
            Debug.Log("You're dead Game Over");
        }
    }
}

