using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManagmentMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text[] scores;
    [SerializeField] private TMP_Text[] texts;

    int kaltmiete = 1;
    int semesterbeitrag = 1;
    int lebensmittel = 1;
    int warmmiete = 1;

    // Start is called before the first frame update
    void Start()
    {
        scores[0].text = ScoreInfo.Instance.score.ToString();
        scores[1].text = kaltmiete.ToString();
        scores[2].text = semesterbeitrag.ToString();
        scores[3].text = lebensmittel.ToString();
        scores[4].text = warmmiete.ToString();

        scores[5].text = ScoreInfo.Instance.sadStudents.ToString();
        scores[6].text = (ScoreInfo.Instance.sadStudents / 3).ToString();
        scores[7].text = ScoreInfo.Instance.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
