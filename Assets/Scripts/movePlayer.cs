using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    private new Rigidbody rigidbody;
    public int vida = 100;
    public float normalSpeed = 500.0f;  // Velocidad normal de movimiento
    public float runningSpeed = 750.0f; // Velocidad cuando se corre
    public float rotationSpeed = 180.0f; // Velocidad de rotaci�n

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runningSpeed : normalSpeed;

        // Movimiento
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            Vector3 direction = transform.forward * verticalInput + transform.right * horizontalInput;
            rigidbody.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime);
        }

        // Rotaci�n alrededor del eje vertical
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
