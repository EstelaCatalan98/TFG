using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Volumen : MonoBehaviour
{
    [SerializeField] Slider slider;
    void Start()
    {
        if (PlayerPrefs.HasKey("musicvolume"))
        {
            PlayerPrefs.SetFloat("musicvolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }
    public void ChangeVolume()
    {
        AudioListener.volume = slider.value;
        //Save();

    }
    public void Load()
    {
        slider.value = PlayerPrefs.GetFloat("musicvolume");
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("musicvolume", slider.value);

    }
}
