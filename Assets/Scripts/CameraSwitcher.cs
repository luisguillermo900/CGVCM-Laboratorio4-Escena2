using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera firstPersonCamera; // Cámara en primera persona
    public Camera thirdPersonCamera; // Cámara en tercera persona

    public KeyCode switchKey = KeyCode.C; // Tecla para alternar entre cámaras
    public LayerMask collisionLayers; // Capas que activan el cambio de cámara mediante colisión

    private bool isFirstPerson = true; // Estado de la cámara (true = primera persona, false = tercera persona)

    void Start()
    {
        // Activar la cámara inicial
        SetCameraView(isFirstPerson);
    }

    void Update()
    {
        // Cambiar de cámara al presionar la tecla "switchKey"
        if (Input.GetKeyDown(switchKey))
        {
            isFirstPerson = !isFirstPerson;
            SetCameraView(isFirstPerson);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detectar colisión con objetos en la capa especificada
        if (((1 << other.gameObject.layer) & collisionLayers) != 0)
        {
            // Cambiar de cámara al entrar en la zona de colisión
            isFirstPerson = !isFirstPerson;
            SetCameraView(isFirstPerson);
        }
    }

    private void SetCameraView(bool firstPerson)
    {
        if (firstPerson)
        {
            // Activar cámara de primera persona y desactivar la de tercera persona
            firstPersonCamera.enabled = true;
            thirdPersonCamera.enabled = false;
        }
        else
        {
            // Activar cámara de tercera persona y desactivar la de primera persona
            firstPersonCamera.enabled = false;
            thirdPersonCamera.enabled = true;
        }
    }
}
