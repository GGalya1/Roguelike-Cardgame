using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    //так как при смене сцены сбрасывютс€ все переменные [SerializedField] - было прин€то волевое решение
    //помен€ть логику этого класса и сделать дл€ колоды другой синглтон
    //public static DeckManager Instance { get; private set; }

    [SerializeField] private Transform hand;
    [SerializeField] public int maxCardsInHand = 5;
    [SerializeField] private CardManager cardManager;

    private List<Card> deck;

    /*private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }*/

    private void Update()
    {
        if (deck == null)
        {
            deck = new List<Card>(DeckInfoSingel.Instance.cards);
        }

        if (hand != null && hand.transform.childCount < maxCardsInHand-1 && deck.Count > 0)
        {
            cardManager.DrawCard();
        }
    }

    public void setDeck(List<Card> helpMe)
    {
        deck = helpMe;
    }
    public List<Card> getDeck()
    {
        return deck;
    }
    public void AddCardToDeck(Card card)
    {
        deck.Add(card);
    }
    public void RemoveCardFromDeck(Card card)
    {
        deck.Remove(card);
    }
    
    //ћы хотим брать случайную карту из колоды, а не по пор€дку (потому что она отсортированна€ по типам, лол)
    /*public Card DrawCard()
    {
        if (deck.Count == 0)
        {
            return null;
        }
        Card temp = deck[0];
        deck.Remove(temp);
        return temp;
        //kopiert von CardManager
        
    }*/
    public Card DrawCard()
    {
        if(deck.Count == 0)
        {
            return null;
        }
        int randomIndex = Random.Range(0, deck.Count);
        Card temp = deck[randomIndex];
        deck.Remove(temp);
        return temp;
    }

    public void DeckAusprinten()
    {
        string temp = "Das ist den aktuellen Deck: \n";
        for (int i = 0; i < deck.Count; i++)
        {
            temp += deck[i].name + ", ";
        }
        Debug.Log(temp);
    }
}
