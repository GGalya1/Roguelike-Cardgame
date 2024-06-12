using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManagmentMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text[] scores;
    [SerializeField] private TMP_Text[] texts;
    [SerializeField] private Button nextDayButton;
    [SerializeField] private Button gameOverButton;

    [SerializeField] private Toggle kaltmieteToggle;
    [SerializeField] private Toggle dusheUndStromToggle;
    [SerializeField] private Toggle semesterbeitragToggle;
    [SerializeField] private Toggle essenToggle;

    int score;
    int strafe;
    public int summe;
    int kaltmiete = 1;
    int semesterbeitrag = 1;
    int lebensmittel = 1;
    int warmmiete = 1;

    // Start is called before the first frame update
    void Start()
    {
        //um Berechnungen zu ermoeglichen
        score = ScoreInfo.Instance.score;
        strafe = ScoreInfo.Instance.sadStudents / 3;
        summe = score - (strafe + lebensmittel + semesterbeitrag  + warmmiete + kaltmiete);
        UpdateScoreInfo();
        CheckLooseCondition();

        //damit man Werte aus Toggles auslesen kann
        kaltmieteToggle.onValueChanged.AddListener(kaltValueChanged);
        dusheUndStromToggle.onValueChanged.AddListener(warmValueChanged);
        semesterbeitragToggle.onValueChanged.AddListener(semesterbeitragValueChanged);
        essenToggle.onValueChanged.AddListener(essenValueChanged);

        scores[1].text = kaltmiete.ToString();
        scores[2].text = semesterbeitrag.ToString();
        scores[3].text = lebensmittel.ToString();
        scores[4].text = warmmiete.ToString();
        scores[5].text = ScoreInfo.Instance.sadStudents.ToString();

        if (score <= 0)
        {
            texts[0].color = Color.red;
        }
        else if (score < 15)
        {
            texts[0].color = Color.yellow;
        }
        else
        {
            texts[0].color = Color.green;
        }
    }

    public void CheckLooseCondition()
    {
        if (summe < 0)
        {
            nextDayButton.gameObject.SetActive(false);
            gameOverButton.gameObject.SetActive(true);
        }
        else
        {
            nextDayButton.gameObject.SetActive(true);
            gameOverButton.gameObject.SetActive(false);
        }
    }
    private void UpdateScoreInfo()
    {
        if (summe < 0)
        {
            texts[7].color = Color.red;
        }
        else if (summe < score - (kaltmiete + warmmiete + lebensmittel))
        {
            texts[7].color = Color.yellow;
        }
        else
        {
            texts[7].color = Color.green;
        }

        scores[0].text = score.ToString();
        scores[6].text = strafe.ToString();
        scores[7].text = summe.ToString();
    }

    //Считыватели для чекбоксов
    private void kaltValueChanged(bool isOn)
    {
        if (isOn)
        {
            //если чекбокс включен, вычитаем миете из очков (денег)
            summe -= kaltmiete;
        }
        else
        {
            //если Toggle выключен, добавляем
            summe += kaltmiete;
        }
        CheckLooseCondition();
        UpdateScoreInfo();
    }
    private void warmValueChanged(bool isOn)
    {
        if (isOn)
        {
            summe -= warmmiete;
        }
        else
        {
            summe += warmmiete;
        }
        CheckLooseCondition();
        UpdateScoreInfo();
    }
    private void essenValueChanged(bool isOn)
    {
        if (isOn)
        {
            summe -= lebensmittel;
        }
        else
        {
            summe += lebensmittel;
        }
        CheckLooseCondition();
        UpdateScoreInfo();
    }
    private void semesterbeitragValueChanged(bool isOn)
    {
        if (isOn)
        {
            summe -= semesterbeitrag;
        }
        else
        {
            summe += semesterbeitrag;
        }
        CheckLooseCondition();
        UpdateScoreInfo();
    }
}
