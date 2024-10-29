using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    public float movementSpeed = 4.0f;
    public Vector2 sensitivity = new Vector2(2.0f, 2.0f);
    public Transform camera;
    public float gravity = -9.8f;

    public GameObject playerMesh; // Modelo del personaje para hacerlo invisible en primera persona
    private bool isFirstPerson = false; // Estado de la vista (false = tercera persona, true = primera persona)

    private float verticalVelocity = 0.0f; // Para manejar la gravedad

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Iniciar en tercera persona
        SetFirstPersonView(false);
    }

    private void UpdateMovement()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 movement = Vector3.zero;

        if (hor != 0 || ver != 0)
        {
            // Calcula la dirección en función de la orientación del jugador
            Vector3 direction = (transform.forward * ver + transform.right * hor).normalized;
            movement = direction * movementSpeed;
        }

        // Aplicar gravedad
        if (characterController.isGrounded)
        {
            verticalVelocity = 0;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        movement.y = verticalVelocity;

        // Mueve el personaje
        characterController.Move(movement * Time.deltaTime);
    }

    private void UpdateMouseLook()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        if (hor != 0)
        {
            transform.Rotate(0, hor * sensitivity.x, 0);
        }

        if (ver != 0)
        {
            Vector3 rotation = camera.localEulerAngles;
            rotation.x = (rotation.x - ver * sensitivity.y + 360) % 360;
            if (rotation.x > 80 && rotation.x < 180) { rotation.x = 80; }
            else if (rotation.x < 280 && rotation.x > 180) { rotation.x = 280; }

            camera.localEulerAngles = rotation;
        }
    }

    private void ToggleView()
    {
        // Alternar entre primera y tercera persona
        isFirstPerson = !isFirstPerson;
        SetFirstPersonView(isFirstPerson);
    }

    private void SetFirstPersonView(bool firstPerson)
    {
        if (firstPerson)
        {
            // Hacer invisible el modelo del personaje desactivando los Renderers
            SetPlayerMeshVisibility(false);
        }
        else
        {
            // Hacer visible el modelo del personaje activando los Renderers
            SetPlayerMeshVisibility(true);
        }
    }

    private void SetPlayerMeshVisibility(bool visible)
    {
        Renderer[] renderers = playerMesh.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = visible;
        }
    }

    void Update()
    {
        // Alternar la vista entre primera y tercera persona con la tecla "V"
        if (Input.GetKeyDown(KeyCode.V))
        {
            ToggleView();
        }

        UpdateMovement();
        UpdateMouseLook();
    }
}
