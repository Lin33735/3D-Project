using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.Jobs;

public class PlayerMovement : MonoBehaviour
{
    public bool debugs = true;
    public float baseSpeed = 5f;
    public float speed;

    //Dash
    private bool isDashing;
    private Vector3 dashDir;
    public float dashCooldown;
    public float dashSpeed = 120f;
    public float dashCooldownDur = 2f;
    public float dashTime = 0.3f;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        speed = baseSpeed;
    }

    void FixedUpdate()
    {
        //rb.AddForce(transform.TransformDirection(Dir(debugs) * speed));
        Vector3 aimDir = (transform.TransformDirection(Dir(debugs)));
        //rb.MovePosition(transform.position + aimDir * speed * Time.deltaTime);
        rb.velocity = new Vector3(aimDir.x * speed, rb.velocity.y, aimDir.z * speed);

        if (dashCooldown > 0 )
        {
            dashCooldown -= Time.fixedDeltaTime;
        }
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
        var dashInput = Input.GetButtonDown("Dash");

        if (dashInput && dashCooldown <= 0)
        {
            isDashing = true;
            dashDir = Dir(debugs);
            StartCoroutine(StopDashing());
        }

        if (isDashing) {
            speed = dashSpeed;
        }
        else{
            speed = baseSpeed;
        }
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashTime);
        Debug.Log("Stopped Dashing");
        isDashing = false;
        dashCooldown = dashCooldownDur;

    }
}
