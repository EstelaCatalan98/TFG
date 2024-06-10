using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volumen : MonoBehaviour
{
   public Slider slider;
   public float slidervalue;
   public Image imagenMute;
   void  Start(){
    slider.value= PlayerPrefs.GetFloat("volumenAudio", 0.5f);
    AudioListener.volume =slidervalue;
    RevisarSiEstoyMute();
   }
   public void RevisarSiEstoyMute(){
    if(slidervalue==0){
        imagenMute.enabled=true;
    }
    else{
        imagenMute.enabled=false;
    }
   }   
}
