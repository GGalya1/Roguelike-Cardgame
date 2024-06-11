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
        //������� �����
        new Card("���������", "����", "���������", "None", "None", "None", "None", "��������� - 3 �����\n������������� - 3", "Vegetarisch"),
        new Card("���", "�����", "���", "None", "None", "None", "None", "��������� - 1 �����\n������������� - 1", "Vegetarisch"),
        new Card("�����", "�����", "�����", "None", "None", "None", "None", "���� ����. �������� ���� �� ������", "Vegetarisch"),
        new Card("�������", "����", "�������", "None", "None", "None", "None", "������� - ��� ������� ����", "Vegetarisch"),
        new Card("���������", "����", "���������", "None", "None", "None", "None", "�� ������ � �������������� � �����������...", "Vegetarisch"),
        //������-�����
        new Card("������", "�������", "������", "None", "None", "None", "None", "��� ������������", "Joker"),
        new Card("���", "�������", "���", "None", "None", "None", "None", "����� ����� - ���� ���� (", "Joker"),
        new Card("Bad Apple!", "None", "Bad Apple!" , "None", "None", "None", "None", "������� �������", "Joker"),
        //��������� �����
        new Card("�������", "�����", "�������", "�����", "����", "����", "None", "��������� - 5 ������\n������������� - 10", "Vegetarisch"),
        new Card("Tomatenpasta", "�����", "Tomatenpasta", "�����", "����", "None", "None", "��������� - 5 ������\n������������� - 10", "Vegetarisch"),
        new Card("Cremesuppe", "�����", "Cremesuppe", "�������", "����", "�������", "����", "��������� - 5 ������\n������������� - 10", "Vegetarisch")
    };

    
   //erstellen ein einziges Database fuer das ganze Spiel
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ��������� ������ ��� ����� ����
        }
        else
        {
            Destroy(gameObject);
        }

        //Typen speichern
        typesTable.Add("None", 0);
        typesTable.Add("�����", 1);
        typesTable.Add("����", 2);
        typesTable.Add("����", 3);
        typesTable.Add("�������", 4);
        typesTable.Add("�����", 5);

        //Images von jeden Gericht speichern
        dishesTable.Add("Bad Apple!", 0);
        dishesTable.Add("�����", 1);
        dishesTable.Add("�������", 2);
        dishesTable.Add("���", 3);
        dishesTable.Add("���������", 4);
        dishesTable.Add("���������", 5);
        dishesTable.Add("������", 6);
        dishesTable.Add("���", 7);
        // �������� ������� ����
        dishesTable.Add("�������", 8);
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
            return 0; // ���������� 0, ���� ����� �� ������� (�� ���� ������ ������)
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
            return 0; // ���������� 0, ���� ����� �� ������� (�� ���� ������ ������)
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

