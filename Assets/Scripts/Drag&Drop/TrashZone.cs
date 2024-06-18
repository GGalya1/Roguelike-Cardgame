using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashZone : DropZone
{
    [SerializeField] private DeckManager deckManager;
    [SerializeField] private CardManager cardManager;

    [SerializeField] private AudioClip audioClip;

    public override void OnDrop(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerDrag.name + " was dropped to " + gameObject.name + " and will be destroyed");
        base.OnDrop(eventData);
        // �������� ������ �� ������, ������� ��� ���������
        GameObject cardObj = eventData.pointerDrag;
        
        // ������� �������� �� ����
        if (cardObj != null)
        {
            Card card = CardDatabase.Instance.GetCardByName(cardObj.GetComponent<CardDecorationManager>().cardNameText.text);
            Draggable dragCard = eventData.pointerDrag.GetComponent<Draggable>();
            
            if(card != null)
            {
                SoundManager.Instance.PlaySound(audioClip);

                //� ���� �������� ��������������� ������ �� "�����" ������, �.�. ��� ������, � �� �� �������
                //DeckManager.Instance.RemoveCardFromDeck(card);
                if (Random.value > 0.5f)
                {
                    Debug.Log("Card " + card.name + " was added to deck!");
                    deckManager.AddCardToDeck(card);
                }
                if (dragCard == null)
                {
                    DraggableDish dragDish = eventData.pointerDrag.GetComponent<DraggableDish>();
                    dragDish.CleanUp();
                }
                else dragCard.CleanUp();

                if (cardManager.hand.transform.childCount < 5)
                {
                    cardManager.DrawCard();
                }
                
                //Destroy(cardObj);

            }
            else
            {
                Debug.Log("Card is null and can't be moved in trash!");
            }
        }
        else
        {
            Debug.Log("Draggable Objekt was null and can't be moved in trash!");
        }
    }
}
