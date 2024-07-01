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
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject prefab;
    private List<GameObject> instantiatedPrefabs = new List<GameObject>();
   

    private Dictionary<string, int> productoCantidad = new Dictionary<string, int>();

    void Start()
    {
    cantidad.contentType = TMP_InputField.ContentType.IntegerNumber;
    nproductos.contentType = TMP_InputField.ContentType.IntegerNumber;
    ndistracciones.contentType = TMP_InputField.ContentType.IntegerNumber;
        
        CargarDatos();
        //ActualizarLista();
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
        prefab=Instantiate(prefab, panel.transform);
           
            prefab.GetComponentInChildren<TMP_Text>().text = producto + " x" + cantidadProducto + "  ";
            instantiatedPrefabs.Add(prefab);

        //ActualizarLista();
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
                prefab=Instantiate(prefab, panel.transform);
                prefab.GetComponentInChildren<TMP_Text>().text = producto + " x" + cantidadProducto + "  ";
                instantiatedPrefabs.Add(prefab);
               
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

        foreach (GameObject instanciaPrefab in instantiatedPrefabs)
    {
        if (instanciaPrefab != null)
        {
            Destroy(instanciaPrefab);
        }
    }

    // Limpiar la lista de prefabs instanciados
    instantiatedPrefabs.Clear();
       
        //ActualizarLista();
    }

    
}


