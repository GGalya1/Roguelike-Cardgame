using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class CardDatabase: MonoBehaviour
{
    //singelton, damit diese Klasse fur alle gleich bleibt und von aussen erreichbar ist
    public static CardDatabase Instance { get; private set; }

    //hier werden alle Typen gespeichert
    private Dictionary<string, int> typesTable = new Dictionary<string, int>();
    private Dictionary<string, int> dishesTable = new Dictionary<string, int>();

    //чтобы можно было найти карты по типу
    public Dictionary<string, List<Card>> cardByType = new Dictionary<string, List<Card>>();

    //hier werden alle mogliche Karten gespeichert

    /*
     * 1) Name, 
     * 2) Type, 
     * 3) IndexOfImage, 
     * 4) ErstenBestandteil, 
     * 5) ZweitenBestandteil, 
     * 6) DrittenBestandteil, 
     * 7) ViertenBestandteil, 
     * 8) Beschreibung, 
     * 9) Diat
     */
    private List<Card> cards = new List<Card>() {
        //карта джокер + если возникнет ошибка, то вызовиться именно она
        new Card("Bad Apple!", "None", "Bad Apple!" , "None", "None", "None", "None", "перенял челендж", "Joker"),
        //обычные блюда
        new Card("Kartoffel", "Obst", "Kartoffel", "None", "None", "None", "None", "Стоимость - 3 Рубля\nКаллорийность - 3", "Vegetarisch"),
        new Card("Reis", "Getreide", "Reis", "None", "None", "None", "None", "Стоимость - 1 Рубль\nКаллорийность - 1", "Vegetarisch"),
        new Card("Pasta", "Getreide", "Pasta", "None", "None", "None", "None", "Норм тема. Особенно если не варить", "Vegetarisch"),
        new Card("Tomate", "Obst", "Tomate", "None", "None", "None", "None", "красный - мой любимый цвет", "Vegetarisch"),
        new Card("Champignons", "Pilz", "Champignons", "None", "None", "None", "None", "мы играли в красноармейцев и шампиньонов...", "Vegetarisch"),
        new Card("Tofu", "Fleisch", "Tofu", "None", "None", "None", "None", "aue", "Vegetarisch"),
        new Card("Rindfleisch", "Fleisch", "Rindfleisch", "None", "None", "None", "None", "я сейчас просто с ума сойду, всем спасибо", "nicht Vegetarisch"),
        //джокер-карты
        new Card("Milch", "Milchprodukt", "Milch", "None", "None", "None", "None", "без комментариев", "Joker"),
        new Card("Cheese", "Milchprodukt", "Cheese", "None", "None", "None", "None", "много дырок - мало сыра (", "Joker"),
        
        //необычные блюда
        new Card("Rizotto", "Gericht", "Rizotto", "Getreide", "Obst", "Obst", "None", "Стоимость - 5 Рублей\nКаллорийность - 10", "Vegetarisch"),
        new Card("Tomatenpasta", "Gericht", "Tomatenpasta", "Getreide", "Obst", "None", "None", "Стоимость - 5 Рублей\nКаллорийность - 10", "Vegetarisch"),
        new Card("Cremesuppe", "Gericht", "Cremesuppe", "Milchprodukt", "Pilz", "Milchprodukt", "Obst", "Стоимость - 5 Рублей\nКаллорийность - 10", "Vegetarisch"),
        new Card("Gulasch", "Gericht", "Gulasch", "Getreide", "Obst", "Fleisch", "Milchprodukt", "мне хочется плакать и кричать", "nicht Vegetarisch"),
        new Card("Gulasch vegan", "Gericht", "Gulasch vegan", "Getreide", "Obst", "Fleisch", "Milchprodukt", "Стоимость - 5 Рублей\nКаллорийность - 10", "Vegetarisch")
    };

    
   //erstellen ein einziges Database fuer das ganze Spiel
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем объект при смене сцен
        }
        else
        {
            Destroy(gameObject);
        }

        //Typen speichern
        typesTable.Add("None", 0);
        typesTable.Add("Getreide", 1);
        typesTable.Add("Obst", 2);
        typesTable.Add("Pilz", 3);
        typesTable.Add("Milchprodukt", 4);
        typesTable.Add("Gericht", 5);
        typesTable.Add("Fleisch", 6);

        //Images von jeden Gericht speichern
        dishesTable.Add("Bad Apple!", 0);
        dishesTable.Add("Pasta", 1);
        dishesTable.Add("Tomate", 2);
        dishesTable.Add("Reis", 3);
        dishesTable.Add("Champignons", 4);
        dishesTable.Add("Kartoffel", 5);
        dishesTable.Add("Milch", 6);
        dishesTable.Add("Cheese", 7);
        // Картинки готовых блюд
        dishesTable.Add("Rizotto", 8);
        dishesTable.Add("Tomatenpasta", 9);
        dishesTable.Add("Cremesuppe", 10);


        //Blyat, новые карты добавлять нужно 
        dishesTable.Add("Gulasch", 11);
        dishesTable.Add("Tofu", 12);
        dishesTable.Add("Rindfleisch", 13);
        dishesTable.Add("Gulasch vegan", 14);

        for (int i = 0; i < cards.Count; i++)
        {
            // Pruefen, ob der Schluessel bereits im Dictionary existiert
            if (!cardByType.ContainsKey(cards[i].type))
            {
                // Falls nicht, neuen Eintrag mit einer neuen Liste erstellen
                cardByType[cards[i].type] = new List<Card>();
            }
            // Die Karte zur Liste des entsprechenden Typs hinzufuegen
            cardByType[cards[i].type].Add(cards[i]);
        }

    }

    public Card GetCardByName(string cardName)
    {
        return cards.FirstOrDefault(card => card.name.Equals(cardName));
    }

    public int GetIndexOfType(string type)
    {
        //liefern Index von Typ, falls gefunden
        if (typesTable.TryGetValue(type, out int value))
        {
            //Debug.Log("gewaehlten Typ: " + value.ToString());
            return value;
        }
        else
        {
            Debug.LogWarning("Card not found: " + type + " !!!");
            return 0; // Возвращаем 0, если карта не найдена (то есть тухлое яблоко)
        }
    }

    public int GetIndexOfDish(string type)
    {
        //liefern Index von Image, falls gefunden
        if (dishesTable.TryGetValue(type, out int value))
        {
            //Debug.Log("gewaehlten Image: " + value);
            return value;
        }
        else
        {
            Debug.LogWarning("Image for card not found: " + type + " !!!");
            return 0; // Возвращаем 0, если карта не найдена (то есть тухлое яблоко)
        }
    }

    public Card GetRandomCard()
    {
        if (cards == null || cards.Count == 0)
        {
            return null;
        }
        return cards[Random.Range(1, cards.Count)];
    }

    public List<Card> GetAllCardsAsList()
    {
        return cards;
    }
}

