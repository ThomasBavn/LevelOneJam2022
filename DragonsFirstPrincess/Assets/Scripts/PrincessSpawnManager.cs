using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessSpawnManager : MonoBehaviour
{
    private GameObject[] princessSpawns;
    [SerializeField] private GameObject princess;

    // Start is called before the first frame update
    void Start()
    {
        princessSpawns = GameObject.FindGameObjectsWithTag("PrincessSpawn");
        SpawnPrincess();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnPrincess()
    {
        int index = Random.Range(0, princessSpawns.Length);

        GameObject spawnedPrincess = Instantiate(princess);
        spawnedPrincess.transform.position = princessSpawns[index].transform.GetChild(0).position;
     
    }
}
