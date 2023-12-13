using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class movePlayer : MonoBehaviour
{
    private new Rigidbody rigidbody;
    public int vida = 100;
    public float normalSpeed = 500.0f;  // Velocidad normal de movimiento
    public float runningSpeed = 750.0f; // Velocidad cuando se corre
    public float rotationSpeed = 180.0f; // Velocidad de rotaci�n
    public float tiempoGolpe;
    public float tiempoSigGolpe = 0;
    public GameObject winner;
    public GameObject[] zombies;
    public GameObject jugador;
    public TMP_Text txtvida;

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "zombie" && tiempoSigGolpe <= 0)
        {
            vida = vida - 10;
            tiempoSigGolpe = tiempoGolpe;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        zombies = GameObject.FindGameObjectsWithTag("zombie");
        winner = GameObject.Find("Canvas").transform.Find("Dead Screen").gameObject;
        jugador = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runningSpeed : normalSpeed;
        txtvida.text = vida.ToString();
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
            this.transform.Rotate(new Vector3(0, -rotationSpeed,0));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(new Vector3(0, rotationSpeed, 0));
        }
        if (tiempoSigGolpe > 0)
        {
            tiempoSigGolpe -= Time.deltaTime;
        }
        if(vida <= 0)
        {
            winner.SetActive(true);
            foreach (GameObject z in zombies)
            {
                z.GetComponent<StateZombie>().enabled = false;
                z.GetComponent<NavMeshAgent>().enabled = false;
            }
            jugador.GetComponent<movePlayer>().enabled = false;
        }
    }
}

