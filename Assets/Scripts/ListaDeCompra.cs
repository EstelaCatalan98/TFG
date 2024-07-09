using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ListaDeCompra : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown elegirProducto;
    [SerializeField] private TMP_InputField cantidad;
    [SerializeField] private TMP_InputField nproductos;
    [SerializeField] private TMP_InputField ndistracciones;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject mensajeError; 
    [SerializeField] private TMP_Text mensajeErrorText;  

    private List<GameObject> instantiatedPrefabs = new List<GameObject>();

    private const int MAX_PRODUCTOS = 12;
    private const int MAX_CANTIDAD = 10;
    private const float ERROR_MESSAGE_DURATION = 3f; 

    void Start()
    {
        cantidad.contentType = TMP_InputField.ContentType.IntegerNumber;
        nproductos.contentType = TMP_InputField.ContentType.IntegerNumber;
        ndistracciones.contentType = TMP_InputField.ContentType.IntegerNumber;

        CargarDatos();

        nproductos.onEndEdit.AddListener(delegate { ValidarNumeroProductos(); });
        ndistracciones.onEndEdit.AddListener(delegate { ValidarNumeroDistracciones(); });

        ValidarNumeroProductos();
    }

    public void Agregar()
    {
        string producto = elegirProducto.options[elegirProducto.value].text;
        int cantidadProducto;

        if (!int.TryParse(cantidad.text, out cantidadProducto) || cantidadProducto <= 0)
        {
            MostrarMensajeErrorTemporal("Cantidad no válida. Debe ser un número mayor que 0.");
            return;
        }

        int cantidadActual = PlayerPrefs.GetInt(producto, 0);

        
        if (cantidadActual + cantidadProducto > MAX_CANTIDAD)
        {
            MostrarMensajeErrorTemporal("La cantidad total del producto no puede ser mayor que " + MAX_CANTIDAD + ".");
            return;
        }

        PlayerPrefs.SetInt(producto, cantidadActual + cantidadProducto);

        GameObject existingPrefab = null;
        foreach (var prefabInstance in instantiatedPrefabs)
        {
            if (prefabInstance.GetComponentInChildren<TMP_Text>().text.StartsWith(producto + " x"))
            {
                existingPrefab = prefabInstance;
                break;
            }
        }

        if (existingPrefab != null)
        {
            existingPrefab.GetComponentInChildren<TMP_Text>().text = producto + " x" + (cantidadActual + cantidadProducto);
        }
        else
        {
            GameObject prefab1 = Instantiate(prefab, panel.transform);
            prefab1.GetComponentInChildren<TMP_Text>().text = producto + " x" + (cantidadActual + cantidadProducto);
            instantiatedPrefabs.Add(prefab1);
        }

        PlayerPrefs.Save();
        ValidarNumeroProductos();
    }

    public void guardar()
    {
        if (!int.TryParse(nproductos.text, out int numProductos) || numProductos <= 0)
        {
            MostrarMensajeErrorTemporal("Número de productos no válido. Debe ser un número mayor que 0.");
            return;
        }
        if (!int.TryParse(ndistracciones.text, out int numDistracciones) || numDistracciones <= 0)
        {
            MostrarMensajeErrorTemporal("Número de distracciones no válido. Debe ser un número mayor que 0.");
            return;
        }

        int totalProductos = numProductos + instantiatedPrefabs.Count;

        if (totalProductos > MAX_PRODUCTOS)
        {
            MostrarMensajeErrorTemporal("El número total de productos supera el máximo permitido de " + MAX_PRODUCTOS + ".");
            return;
        }

        PlayerPrefs.SetInt("NumeroProductos", numProductos);
        PlayerPrefs.SetInt("NumeroDistracciones", numDistracciones);

        PlayerPrefs.Save();

        Debug.Log("Datos guardados: númeroProductos y númeroDistracciones.");

        
        CambiarEscena();
    }

    private void CambiarEscena()
    {
        SceneManager.LoadScene("MainMenu");
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
        CargarDatos();
    }

    public void EliminarProducto(GameObject prefab)
    {
        instantiatedPrefabs.Remove(prefab);
        Destroy(prefab);
        ValidarNumeroProductos();
    }

    private void ValidarNumeroProductos()
    {
        if (!int.TryParse(nproductos.text, out int numProductos) || numProductos <= 0)
        {
            numProductos = 0;
        }
        
        int totalProductos = numProductos + instantiatedPrefabs.Count;

        Debug.Log("Total Productos: " + totalProductos + " (NumProductos: " + numProductos + " + Instanciated: " + instantiatedPrefabs.Count + ")");

        if (totalProductos > MAX_PRODUCTOS)
        {
            MostrarMensajeErrorTemporal("El número total de productos supera el máximo permitido de " + MAX_PRODUCTOS + ".");
        }
        else
        {
            if (mensajeError != null)
            {
                mensajeError.SetActive(false);
            }
        }
    }

    private void ValidarNumeroDistracciones()
    {
        if (!int.TryParse(ndistracciones.text, out int numDistracciones) || numDistracciones <= 0)
        {
            MostrarMensajeErrorTemporal("Número de distracciones no válido. Debe ser un número mayor que 0.");
            ndistracciones.text = "1";  // Establece un valor por defecto si el valor ingresado no es válido
        }
    }

    private void MostrarMensajeErrorTemporal(string mensaje)
    {
        if (mensajeErrorText != null)
        {
            mensajeErrorText.text = mensaje;
        }
        if (mensajeError != null)
        {
            mensajeError.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(OcultarMensajeError());
        }
    }

    private IEnumerator OcultarMensajeError()
    {
        yield return new WaitForSeconds(ERROR_MESSAGE_DURATION);
        if (mensajeError != null)
        {
            mensajeError.SetActive(false);
        }
    }
}



