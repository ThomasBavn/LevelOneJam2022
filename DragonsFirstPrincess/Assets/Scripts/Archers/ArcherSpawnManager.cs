using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherSpawnManager : MonoBehaviour
{

    [SerializeField] private float cooldown;
    [SerializeField] private float cooldownIncrements;
    [SerializeField] private float minCooldown;
    private bool cooledDown=true;
    [SerializeField] GameObject archer;
    GameObject player;

    private  GameObject[] archerSpawns;
    // Start is called before the first frame update
    void Start()
    {
        archerSpawns = GameObject.FindGameObjectsWithTag("ArcherSpawn");
        player = GameObject.FindGameObjectWithTag("Dragon");
    }

    // Update is called once per frame
    void Update()
    {
        if (cooledDown)
        {
            AddArcher();
            StartCoroutine(Cooldown());
        }
    }

     IEnumerator Cooldown()
    {
        cooledDown = false;
        yield return new WaitForSeconds(cooldown);
        cooldown -= cooldownIncrements;
        if (cooldown < minCooldown) cooldown = minCooldown;
        cooledDown = true;
    }

    private void AddArcher()
    {
        int index = Random.Range(0, archerSpawns.Length);


        for (int i = 0; i<archerSpawns.Length; i++)
        {
            if (index==archerSpawns.Length)
            {
                index = 0;
            }
            ArcherSpawn archerSpawn = archerSpawns[index].GetComponent<ArcherSpawn>();
            if (archerSpawn.archer == null)
            {
                GameObject spawnedArcher = Instantiate(archer);
                archerSpawn.SetArcher(spawnedArcher, player);
                return;
            }
            index++;
        }
        return;
    }
}
