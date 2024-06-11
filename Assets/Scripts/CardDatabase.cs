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
        //обычные блюда
        new Card("Картофель", "Овощ", "Картофель", "None", "None", "None", "None", "Стоимость - 3 Рубля\nКаллорийность - 3", "Vegetarisch"),
        new Card("Рис", "Крупа", "Рис", "None", "None", "None", "None", "Стоимость - 1 Рубль\nКаллорийность - 1", "Vegetarisch"),
        new Card("Паста", "Крупа", "Паста", "None", "None", "None", "None", "Норм тема. Особенно если не варить", "Vegetarisch"),
        new Card("Помидор", "Овощ", "Помидор", "None", "None", "None", "None", "красный - мой любимый цвет", "Vegetarisch"),
        new Card("Шампиньон", "Гриб", "Шампиньон", "None", "None", "None", "None", "мы играли в красноармейцев и шампиньонов...", "Vegetarisch"),
        //джокер-карты
        new Card("Молоко", "Молочка", "Молоко", "None", "None", "None", "None", "без комментариев", "Joker"),
        new Card("Сыр", "Молочка", "Сыр", "None", "None", "None", "None", "много дырок - мало сыра (", "Joker"),
        new Card("Bad Apple!", "None", "Bad Apple!" , "None", "None", "None", "None", "перенял челендж", "Joker"),
        //необычные блюда
        new Card("Ризотто", "Блюдо", "Ризотто", "Крупа", "Овощ", "Овощ", "None", "Стоимость - 5 Рублей\nКаллорийность - 10", "Vegetarisch"),
        new Card("Tomatenpasta", "Блюдо", "Tomatenpasta", "Крупа", "Овощ", "None", "None", "Стоимость - 5 Рублей\nКаллорийность - 10", "Vegetarisch"),
        new Card("Cremesuppe", "Блюдо", "Cremesuppe", "Молочка", "Гриб", "Молочка", "Овощ", "Стоимость - 5 Рублей\nКаллорийность - 10", "Vegetarisch")
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
        typesTable.Add("Крупа", 1);
        typesTable.Add("Овощ", 2);
        typesTable.Add("Гриб", 3);
        typesTable.Add("Молочка", 4);
        typesTable.Add("Блюдо", 5);

        //Images von jeden Gericht speichern
        dishesTable.Add("Bad Apple!", 0);
        dishesTable.Add("Паста", 1);
        dishesTable.Add("Помидор", 2);
        dishesTable.Add("Рис", 3);
        dishesTable.Add("Шампиньон", 4);
        dishesTable.Add("Картофель", 5);
        dishesTable.Add("Молоко", 6);
        dishesTable.Add("Сыр", 7);
        // Картинки готовых блюд
        dishesTable.Add("Ризотто", 8);
        dishesTable.Add("Tomatenpasta", 9);
        dishesTable.Add("Cremesuppe", 10);
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
        return cards[Random.Range(0, cards.Count)];
    }

    public List<Card> GetAllCardsAsList()
    {
        return cards;
    }
}

