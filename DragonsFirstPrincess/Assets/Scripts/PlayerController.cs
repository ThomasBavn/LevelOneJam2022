using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementStiffness = 1;
    [SerializeField] float movementDamping = 1;
    [SerializeField] float movementSpeed = 1;
    [SerializeField] float cameraFollowSpeed = 1;
    private float prevMovementStiffness;
    private float prevMovementDamping;

    private Vector3 desiredDirection;
    private Vector3 currentDirection;
    private Vector3 currentVelocity;
    private SpringDamper damper;

    private Vector3 initialCamOffset;


    void Start()
    {
        initialCamOffset = Camera.main.transform.position - transform.position;
        prevMovementStiffness = movementStiffness;
        prevMovementDamping = movementDamping;
        damper = new SpringDamper(movementStiffness, movementDamping);
    }

    // Update is called once per frame
    void Update()
    {
        ManageInputs();
        UpdateValues();

        //transform.LookAt(desiredDirection, Vector3.up);
        //transform.position += desiredDirection;

        Camera.main.transform.position += desiredDirection * movementSpeed;
        Camera.main.transform.LookAt(Camera.main.transform.position + currentDirection * cameraFollowSpeed, Vector3.up);
        transform.position = Camera.main.transform.TransformPoint(-initialCamOffset);
        float mouseCenterOffsetX = 0.5f - Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
        float mouseCenterOffsetY = 0.5f - Camera.main.ScreenToViewportPoint(Input.mousePosition).y;
        Quaternion roll = Quaternion.AngleAxis(mouseCenterOffsetX * 90, transform.forward);
        Quaternion pitch = Quaternion.AngleAxis(mouseCenterOffsetY * 90, Camera.main.transform.right);
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, roll * Vector3.up) * pitch;
    }

    private void UpdateValues()
    {
        if (movementStiffness!= prevMovementStiffness)
        {
            damper.Stiffness = movementStiffness;
            prevMovementStiffness = movementStiffness;
        }
        if (movementDamping != prevMovementDamping)
        {
            damper.Damping = movementDamping;
            prevMovementDamping = movementDamping;
        }

        damper.Dampen(currentDirection, currentVelocity, desiredDirection, Time.deltaTime, out currentDirection, out currentVelocity);

    }

    private void ManageInputs()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
        desiredDirection = (Camera.main.ScreenToWorldPoint(mousePos) - Camera.main.transform.position).normalized;
    }
}
