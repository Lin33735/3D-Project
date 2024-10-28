using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.Jobs;

public class PlayerMovement : MonoBehaviour
{
    public bool debugs = true;
    float speed = 10f;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.TransformDirection(Dir(debugs) * speed));
    }

    Vector3 Dir (bool debugs)
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 curDir = new Vector3(x, 0, y);

        if (debugs)
        {
            Debug.DrawRay(transform.position, rb.velocity, Color.yellow);
            //Debug.Log("Vector: " + curDir);
            Debug.DrawRay(transform.position, transform.TransformDirection(curDir) * 2f, Color.yellow);
        }
        return curDir;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
