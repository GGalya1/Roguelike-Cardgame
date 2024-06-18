using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private DeckManager deckManager;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] public float roundDuration = 5f; //300f for 5 minutes
    float time;

    // Start is called before the first frame update
    void Start()
    {
        time = roundDuration;
        UpdateTimer();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            List<Card> temp = DeckInfoSingel.Instance.cards;

            deckManager.setDeck(temp);
            ScoreInfo.Instance.day++;
            //deckManager.DeckAusprinten();
            EndOfRound();
        }
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        if (minutes <= 0 && seconds <= 0)
        {
            timerText.text = $"Day: {ScoreInfo.Instance.day}\n" + string.Format("Time left: 0:00");
        }
        else timerText.text = $"Day: {ScoreInfo.Instance.day}\n" + string.Format("Time left: {0:0}:{1:00}", minutes, seconds);
    }

    private void EndOfRound()
    {
        SceneManager.LoadScene("ManagmentScene");
    }
}
