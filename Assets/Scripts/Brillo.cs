using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brillo : MonoBehaviour
{
   public Slider slider;
   public float sliderValue;
   public Image panelBrillo;

    void Start()
    {
        sliderValue= PlayerPrefs.GetFloat("brillo",0.5f);
        ChangeSlider(sliderValue);
        panelBrillo.color= new Color(panelBrillo.color.r,panelBrillo.color.g,panelBrillo.color.b,sliderValue);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeSlider(float valor){
        slider.value=valor;
        //PlayerPrefs.SetFloat("brillo",sliderValue);
        panelBrillo.color=new Color(panelBrillo.color.r,panelBrillo.color.g,panelBrillo.color.b,slider.value);   
    }
    public void Save(){
        PlayerPrefs.SetFloat("brillo",slider.value);
    }
   
}
