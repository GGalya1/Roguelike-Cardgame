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

    [SerializeField] public Toggle kaltmieteToggle;
    [SerializeField] public Toggle kautionToggle;
    [SerializeField] public Toggle semesterbeitragToggle;
    [SerializeField] public Toggle essenToggle;

    //тут секция с доп инфой для каждого из пунктов
    [SerializeField] private TMP_Text beitragCount;
    private bool needPayBeitrag = false;
    [SerializeField] private TMP_Text hungerWarning;
    [SerializeField] private TMP_Text kautionWarning;


    int score;
    int strafe;
    public int summe;
    int kaltmiete = 1;
    int semesterbeitrag;
    int lebensmittel = 1;
    int kaution = 1;

    // Start is called before the first frame update
    void Start()
    {
        ScoreInfo scoreInfo = ScoreInfo.Instance;
        semesterbeitrag = scoreInfo.semesterbeitragToPay;

        //um Berechnungen zu ermoeglichen
        score = scoreInfo.score;
        strafe = scoreInfo.sadStudents / 3;
        if (scoreInfo.isHomeless)
        {
            scores[4].gameObject.SetActive(true);
            kautionToggle.gameObject.SetActive(true);
            scores[4].text = kaution.ToString();
            kautionToggle.onValueChanged.AddListener(kautionValueChanged);

            kautionWarning.gameObject.SetActive(true);

            summe = score - (strafe + lebensmittel + semesterbeitrag + kaution + kaltmiete);
        }
        else
        {
            scores[4].gameObject.SetActive(false);
            kautionToggle.gameObject.SetActive(false);
            kautionWarning.gameObject.SetActive(false);

            //передвинуто снизу наверх, потому что неизвестно ебанёт или не ебанёт, если компонент выключен
            summe = score - (strafe + lebensmittel + semesterbeitrag + kaltmiete);
        }

        UpdateScoreInfo();
        CheckLooseCondition();

        //damit man Werte aus Toggles auslesen kann
        kaltmieteToggle.onValueChanged.AddListener(kaltValueChanged);
        //kautionToggle.onValueChanged.AddListener(kautionValueChanged);
        semesterbeitragToggle.onValueChanged.AddListener(semesterbeitragValueChanged);
        essenToggle.onValueChanged.AddListener(essenValueChanged);

        scores[1].text = kaltmiete.ToString();
        scores[2].text = semesterbeitrag.ToString();
        scores[3].text = lebensmittel.ToString();
        //scores[4].text = kaution.ToString();
        scores[5].text = scoreInfo.sadStudents.ToString();

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

        if (scoreInfo.day % 7 == 0 && scoreInfo.day != 0)
        {
            needPayBeitrag = true;
            beitragCount.text = "Bezahlen Sie bitte die vorliegende Summe !";
            beitragCount.color = Color.yellow;
        }
        else
        {
            beitragCount.text = $"{7 - (scoreInfo.day % 7)} Tagen bis zur naechsten Zahlung.";
        }

        if (scoreInfo.daysWithoutFood > 2)
        {
            scoreInfo.cookingTime *= 2;
            hungerWarning.gameObject.SetActive(true);
            hungerWarning.color = Color.yellow;
            hungerWarning.text = $"wegen Hunger ist die Zeit fuer Kochen auf {scoreInfo.cookingTime} Sekunden erhoeht";
        }
        else
        {
            hungerWarning.gameObject.SetActive(false);
            scoreInfo.cookingTime = scoreInfo.defaultCookingTime;
        }
        
    }

    public void CheckLooseCondition()
    {
        if (summe < 0 || (needPayBeitrag && !semesterbeitragToggle.isOn))
        {
            nextDayButton.gameObject.SetActive(false);
            gameOverButton.gameObject.SetActive(true);
        }
        else
        {
            gameOverButton.gameObject.SetActive(false);
            nextDayButton.gameObject.SetActive(true);
        }
    }
    private void UpdateScoreInfo()
    {
        if (summe < 0)
        {
            texts[7].color = Color.red;
        }
        else if (summe < score - (kaltmiete + kaution + lebensmittel))
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
    private void kautionValueChanged(bool isOn)
    {
        if (isOn)
        {
            summe -= kaution;
        }
        else
        {
            summe += kaution;
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

    public void setGlobalVariablesToDefault()
    {
        ScoreInfo scoreInfo = ScoreInfo.Instance;
        scoreInfo.day = 1;
        scoreInfo.cookingTime = scoreInfo.defaultCookingTime;

        scoreInfo.happyStudents = 0;
        scoreInfo.sadStudents = 0;
        scoreInfo.score = 0;

        scoreInfo.daysWithoutFood = 0;
        scoreInfo.isHomeless = false;
    }
}
