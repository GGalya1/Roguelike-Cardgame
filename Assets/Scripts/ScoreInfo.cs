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
    public float defaultCookingTime = 3f;

    //������������ ���������� ��� ����� ���������
    public int day = 1;
    public int daysWithoutFood = 0;
    public int semesterbeitragToPay = 1;
    public bool isHomeless = false;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ��������� ������ ��� ����� ����
        }
        else
        {
            Destroy(gameObject);
        }
        day = 1;
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
