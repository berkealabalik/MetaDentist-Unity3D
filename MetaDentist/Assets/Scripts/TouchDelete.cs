
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchDelete : MonoBehaviour
{
    public float DelayTime;
    public static int MineyeGirmeSayisi = 0;
   
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Curuk")
        {

         //Gamepad.current.SetMotorSpeeds(0.12f,0.12f);   
            //Gamepad.all[0].SetMotorSpeeds(0.12f, 0.12f);
            Destroy(collision.gameObject, DelayTime);
        }

        if(collision.gameObject.tag == "Mine")
        {
            MineyeGirmeSayisi += 1;
        }

       

    }

}

