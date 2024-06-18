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
        ScoreInfo scoreInfo = ScoreInfo.Instance;
        //������ �������� ���������� � ��������� ����������� �� ������ � ����
        scoreInfo.score = managmentMenu.summe;
        //����� ������ ���������� sadStudentsToday?
        scoreInfo.sadStudents = 0;

        //Semesterbeitrag logic
        if (!managmentMenu.semesterbeitragToggle.isOn)
        {
            scoreInfo.semesterbeitragToPay++;
        }
        else if (managmentMenu.semesterbeitragToggle.isOn && scoreInfo.semesterbeitragToPay > 1)
        {
            scoreInfo.semesterbeitragToPay = 1;
        }

        //������ ���������
        if (!managmentMenu.essenToggle.isOn)
        {
            scoreInfo.daysWithoutFood++;
        }
        else
        {
            scoreInfo.daysWithoutFood = 0;
        }

        //������ ��� �����
        if (!managmentMenu.kaltmieteToggle.isOn || (scoreInfo.isHomeless && !managmentMenu.kautionToggle.isOn))
        {
            scoreInfo.isHomeless = true;
            int rnd = Random.Range(0, 11);
            if (rnd < 4)
            {
                SceneManager.LoadScene("KrankScene");
                //����� ����� �� �������� ������������ � �� ��������� � Load(GameScene)
                return;
            }
            else
            {
                SceneManager.LoadScene("GameScene");
            }
        }
        else if(scoreInfo.isHomeless && managmentMenu.kautionToggle.isOn && managmentMenu.kaltmieteToggle.isOn)
        {
            scoreInfo.isHomeless = false;
        }
        SceneManager.LoadScene("GameScene");
    }

    public void GameOver()
    {
        //�������� ���� ��� �������� ��� ���������� ���� (�������� ����� ������� ����� ��� ����� � ����� ScoreInfo)
        managmentMenu.setGlobalVariablesToDefault();
        SceneManager.LoadScene("MainMenu");
    }
}
