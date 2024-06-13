using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IDropHandler
{
    public float waitTime = 2f; //��� ����� ��� ����� ����� (� ��������)
    private float timeLeft;
    public Slider timeSlider;
    public Image fillImage; //������ �� Image ������ Fill Area �������� (����� ������ ���� �� ���� �������)
    public Image knobImage; //������ �� ��������/�������� (����� ���� ������ � ����)
    public TMP_Text orderAsText;
    [SerializeField] private List<Image> orderAsImage;
    [SerializeField] private List<Image> studetsImages;
    public Card orderedDish;



    private void Start()
    {
        timeLeft = waitTime;
        timeSlider.maxValue = waitTime;
        timeSlider.value = waitTime;
        fillImage.color = Color.green; //��������� ���� - �������
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

        // ��������� ����� � ����������� �� ����������� �������
        if (timeLeft <= waitTime / 3)
        {
            fillImage.color = Color.red; // ������� ���� ��� ������� ������ ����� �������
            knobImage.color = Color.red;
        }
        else if (timeLeft <= 2 * waitTime / 3)
        {
            fillImage.color = Color.yellow; // ������ ���� ��� ������� ������ ���� ������ �������
            knobImage.color = Color.yellow;
        }

        if (timeLeft <= 0)
        {
            //����� �� ��������, ��������� ������
            ScoreInfo.Instance.score -= 5;
            ScoreInfo.Instance.sadStudents++;
            //Debug.Log("Enemy left, order not fulfilled! Your current score: " + playerScore);
            Destroy(gameObject); //������� ������ ����������
        }
    }

    //������������� ����� �� ����� (��������)
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped to " + gameObject.name);

        GameObject draggableCard = eventData.pointerDrag;
        Draggable temp = eventData.pointerDrag.GetComponent<Draggable>();
        //��� ��� � ������� ���� ����� ��� ��������������, �� GetComponent<Draggable> ������ �� ������
        //� ���� ������ ���� ������ ������� ���� ���������� ��������� � �������
        DraggableDish heh = draggableCard.GetComponent<DraggableDish>();

        if (draggableCard != null && (temp != null || heh != null))
        {
            Card card = CardDatabase.Instance.GetCardByName(draggableCard.GetComponent<CardDecorationManager>().cardNameText.text);
            //um "platzahlter auch loeschen"
            
            if (card != null)
            {
                if (card.name.Equals(orderedDish.name))
                {
                    //���������� �����
                    ScoreInfo.Instance.score += 10; // ����������� ���� ������
                    ScoreInfo.Instance.happyStudents++;
                    //Debug.Log("Order fulfilled! Score: " + playerScore);
                }
                else
                {
                    //������������ �����
                    ScoreInfo.Instance.score -= 5; // ��������� ���� ������
                    ScoreInfo.Instance.sadStudents++;
                    //Debug.Log("Wrong order! Score: " + playerScore);
                }

                if (temp == null)
                {
                    heh.CleanUp();
                }
                else temp.CleanUp();
                //Destroy(draggableCard); // ������� �����
                Destroy(gameObject); // ������� �����
            }
        }
    }
}
