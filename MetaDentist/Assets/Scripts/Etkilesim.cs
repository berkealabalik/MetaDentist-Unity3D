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

        if (Physics.Raycast(transform.position, ileri, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.blue);
         
            if (hit.collider.gameObject.CompareTag("HighlightedObject"))
            {
                nokta.color = Color.red;
                hit.collider.gameObject.GetComponent<Outline>().OutlineWidth = 10.0f;
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("seçildi: " + hit.collider.gameObject.name);
                }

                sonHit = hit;
            }
            else if (hit.collider.gameObject.CompareTag("Button"))
            {   nokta.color = Color.red;
                //hit.collider.gameObject.GetComponent<Button>().colors.highlightedColor= Color.yellow;

                var colors = hit.collider.gameObject.GetComponent<Button> ().colors;
                colors.normalColor = Color.red;
                hit.collider.gameObject.GetComponent<Button> ().colors = colors;
                if (Input.GetMouseButtonDown(0))
                {
                    int scenenum = 1;
                    // SceneManager.LoadScene("Gokberk");
                    LoadScene(scenenum);
                    Debug.Log("Button T?kland?");

                }
            }
            else if (hit.collider.gameObject.CompareTag("Button1"))
            {nokta.color = Color.red;
            var colors = hit.collider.gameObject.GetComponent<Button> ().colors;
                colors.normalColor = Color.red;
                hit.collider.gameObject.GetComponent<Button> ().colors = colors;
                if (Input.GetMouseButtonDown(0))
                {
                    int scenenum = 2;
                    // SceneManager.LoadScene("Gokberk");
                    LoadScene(scenenum);
                    Debug.Log("Button T?kland?");

                }
            }
            else if (hit.collider.gameObject.CompareTag("Button2"))
            {nokta.color = Color.red;
            var colors = hit.collider.gameObject.GetComponent<Button> ().colors;
                colors.normalColor = Color.red;
                hit.collider.gameObject.GetComponent<Button> ().colors = colors;
                if (Input.GetMouseButtonDown(0))
                {
                    int scenenum = 3;
                    // SceneManager.LoadScene("Gokberk");
                    LoadScene(scenenum);
                    Debug.Log("Button T?kland?");

                }
            }
            try { 
            if (sonHit.collider.gameObject != hit.collider.gameObject)
            {
                sonHit.collider.gameObject.GetComponent<Outline>().OutlineWidth = 0f;
            }
                
            } catch
            {
                Debug.Log("nil");
            }
        }
        else
        {
            try
            {
                sonHit.collider.gameObject.GetComponent<Outline>().OutlineWidth = 0f;
            }
            catch (NullReferenceException e)
            {
                //Bir ?ey yapma
            }
        }
    }
    public void LoadScene(int n)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + n);
    }
}
//hit.distance <= mesafe &&t.distance <= mesafe &&