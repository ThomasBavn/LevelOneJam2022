using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private bool canBreak = false;
    private float connectTime;
    private Dictionary<GameObject, FixedJoint> connectedBricks = new Dictionary<GameObject, FixedJoint>();

    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!canBreak && !connectedBricks.ContainsKey(collision.gameObject) && collision.collider.CompareTag("Brick"))
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            connectTime = Time.time;
            joint.connectedBody = collision.gameObject.GetComponent<Rigidbody>();
            connectedBricks.Add(collision.gameObject, joint);
            joint.breakForce = 1000;
            joint.breakTorque = 1000;
        }
    }

    void Update()
    {
    }
}
