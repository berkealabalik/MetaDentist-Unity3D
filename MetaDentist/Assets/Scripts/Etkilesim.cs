using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Etkilesim : MonoBehaviour
{
    public Image nokta;
    public float mesafe;
    public float kirmiziFloat;
    public Outline outline;
    private RaycastHit sonHit;
    
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

            if ( hit.collider.gameObject.CompareTag("HighlightedObject"))
            {
                Debug.Log("hit: " +hit.collider.gameObject.name);
                try
                {
                    if (sonHit.collider.gameObject.name != hit.collider.gameObject.name)
                    {
                        
                        sonHit.collider.gameObject.GetComponent<Outline>().OutlineWidth = 0f;
                        sonHit = hit;

                    }
                   
                }
                catch (NullReferenceException e)
                {
                    sonHit = hit;
                }
                
                nokta.color = Color.red;
                
                hit.collider.gameObject.GetComponent<Outline>().OutlineWidth = 10.0f;
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("seçildi: " + hit.collider.gameObject.name);
                }


            }
            else if(hit.collider.gameObject.CompareTag("Button"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    // SceneManager.LoadScene("Gokberk");
                    LoadScene();
                    Debug.Log("Button Týklandý");
                   
                }
            }
            //Debug.Log("burada");
        }
        else
        {
            try
            {
                sonHit.collider.gameObject.GetComponent<Outline>().OutlineWidth = 0f;
            }
            catch(NullReferenceException e)
            {
                //Bir þey yapma
            }  
        }
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
//hit.distance <= mesafe &&