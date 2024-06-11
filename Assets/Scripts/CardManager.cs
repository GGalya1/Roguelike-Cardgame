using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform hand;

    public void CreateCard(string cardName)
    {
        //schauen, ob in DB vorhanden ist
        Card card = CardDatabase.Instance.GetCardByName(cardName);
        if (card != null)
        {
            // Instantiate und platzieren im Hand (Canvas)
            GameObject newCard = Instantiate(cardPrefab, hand);
            CardDecorationManager temp = newCard.GetComponent<CardDecorationManager>();
            if (temp != null)
            {
                temp.DecorateCard(card.name);
            }
            else
            {
                Debug.LogError("CardDecorationManager component not found on cardPrefab!");
            }
        }
        else
        {
            Debug.LogWarning("Card not found and can't instantiate: " + cardName + "!");
        }
    }

    public void DrawCard()
    {
        if(DeckManager.Instance.getDeck().Count == 0)
        {
            Debug.LogWarning("Deck is empty !");
            return;
        }
        //schauen, ob in DB vorhanden ist
        Card card = DeckManager.Instance.DrawCard();
        if (card != null)
        {
            // Instantiate und platzieren im Hand (Canvas)
            GameObject newCard = Instantiate(cardPrefab, hand);
            CardDecorationManager temp = newCard.GetComponent<CardDecorationManager>();
            if (temp != null)
            {
                temp.DecorateCard(card.name);
            }
            else
            {
                Debug.LogError("CardDecorationManager component not found on cardPrefab!");
            }
        }
        else
        {
            Debug.LogWarning("Deck is empty !");
        }
    }
}
