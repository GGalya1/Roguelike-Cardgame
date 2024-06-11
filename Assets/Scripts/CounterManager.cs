using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CounterManager : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject dishPrefab; //������ ������� � ����, ������� ����� ������� �� �����
    [SerializeField] private Transform counterZone; //��� �� ����� �������������
    [SerializeField] public int maxDishesAtCounter;

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerDrag.name + " was dropped to " + gameObject.name + " and will be converted in dish on counter");
        //�������� ������ �� ������, ������� ��� ���������
        GameObject cardObj = eventData.pointerDrag;

        //������� ������� � ������� ����� ������ � platzhalter
        if (cardObj != null)
        {
            CardDecorationManager cardDecorationOfCard = cardObj.GetComponent<CardDecorationManager>();
            Card card = CardDatabase.Instance.GetCardByName(cardDecorationOfCard.cardNameText.text);
            Draggable dragCard = eventData.pointerDrag.GetComponent<Draggable>();

            //���� ����� ���������� ���, ������ �� ���������� �����, � �� �������
            DraggableDish dragDish = eventData.pointerDrag.GetComponent<DraggableDish>();

            if (card != null)
            {
                if (counterZone.transform.childCount < maxDishesAtCounter && dragDish == null)
                {
                    GameObject temp = Instantiate(dishPrefab, counterZone);
                    temp.GetComponent<Dish>().DecorateDish(card.name);

                    //CardDecorationManagerCopy
                    CardDecorationManager ofDish = temp.AddComponent<CardDecorationManager>();
                    ofDish.cardNameText = cardDecorationOfCard.cardNameText;

                    DraggableDish drag = temp.GetComponent<DraggableDish>();
                    drag.parentReturnTo = counterZone;

                    Debug.Log(eventData.pointerDrag.name + " was dropped to " + gameObject.name + " and will be converted in dish on counter");
                    dragCard.CleanUp();
                }
                else
                {
                    Debug.Log("Its no more place here! You need to move something to trash can");
                }
                

            }
            else
            {
                Debug.Log("Card is null and can't be moved to counter !");
            }
        }
        else
        {
            Debug.Log("Draggable Objekt was null and can't be moved to counter!");
        }
    }
}
