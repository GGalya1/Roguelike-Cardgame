using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KrankScene : MonoBehaviour
{
    public void NextDay()
    {
        ScoreInfo.Instance.day++;
        SceneManager.LoadScene("ManagmentScene");
    }
}
