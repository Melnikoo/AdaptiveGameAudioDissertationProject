using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public int numberToSpawn;
    public GameObject objectToSpawn;
    void Start()
    {
        for(int i = 0; i < numberToSpawn; i++)
        {
            Instantiate(objectToSpawn, transform.position, Quaternion.identity, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
