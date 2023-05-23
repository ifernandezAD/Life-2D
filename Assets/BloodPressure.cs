using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class BloodPressure : MonoBehaviour
{
    public static BloodPressure Instance;

    public float pressureLevel = 50;
    public TextMeshProUGUI pressureText;

    public int lowPressureCadence;
    public int pressureDecreaseRate;
    public float modifierMultiplier;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private Volume highPressureVolume;
    [SerializeField] private Volume lowPressureVolume;

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
        }
        else if (pressureLevel <= 0 && GameManager.Instance.gameIsRunning)
        {
            pressureLevel = 0;
            GameManager.Instance.gameIsRunning = false;
            Debug.Log("You're dead Game Over");
        }
        else if (pressureLevel > 50 && pressureLevel < 100 && GameManager.Instance.gameIsRunning)
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
        //Falta la frecuencia del sonido de latido, la música y el postprocessing

        //Postprocessing
        highPressureVolume.enabled = true;
        lowPressureVolume.enabled = false;

        highPressureVolume.weight = (pressureLevel - 50) / 50;

        //Movement
        playerController.speed = playerController.initialSpeed * (pressureLevel / modifierMultiplier);
        playerController.jumpForce = playerController.initialJumpForce * (pressureLevel / modifierMultiplier);



        //Debug.Log("Speed is" + playerController.speed);
        Debug.Log("Caution High Blood Pressure");
    }

    private void OnLowBloodPressure()
    {
        //Postprocessing
        highPressureVolume.enabled = false;
        lowPressureVolume.enabled = true;

        lowPressureVolume.weight = (50 - pressureLevel) / 50 ;

        //Movement
        playerController.speed = playerController.initialSpeed * (modifierMultiplier / (100 - pressureLevel) );
        playerController.jumpForce = playerController.initialJumpForce * (modifierMultiplier / (100 - pressureLevel));

        Debug.Log("Caution Low Blood Pressure");
    }
}

