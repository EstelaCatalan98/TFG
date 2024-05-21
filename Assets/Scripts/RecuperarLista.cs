using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class RecuperarLista : MonoBehaviour
{
    public TextMeshProUGUI lista;
     [SerializeField] Image win;
    //[SerializeField] string productoSeleccionado;
    string finalList;
    void Start()
    {
        lista.text=StaticData.listaDef;

    }

   public void seleccionarProducto(string productoSeleccionado){

     if(StaticData.listaDef.Contains(productoSeleccionado)){
            finalList=new String(StaticData.listaDef.Replace(productoSeleccionado,"<s>"+productoSeleccionado+"</s>"));
            lista.text=finalList;
            StaticData.listaDef=finalList;
            StaticData.cont--;
            if (StaticData.cont<=0){
              win.gameObject.SetActive(true);
            
            }
            }
   }
}