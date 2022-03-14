/* 
 * author : jiankaiwang
 * description : The script provides you with basic operations of first personal control.
 * platform : Unity
 * date : 2017/12
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public static CharacterController Instance;

    public Rigidbody Rigidbody;
    public float speed = 10.0f;
    private float translation;
    private float straffe;

    bool locked;

    // Use this for initialization
    void Start()
    {
        Instance = this;

        // turn off the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ChangeLockState(bool state)
    {
        locked = state;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Input.GetAxis() is used to get the user's input
        // You can furthor set it on Unity. (Edit, Project Settings, Input)
        translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        straffe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        //Rigidbody.AddForce(translation * transform.forward);
        //Rigidbody.AddForce(straffe * transform.right);

        if (!locked)
            transform.Translate(new Vector3(straffe, 0, translation));

        if (Input.GetKeyDown("escape"))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                // turn on the cursor
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                // turn off the cursor
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}