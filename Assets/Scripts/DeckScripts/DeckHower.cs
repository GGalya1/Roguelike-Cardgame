using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DeckHower : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject infoPanel; // Панель для отображения информации

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("DeckInfo is on");
        infoPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("DeckInfo is off");
        infoPanel.SetActive(false);
    }

    private void Update()
    {
        if (infoPanel.activeSelf)
        {
            Vector2 mousePosition = Input.mousePosition;
            infoPanel.transform.position = mousePosition;
        }
    }
}
