using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{   public float time = 5;
    // Start is called before the first frame update
     IEnumerator Start ()
     {
         yield return new WaitForSeconds(time);
         Destroy(gameObject);
     }

    // Update is called once per frame
    void Update()
    {
        
    }
}


