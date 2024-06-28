using UnityEngine;
using UnityEngine.UI;

public class Volumen : MonoBehaviour
{
    [SerializeField] private Slider slider;

    void Start()
    {
        if (PlayerPrefs.HasKey("musicvolume"))
        {
            Load();
        }
        else
        {
            // Aquí podrías establecer un valor por defecto si lo deseas
            // slider.value = 1.0f; // Por ejemplo, establece un valor por defecto
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = slider.value;
        Save();
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
