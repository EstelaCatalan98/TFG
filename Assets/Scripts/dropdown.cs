using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ListaDeCompra : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown elegirProducto;
    [SerializeField] private TMP_InputField cantidad;
    [SerializeField] private TMP_InputField nproductos;
     [SerializeField] private TMP_InputField ndistracciones;
    [SerializeField] private TMP_Text lista;
    int numerodeproductos = 0;
    int numerodedistracciones = 0;

    private Dictionary<string, int> productoCantidad = new Dictionary<string, int>();

    void Start()
    {
        ActualizarLista();
    }

    public void Agregar()
    {
        string producto = elegirProducto.options[elegirProducto.value].text;
        int cantidadProducto;

        if (!int.TryParse(cantidad.text, out cantidadProducto))
        {
            Debug.LogError("Cantidad no válida.");
            return;
        }

        if (productoCantidad.ContainsKey(producto))
        {
            Debug.LogWarning("El producto ya está en la lista. Actualizando cantidad.");
            productoCantidad[producto] += cantidadProducto;
        }
        else
        {
            productoCantidad.Add(producto, cantidadProducto);
        }

        ActualizarLista();
    }

    public void ActualizarLista()
    {
        lista.text = ""; // Limpiar la lista antes de actualizarla

        foreach (var kvp in productoCantidad)
        {
            lista.text += kvp.Key + " x" + kvp.Value + "  "; // Mostrar el producto y su cantidad
            lista.text += "\n"; // Nueva línea para el siguiente elemento
        }
    }

    public void guardar()
    {
        // Verificar si la entrada de nproductos es un entero válido
        if (!int.TryParse(nproductos.text, out numerodeproductos))
        {
            Debug.LogError("Número de productos no válido.");
            return;
        }
        if (!int.TryParse(ndistracciones.text, out numerodedistracciones))
        {
            Debug.LogError("Número de productos no válido.");
            return;
        }

        StaticData.listaEditor = productoCantidad;
        StaticData.numeroProductos = numerodeproductos;
        StaticData.numeroDistracciones=numerodedistracciones;
        Debug.Log(StaticData.numeroProductos);
        Debug.Log(StaticData.numeroDistracciones);

        Debug.Log("Datos guardados: listaEditor y numeroProductos.");
    }
}
