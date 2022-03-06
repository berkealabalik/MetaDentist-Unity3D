using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Etkilesim : MonoBehaviour
{
    public Image nokta;
    public float mesafe;
    public float kirmiziFloat;
    public Outline outline;
    
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update() 
    {
        RaycastHit hit;
        Vector3 ileri = transform.TransformDirection(Vector3.forward);

        nokta.color = Color.white;
        if (Physics.Raycast(transform.position,ileri,out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.blue);
            Debug.Log(hit.collider.gameObject.name);
            if ( hit.collider.gameObject.tag == "HighlightedObject")
            {
                Debug.Log("hit");
                nokta.color = Color.red;
                //outline.enabled = true;
                //hit.collider.gameObject.GetComponent<Outline>().enabled = true;
                //if (Input.GetMouseButtonDown(0))
                //{
                //   // Debug.Log("burada");
                //}


            }
            //Debug.Log("burada");
        }
    }
}
//hit.distance <= mesafe &&