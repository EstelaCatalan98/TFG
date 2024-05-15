using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Purchasing;
using Unity.VisualScripting;

public class CrearListaCompra : MonoBehaviour
{

   int nProductos ;
  public TextMeshProUGUI lista;
  String finalList;
  String listaseleccionada;

  List<string> Productos = new List<string>();
  public Button[] botones; // Array de botones 



  public List<T> GetProductos<T>(List<T> inputList, int count)
  {
    List<T> outputList = new List<T>();
    for (int i = 0; i < count; i++)
    {

      int index = UnityEngine.Random.Range(0, inputList.Count);
      outputList.Add(inputList[index]);
      Productos.RemoveAt(index);
    }

    return outputList;
  }

  void Awake()
  {
    finalList = StaticData.valueToKeep;
    listaseleccionada = StaticData.ListaSeleccionable;
    nProductos = StaticData.numeroProductos;
    foreach (Button boton in botones)
    {
      // Obtener el nombre del botón y agregarlo a la lista de nombres
      Productos.Add(boton.name);
    }
  
    StaticData.botones=botones;
    if (nProductos <= 0)
    {
      nProductos = 4;
    }
    if (finalList == null && listaseleccionada == null)
    {
      List<String> listaCompra = GetProductos(Productos, nProductos);
      for (int i = 0; i < listaCompra.Count; i++)
      {
        if (i == 0)
        {
          finalList = listaCompra[0];
        }
        else
        {
          finalList = new String(finalList + Environment.NewLine + listaCompra[i]);
        }
        StaticData.valueToKeep = finalList;
        StaticData.cont = nProductos;



      }
    }
    else if (listaseleccionada != null)
    {
      StaticData.valueToKeep = listaseleccionada;
      StaticData.ListaSeleccionable = null;
      lista.text = listaseleccionada;
    }
    lista.text = finalList;


  }
  public void seleccionarProducto(string productoSeleccionado)
  {
    if (StaticData.valueToKeep.Contains(productoSeleccionado))
    {

      finalList = new String(StaticData.valueToKeep.Replace(productoSeleccionado, "<s>" + productoSeleccionado + "</s>"));
      lista.text = finalList;
      StaticData.valueToKeep = finalList;
      StaticData.cont--;

    }
    else
    {
      Debug.Log("error");
    }
  }

}
