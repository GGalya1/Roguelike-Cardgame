using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentReturnTo = null;
    public Transform platzhalterParent = null;

    //wegen TopfManager
    public GameObject platzhalter = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        // erstellt eine "leere" Karte als Platzhalter
        platzhalter = new GameObject();
        platzhalter.transform.SetParent(this.transform.parent);

        LayoutElement le = platzhalter.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        platzhalter.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        parentReturnTo = this.transform.parent;
        platzhalterParent = parentReturnTo;
        this.transform.SetParent(this.transform.parent.parent);
        this.transform.SetSiblingIndex(platzhalter.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        this.transform.position = eventData.position;

        if (platzhalter.transform.parent != platzhalterParent)
        {
            platzhalter.transform.SetParent(platzhalterParent);
        }

        int newSiblingIndex = platzhalterParent.childCount;
        for (int i = 0; i < platzhalterParent.childCount; i++)
        {
            if (this.transform.position.x < platzhalterParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;

                if (platzhalter.transform.GetSiblingIndex() < newSiblingIndex)
                {
                    newSiblingIndex--;
                }
                break;
            }
        }

        platzhalter.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        this.transform.SetParent(parentReturnTo);
        this.transform.SetSiblingIndex(platzhalter.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(platzhalter);
    }

    //так как остаются заглушки (platzhalter) после перетаскивания в мусорку и к студентам
    //и они просто висят мёртвым грузом
    public void CleanUp()
    {
        if(platzhalter != null)
        {
            Destroy(platzhalter);
        }
        Destroy(gameObject);
    }
}

