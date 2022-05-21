
using UnityEngine;

public class TouchDelete : MonoBehaviour
{
    public static int DeletedDecayNumber;
    public static int MineEntered;
    public float DelayTime;
 
    void Update()
    {
       
    }
  
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Curuk"))
        {
            DeletedDecayNumber = DeletedDecayNumber + 1;
            Debug.Log("IPEK " + DeletedDecayNumber);
            Destroy(collision.gameObject, DelayTime);
        }
        else if (collision.gameObject.CompareTag("mine"))
        {
            MineEntered += 1;
            Debug.Log("Ipek Mineye Girdi kafa göz " + MineEntered);
        } 
        
    }
}

