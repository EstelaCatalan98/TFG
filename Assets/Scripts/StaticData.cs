using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Purchasing;
using Unity.VisualScripting;

public class StaticData : MonoBehaviour
{
   public static string valueToKeep;
   public static int cont;
   public static string ListaSeleccionable;
   public static int numeroProductos =7;
   public static Dictionary<string, int> lista =
    new Dictionary<string, int>();

    public static int numeroDistracciones;
    public static Button[] botones; 
    
}
