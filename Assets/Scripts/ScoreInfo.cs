using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreInfo : MonoBehaviour
{

    public static ScoreInfo Instance { get; private set; }
    //[SerializeField] private TMP_Text scoreInfo;
    public int score;
    public int sadStudents;
    public int happyStudents;
    public float cookingTime = 3f; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем объект при смене сцен
        }
        else
        {
            Destroy(gameObject);
        }

        cookingTime = 9f;
        //scoreInfo.text = "Score: 0";
    }

    // Update is called once per frame
    /*void Update()
    {
        scoreInfo.text = "Score: " + score + "\n";
        if (sadStudents > 0)
        {
            scoreInfo.text += "Students sad: " + sadStudents + "\n";
        }
        if (happyStudents > 0)
        {
            scoreInfo.text += "Students happy: " + happyStudents + "\n";
        }
    }*/
}
