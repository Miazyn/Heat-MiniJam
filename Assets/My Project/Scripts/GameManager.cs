using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Slider bodySlider, temperatureSlider, fireSlider;

    private float temperatureIncrease = 0.1f;

    private float bodyDecrease = 1f;
    private float bodyIncrease = 0.05f;

    private float fireIncrease = 20f;
    private float fireDecrease = 0.5f;

    private float waitTime = 5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        bodySlider.value = bodySlider.minValue;
        temperatureSlider.value = temperatureSlider.minValue;
        fireSlider.value = fireSlider.maxValue;

        StartCoroutine(ValueChange());
    }

    IEnumerator ValueChange()
    {
        Debug.Log("Values are changing.");
        IncreaseTemperature();
        IncreaseBodyTemperatur();
        DecreaseFire();
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(ValueChange());
    }

    private void IncreaseBodyTemperatur()
    {
        bodySlider.value += bodyIncrease;
    }

    private void IncreaseTemperature()
    {
        Debug.Log("TEMP increase");
        temperatureSlider.value += temperatureIncrease;
    }

    private void IncreaseFire()
    {
        temperatureSlider.value += fireIncrease;
    }

    private void DecreaseFire()
    {
        temperatureSlider.value -= fireDecrease;
    }

    private void DecreaseBodyTemperature()
    {
        bodySlider.value -= bodyDecrease;
    }

}
