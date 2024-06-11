using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDecorationManager : MonoBehaviour
{
    [SerializeField] public TMP_Text cardNameText;
    [SerializeField] public TMP_Text descriptionText;
    public GameObject[] typeIcons; // »конки дл€ типа карты (по индексу)
    public GameObject[] dishImages; // »зображени€ дл€ блюд (по индексу)
    // »конки составных частей (по индексу + дл€ каждого окошка своЄ)
    public GameObject[] firstElement;
    public GameObject[] secondElement;
    public GameObject[] thirdElement;
    public GameObject[] fourthElement;

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
}
