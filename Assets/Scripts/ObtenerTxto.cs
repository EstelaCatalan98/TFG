using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ToggleProductController : MonoBehaviour
{
    public Dictionary<string, int> result = new Dictionary<string, int>(); // Diccionario que almacena los productos y sus valores
    public TMP_InputField inputField; // Referencia al campo de entrada de texto

    private Dictionary<Toggle, string> toggleProductos = new Dictionary<Toggle, string>(); // Diccionario que asocia cada toggle con su producto

    void Awake()
    {
        // Obtener todos los toggles hijos del GameObject actual y asociarlos con sus productos
        Toggle[] toggles = GetComponentsInChildren<Toggle>();
        foreach (Toggle toggle in toggles)
        {
            // Asignar un nombre de producto basado en el nombre del toggle (puedes ajustar esto según tu estructura)
            string producto = toggle.gameObject.name;
            toggleProductos.Add(toggle, producto);
            toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(toggle); });
        }
    }

    // Método para manejar la selección/deselección de una opción cuando se cambia el estado del toggle
    void ToggleValueChanged(Toggle toggle)
    {
        string producto = toggleProductos[toggle];

        if (toggle.isOn)
        {
            // Llamar al método para activar el campo de entrada y asociar el producto
            ActivarCampoDeEntrada(producto);
        }
        else
        {
            // Llamar al método para desactivar el campo de entrada y borrar el producto
            DesactivarCampoDeEntrada();
            Borrar(producto);
        }
    }

    // Método para activar el campo de entrada y asociarlo con el producto
    void ActivarCampoDeEntrada(string producto)
    {
        inputField.interactable = true;
        inputField.onEndEdit.AddListener(delegate { ObtenerInt(producto); });
    }

    // Método para desactivar el campo de entrada
    void DesactivarCampoDeEntrada()
    {
        inputField.interactable = false;
        inputField.text = ""; // Limpiar el texto cuando se desactiva
        inputField.onEndEdit.RemoveAllListeners(); // Remover todos los listeners del evento
    }

    // Método para obtener el texto del InputField y asociarlo con el producto
    public void ObtenerInt(string producto)
    {
        if (int.TryParse(inputField.text, out int valorEntero))
        {
            result[producto] = valorEntero; // Asociar el valor entero con el producto
        }
        else
        {
            Debug.LogWarning("Texto ingresado no es un número entero válido.");
        }
    }

    // Método para borrar el producto del diccionario
    public void Borrar(string producto)
    {
        if (result.ContainsKey(producto))
        {
            result.Remove(producto);
        }
    }

    // Método para guardar el diccionario en StaticData.lista
    public void GuardarInt()
    {
        StaticData.lista = result;
    }
}
