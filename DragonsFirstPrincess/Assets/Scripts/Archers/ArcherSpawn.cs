using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherSpawn : MonoBehaviour
{
    public GameObject archer { get; set; }
    private Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.GetChild(0).transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetArcher(GameObject archer, GameObject player)
    {
        archer.GetComponent<Archer>().target = player;
        this.archer = archer;
        archer.transform.position = spawnPosition;
    }
}
