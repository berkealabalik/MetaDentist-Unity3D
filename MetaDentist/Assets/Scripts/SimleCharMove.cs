
using UnityEngine;

public class SimleCharMove : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontal, 0, vertical) * (speed * Time.deltaTime));
    }
}