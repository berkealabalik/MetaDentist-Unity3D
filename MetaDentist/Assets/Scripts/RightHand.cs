using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RightHand : MonoBehaviour
{
    public GameObject cube;

    void Start()
    {
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            Debug.Log(Gamepad.all[i].name);
        }

        //cube = GameObject.Find("Cube");
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.all.Count > 0)
        {
            if (Gamepad.all[0].rightStick.left.isPressed)
            {
                cube.transform.position += Vector3.left * Time.deltaTime * 5f;
            }

            if (Gamepad.all[0].rightStick.right.isPressed)
            {
                cube.transform.position += Vector3.right * Time.deltaTime * 5f;
            }

            if (Gamepad.all[0].rightStick.up.isPressed)
            {
                cube.transform.position += Vector3.up * Time.deltaTime * 5f;
            }

            if (Gamepad.all[0].rightStick.down.isPressed)
            {
                cube.transform.position += Vector3.down * Time.deltaTime * 5f;
            }


        }


    }
}

