using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPS_Counter : MonoBehaviour
{
    public float timer, refresh, avgFramerate;
    public TMP_Text m_Text;

    private void Update()
    {
        float timeLapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timeLapse;

        if (timer<=0)
        {
            avgFramerate=(int)(1f/timeLapse);
        }

        m_Text.text = avgFramerate.ToString();
    }
}
