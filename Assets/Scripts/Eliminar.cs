using TMPro;
using UnityEngine;

public class Eliminar : MonoBehaviour
{
    private ListaDeCompra listaDeCompra;

    void Start()
    {
        // Encuentra el script ListaDeCompra en la escena
        listaDeCompra = FindObjectOfType<ListaDeCompra>();

        if (listaDeCompra == null)
        {
            Debug.LogError("No se encontró un componente ListaDeCompra en la escena.");
        }
    }

    
    public void DeleteParentObject()
    {
        // Obtiene el objeto padre
        GameObject parentObject = transform.parent.gameObject;

        // Obtiene el TMP_Text del padre
        TMP_Text tmpTextComponent = parentObject.GetComponentInChildren<TMP_Text>();

        if (tmpTextComponent != null)
        {
            // Obtiene el texto del TMP_Text
            string text = tmpTextComponent.text;

            // Parsear el texto para obtener el nombre del producto
            string productName = ParseProductName(text);
            PlayerPrefs.DeleteKey(productName);
        }
        else
        {
            Debug.LogError("No se encontró un componente TMP_Text en el objeto padre.");
        }

        // Notifica a ListaDeCompra para eliminar el prefab y actualizar la lista
        if (listaDeCompra != null)
        {
            listaDeCompra.EliminarProducto(parentObject);
        }
        else
        {
            Debug.LogError("No se ha encontrado una referencia a ListaDeCompra.");
            // Destruye el objeto padre
            Destroy(parentObject);
        }

        PlayerPrefs.Save();
    }

    private string ParseProductName(string text)
    {
        // Divide el texto en partes usando 'x' como delimitador
        string[] parts = text.Split('x');

        if (parts.Length != 2)
        {
            Debug.LogError("El formato del texto no es correcto.");
            return null;
        }

        // El nombre del producto es la primera parte
        return parts[0].Trim();
    }
}




