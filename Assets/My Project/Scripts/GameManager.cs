using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float waitTime = 1f;
    private int timePassed = 0;

    [SerializeField]
    private TextMeshProUGUI scoreTxt;
    private int scorePts = 0;

    [SerializeField]
    private Image fanButton;
    private Color activeFan, unactiveFan;

    private bool fanActive = false;

    private bool gameOver = false;

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

    public delegate void OnValueChange();
    public OnValueChange onValueChangeCallback;

    public delegate void OnFanBody();
    public OnFanBody onFanBodyCallback;
    public delegate void OnFanFire();
    public OnFanFire onFanFireCallback;

    private void Start()
    {
        activeFan = Color.blue;
        unactiveFan = Color.white;

        StartCoroutine(ValueChange());
        StartCoroutine(TimePassing());
    }
    IEnumerator TimePassing()
    {
        yield return new WaitForSeconds(5f);
        scorePts += 5;
        UpdateScore();

        if (!gameOver)
        {
            StartCoroutine(TimePassing());
        }
    }

    IEnumerator ValueChange()
    {
        if (timePassed < 50)
        {
            timePassed++;
            if (timePassed % 5 == 0)
            {
                Debug.Log("Time speed up");
                waitTime -= 0.09f;
            }
        }
        onValueChangeCallback?.Invoke();
        yield return new WaitForSeconds(waitTime);
        if(!gameOver)
        {
            StartCoroutine(ValueChange());
        }
    }

    public void Fan()
    {
        fanActive = !fanActive;
        if(fanButton == null) { return; }
        if (fanActive)
        {
            fanButton.color = activeFan;
        }
        else
        {
            fanButton.color = unactiveFan;
        }
    }

    public void CoolBody()
    {
        if (!fanActive) { return; }
        if (gameOver) { return; }

        onFanBodyCallback?.Invoke();
    }

    public void HeatFire()
    {
        if (!fanActive) { return; }
        if(gameOver) { return; }

        onFanFireCallback?.Invoke();
        
    }

    public void GameOver()
    {
        gameOver = true;
        StopCoroutine(ValueChange());
        Debug.Log("Game Over");
    }

    public bool GetGameOverStatus()
    {
        return gameOver;
    }

    public void AddScorePts(int pts)
    {
        scorePts += pts;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreTxt.text = scorePts + " pts";
    }
}
