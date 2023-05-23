using System.Collections;
using TMPro;
using UnityEngine;

public class BloodPressure : MonoBehaviour
{
    public static BloodPressure Instance;

    public float pressureLevel = 50;
    public TextMeshProUGUI pressureText;

    public int lowPressureCadence;
    public int pressureDecreaseRate;

    [SerializeField] private PlayerController playerController;

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

        playerController = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerController>();
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
        Debug.Log("Blood pressure corroutine is running");
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
        if (pressureLevel >= 100 && GameManager.Instance.gameIsRunning)
        {
            pressureLevel = 100;
            GameManager.Instance.gameIsRunning = false;
            Debug.Log("You're dead Game Over");
        }else if (pressureLevel <= 0 && GameManager.Instance.gameIsRunning)
        {
            pressureLevel = 0;
            GameManager.Instance.gameIsRunning = false;
            Debug.Log("You're dead Game Over");
        }else if (pressureLevel > 50 && pressureLevel < 100 && GameManager.Instance.gameIsRunning)
        {
            OnHighBloodPressure();
        }
        else if (pressureLevel > 0 && pressureLevel < 50 && GameManager.Instance.gameIsRunning)
        {
            OnLowBloodPressure();
        }
    }

    private void OnHighBloodPressure()
    {
        playerController.speed = playerController.speed * (pressureLevel / 10);
        Debug.Log("Speed is" + playerController.speed);
        Debug.Log("Caution High Blood Pressure");
    }

    private void OnLowBloodPressure()
    {
        Debug.Log("Caution Low Blood Pressure");
    }
}

