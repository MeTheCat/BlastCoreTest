using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    Text scoreText;
    Text Text
    {
        get
        {
            if (scoreText == null)
            {
                scoreText = GetComponent<Text>();
            }
            return scoreText;
        }
    }

    void Start()
    {
        Text.text = "0";
        ScoreSystem.Instance.ScoreChangedEvent.AddListener((val) => { Text.text = val; });
    }
}
