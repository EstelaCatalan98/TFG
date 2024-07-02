using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ListaDeCompra : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown elegirProducto;
    [SerializeField] private TMP_InputField cantidad;
    [SerializeField] private TMP_InputField nproductos;
    [SerializeField] private TMP_InputField ndistracciones;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject prefab;
    private List<GameObject> instantiatedPrefabs = new List<GameObject>();

    void Start()
    {
        cantidad.contentType = TMP_InputField.ContentType.IntegerNumber;
        nproductos.contentType = TMP_InputField.ContentType.IntegerNumber;
        ndistracciones.contentType = TMP_InputField.ContentType.IntegerNumber;

        CargarDatos();
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

        int cantidadActual = PlayerPrefs.GetInt(producto, 0);
        PlayerPrefs.SetInt(producto, cantidadActual + cantidadProducto);

        GameObject prefab1 = Instantiate(prefab, panel.transform);
        prefab1.GetComponentInChildren<TMP_Text>().text = producto + " x" + (cantidadActual + cantidadProducto);
        instantiatedPrefabs.Add(prefab1);

        PlayerPrefs.Save();
    }

    public void guardar()
    {
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

       

        PlayerPrefs.SetInt("NumeroProductos", numProductos);
        PlayerPrefs.SetInt("NumeroDistracciones", numDistracciones);

        PlayerPrefs.Save();

        Debug.Log("Datos guardados: númeroProductos y númeroDistracciones.");
    }

    private void CargarDatos()
    {
       
        nproductos.text = PlayerPrefs.GetInt("NumeroProductos", 5).ToString();
        ndistracciones.text = PlayerPrefs.GetInt("NumeroDistracciones", 6).ToString();

        foreach (var option in elegirProducto.options)
        {
            string producto = option.text;

            if (PlayerPrefs.HasKey(producto))
            {
                int cantidadProducto = PlayerPrefs.GetInt(producto);
                GameObject prefab2 = Instantiate(prefab, panel.transform);
                prefab2.GetComponentInChildren<TMP_Text>().text = producto + " x" + cantidadProducto;
                instantiatedPrefabs.Add(prefab2);
            }
        }
    }

    public void ResetearDatos()
    {
        PlayerPrefs.DeleteAll();

     

        foreach (GameObject instanciaPrefab in instantiatedPrefabs)
        {
            if (instanciaPrefab != null)
            {
                Destroy(instanciaPrefab);
            }
        }

        instantiatedPrefabs.Clear();
    }
}
