using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableDish : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentReturnTo = null;
    private Transform placeholderParent = null;

    GameObject placeholder = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Создаём placeholder
        Debug.Log("OnBeginDrag");
        placeholder = new GameObject();
        
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        //Debug.Log("1");
        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
        //Debug.Log("2");
        parentReturnTo = this.transform.parent;
        //Debug.Log("3");
        placeholderParent = parentReturnTo;
        //Debug.Log("PlaceholderParent ist zugewiesen: " + placeholderParent.name);
        this.transform.SetParent(this.transform.parent.parent);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        this.transform.position = eventData.position;

        if (placeholder.transform.parent != placeholderParent)
        {
            placeholder.transform.SetParent(placeholderParent);
        }

        int newSiblingIndex = placeholderParent.childCount;
        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            if (this.transform.position.x < placeholderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;

                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                {
                    newSiblingIndex--;
                }
                break;
            }
        }

        placeholder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        // Проверка на допустимые зоны перетаскивания
        if (eventData.pointerEnter != null && 
            (eventData.pointerEnter.CompareTag("StudentZone") || eventData.pointerEnter.CompareTag("TrashZone")))
        {
            // Устанавливаем нового родителя и удаляем placeholder
            this.transform.SetParent(eventData.pointerEnter.transform);
            this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        }
        else
        {
            // Если тарелку нельзя перетащить, возвращаем её на прежнее место
            this.transform.SetParent(parentReturnTo);
            this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        }
        Destroy(placeholder);
    }

    public void CleanUp()
    {
        if (placeholder != null)
        {
            Destroy(placeholder);
        }
        Destroy(gameObject);
    }
}
