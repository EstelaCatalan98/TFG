using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CargarBrillo : MonoBehaviour
{
    [SerializeField] Image panelBrillo;
    float sliderValue;
    void Start()
    {

        sliderValue = PlayerPrefs.GetFloat("brillo",0.5f);
        panelBrillo.color= new Color(panelBrillo.color.r,panelBrillo.color.g,panelBrillo.color.b,sliderValue);
       


    }


    void Update()
    {

    }
}
