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
}

