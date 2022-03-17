using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharControl : MonoBehaviour
{
    //public GameObject Charachter;

    void Start()
    {
        //for (int i = 0; i < Gamepad.all.Count; i++)
       // {
          //  Debug.Log(Gamepad.all[i].name);
        //}

        //cube = GameObject.Find("Cube");
    }

    // Update is called once per frame
    void Update()
    {
       
        if(Input.GetKeyDown(KeyCode.A))
        {
            Vector3 position = this.transform.position;
            position.x--;
            this.transform.position = position;
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            Vector3 position = this.transform.position;
            position.x++;
            this.transform.position = position;
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            Vector3 position = this.transform.position;
            position.y++;
            this.transform.position = position;
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            Vector3 position = this.transform.position;
            position.y--;
            this.transform.position = position;
        }


       
     }


    
}

