using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] float cameraSpeed = 0.03f;
    [SerializeField] float zoomSpeed = 10.0f;
    [SerializeField] float rotateSpeed = 1;
    [SerializeField] float maxHeight = 40f;
    [SerializeField] float minHeight = 4f; 

    Vector2 p1;
    Vector2 p2;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
 
        float hsp = transform.position.y * cameraSpeed * Input.GetAxis("Horizontal");
        float vsp = transform.position.y * cameraSpeed * Input.GetAxis("Vertical");
        float scrollSP = Mathf.Log(transform.position.y) * -zoomSpeed * Input.GetAxis("Mouse ScrollWheel");

        if((transform.position.y >= maxHeight) && (scrollSP > 0))
        {
            scrollSP = 0;
        }
        else if((transform.position.y <= minHeight) && (scrollSP < 0))
        {
            scrollSP = 0;
        }

        if((transform.position.y + scrollSP) > maxHeight)
        {
            scrollSP = maxHeight - transform.position.y;
        }
        else if((transform.position.y + scrollSP) < minHeight)
        {
            scrollSP = minHeight - transform.position.y;
        }

        Vector3 verticalMove = new Vector3(0, scrollSP, 0);
        Vector3 lateralMove = hsp * transform.right;
        Vector3 forwardMove = transform.forward;

        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= vsp;

        Vector3 move = verticalMove + lateralMove + forwardMove;

        transform.position += move;

        getCameraRotation();

    }

    void getCameraRotation()
    {

        if(Input.GetMouseButtonDown(2)) //check if the middlemouse button is pressed
        {
            p1 = Input.mousePosition;
        }

        if(Input.GetMouseButton(2)) //check if the middle mouse button is being held down
        {
            p2 = Input.mousePosition;

            float dx = (p2 - p1).x * rotateSpeed;
            float dy = (p2 - p1).y * rotateSpeed;

            transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0)); //y rotation
            transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(-dy,0,0));
            p1 = p2;
        }

    } 

}
