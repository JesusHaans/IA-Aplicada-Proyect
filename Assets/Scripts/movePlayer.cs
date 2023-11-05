using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    private new Rigidbody rigidbody;
    public float speed = 500.0f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 750.0f;
        }
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput   = Input.GetAxisRaw("Vertical");
        
        if(horizontalInput != 0 || verticalInput != 0)
        {
            Vector3 direction = transform.forward * verticalInput + transform.right * horizontalInput;
            rigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }

    }
}
