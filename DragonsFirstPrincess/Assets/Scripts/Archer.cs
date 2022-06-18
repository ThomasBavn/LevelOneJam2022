using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    [SerializeField] private float hp;

    [SerializeField] private GameObject target;
    [SerializeField] private GameObject Arrow;

    [SerializeField] private float cooldown;
    [SerializeField] private float pojectileSpeed;
    [SerializeField] private float aboveFactor;
    [SerializeField] private float infrontFactor;
    [SerializeField] private float maxRandom;
    private bool cooledDown = true;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform.position);
        if (cooledDown)
        {
            Shoot();
            StartCoroutine(Cooldown());
        }
    }

    private void Shoot()
    {
        float distance = (target.transform.position - transform.position).magnitude;
        Vector3 targetPosition = target.transform.position + Vector3.up * aboveFactor * distance + transform.forward * infrontFactor * distance;

        Vector3 velocity = (targetPosition - transform.position).normalized * pojectileSpeed;
        velocity += new Vector3(Random.Range(-maxRandom, maxRandom), Random.Range(-maxRandom, maxRandom), Random.Range(-maxRandom, maxRandom));

        GameObject arrowObject = Instantiate(Arrow, transform.position, Quaternion.identity);
        Arrow arrowScript = arrowObject.GetComponent<Arrow>();
        arrowScript.Init(velocity);

    }

    IEnumerator Cooldown()
    {
        cooledDown = false;
        yield return new WaitForSeconds(cooldown);
        cooledDown = true;
    }

    public void Damage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            die();
        }
    }

    private void die()
    {
        Destroy(gameObject);
    }

}