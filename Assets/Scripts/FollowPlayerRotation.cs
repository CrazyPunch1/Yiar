using UnityEngine;

public class FollowPlayerRotation : MonoBehaviour
{
    // Asigna aqu� el objeto del jugador en el Inspector
    public Transform player;

    private Quaternion initialRotation;
    [SerializeField] float rotationOffset;

    void Start()
    {
        // Guarda la rotaci�n inicial del objeto
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (player != null)
        {
            // Obtiene solo la rotaci�n del jugador (sin cambiar la posici�n del objeto)
            transform.rotation = player.rotation * initialRotation;
            Vector3 currentR = transform.rotation.eulerAngles;


            transform.rotation = Quaternion.Euler(currentR.x, currentR.y + rotationOffset, currentR.z);
        }
    }
}
