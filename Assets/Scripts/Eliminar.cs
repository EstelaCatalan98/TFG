using TMPro;
using UnityEngine;

public class Eliminar : MonoBehaviour

{
    // Esta función se llamará desde el botón
    public void DeleteParentObject()
    {
        Debug.Log("ksajhfbrkqb");
        // Obtiene el objeto padre
        GameObject parentObject = transform.parent.gameObject;
        Debug.Log("" + parentObject.name);

        // Obtiene el TMP_Text del padre
        TMP_Text tmpTextComponent = parentObject.GetComponentInChildren<TMP_Text>();

        if (tmpTextComponent != null)
        {
            // Obtiene el texto del TMP_Text
            string text = tmpTextComponent.text;

            // Parsear el texto para obtener el nombre del producto
            string productName = ParseProductName(text);
            PlayerPrefs.DeleteKey(productName);
            
           


            // Loguea el nombre del producto (opcional)
            Debug.Log("Producto: " + productName);
        }
        else
        {
            Debug.LogError("No se encontró un componente TMP_Text en el objeto padre.");
        }

        // Destruye el objeto padre
        Destroy(parentObject);
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


