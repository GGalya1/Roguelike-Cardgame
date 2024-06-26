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
    [SerializeField] private GameObject allMeat;

    //��� ���������� ����� ��������� �� ����/ ������, ������� �� ��������� � �����
    private List<GameObject> allPages;
    private int actualPageIndex = 0;

    //����� ���������� ��� ������� ������ ������ - ����� ��������� ��������� (������� ��� ��� ��� ����������)
    [SerializeField] private GameObject nextPageButton;
    [SerializeField] private GameObject prevPageButton;

    [SerializeField] private AudioClip openBook;
    [SerializeField] private AudioClip closeBook;
    [SerializeField] private AudioClip turnPage;

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
        SoundManager.Instance.PlaySound(turnPage);
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
        SoundManager.Instance.PlaySound(turnPage);
        DisableAllPagesAndArrows();
        allObst.SetActive(true);
    }
    public void OpenPilz()
    {
        SoundManager.Instance.PlaySound(turnPage);
        DisableAllPagesAndArrows();
        allPilz.SetActive(true);
    }
    public void OpenMilk()
    {
        SoundManager.Instance.PlaySound(turnPage);
        DisableAllPagesAndArrows();
        allMilk.SetActive(true);
    }
    public void OpenGetreide()
    {
        SoundManager.Instance.PlaySound(turnPage);
        DisableAllPagesAndArrows();
        allGetreide.SetActive(true);
    }
    public void OpenMeat()
    {
        SoundManager.Instance.PlaySound(turnPage);
        DisableAllPagesAndArrows();
        allMeat.SetActive(true);
    }

    public void NextPage()
    {
        if (actualPageIndex >= allPages.Count-1)
        {
            return;
        }
        allPages[actualPageIndex].SetActive(false);
        SoundManager.Instance.PlaySound(turnPage);
        //���� ����������� �������� ����� - �� ������� ��� ����� ������ � ������ ������� => �������� �����
        prevPageButton.SetActive(true);

        actualPageIndex++;
        allPages[actualPageIndex].SetActive(true);

        //���� ��������� �� ����� - ��������� ����� ������� �������
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
        SoundManager.Instance.PlaySound(turnPage);
        nextPageButton.SetActive(true);

        actualPageIndex--;
        allPages[actualPageIndex].SetActive(true);

        //���� ��������� �� ����� - ��������� ����� ������� ������
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
        allMeat.SetActive(false);
    }

    //um das Buch zu oeffnen
    public void OpenBook()
    {
        SoundManager.Instance.PlaySound(openBook);
        book.SetActive(true);
    }
    public void CloseBook()
    {
        SoundManager.Instance.PlaySound(closeBook);
        book.SetActive(false);
    }
}
