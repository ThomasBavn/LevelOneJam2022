using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody _rb;
    private float damage;
    private float spawnTime;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(_rb.velocity);

        if (Time.time - spawnTime > 10)
        {
            Destroy(gameObject);
        }
    }

    public void Init(Vector3 velocity,float damage)
    {
        this.damage = damage;
        _rb.AddForce(velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dragon"))
        {
            FindObjectOfType<PlayerController>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
