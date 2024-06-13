using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform mensaCounter; //где они будут поялвяться/ стоять
    [SerializeField] public int maxStudentsAtCounter = 5;
    
    [SerializeField] private float intervalBetweenTwoDefaultStudents = 20f; //20 f fuer 20 Sekunden
    private float count1;
    [SerializeField] private float fasterThenThatIsBad = 5f; //если между двумя выполнеными заказами было меньше этого числа
                                                             //то игрок слишком хорошо

    [SerializeField] private float nachDemLetztenStudent = 5f; //если стойка пустая минимум столько времени, то спавни нового студента
    private float count2;

    public void Start()
    {
        count1 = intervalBetweenTwoDefaultStudents;
        count2 = nachDemLetztenStudent;
    }

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

    public void Update()
    {
        count1 -= Time.deltaTime;
        if (mensaCounter.transform.childCount == 0)
        {
            count2 -= Time.deltaTime;
        }
        else
        {
            count2 = nachDemLetztenStudent;
        }
        
        if (count1 <= 0)
        {
            count1 = intervalBetweenTwoDefaultStudents;
            CreateEnemy();
        }
        if (count2 <= 0)
        {
            count2 = nachDemLetztenStudent;
            CreateEnemy();
        }
    }
}
