using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alarm : MonoBehaviour
{
    AudioSource audioSource;
    public GameObject enemySpawner;

    private void OnTriggerEnter(Collider other)
    {
        audioSource = GetComponent<AudioSource>();
        if(other.tag == "Player")
        {
            enemySpawner.SetActive(true);
            audioSource.Play();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
