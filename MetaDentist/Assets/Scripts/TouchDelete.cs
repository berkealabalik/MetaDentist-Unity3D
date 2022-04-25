
using UnityEngine;

public class TouchDelete : MonoBehaviour
{
    public float DelayTime;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Curuk")
        {
            Destroy(collision.gameObject, DelayTime);
        }
       

    }

}

