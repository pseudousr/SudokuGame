using System;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    private int hour_ = 0;
    private int minute_ = 0;
    private int seconds_ = 0;

    private Text textClock;
    private float delta_time;
    private bool stop_clock_ = false;

    public static Clock instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        textClock = GetComponent<Text>();

        if (GameSettings.Instance.GetContinuePreviousGame())
            delta_time = Config.ReadGameTime();
        else    
            delta_time = 0;
    }


    // Start is called before the first frame update
    void Start()
    {
        stop_clock_ = false;
    }

    public static string LeadingZero(int n)
    {
        return n.ToString().PadLeft(totalWidth: 2, paddingChar: '0');
    }

    // Update is called once per frame
    void Update()
    {
       if( GameSettings.Instance.GetPaused() == false && stop_clock_ == false)
        {
            delta_time += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(delta_time);

            string hour = LeadingZero(span.Hours);
            string minute = LeadingZero(span.Minutes);
            string seconds = LeadingZero(span.Seconds);

            textClock.text = hour + ":" + minute + ":" + seconds;
        }
        
    }


    public void  OnGameOver()
    {
        stop_clock_ = true;
    }

    private void OnEnable()
    {
        GameEvents.OnGameOver += OnGameOver; 
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= OnGameOver;
    }
    
    public static string GetCurrentTime()
    {
        TimeSpan span = TimeSpan.FromSeconds(instance.delta_time);
        return $"{LeadingZero(span.Hours)}:{LeadingZero(span.Minutes)}:{LeadingZero(span.Seconds)}";
    }


    public string GetCurrentTimeText()
    {
        return textClock.text;
    }

}
