
using UnityEngine;

public class TouchDelete : MonoBehaviour
{
    public float DelayTime;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Curuk"))
        {
            Destroy(collision.gameObject, DelayTime);
        }
       

    }

}

