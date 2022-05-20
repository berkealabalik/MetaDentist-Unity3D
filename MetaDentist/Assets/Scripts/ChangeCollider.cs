using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ChangeCollider : MonoBehaviour
{
  
    private BoxCollider Aerat�rCollider;
    private bool pressed = false;
    public GameObject AeratorHead;
    public GameObject buyukucImage;
    public GameObject kucukucImage;
   
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
        //heads[HeadState.Big] = Bighead;
        //heads[HeadState.small] = smallHead;
        state = HeadState.small;
        Aerat�rCollider = AeratorHead.GetComponent<BoxCollider>();


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
                    Aerat�rCollider.size = new Vector3(Aerat�rCollider.size.x, Aerat�rCollider.size.y, Aerat�rCollider.size.z * 2);
                    pressed = true;
                    kucukucImage.SetActive(false);
                    buyukucImage.SetActive(true);
                    state = HeadState.Big;
                   
                }
                else
                {
                    Aerat�rCollider.size = new Vector3(Aerat�rCollider.size.x, Aerat�rCollider.size.y, Aerat�rCollider.size.z / 2);
                    pressed = false;
                    state = HeadState.small;
                    kucukucImage.SetActive(true);
                    buyukucImage.SetActive(false);
                }
            }
            
         
        }
    }

  
}


