using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndOfDayScript : MonoBehaviour
{
    [SerializeField] private ManagmentMenu managmentMenu;

    public void StartNewDay()
    {
        //������ �������� ���������� � ��������� ����������� �� ������ � ����
        ScoreInfo.Instance.score = managmentMenu.summe;
        //����� ������ ���������� sadStudentsToday?
        ScoreInfo.Instance.sadStudents = 0;

        SceneManager.LoadScene("GameScene");
    }

    public void GameOver()
    {
        //�������� ���� ��� �������� ��� ���������� ���� (�������� ����� ������� ����� ��� ����� � ����� ScoreInfo)
        ScoreInfo.Instance.score = 0;
        ScoreInfo.Instance.sadStudents = 0;
        ScoreInfo.Instance.happyStudents = 0;

        SceneManager.LoadScene("MainMenu");
    }
}
