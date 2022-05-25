
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadHandControl : MonoBehaviour
{
    public float speed;
    public GameObject Charachter;

    void Start()
    {
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            Debug.Log(Gamepad.all[i].name);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.all.Count > 0)
        {
            if (Gamepad.all[0].rightStick.left.isPressed)
            {
                Charachter.transform.position += Vector3.left * Time.deltaTime * speed;
            }

            if (Gamepad.all[0].rightStick.right.isPressed)
            {
                Charachter.transform.position += Vector3.right * Time.deltaTime * speed;
            }

            if (Gamepad.all[0].rightStick.up.isPressed)
            {
                Charachter.transform.position += Vector3.forward * Time.deltaTime * speed;
            }

            if (Gamepad.all[0].rightStick.down.isPressed)
            {
                Charachter.transform.position += Vector3.back * Time.deltaTime * speed;
            }
            if (Gamepad.all[0].rightTrigger.isPressed)
            {
                Charachter.transform.position += Vector3.down * Time.deltaTime * speed;
            }

            if (Gamepad.all[0].rightShoulder.isPressed)
            {
                Charachter.transform.position += Vector3.up * Time.deltaTime * speed;
            }


            if (Gamepad.all[0].leftStick.left.isPressed)
            {
                //Charachter.transform.rotation = 
            }


        }


    }
}

