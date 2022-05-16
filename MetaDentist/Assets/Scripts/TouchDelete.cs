
using UnityEngine;

public class TouchDelete : MonoBehaviour
{
    public float DelayTime;
    void Update()
    {
      
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.bounds);
        if(collision.gameObject.CompareTag("Curuk"))
        {
            
            Destroy(collision.gameObject, DelayTime);
        }
  
    }

}

