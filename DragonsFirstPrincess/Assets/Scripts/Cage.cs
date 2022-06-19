using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{

    private PointManager pointManager;
    private PrincessSpawnManager princessSpawnManager;
    // Start is called before the first frame update
    void Start()
    {
        pointManager = GameObject.FindGameObjectWithTag("PointManager").GetComponent<PointManager>();
        princessSpawnManager = GameObject.FindGameObjectWithTag("PrincessSpawnManager").GetComponent<PrincessSpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Princess"))
        {
            pointManager.AddPoints(1);
            princessSpawnManager.SpawnPrincess();
            Destroy(other.gameObject);
        }
      
    }
}
