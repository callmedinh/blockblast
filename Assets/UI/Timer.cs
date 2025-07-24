using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Timer
    {
        private float _elapsedTime = 0f;
        private TMP_Text _timerText;

        public Timer(TMP_Text timerText)
        {
            _timerText = timerText;
            timerText.text = "00:00";
            _elapsedTime = 0f;
        }

        public void DisplayTimer()
        {
            _elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(_elapsedTime/60);
            int seconds = Mathf.FloorToInt(_elapsedTime % 60);
            _timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}