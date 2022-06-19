using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{

    private PointManager pointManager;
    private PrincessSpawnManager princessSpawnManager;
    private GameObject spawnPoint;
    [SerializeField]private float maxSpawnRadius;
    // Start is called before the first frame update
    void Start()
    {
        pointManager = GameObject.FindGameObjectWithTag("PointManager").GetComponent<PointManager>();
        princessSpawnManager = GameObject.FindGameObjectWithTag("PrincessSpawnManager").GetComponent<PrincessSpawnManager>();
        spawnPoint=transform.GetChild(0).gameObject;
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
            other.GetComponent<Princess>().Detach();
            other.tag = "Untagged";
            Destroy(other.GetComponent<Princess>());
            PutInCage(other.gameObject);
        }
      
    }

    private void PutInCage(GameObject princess)
    {
        Vector3 spawnVector = new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));

        princess.transform.position = spawnPoint.transform.position + spawnVector.normalized * Random.Range(0, maxSpawnRadius);
        princess.transform.parent = transform;
    }
}
