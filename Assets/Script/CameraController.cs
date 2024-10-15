using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 myLook;
    float lookSpeed = 400f;
    public Camera myCam;
    public float camLock = 90f;
    float onStartTimer;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        myLook = transform.localEulerAngles;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onStartTimer += Time.deltaTime;
        myLook += DeltaLook() * lookSpeed * Time.deltaTime;
        myLook.y = Mathf.Clamp(myLook.y, -camLock, camLock);
        transform.rotation = Quaternion.Euler(0f, myLook.x, 0f);
        myCam.transform.rotation = Quaternion.Euler(-myLook.y, myLook.x, 0f);

        Debug.DrawRay(myCam.transform.position, myCam.transform.forward * 10f, Color.black);
    }

    Vector3 DeltaLook()
    {
        Vector3 dLook;
        float rotY = Input.GetAxisRaw("Mouse Y");
        float rotX = Input.GetAxisRaw("Mouse X");
        dLook = new Vector3 (rotX, rotY, 0);

        if (dLook != Vector3.zero) { Debug.Log("delta look " + dLook); }

        if (onStartTimer < 1f)
        {
            dLook = Vector3.ClampMagnitude(dLook, onStartTimer);
        }

        return dLook;
    }
}
