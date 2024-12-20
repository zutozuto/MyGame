
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;
    
    [SerializeField]private float parallaxSpeed;
    private float xPosition;
    private float length;
    void Start()
    {
        cam = GameObject.Find("Virtual Camera");
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMove = cam.transform.position.x * (1 - parallaxSpeed);
        float distanceToMove = cam.transform.position.x * parallaxSpeed;
        
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

        if (distanceMove > length + xPosition)
        {
            xPosition += length;
        }
        else if (distanceMove < xPosition - length)
        {
            xPosition -= length;
        }
    }
}
