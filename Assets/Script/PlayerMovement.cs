using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool debugs = true;
    public float speed = 10;

    public bool isGrounded;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(Dir(debugs) * speed);
    }

    private void OnCollisionEnter()
    {
        isGrounded = true;
    }
    Vector3 Dir(bool debugs)
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 curDir = new Vector3(x, 0, z);

        if (debugs)
        {
            Debug.DrawRay(transform.position, rb.velocity, Color.yellow);
            Debug.Log("vector: " + curDir);
            Debug.DrawRay(transform.position, curDir * 2f, Color.yellow);
        }

        return curDir;
    }
}
