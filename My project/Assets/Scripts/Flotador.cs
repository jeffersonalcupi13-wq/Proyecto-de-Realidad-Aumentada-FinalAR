using UnityEngine;

public class Flotador : MonoBehaviour
{
    [Header("Configuración de Flotación")]
    [Tooltip("Qué tan rápido sube y baja")]
    public float velocidad = 2.0f;

    [Tooltip("Qué tan alto se mueve desde su posición original")]
    public float amplitud = 0.5f;

    private Vector3 posicionInicial;

    void Start()
    {
        // Guardamos la posición original para tener un punto de referencia
        posicionInicial = transform.position;
    }

    void Update()
    {
        // Calculamos el nuevo desplazamiento en el eje Y
        float nuevoY = posicionInicial.y + Mathf.Sin(Time.time * velocidad) * amplitud;

        // Aplicamos la posición manteniendo X y Z originales
        transform.position = new Vector3(posicionInicial.x, nuevoY, posicionInicial.z);
    }
}