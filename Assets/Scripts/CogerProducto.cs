using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
public class CogerProducto : MonoBehaviour
{
    string finalList;
    public TextMeshProUGUI lista;
    // Start is called before the first frame update
    void Start()
    {
         lista.text=StaticData.valueToKeep;
    }
   public void seleccionarProducto(string productoSeleccionado){

     if(StaticData.valueToKeep.Contains(productoSeleccionado)){
            finalList=new String(StaticData.valueToKeep.Replace(productoSeleccionado,"<s>"+productoSeleccionado+"</s>"));
            lista.text=finalList;
            StaticData.valueToKeep=finalList;
            StaticData.cont--;
           
            }
   }
}
