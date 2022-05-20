using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ChangeCollider : MonoBehaviour
{
  
    private BoxCollider AeratörCollider;
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
        AeratörCollider = AeratorHead.GetComponent<BoxCollider>();


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
                    AeratörCollider.size = new Vector3(AeratörCollider.size.x, AeratörCollider.size.y, AeratörCollider.size.z * 2);
                    pressed = true;
                    kucukucImage.SetActive(false);
                    buyukucImage.SetActive(true);
                    state = HeadState.Big;
                   
                }
                else
                {
                    AeratörCollider.size = new Vector3(AeratörCollider.size.x, AeratörCollider.size.y, AeratörCollider.size.z / 2);
                    pressed = false;
                    state = HeadState.small;
                    kucukucImage.SetActive(true);
                    buyukucImage.SetActive(false);
                }
            }
            
         
        }
    }

  
}


