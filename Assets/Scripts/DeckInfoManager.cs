using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckInfoManager : MonoBehaviour
{
    [SerializeField] private TMP_Text deckInfo;
    private Dictionary<string, int> typesOfCard;

    void Update()
    {
        deckInfo.text = GetDeckInfo();
    }

    private string GetDeckInfo()
    {
        typesOfCard = new Dictionary<string, int>();
        List<Card> localDeck = DeckManager.Instance.getDeck();
        string temp = "Cards left: " + localDeck.Count + "\n";

        //speichern der aktuellen Typen und deren Anzahl
        for (int i = 0; i < localDeck.Count; i++)
        {
            if (typesOfCard.ContainsKey(localDeck[i].type))
            {
                typesOfCard[localDeck[i].type]++;
            }
            else
            {
                typesOfCard[localDeck[i].type] = 1;
            }
        }

        //schreiben der Typen und deren Anzahl in den String
        foreach (var entry in typesOfCard)
        {
            temp += "Cards of type " + entry.Key + ": " + entry.Value + "\n";
        }
        return temp;
    }
}
