using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject readMePanel;
    [SerializeField] private Button readMeButton;
    [SerializeField] private GameObject cardsPanel;
    [SerializeField] private Button cardsButton;
    [SerializeField] private GameObject dragDropPanel;
    [SerializeField] private Button dragDropButton;
    [SerializeField] private GameObject enemyPanel;
    [SerializeField] private Button enemyButton;
    [SerializeField] private GameObject trashCanPanel;
    [SerializeField] private Button trashCanButton;
    [SerializeField] private GameObject deckPanel;
    [SerializeField] private Button deckButton;
    [SerializeField] private GameObject kochbuchPanel;
    [SerializeField] private Button kochbuchButton;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private Button scoreButton;
    [SerializeField] private GameObject cookingPanel;
    [SerializeField] private Button cookingButton;

    public void DisableAllText()
    {
        readMePanel.SetActive(false);
        cardsPanel.SetActive(false);
        dragDropPanel.SetActive(false);
        enemyPanel.SetActive(false);
        trashCanPanel.SetActive(false);
        deckPanel.SetActive(false);
        kochbuchPanel.SetActive(false);
        scorePanel.SetActive(false);
        cookingPanel.SetActive(false);

        readMeButton.image.color = Color.white;
        cardsButton.image.color = Color.white;
        dragDropButton.image.color = Color.white;
        enemyButton.image.color = Color.white;
        trashCanButton.image.color = Color.white;
        deckButton.image.color = Color.white;
        kochbuchButton.image.color = Color.white;
        scoreButton.image.color = Color.white;
        cookingButton.image.color = Color.white;
    }
    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }


    //т.к. эта функция не отображается в юнити, то я просто захардкожу (жожу)
    /*public void OpenPanel(Image imag, GameObject panel)
    {
        DisableAllText();
        imag.color = Color.yellow;
        panel.SetActive(true);
    }*/
    public void OpenReadMe()
    {
        DisableAllText();
        readMeButton.image.color = Color.yellow;
        readMePanel.SetActive(true);
    }
    public void OpenCards()
    {
        DisableAllText();
        cardsButton.image.color = Color.yellow;
        cardsPanel.SetActive(true);
    }
    public void OpenDragNDrop()
    {
        DisableAllText();
        dragDropButton.image.color = Color.yellow;
        dragDropPanel.SetActive(true);
    }
    public void OpenEnemy()
    {
        DisableAllText();
        enemyButton.image.color = Color.yellow;
        enemyPanel.SetActive(true);
    }
    public void OpenTrash()
    {
        DisableAllText();
        trashCanButton.image.color = Color.yellow;
        trashCanPanel.SetActive(true);
    }
    public void OpenDeck()
    {
        DisableAllText();
        deckButton.image.color = Color.yellow;
        deckPanel.SetActive(true);
    }
    public void OpenKochbuch()
    {
        DisableAllText();
        kochbuchButton.image.color = Color.yellow;
        kochbuchPanel.SetActive(true);
    }
    public void OpenScore()
    {
        DisableAllText();
        scoreButton.image.color = Color.yellow;
        scorePanel.SetActive(true);
    }
    public void OpenCooking()
    {
        DisableAllText();
        cookingButton.image.color = Color.yellow;
        cookingPanel.SetActive(true);
    }
}
