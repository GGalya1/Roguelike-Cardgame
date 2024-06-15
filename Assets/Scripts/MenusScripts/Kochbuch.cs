using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kochbuch : MonoBehaviour
{
    [SerializeField] private GameObject book;
    [SerializeField] private List<GameObject> allDishes;
    [SerializeField] private GameObject allObst;
    [SerializeField] private GameObject allPilz;
    [SerializeField] private GameObject allMilk;
    [SerializeField] private GameObject allGetreide;

    //эта переменная будет ссылаться на лист/ раздел, который мы открываем в книге
    private List<GameObject> allPages;
    private int actualPageIndex = 0;

    //чтобы обозначить что листать дальше нельзя - будем выключать стрелочки (поэтому они тут как переменные)
    [SerializeField] private GameObject nextPageButton;
    [SerializeField] private GameObject prevPageButton;

    // Start is called before the first frame update
    void Start()
    {
        allPages = allDishes;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDishes()
    {
        DisableAllPagesAndArrows();
        allPages = allDishes;

        
        if (actualPageIndex == 0)
        {
            nextPageButton.SetActive(true);
        }
        else if (actualPageIndex == allDishes.Count - 1)
        {
            prevPageButton.SetActive(true);
        }
        else
        {
            nextPageButton.SetActive(true);
            prevPageButton.SetActive(true);
        }
        allPages[actualPageIndex].SetActive(true);
        
    }
    public void OpenObst()
    {
        DisableAllPagesAndArrows();
        allObst.SetActive(true);
    }
    public void OpenPilz()
    {
        DisableAllPagesAndArrows();
        allPilz.SetActive(true);
    }
    public void OpenMilk()
    {
        DisableAllPagesAndArrows();
        allMilk.SetActive(true);
    }
    public void OpenGetreide()
    {
        DisableAllPagesAndArrows();
        allGetreide.SetActive(true);
    }

    public void NextPage()
    {
        if (actualPageIndex >= allPages.Count-1)
        {
            return;
        }
        allPages[actualPageIndex].SetActive(false);
        //если перевернули страницу вперёд - то логично что можем теперь и налево листать => включаем опцию
        prevPageButton.SetActive(true);

        actualPageIndex++;
        allPages[actualPageIndex].SetActive(true);

        //если долистали до конца - выключаем опцию листать направо
        if (actualPageIndex == allPages.Count - 1)
        {
            nextPageButton.SetActive(false);
        }
    }
    public void PrevPage()
    {
        if (actualPageIndex <= 0)
        {
            return;
        }
        allPages[actualPageIndex].SetActive(false);
        nextPageButton.SetActive(true);

        actualPageIndex--;
        allPages[actualPageIndex].SetActive(true);

        //если долистали до конца - выключаем опцию листать налево
        if (actualPageIndex == 0)
        {
            prevPageButton.SetActive(false);
        }
    }

    private void DisableAllPagesAndArrows()
    {
        for (int i = 0; i < allDishes.Count; i++)
        {
            allDishes[i].SetActive(false);
        }
        nextPageButton.SetActive(false);
        prevPageButton.SetActive(false);
        allObst.SetActive(false);
        allPilz.SetActive(false);
        allMilk.SetActive(false);
        allGetreide.SetActive(false);
    }

    //um das Buch zu oeffnen
    public void OpenBook()
    {
        book.SetActive(true);
    }
    public void CloseBook()
    {
        book.SetActive(false);
    }
}
