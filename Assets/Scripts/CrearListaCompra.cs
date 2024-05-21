using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Purchasing;
using Unity.VisualScripting;
using UnityEngine.Purchasing.MiniJSON;


public class CrearListaCompra : MonoBehaviour
{
  List<string> Productos = new List<string>();
  Dictionary<string, int> listaseleccionada;
  int nProductos;
  public TextMeshProUGUI lista;
  public TextMeshProUGUI textoAviso;
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
  public void GuardarProductos()
  {
    // Convertir la lista a formato JSON
    string json = JsonUtility.ToJson(Productos);

    // Guardar la cadena JSON en PlayerPrefs
    PlayerPrefs.SetString("Productos", json);

    // Guardar los cambios en PlayerPrefs
    PlayerPrefs.Save();
  }
  public int getCantidad()
  {
    return UnityEngine.Random.Range(1, 5);
  }

  public void seleccionarProducto(string productoSeleccionado)
  {
    // Divide el texto en líneas, eliminando posibles espacios en blanco alrededor
    string[] lines = StaticData.listaDef.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
    string newText = "";
    bool allProductsZeroOrStriked = true; // Flag para controlar si todos los productos están a cero o tachados
    bool productoEncontrado = false; // Flag para controlar si el producto seleccionado se encontró en la lista

    for (int i = 0; i < lines.Length; i++)
    {
      string line = lines[i].Trim();
      string[] parts = line.Split(new[] { " x " }, System.StringSplitOptions.None);

      if (parts.Length == 2)
      {
        string productName = parts[0].Replace("<s>", "").Replace("</s>", "").Trim();

        if (productName.Equals(productoSeleccionado, System.StringComparison.OrdinalIgnoreCase))
        {
          productoEncontrado = true; // Marcar que el producto ha sido encontrado en la lista

          string numberPart = parts[1].Trim();

          if (int.TryParse(numberPart, out int number))
          {
            if (number > 0)
            {
              number--;
            }

            if (number == 0)
            {
              parts[0] = $"<s>{productName}</s>";
            }
            else
            {
              parts[0] = productName;
              allProductsZeroOrStriked = false; // Hay al menos un producto no tachado y con cantidad mayor a cero
            }

            line = $"{parts[0]} x {number}";
          }
        }
        else if (!productName.StartsWith("<s>")) // Si el producto no coincide y no está tachado
        {
          allProductsZeroOrStriked = false; // Hay al menos un producto no tachado y con cantidad mayor a cero
        }
      }

      newText += line;

      if (i < lines.Length - 1)
      {
        newText += "\n";
      }
    }

    StaticData.listaDef = newText;
    lista.text = newText;
   Debug.Log("NO en el if zero");
    // Verificar si todos los productos están a cero o tachados
    if (allProductsZeroOrStriked)
    {
      Debug.Log("en el if zero");
      MostrarVentanaFinal();
    }

    // Mostrar un aviso si el producto seleccionado no se encuentra en la lista
    if (!productoEncontrado)
    {
      Debug.Log("El producto seleccionado no se encuentra en la lista.");
    }

    // Restablecer el flag a true si es necesario
    allProductsZeroOrStriked = true;
  }
  public void MostrarAviso(string mensaje)
  {
    // Actualiza el texto del objeto de texto con el mensaje de aviso
    textoAviso.text = mensaje;
  }

  void MostrarVentanaFinal()
  {
    // Aquí puedes mostrar una ventana de final de juego, por ejemplo:
    Debug.Log("¡Fin del juego! Todos los productos han sido agotados o tachados.");
    // Aquí puedes llamar a una función que muestre la ventana final de juego, o ejecutar cualquier otra lógica de finalización de juego.
  }




  void Awake()
  {
    listaseleccionada = StaticData.listaEditor;
    foreach(var item in StaticData.listaEditor){
      Debug.Log(item.Value.ToString());
    }
    nProductos = StaticData.numeroProductos;
    string texto = "";
    foreach (Button boton in botones)
    {
      // Obtener el nombre del botón y agregarlo a la lista de nombres
      Productos.Add(boton.name);
    }
    StaticData.botones = botones;
    StaticData.productos = Productos;
    GuardarProductos();

    
    if (listaseleccionada.Count == 0)
    {
      
      //cogemos nproductos aleatorios
      List<String> listaCompra = GetProductos(Productos, nProductos);
      for (int i = 0; i < listaCompra.Count; i++)
      {
        listaseleccionada.Add(listaCompra[i], getCantidad());
      }
    }
    else 
    {
      
      
      List<string> lista = new List<string>();
     
      foreach (var kvp in listaseleccionada)
      {
        if (Productos.Contains(kvp.Key))
        {
          Productos.Remove(kvp.Key);
        }
      }
      //cogemos nproductos aleatorios
      lista = GetProductos(Productos, nProductos);
      for (int i = 0; i < lista.Count; i++)
      {
        listaseleccionada.Add(lista[i], getCantidad());
      }
    }
    StaticData.lista = listaseleccionada;
    StaticData.cont = nProductos;
    foreach (var kvp in listaseleccionada)
    {
      texto += kvp.Key + " x " + kvp.Value + "\n";
    }
    lista.text = texto;
    StaticData.listaDef = texto;
  }
}