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
            AudioListener.volume = savedVolume; // Asegúrate de que el volumen del AudioListener se actualice también
        }
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("musicvolume", slider.value);
        PlayerPrefs.Save(); // Asegúrate de guardar los PlayerPrefs inmediatamente
    }
}
