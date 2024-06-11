using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameInfoPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreInfo;


    // Update is called once per frame
    void Update()
    {
    int score = ScoreInfo.Instance.score;
    int sadStudents = ScoreInfo.Instance.sadStudents;
    int happyStudents = ScoreInfo.Instance.happyStudents;

    scoreInfo.text = "Score: " + score + "\n";
        if (sadStudents > 0)
        {
            scoreInfo.text += "Students sad: " + sadStudents + "\n";
        }
        if (happyStudents > 0)
        {
            scoreInfo.text += "Students happy: " + happyStudents + "\n";
        }
    }
}
