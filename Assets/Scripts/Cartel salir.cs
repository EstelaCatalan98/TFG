using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartelSalir : MonoBehaviour
{
    [SerializeField] private GameObject cartelPrefab; // Prefab del cartel
    [SerializeField] private GameObject panel; // Panel en el que se instanciará el cartel

    private GameObject instantiatedCartel; // Referencia al cartel instanciado

    // Mostrar el cartel
    public void MostrarCartel()
    {
      
            // Instanciar el cartel si aún no se ha instanciado
            cartelPrefab=Instantiate(cartelPrefab, panel.transform);
        
        
            // Activar el cartel si ya está instanciado
            cartelPrefab.SetActive(true);
        
    }

    // Quitar el cartel
    public void QuitarCartel()
    {
      GameObject parentObject = transform.parent.gameObject;
      parentObject.SetActive(false);
        
    }
}

