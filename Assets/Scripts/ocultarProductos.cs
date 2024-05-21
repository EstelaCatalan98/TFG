using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class OcultarProductos : MonoBehaviour
{
    public Button[] botones; 
    public int cantidadBotonesAleatorios =StaticData.numeroDistracciones; // Número de botones aleatorios a mostrar al inicio

    void Start()
    {
        botones=StaticData.botones;//aqui estan todos los productos(botones)
        OcultarBotones(); // Ocultar todos los botones al inicio del juego
        MostrarBotonesAleatorios(); // Mostrar botones aleatorios después de ocultar todos
    }

 
    void OcultarBotones()
{
   
    foreach (Button boton in botones)
    {
       
       
            boton.gameObject.SetActive(false);
            
      
    }
}


    // Método para mostrar un número específico de botones de forma aleatoria además de los botones especificados
    void MostrarBotonesAleatorios()
    {
        // Obtener los botones a mantener
        string botonesAMantener = StaticData.listaDef;
       
        

        foreach (Button boton in botones )
        {
           
            if (botonesAMantener.Contains(boton.name))
            {
                
                
                boton.gameObject.SetActive(true);
               
            }
        }

        // Obtener los botones que pueden ser aleatorios (no están en la lista de botones a mantener)
        List<Button> botonesNoAMantener = new List<Button>(botones);
       
        
        
        botonesNoAMantener.RemoveAll(boton => botonesAMantener.Contains(boton.name));
     
       

        // Mostrar botones aleatorios adicionales hasta alcanzar la cantidad deseada
        for (int i = 0; i < cantidadBotonesAleatorios; i++)
        {
            if (botonesNoAMantener.Count > 0)
            {
                Button botonAleatorio = botonesNoAMantener[UnityEngine.Random.Range(0, botonesNoAMantener.Count)];
                botonAleatorio.gameObject.SetActive(true);
                botonesNoAMantener.Remove(botonAleatorio); // Evitar duplicados
            }
            else
            {
                break; // Si ya no quedan botones para elegir, salir del bucle
            }
        }
    }
}
