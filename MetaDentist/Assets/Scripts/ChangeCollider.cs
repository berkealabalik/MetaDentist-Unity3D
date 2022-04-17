using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ChangeCollider : MonoBehaviour
{
  
    private BoxCollider Aerat�rCollider;
    private bool pressed = false;

    public GUIStyle aeirat�rStilib�y�k;
    public GUIStyle aeirat�rStilik���k;
    private float timer = 0;
    private float duration = 0.15f;
    // Start is called before the first frame update

    public enum HeadState { Big, small}
    public delegate void HeadMethod();
    private HeadState state;
    private Dictionary<HeadState, HeadMethod> heads;


    void Start()
    {
        heads = new Dictionary<HeadState, HeadMethod>();
        heads[HeadState.Big] = Bighead;
        heads[HeadState.small] = smallHead;
        state = HeadState.small;

        Aerat�rCollider = GetComponent<BoxCollider>();


    }

    // Update is called once per frame
    void Update()
    {
        if(Gamepad.all[0].triangleButton.isPressed) {

            timer += Time.deltaTime;
            if (timer >= duration)
            {
                timer = 0;
                if (pressed == false)
                {
                    Aerat�rCollider.size = new Vector3(Aerat�rCollider.size.x, Aerat�rCollider.size.y * 2, Aerat�rCollider.size.z * 2);
                    pressed = true;
                    state = HeadState.Big;
                   
                }
                else
                {
                    Aerat�rCollider.size = new Vector3(Aerat�rCollider.size.x, Aerat�rCollider.size.y / 2, Aerat�rCollider.size.z / 2);
                    pressed = false;
                    state = HeadState.small;
                }
            }
            
         
        }
    }


    void OnGUI()
    {
        heads[state]();
    }

    public void smallHead()
    {
             GUI.Label(new Rect(0, 0, Screen.width, 50), "0.5MM", aeirat�rStilik���k);
        // main menu gui goes here
    }

    public void Bighead()
    {
        GUI.Label(new Rect(0, 0, Screen.width, 50), "0.5MM", aeirat�rStilib�y�k);
    }



}


