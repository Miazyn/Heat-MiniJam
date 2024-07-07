using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    GameManager manager;

    [SerializeField]
    private SO_Meal[] allMeals;
    [SerializeField]
    private Sprite[] mealImages;

    private int mealID = 0;
    public SO_Meal pickedMeal;

    [SerializeField]
    private Slider patienceSlider;
    private float patienceReduction = 0.1f;

    private bool finishedRequest = false;

    [SerializeField]
    private Image RequestedItem;
    private void Start()
    {
        manager = GameManager.Instance;
        NewCustomer();
        StartCoroutine(ReducePatience());
    }

    public void NewCustomer()
    {
        
        RandomizeMeal();
        SetImage();

        Debug.Log(pickedMeal.Name + " was picked.");

        SetUpPatienceBar();
        finishedRequest = false;
    }

    public void FinishRequest()
    {
        StopCoroutine(ReducePatience());
        finishedRequest = true;
        manager.AddScorePts(pickedMeal.points);
        NewCustomer();
    }

    IEnumerator ReducePatience()
    {
        yield return new WaitForSeconds(1f);
        patienceSlider.value -= patienceReduction;
        if(patienceSlider.value <= patienceSlider.minValue)
        {
            Debug.Log("Patience ran out");
            NewCustomer();
        }
        if (!manager.GetGameOverStatus() && !finishedRequest)
        {
            StartCoroutine(ReducePatience());
        }
    }

    private void SetUpPatienceBar()
    {
        patienceSlider.value = patienceSlider.maxValue;
    }

    private void RandomizeMeal()
    {
        int rdm = Random.Range(0, allMeals.Length);

        pickedMeal = allMeals[rdm];
        mealID = rdm;
    }

    private void SetImage()
    {
        RequestedItem.sprite = mealImages[mealID];
    }

}