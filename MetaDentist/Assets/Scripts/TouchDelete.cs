
using UnityEngine;

public class TouchDelete : MonoBehaviour
{
    public int DeletedDecayNumber;
    public static int MineEntered=0;
    public float DelayTime;
    void Update()
    {
      
    }

    private void Awake()
    {
        DeletedDecayNumber = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Curuk"))
        {
            DeletedDecayNumber = DeletedDecayNumber + 1;
            Debug.Log("IPEK " + DeletedDecayNumber);
            Destroy(collision.gameObject, DelayTime);
        }
        
    }
}

