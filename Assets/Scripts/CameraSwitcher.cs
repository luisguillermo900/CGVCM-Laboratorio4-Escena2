using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera firstPersonCamera; // C�mara en primera persona
    public Camera thirdPersonCamera; // C�mara en tercera persona

    public KeyCode switchKey = KeyCode.C; // Tecla para alternar entre c�maras
    public LayerMask collisionLayers; // Capas que activan el cambio de c�mara mediante colisi�n

    private bool isFirstPerson = true; // Estado de la c�mara (true = primera persona, false = tercera persona)

    void Start()
    {
        // Activar la c�mara inicial
        SetCameraView(isFirstPerson);
    }

    void Update()
    {
        // Cambiar de c�mara al presionar la tecla "switchKey"
        if (Input.GetKeyDown(switchKey))
        {
            isFirstPerson = !isFirstPerson;
            SetCameraView(isFirstPerson);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detectar colisi�n con objetos en la capa especificada
        if (((1 << other.gameObject.layer) & collisionLayers) != 0)
        {
            // Cambiar de c�mara al entrar en la zona de colisi�n
            isFirstPerson = !isFirstPerson;
            SetCameraView(isFirstPerson);
        }
    }

    private void SetCameraView(bool firstPerson)
    {
        if (firstPerson)
        {
            // Activar c�mara de primera persona y desactivar la de tercera persona
            firstPersonCamera.enabled = true;
            thirdPersonCamera.enabled = false;
        }
        else
        {
            // Activar c�mara de tercera persona y desactivar la de primera persona
            firstPersonCamera.enabled = false;
            thirdPersonCamera.enabled = true;
        }
    }
}
