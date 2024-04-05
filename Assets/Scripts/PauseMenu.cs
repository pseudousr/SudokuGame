using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Text time_next;

    public void DisplayTime()
    {
        time_next.text = Clock.instance.GetCurrentTimeText();  
    }
}
