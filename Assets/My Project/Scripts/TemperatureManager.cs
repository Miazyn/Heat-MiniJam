using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class TemperatureManager : MonoBehaviour
{
    private GameManager manager;

    [SerializeField]
    private Slider bodySlider, temperatureSlider, fireSlider;

    private float temperatureIncrease = 0.5f;

    private float bodyDecrease = 1f;
    private float bodyIncrease = 0.05f;

    private float fireIncrease = 20f;
    private float fireDecrease = 1f;


    private void Start()
    {
        bodySlider.value = bodySlider.minValue;
        temperatureSlider.value = temperatureSlider.minValue;
        fireSlider.value = fireSlider.maxValue;

        manager = GameManager.Instance;

        manager.onValueChangeCallback += IncreaseBodyTemperatur;
        manager.onValueChangeCallback += DecreaseFire;
        manager.onValueChangeCallback += IncreaseTemperature;

        manager.onFanFireCallback += IncreaseFire;
        manager.onFanBodyCallback += DecreaseBodyTemperature;

    }

    private void OnDisable()
    {
        manager.onValueChangeCallback -= IncreaseBodyTemperatur;
        manager.onValueChangeCallback -= DecreaseFire;
        manager.onValueChangeCallback -= IncreaseTemperature;

        manager.onFanFireCallback -= IncreaseFire;
        manager.onFanBodyCallback -= DecreaseBodyTemperature;
    }

    private void IncreaseBodyTemperatur()
    {
        bodySlider.value += bodyIncrease;
        if(bodySlider.value >= bodySlider.maxValue)
        {
            manager.GameOver();
        }
    }

    private void IncreaseTemperature()
    {
        Debug.Log("TEMP increase");
        temperatureSlider.value += temperatureIncrease;
    }

    private void DecreaseFire()
    {
        Debug.Log("Decrease Fire");
        fireSlider.value -= fireDecrease;
        if(fireSlider.value <= fireSlider.minValue)
        {
            manager.GameOver();
        }
    }

    public void DecreaseBodyTemperature()
    {
        bodySlider.value -= bodyDecrease;
    }
    public void IncreaseFire()
    {
        fireSlider.value += fireIncrease;
    }
}
