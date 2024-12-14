using UnityEngine;
using System.Collections;

public class ImageToggleOnClick : MonoBehaviour
{
    public GameObject image1; // Primera imagen
    public GameObject image2; // Segunda imagen
    public float toggleTime = 1f; // Tiempo antes de revertir las imágenes

    private Coroutine toggleCoroutine; // Corrutina en ejecución
    private bool isToggling = false; // Estado de alternancia

    // Propiedad pública para verificar el estado
    public bool IsToggling
    {
        get { return isToggling; }
    }

    void Start()
    {
        // Asegurar estados iniciales
        if (image1 != null) image1.SetActive(true);
        if (image2 != null) image2.SetActive(false);
    }

    void Update()
    {
        // Detectar clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            // Inicia alternancia si no está en curso
            if (!isToggling)
            {
                toggleCoroutine = StartCoroutine(ToggleImages());
            }
        }
    }

    private IEnumerator ToggleImages()
    {
        isToggling = true; // Marcar que se está alternando

        // Verificar que ambas imágenes estén asignadas
        if (image1 == null || image2 == null)
        {
            Debug.LogWarning("Una de las imágenes no está asignada en el inspector.");
            isToggling = false;
            yield break;
        }

        // Desactivar la primera y activar la segunda
        image1.SetActive(false);
        image2.SetActive(true);

        // Esperar el tiempo configurado
        yield return new WaitForSeconds(toggleTime);

        // Revertir: activar la primera y desactivar la segunda
        image1.SetActive(true);
        image2.SetActive(false);

        // Finalizar alternancia
        isToggling = false;
    }
}
