using UnityEngine;
using UnityEngine.UI;

public class Volumen : MonoBehaviour
{
    [SerializeField] private Slider slider;

    void Start()
    {
        Load();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = slider.value;
        Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("musicvolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("musicvolume");
            slider.value = savedVolume;
            AudioListener.volume = savedVolume; 
        }
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("musicvolume", slider.value);
        PlayerPrefs.Save(); 
    }
}
