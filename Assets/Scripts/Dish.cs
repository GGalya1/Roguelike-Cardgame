using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    [SerializeField] private GameObject[] dishImages;

    public void DecorateDish(string cardName)
    {
        int rightDish = CardDatabase.Instance.GetIndexOfDish(cardName);

        for (int i = 0; i < dishImages.Length; i++)
        {
            dishImages[i].gameObject.SetActive(i == rightDish);
        }
    }
}
