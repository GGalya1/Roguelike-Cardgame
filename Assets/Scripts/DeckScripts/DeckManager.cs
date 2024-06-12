using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    //так как при смене сцены сбрасывются все переменные [SerializedField] - было принято волевое решение
    //поменять логику этого класса и сделать для колоды другой синглтон
    //public static DeckManager Instance { get; private set; }

    [SerializeField] private Transform hand;
    [SerializeField] public int maxCardsInHand = 5;
    [SerializeField] private CardManager cardManager;

    private List<Card> deck = new List<Card>();

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

    public void Start()
    {
        deck.Add(CardDatabase.Instance.GetCardByName("Картофель"));
        deck.Add(CardDatabase.Instance.GetCardByName("Cremesuppe"));
        deck.Add(CardDatabase.Instance.GetCardByName("Tomatenpasta"));
        deck.Add(CardDatabase.Instance.GetCardByName("Картофель"));
        deck.Add(CardDatabase.Instance.GetCardByName("Cremesuppe"));
        deck.Add(CardDatabase.Instance.GetCardByName("Tomatenpasta"));
        deck.Add(CardDatabase.Instance.GetCardByName("Картофель"));
        deck.Add(CardDatabase.Instance.GetCardByName("Cremesuppe"));
        deck.Add(CardDatabase.Instance.GetCardByName("Tomatenpasta"));
        deck.Add(CardDatabase.Instance.GetCardByName("Картофель"));
        deck.Add(CardDatabase.Instance.GetCardByName("Cremesuppe"));
        deck.Add(CardDatabase.Instance.GetCardByName("Tomatenpasta"));
    }

    private void Update()
    {
        if (hand != null && hand.transform.childCount < maxCardsInHand-1 && deck.Count > 0)
        {
            cardManager.DrawCard();
        }
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
    public Card DrawCard()
    {
        if (deck.Count == 0)
        {
            return null;
        }
        Card temp = deck[0];
        deck.Remove(temp);
        return temp;
        //kopiert von CardManager
        
    }
}
