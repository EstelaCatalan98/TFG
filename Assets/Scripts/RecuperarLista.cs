using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RecuperarLista : MonoBehaviour
{
    public TextMeshProUGUI lista;
    [SerializeField] string productoSeleccionado;
    string finalList;
    void Start()
    {
        lista.text=StaticData.valueToKeep;     //PlayerPrefs.GetString("Productos");
    }

   public void seleccionarProducto(string productoSeleccionado){
     if(StaticData.valueToKeep.Contains(productoSeleccionado)){
            finalList=new String(StaticData.valueToKeep.Replace(productoSeleccionado,"<s>"+productoSeleccionado+"</s>"));
            lista.text=finalList;
            StaticData.valueToKeep=finalList;//PlayerPrefs.SetString("Productos",finalList);
            
          }
   }
}