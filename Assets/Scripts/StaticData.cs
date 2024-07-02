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

    public static int cont;
    public static int numeroProductos=4;
    public static Dictionary<string, int> lista =
   new Dictionary<string, int>();
    public static List<string> productos = new List<string>();

    public static int numeroDistracciones=2;
    public static Button[] botones;
    public static string listaDef;

}
