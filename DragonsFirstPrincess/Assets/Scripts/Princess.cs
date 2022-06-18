using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Princess : MonoBehaviour
{
    private bool attached = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    private void OnTriggerExit(Collider other)
    {
        if (!attached && other.CompareTag("Dragon"))
        {
            Collider collider = GetComponent<Collider>();

            Vector3 connectionPoint = collider.bounds.center;
            connectionPoint.y = collider.bounds.max.y;

            Vector3 dragonConnectionPoint = other.bounds.center;
            dragonConnectionPoint.y = other.bounds.min.y;

            transform.position = dragonConnectionPoint;
            
            Rigidbody dragonRb = other.GetComponent<Rigidbody>();

            if (dragonRb)
            {
                AttachTo(dragonRb);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dragon"))
        {
            attached = false;
        }
    }

    private void AttachTo(Rigidbody otherRb)
    {
        if (TryGetComponent(out Rigidbody rb))
        {
            if (!otherRb.gameObject.TryGetComponent(out FixedJoint joint))
            {
                joint = otherRb.gameObject.AddComponent<FixedJoint>();
            }
            joint.breakForce = 10f;

            joint.connectedBody = rb;

            attached = true;
        }
    }
}
