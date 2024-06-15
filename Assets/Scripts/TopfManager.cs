using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TopfManager : MonoBehaviour
{
    //потому что когда мы перетаскиваем карты, но не нажимаем кнопку - мы хотим чтобы для 
    //брошенных в котёл карт оставалось место в руке, куда они могут вернуться
    public List<GameObject> placeholders;
    public List<string> cardNames;
    public List<string> cardTypes;
    [SerializeField] private Button cookingButton;
    [SerializeField] private CardManager cardManager;

    [SerializeField] private Slider cookingSlider;
    [SerializeField] private float cookingTime = 3f;
    private float timeLeft;

    public bool dishInProcess = false;
    private string dishToCook = "Bad Apple!";

    //массив всех блюд == всё что имеет составные части
    public List<Card> dishes = new List<Card>();
    public List<List<string>> bestandteileVonDishes = new List<List<string>>();

    void Start()
    {
        cookingTime = ScoreInfo.Instance.cookingTime;

        List<Card> allCards = CardDatabase.Instance.GetAllCardsAsList();
        for (int i = 0; i < allCards.Count; i++)
        {
            if (allCards[i].type.Equals("Gericht")) dishes.Add(allCards[i]);
        }
        for (int i = 0; i < dishes.Count; i++)
        {
           bestandteileVonDishes.Add(dishes[i].GetCookingComponents());
        }

        timeLeft = cookingTime;
        cookingSlider.maxValue = cookingTime;
        cookingSlider.value = cookingTime;

    }

    private void Update()
    {
        if (dishInProcess)
        {
            UpdateOfCooking();
        }
    }
    private void UpdateOfCooking()
    {
        timeLeft -= Time.deltaTime;
        cookingSlider.value = timeLeft;
        if (timeLeft <= 0)
        {
            //заменяем одну заглушку на готовое Gericht
            if (placeholders.Count > 0)
            {
                Debug.Log("placeholder count is: " + placeholders.Count);
                Destroy(placeholders[0]);
                Debug.Log("placeholder count after destroy() is: " + placeholders.Count);
                cardManager.CreateCard(dishToCook);
            }
            else
            {
                cardManager.CreateCard(dishToCook);
            }
            cleanHandAndTopf();
            cookingSlider.gameObject.SetActive(false);
            cookingButton.gameObject.SetActive(true);
            dishInProcess = false;
        }
    }

    //проблема в том, является ли множество всех перетащенных типов валидно для одного из блюд
    //какие есть для блюд варианты - заранее сохранено

    public void StartCooking()
    {
        //если кинули недостаточно компонентов - нужно пустые места заполнить None
        while (cardTypes.Count < 4)
        {
            cardTypes.Add("None");
        }

        bool flag = false;
        for (int i = 0; i < bestandteileVonDishes.Count; i++)
        {
            flag = flag || CompareCookingComponents(bestandteileVonDishes[i], cardTypes);
            if (flag)
            {
                dishToCook = dishes[i].name;
                break;
            }
        }
        //если мы так и не нашли блюда, которое можно приготовить с текущими компонентами
        if (!flag)
        {
            //возращаем все карты с котла назад в руку
            for (int i = 0; i < placeholders.Count; i++)
            {
                cardManager.CreateCard(cardNames[i]);
            }

            //чистим руку от пустых элементов и горшок от закинутых
            cleanHandAndTopf();
            dishInProcess = false;
            return;
        }
        
        //если смогли найти, то запускаем готовку
        cookingButton.gameObject.SetActive(false);
        dishInProcess = true;

        timeLeft = cookingTime;
        cookingSlider.maxValue = cookingTime;
        cookingSlider.value = cookingTime;
        cookingSlider.gameObject.SetActive(true);
    }

    //um zwei Listen auf Gleicheit zu vergleiche (Reihenfolge von Elementen ist unwichtig)
    public static bool CompareCookingComponents(List<string> aListA, List<string> aListB)
    {
        if (aListA == null || aListB == null || aListA.Count != aListB.Count)
            return false;
        if (aListA.Count == 0)
            return true;

        //справка: библиотека в питоне или хэшсет в джаве. Каждый тип - ключ, значение - кол-во повторений в листе
        Dictionary<string, int> lookUp = new Dictionary<string, int>();
        // create index for the first list
        for (int i = 0; i < aListA.Count; i++)
        {
            int count = 0;
            if (!lookUp.TryGetValue(aListA[i], out count))
            {
                lookUp.Add(aListA[i], 1);
                continue;
            }
            lookUp[aListA[i]] = count + 1;
        }
        for (int i = 0; i < aListB.Count; i++)
        {
            int count = 0;
            if (!lookUp.TryGetValue(aListB[i], out count))
            {
                // early exit as the current value in B doesn't exist in the lookUp (and not in ListA)
                return false;
            }
            count--;
            if (count <= 0)
                lookUp.Remove(aListB[i]);
            else
                lookUp[aListB[i]] = count;
        }
        // if there are remaining elements in the lookUp, that means ListA contains elements that do not exist in ListB
        return lookUp.Count == 0;
    }

    private void cleanHandAndTopf()
    {
        for (int i = 0; i < placeholders.Count; i++)
        {
            Destroy(placeholders[i]);
        }
        placeholders = new List<GameObject>();
        cardTypes = new List<string>();
        cardNames = new List<string>();
    }
}
