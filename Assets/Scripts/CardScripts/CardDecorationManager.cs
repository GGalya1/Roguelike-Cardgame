using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDecorationManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public TMP_Text cardNameText;
    [SerializeField] public TMP_Text descriptionText;
    public GameObject[] typeIcons; // Иконки для типа карты (по индексу)
    public GameObject[] dishImages; // Изображения для блюд (по индексу)
    // Иконки составных частей (по индексу + для каждого окошка своё)
    public GameObject[] firstElement;
    public GameObject[] secondElement;
    public GameObject[] thirdElement;
    public GameObject[] fourthElement;

    //код для трансформирования карты по даблклику
    [SerializeField] private Image cardFace;
    private bool isGreen;
    private float lastClickTime;
    private const float doubleClickThreshold = 0.2f; // Zeitfenster für Doppelklick in Sekunden

    public void DecorateCard(string cardName)
    {
        Card current = CardDatabase.Instance.GetCardByName(cardName);
        if (current == null)
        {
            //Debug.Log("Card with name " + cardName + " is not found !");
            return;
        }

        cardNameText.text = current.name;
        descriptionText.text = current.description;

        // setzen richtigen Typ der Karte
        int cardType = CardDatabase.Instance.GetIndexOfType(current.type);
        for (int i = 0; i < typeIcons.Length; i++)
        {
            //Debug.Log("Typ of card has index: " + cardType + " and this is true with index " + i + ": " + i.Equals(cardType));
            typeIcons[i].gameObject.SetActive(i.Equals(cardType));
        }

        //setzen richtiges Image fuer Karte
        int cardImage = CardDatabase.Instance.GetIndexOfDish(cardName);
        for (int i = 0; i < dishImages.Length; i++)
        {
            //Debug.Log("Image has index: " + cardImage + " and this is true with index " + i + ": " + i.Equals(cardImage));
            dishImages[i].gameObject.SetActive(i.Equals(cardImage));
        }

        //Komponenten setzen
        List<string> components = current.GetCookingComponents();
        int first = CardDatabase.Instance.GetIndexOfType(components[0]);
        int second = CardDatabase.Instance.GetIndexOfType(components[1]);
        int third = CardDatabase.Instance.GetIndexOfType(components[2]);
        int fourth = CardDatabase.Instance.GetIndexOfType(components[3]);

        for (int i = 0; i < firstElement.Length; i++)
        {
            //Debug.Log("First Element has index: " + first + " and this is true with index " + i + ": " + i.Equals(first));
            firstElement[i].SetActive(i.Equals(first));
        }
        for (int i = 0; i < secondElement.Length; i++)
        {
            //Debug.Log("Second Element has index: " + second + " and this is true with index " + i + ": " + i.Equals(second));
            secondElement[i].SetActive(i.Equals(second));
        }
        for (int i = 0; i < thirdElement.Length; i++)
        {
            //Debug.Log("Third Element has index: " + third + " and this is true with index " + i + ": " + i.Equals(third));
            thirdElement[i].SetActive(i.Equals(third));
        }
        for (int i = 0; i < fourthElement.Length; i++)
        {
            //Debug.Log("Fourth Element has index: " + fourth + " and this is true with index " + i + ": " + i.Equals(fourth));
            fourthElement[i].SetActive(i.Equals(fourth));
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            //Doppelclick erkannt
            isGreen = !isGreen;
            TransformCardByDoubleClick();
        }
        lastClickTime = Time.time;
    }
    public void TransformCardByDoubleClick()
    {
        //здесь мы превращаем одно в другое, если прозошёл даблклик (чтобы не искать 10 лет в колоде)
        //рис <==> макароны
        if (cardNameText.text.Equals("Reis"))
        {
            DecorateCard("Pasta");
        }
        else if (cardNameText.text.Equals("Pasta"))
        {
            DecorateCard("Reis");
        }
        //помидор <==> картошка
        else if (cardNameText.text.Equals("Tomate"))
        {
            DecorateCard("Kartoffel");
        }
        else if (cardNameText.text.Equals("Kartoffel"))
        {
            DecorateCard("Tomate");
        }
        //молоко <==> сыр
        else if (cardNameText.text.Equals("Milch"))
        {
            DecorateCard("Cheese");
        }
        else if (cardNameText.text.Equals("Cheese"))
        {
            DecorateCard("Milch");
        }
        //тофу <==> мясо
        else if (cardNameText.text.Equals("Tofu"))
        {
            DecorateCard("Rindfleisch");
        }
        else if (cardNameText.text.Equals("Rindfleisch"))
        {
            DecorateCard("Tofu");
        }
        /*if (isGreen)
        {
            cardFace.color = Color.green;
        }
        else
        {
            cardFace.color = Color.white;
        }*/
    }
}
