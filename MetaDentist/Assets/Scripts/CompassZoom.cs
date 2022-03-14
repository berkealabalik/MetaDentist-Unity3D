using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class CompassZoom : MonoBehaviour {



  void Start()
    {
       Awake();

    }

     void Update()
    {
      

        
    }
      void OnGUI()  {
            GUI.Label ( new Rect(10,10,Screen.width ,50),"Lokasyon Compas", Input.compass.rawVector.ToString());
        }

    // Start is called before the first frame update

    public void Awake()
 {
     StartCoroutine(InitializeLocation());
 }
 public IEnumerator InitializeLocation()
 {
     // First, check if user has location service enabled
     if (!Input.location.isEnabledByUser)
     {
         Debug.Log("location disabled by user");
         yield break;
     }
     // enable compass
     Input.compass.enabled = true;
     // start the location service
     Debug.Log("start location service");
     Input.location.Start(10, 0.01f);
     // Wait until service initializes
     int maxSecondsToWaitForLocation = 20;
     while (Input.location.status == LocationServiceStatus.Initializing && maxSecondsToWaitForLocation > 0)
     {
         yield return new WaitForSeconds(1);
         maxSecondsToWaitForLocation--;
     }
     
     // Service didn't initialize in 20 seconds
     if (maxSecondsToWaitForLocation < 1)
     {
         Debug.Log("location service timeout");
         yield break;
     }
     // Connection has failed
     if (Input.location.status == LocationServiceStatus.Failed)
     {
         Debug.Log("unable to determine device location");
         yield break;
     }
     Debug.Log("location service loaded");
     yield break;
 }
}
