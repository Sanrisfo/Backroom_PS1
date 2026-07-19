using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.VirtualTexturing;

public class MovimientoJugador : MonoBehaviour
{
    public CharacterController controller;
    public float velocidad = 4.0f;
    public float sensibilidadMouse = 2.0f;

    private float rotacionX = 0f;
    void Start()
    {
        //Al inicio ocultamos el cursor del mouse y lo bloqueamos
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Definimos las variables que se cambian con el mouse
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse;

        //Toda la logica del movimiento de la camara con el mouse
        rotacionX -= mouseY;
        //con este clamp la camara se bloquea cuando el jugador ve hacia arriba o abajo
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);

        //transformamos los grados a quaterniones y los inyectamos en la rotación de la camara
        Camera.main.transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        //Movimiento con las teclas wasd
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 mover = transform.right * x + transform.forward * z;

        //Move es la funcion que nos ayuda a mover el personaje.
        controller.Move(mover * velocidad * Time.deltaTime);
    }
}
