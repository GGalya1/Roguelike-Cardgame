using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform mensaCounter; //где они будут поялвяться/ стоять
    [SerializeField] public int maxStudentsAtCounter = 5;

    public void CreateEnemy()
    {
        if (mensaCounter.transform.childCount < maxStudentsAtCounter)
        {
            Instantiate(enemyPrefab, mensaCounter);
            Debug.Log("Enemy was created !");
        }
        else
        {
            Debug.Log("Students Hub is full! Cannot add a new student");
        }
    }
}
