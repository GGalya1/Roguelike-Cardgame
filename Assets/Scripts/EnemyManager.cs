using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    //damit unseren Sound- und MusikManager funktioniert, lol
    [SerializeField] private AudioClip inGameMusik;
    [SerializeField] private Slider volumeMusikSlider;
    [SerializeField] private Slider volumeSoundSlider;
    [SerializeField] private TMP_Text musikVolume;
    [SerializeField] private TMP_Text soundVolume;

    void OnVolumeMusikSliderValueChanged(float value)
    {
        MusicManager.Instance.audioSource.volume = value;
        musikVolume.text = $"Musik Volume {Mathf.RoundToInt(value * 100)}%";
    }
    void OnVolumeSoundSliderValueChanged(float value)
    {
        SoundManager.Instance.audioSource.volume = value;
        soundVolume.text = $"Sound Volume {Mathf.RoundToInt(value * 100)}%";
    }

    public void Start()
    {
        count1 = intervalBetweenTwoDefaultStudents;
        count2 = nachDemLetztenStudent;

        //damit unseren MusikManager funktioniert, lol
        MusicManager.Instance.clip = inGameMusik;
        MusicManager.Instance.audioSource.volume = volumeMusikSlider.value;
        volumeMusikSlider.onValueChanged.AddListener(OnVolumeMusikSliderValueChanged);

        SoundManager.Instance.audioSource.volume = volumeSoundSlider.value;
        volumeSoundSlider.onValueChanged.AddListener(OnVolumeSoundSliderValueChanged);
        MusicManager.Instance.PlayMusik(inGameMusik);

        musikVolume.text = $"Musik Volume 50%";
        soundVolume.text = $"Sound Volume 100%";
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
