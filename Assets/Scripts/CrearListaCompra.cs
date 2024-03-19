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
    
    [SerializeField] int count =4;
    public TextMeshProUGUI lista;
    String finalList;

//lista con todos los posibles productos
  public List<string> Productos=new List<string>{"tomates","leche","pan","yogures","manzanas"};
//funcion para generar lista con productos aleatorios
 public List<T> GetProductos<T>(List<T> inputList,int count)// metemos lista de posibles productos y numero de productos que queremos en la lista nueva
  
  {
    List<T> outputList = new List<T>();
    for (int i=0;i<count;i++)
    {
      
        int index = UnityEngine.Random.Range(0, inputList.Count);
        outputList.Add(inputList[index]);
        Productos.RemoveAt(index);
    }
    
    return outputList;
  }
  
 void Awake(){
  finalList=StaticData.valueToKeep;
  //finalList=PlayerPrefs.GetString("Productos");
  if (finalList==null){
  List<String> listaCompra=GetProductos(Productos, count);
        for (int i =0;i<listaCompra.Count;i++){
          if (i==0){
            finalList=listaCompra[0];
          }else{
          finalList= new String(finalList + Environment.NewLine +listaCompra[i]);}
          StaticData.valueToKeep=finalList;
          //PlayerPrefs.SetString("Productos",finalList);
          
         
        }
  }
    lista.text=finalList;
    
    
 }
}
