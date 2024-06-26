using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseNewCard : MonoBehaviour
{
    private string cardName;
    [SerializeField] private ManagmentMenu managmentMenu;

    public void SetCardName(string cardName)
    {
        this.cardName = cardName;
    }

    public void AddNewCard(GameObject panel)
    {
        DeckInfoSingel.Instance.addCardInDeck(CardDatabase.Instance.GetCardByName(cardName));
        managmentMenu.cardWasOnceAdded = true;
        panel.gameObject.SetActive(false);
    }
}
