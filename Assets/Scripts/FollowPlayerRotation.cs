using UnityEngine;

public class FollowPlayerRotation : MonoBehaviour
{
    // Asigna aquí el objeto del jugador en el Inspector
    public Transform player;

    private Quaternion initialRotation;
    [SerializeField] float rotationOffset;

    void Start()
    {
        // Guarda la rotación inicial del objeto
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (player != null)
        {
            // Obtiene solo la rotación del jugador (sin cambiar la posición del objeto)
            transform.rotation = player.rotation * initialRotation;
            Vector3 currentR = transform.rotation.eulerAngles;


            transform.rotation = Quaternion.Euler(currentR.x, currentR.y + rotationOffset, currentR.z);
        }
    }
}
