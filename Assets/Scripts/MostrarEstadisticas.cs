using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MostrarEstadisticas : MonoBehaviour
{
    [Serializable]
    public class Partida
    {
        public DateTime fecha;
        public int aciertos;
        public int fallos;
    }

    [SerializeField] private GameObject filaPrefab; // Prefab de la fila de la tabla
    [SerializeField] private Transform content; // Contenedor de las filas en la tabla

    private List<Partida> partidas = new List<Partida>();

    void Start()
    {
        // Cargar las partidas guardadas
        CargarPartidas();

        // Mostrar la tabla con las partidas
        MostrarTabla();
    }

    private void CargarPartidas()
    {
        partidas.Clear();

        // Aquí debes cargar las partidas guardadas desde PlayerPrefs u otra fuente de datos
        // Por ejemplo, cargarlas desde PlayerPrefs en este caso
        int numPartidas = PlayerPrefs.GetInt("NumeroPartidas", 0);
        for (int i = 0; i < numPartidas; i++)
        {
            Partida partida = new Partida();
            partida.fecha = DateTime.Parse(PlayerPrefs.GetString($"Partida_{i}_Fecha"));
            partida.aciertos = PlayerPrefs.GetInt($"Partida_{i}_Aciertos");
            partida.fallos = PlayerPrefs.GetInt($"Partida_{i}_Fallos");
            partidas.Add(partida);
        }
    }

    private void MostrarTabla()
    {
        LimpiarTabla();

        foreach (Partida partida in partidas)
        {
            GameObject fila = Instantiate(filaPrefab, content.transform);
            fila.GetComponentInChildren<TextMeshProUGUI>().text = $"Fecha: {partida.fecha}\nAciertos: {partida.aciertos}\nFallos: {partida.fallos}";
        }
    }

    private void LimpiarTabla()
    {
       
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
    public void borrartabla(){
        LimpiarTabla();
         int numPartidas = PlayerPrefs.GetInt("NumeroPartidas", 0);
        for (int i = 0; i < numPartidas; i++)
        {
            
            PlayerPrefs.DeleteKey($"Partida_{i}_Fecha");
            PlayerPrefs.DeleteKey($"Partida_{i}_Aciertos");
             PlayerPrefs.DeleteKey($"Partida_{i}_Fallos");
            partidas.Clear();
        }
        PlayerPrefs.DeleteKey("NumeroPartidas");
        
    }
}




