using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;


public class AciertosFallos : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI aciertos;
    [SerializeField] TextMeshProUGUI fallos;
    // Start is called before the first frame update
    void Start()
    {
        int acierto=PlayerPrefs.GetInt("Aciertos");
        int fallo=PlayerPrefs.GetInt("Fallos");
        aciertos.text=acierto.ToString();
        fallos.text=fallo.ToString();
        
        
    }

   
}
