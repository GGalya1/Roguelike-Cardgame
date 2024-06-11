using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndOfDayScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewDay()
    {
        //DeckManager.Instance.deckBetweenRounds = DeckManager.Instance.getDeck();
        SceneManager.LoadScene("GameScene");
    }
}
