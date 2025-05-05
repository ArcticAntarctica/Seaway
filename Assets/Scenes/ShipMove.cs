using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipMove : MonoBehaviour
{
    public Camera cam;
    public Collider planecollider;
    RaycastHit hit;
    Ray ray;

    int MovementSpeed = 5;
    int RotationSpeed = 400;

    void FixedUpdate()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        float VerticalInput = Input.GetAxis("Vertical");
        float HorizontalInput = Input.GetAxis("Horizontal");

        Vector3 MovementDirection = new Vector3(HorizontalInput, 0, VerticalInput);
        MovementDirection.Normalize();

        transform.Translate(MovementDirection * Time.deltaTime * MovementSpeed, Space.World);

        if (MovementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(MovementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, RotationSpeed * Time.deltaTime);
        }
    }

    void Update()
    {
        /*ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            //Cursor.lockState = CursorLockMode.Confined;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Water")))
            if (hit.collider == planecollider)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(hit.point.x, -0.254f, hit.point.z), Time.deltaTime * 5);
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            }
        }*/
    }
}
