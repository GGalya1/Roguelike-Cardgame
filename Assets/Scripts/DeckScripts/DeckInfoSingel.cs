using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckInfoSingel : MonoBehaviour
{
    public static DeckInfoSingel Instance { get; private set; }

    [SerializeField] public List<Card> cards = new List<Card>();
    public int deckSize = 32;
    public int obstAnzahl = 10;
    public int crupaAnzahl = 9;
    public int pilzAnzahl = 4;
    public int milkAnzahl = 9;

    public void Awake()
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
    }

    public void Start()
    {
        Dictionary<string, List<Card>> allTypes = CardDatabase.Instance.cardByType;
        //сначала рандомных овощей
        List<Card> temp = allTypes["Obst"];
        for (int i = 0; i < obstAnzahl; i++)
        {
            int randomIndex = Random.Range(0, temp.Count);
            cards.Add(temp[randomIndex]);
        }

        //рандомных грибов
        temp = allTypes["Pilz"];
        for (int i = 0; i < pilzAnzahl; i++)
        {
            int randomIndex = Random.Range(0, temp.Count);
            cards.Add(temp[randomIndex]);
        }

        //Getreide
        temp = allTypes["Getreide"];
        for (int i = 0; i < crupaAnzahl; i++)
        {
            int randomIndex = Random.Range(0, temp.Count);
            cards.Add(temp[randomIndex]);
        }

        temp = allTypes["Milchprodukt"];
        for (int i = 0; i < milkAnzahl; i++)
        {
            int randomIndex = Random.Range(0, temp.Count);
            cards.Add(temp[randomIndex]);
        }
    }

    public void addCardInDeck(Card card)
    {
        cards.Add(card);
        deckSize++;
    }
}
