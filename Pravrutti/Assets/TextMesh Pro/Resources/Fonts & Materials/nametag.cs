using UnityEngine;

public class nametag : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        //get position in pixels
        //get position in game world units
        transform.position = Input.mousePosition;
    }
    
}
