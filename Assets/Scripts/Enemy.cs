using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IDropHandler
{
    public float waitTime = 2f; //как долго чел будет ждать (в секундах)
    private float timeLeft;
    public Slider timeSlider;
    public Image fillImage; //ссылка на Image внутри Fill Area слайдера (чтобы менять цвет по ходу времени)
    public Image knobImage; //ссылка на ползунок/пимпочку (чтобы тоже менять её цвет)
    public TMP_Text orderAsText;
    [SerializeField] private List<Image> orderAsImage;
    [SerializeField] private List<Image> studetsImages;
    public Card orderedDish;



    private void Start()
    {
        timeLeft = waitTime;
        timeSlider.maxValue = waitTime;
        timeSlider.value = waitTime;
        fillImage.color = Color.green; //начальный цвет - зеленый
        knobImage.color = Color.green;
        orderedDish = CardDatabase.Instance.GetRandomCard();
        orderAsText.text = orderedDish.name;

        if (Random.Range(0, 2) == 0) {
            
            orderAsText.gameObject.SetActive(true);
        }
        else
        {
            orderAsImage[CardDatabase.Instance.GetIndexOfDish(orderedDish.imageName)].gameObject.SetActive(true);
            orderAsText.gameObject.SetActive(false);
        }

        int tempImageOfStudent = Random.Range(0, studetsImages.Count);
        for (int i = 0; i < studetsImages.Count; i++)
        {
            studetsImages[i].gameObject.SetActive(i == tempImageOfStudent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timeSlider.value = timeLeft;

        // Изменение цвета в зависимости от оставшегося времени
        if (timeLeft <= waitTime / 3)
        {
            fillImage.color = Color.red; // Красный цвет при остатке меньше трети времени
            knobImage.color = Color.red;
        }
        else if (timeLeft <= 2 * waitTime / 3)
        {
            fillImage.color = Color.yellow; // Желтый цвет при остатке меньше двух третей времени
            knobImage.color = Color.yellow;
        }

        if (timeLeft <= 0)
        {
            //заказ не выполнен, противник уходит
            ScoreInfo.Instance.score -= 5;
            ScoreInfo.Instance.sadStudents++;
            //Debug.Log("Enemy left, order not fulfilled! Your current score: " + playerScore);
            Destroy(gameObject); //удаляем объект противника
        }
    }

    //перетаскиваем карту на врага (студента)
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped to " + gameObject.name);

        GameObject draggableCard = eventData.pointerDrag;
        Draggable temp = eventData.pointerDrag.GetComponent<Draggable>();
        //так как у тарелок свой класс для перетаскивания, то GetComponent<Draggable> ничего не найдет
        //в этом случае надо самому вручную дать правильный компонент и удалить
        DraggableDish heh = draggableCard.GetComponent<DraggableDish>();

        if (draggableCard != null && (temp != null || heh != null))
        {
            Card card = CardDatabase.Instance.GetCardByName(draggableCard.GetComponent<CardDecorationManager>().cardNameText.text);
            //um "platzahlter auch loeschen"
            
            if (card != null)
            {
                if (card.name.Equals(orderedDish.name))
                {
                    //правильная карта
                    ScoreInfo.Instance.score += 10; // Увеличиваем очки игрока
                    ScoreInfo.Instance.happyStudents++;
                    //Debug.Log("Order fulfilled! Score: " + playerScore);
                }
                else
                {
                    //неправильная карта
                    ScoreInfo.Instance.score -= 5; // Уменьшаем очки игрока
                    ScoreInfo.Instance.sadStudents++;
                    //Debug.Log("Wrong order! Score: " + playerScore);
                }

                if (temp == null)
                {
                    heh.CleanUp();
                }
                else temp.CleanUp();
                //Destroy(draggableCard); // Удаляем карту
                Destroy(gameObject); // Удаляем врага
            }
        }
    }
}
