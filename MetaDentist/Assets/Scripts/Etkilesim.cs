using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Etkilesim : MonoBehaviour
{
    public GameObject cube;
    public GameObject tool04;
    private Vector3 cubeFirstPosition;
    private Vector3 toolFirstPosition;
    private Vector3 hitFirstPosition;
    public Image nokta;
    public float mesafe;
    public float kirmiziFloat;
    public Outline outline;
    private RaycastHit sonHit;

    // Start is called before the first frame update
    void Start()
    {
        this.cubeFirstPosition = cube.transform.position;
        this.toolFirstPosition = tool04.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 ileri = transform.TransformDirection(Vector3.forward);

        //nokta.color = Color.white;

        if (Physics.Raycast(transform.position, ileri, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.blue);
            hitFirstPosition = hit.collider.gameObject.transform.position;

            if (hit.collider.gameObject.CompareTag("HighlightedObject"))
            {
                nokta.color = Color.red;
                hit.collider.gameObject.GetComponent<Outline>().OutlineWidth = 10.0f;
                if (Gamepad.all[0].crossButton.isPressed)
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
                if (Gamepad.all[0].crossButton.isPressed)
                {
                    int scenenum = 1;
                    // SceneManager.LoadScene("Gokberk");
                    LoadScene(scenenum);
                    Debug.Log("Button Tiklandi");

                }
               var colors1 = hit.collider.gameObject.GetComponent<Button> ().colors;
               colors1.normalColor = Color.white;
               hit.collider.gameObject.GetComponent<Button> ().colors = colors1;
            }
            
            
            else if (hit.collider.gameObject.CompareTag("Button1"))
            {   
                nokta.color = Color.red;
                var colors = hit.collider.gameObject.GetComponent<Button> ().colors;
                colors.normalColor = Color.red;
                hit.collider.gameObject.GetComponent<Button> ().colors = colors;
                if (Gamepad.all[0].crossButton.isPressed)
                {
                    int scenenum = 2;
                    // SceneManager.LoadScene("Gokberk");
                    LoadScene(scenenum);
                    Debug.Log("Button T?kland?");

                }
                var colors2 = hit.collider.gameObject.GetComponent<Button> ().colors;
                colors2.normalColor = Color.white;
                hit.collider.gameObject.GetComponent<Button> ().colors = colors2;
            }


            else if (hit.collider.gameObject.CompareTag("Button2"))
            {   nokta.color = Color.red;
                var colors = hit.collider.gameObject.GetComponent<Button> ().colors;
                colors.normalColor = Color.red;
                hit.collider.gameObject.GetComponent<Button> ().colors = colors;
                if (Gamepad.all[0].crossButton.isPressed)
                {
                    int scenenum = 3;
                    // SceneManager.LoadScene("Gokberk");
                    LoadScene(scenenum);
                    Debug.Log("Button T?kland?");

                }
                var colors3 = hit.collider.gameObject.GetComponent<Button> ().colors;
                colors3.normalColor = Color.white;
                hit.collider.gameObject.GetComponent<Button> ().colors = colors3;
            }

            else if (hit.collider.gameObject.CompareTag("DentistTool"))
            {
                nokta.color = Color.red;
                hit.collider.gameObject.GetComponent<Outline>().OutlineWidth = 10.0f;
                Debug.Log("hit name" + hit.collider.name);
                if (Gamepad.all[0].crossButton.isPressed)
                {
                    if (hit.collider.name == "TOOL04")
                    {
                        Debug.Log("seçildi: " + hit.collider.gameObject.name);
                        Debug.Log("Position bilgisi dentist tool" + hit.collider.gameObject.transform.position);
                        Debug.Log("Position bilgisi cube" + cube.transform.position);

                        cube.transform.position = toolFirstPosition;
                        hit.collider.transform.position = cubeFirstPosition;

                        // hit.collider.gameObject.transform.position = this.cubeFirstPosition;
                        hit.collider.gameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
                        // cube.transform.position = this.hitFirstPosition;
                        cube.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                    else if (hit.collider.name == "Cube 1")
                    {
                        Debug.Log("cube 1 carpti");

                        hit.collider.transform.position = cubeFirstPosition;
                        tool04.transform.position = toolFirstPosition;
                    }

                    // cube.SetActive(false);
                }

                sonHit = hit;
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
            catch (NullReferenceException )
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