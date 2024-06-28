using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListaDeCompra : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown elegirProducto;
    [SerializeField] private TMP_InputField cantidad;
    [SerializeField] private TMP_InputField nproductos;
    [SerializeField] private TMP_InputField ndistracciones;
    [SerializeField] private TMP_Text lista;
    [SerializeField] private Button botonEliminarPrefab; // Referencia al prefab del botón eliminar
    [SerializeField] private Transform contenedorLista; // Contenedor donde se agregan los elementos de la lista

    private Dictionary<string, int> productoCantidad = new Dictionary<string, int>();

    void Start()
    {
        cantidad.contentType = TMP_InputField.ContentType.IntegerNumber;
    nproductos.contentType = TMP_InputField.ContentType.IntegerNumber;
    ndistracciones.contentType = TMP_InputField.ContentType.IntegerNumber;
        CargarDatos();
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
        if (!int.TryParse(nproductos.text, out int numProductos))
        {
            Debug.LogError("Número de productos no válido.");
            return;
        }
        if (!int.TryParse(ndistracciones.text, out int numDistracciones))
        {
            Debug.LogError("Número de distracciones no válido.");
            return;
        }

         StaticData.listaEditor = productoCantidad;
        StaticData.numeroProductos = numProductos;
        StaticData.numeroDistracciones = numDistracciones;
        Debug.Log(StaticData.numeroProductos);
        Debug.Log(StaticData.numeroDistracciones);

        GuardarDatos();
        Debug.Log("Datos guardados: númeroProductos y númeroDistracciones.");
    }

    private void GuardarDatos()
    {
        // Guardar la cantidad de productos y distracciones en PlayerPrefs
        PlayerPrefs.SetInt("NumeroProductos", StaticData.numeroProductos);
        PlayerPrefs.SetInt("NumeroDistracciones", StaticData.numeroDistracciones);

        // Guardar la lista de productos y cantidades
        foreach (var kvp in productoCantidad)
        {
            PlayerPrefs.SetInt(kvp.Key, kvp.Value);
        }

        PlayerPrefs.Save();
    }

    private void CargarDatos()
    {
        // Cargar la cantidad de productos y distracciones de PlayerPrefs
        StaticData.numeroProductos = PlayerPrefs.GetInt("NumeroProductos", 4);
        StaticData.numeroDistracciones = PlayerPrefs.GetInt("NumeroDistracciones", 2);

        nproductos.text = StaticData.numeroProductos.ToString();
        ndistracciones.text = StaticData.numeroDistracciones.ToString();

        // Cargar la lista de productos y cantidades
        foreach (var option in elegirProducto.options)
        {
            string producto = option.text;
            if (PlayerPrefs.HasKey(producto))
            {
                int cantidadProducto = PlayerPrefs.GetInt(producto);
                productoCantidad[producto] = cantidadProducto;
            }
        }
    }

    public void ResetearDatos()
    {
        // Limpiar PlayerPrefs
        PlayerPrefs.DeleteAll();
        productoCantidad.Clear();
        StaticData.numeroProductos = 4;
        StaticData.numeroDistracciones = 2;

        // Actualizar campos de entrada y texto de la lista
        nproductos.text = "4";
        ndistracciones.text = "2";
        ActualizarLista();
    }

    public void EliminarProducto()
    {
        string producto = elegirProducto.options[elegirProducto.value].text;

        if (productoCantidad.ContainsKey(producto))
        {
            productoCantidad.Remove(producto);
            PlayerPrefs.DeleteKey(producto);
            ActualizarLista();
        }
        else
        {
            Debug.LogWarning("El producto no está en la lista.");
        }
    }
}


