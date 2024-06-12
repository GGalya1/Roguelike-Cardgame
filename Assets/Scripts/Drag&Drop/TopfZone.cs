using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TopfZone : DropZone
{
    [SerializeField] private TopfManager topfManager;
    [SerializeField] private Transform hand;
    [SerializeField] private Sprite placeholderSprite;

    public override void OnDrop(PointerEventData eventData)
    {
        if (topfManager.dishInProcess)
        {
            return;
        }

        if (topfManager.cardNames.Count >= 4 || topfManager.placeholders.Count >= 4)
        {
            Debug.Log("Too many cooking components in Topf!");
            return;
        }

        Debug.Log(eventData.pointerDrag.name + " was dropped to " + gameObject.name + " and is ready for coocking!");
        base.OnDrop(eventData);
        // Получаем ссылку на объект, который был перетащен
        GameObject cardObj = eventData.pointerDrag;

        // Удаляем карточку из игры
        if (cardObj != null)
        {
            Card card = CardDatabase.Instance.GetCardByName(cardObj.GetComponent<CardDecorationManager>().cardNameText.text);
            Draggable dragCard = eventData.pointerDrag.GetComponent<Draggable>();
            
            //если нет компонента Draggable, значит это не карта и тогда не перемещаем 
            if (dragCard == null) return;

            if (card != null)
            {
                dragCard.platzhalter.transform.SetParent(hand);
                topfManager.placeholders.Add(dragCard.platzhalter);
                topfManager.cardNames.Add(card.name);
                topfManager.cardTypes.Add(card.type);

                //Setzen von Image fuer Platzhalter
                Image platzhlterImage = dragCard.platzhalter.AddComponent<Image>();
                platzhlterImage.sprite = placeholderSprite;
                RectTransform rt = dragCard.platzhalter.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(229 , 325); //чтобы была такого же размера

                //else dragCard.CleanUp();
                Destroy(cardObj);

            }
            else
            {
                Debug.Log("Card is null and can't be moved in topf!");
            }
        }
        else
        {
            Debug.Log("Draggable Objekt was null and can't be moved in topf!");
        }
    }
}
