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
        //должны обновить информацию в синглтоне основываясь на выборе в меню
        ScoreInfo.Instance.score = managmentMenu.summe;
        //может ввести переменную sadStudentsToday?
        ScoreInfo.Instance.sadStudents = 0;

        SceneManager.LoadScene("GameScene");
    }

    public void GameOver()
    {
        //обнуляем весь наш прогресс при завершении игры (наверное можно сделать метод для этого в самом ScoreInfo)
        ScoreInfo.Instance.score = 0;
        ScoreInfo.Instance.sadStudents = 0;
        ScoreInfo.Instance.happyStudents = 0;

        SceneManager.LoadScene("MainMenu");
    }
}
