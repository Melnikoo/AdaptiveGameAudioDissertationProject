using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public GameObject door;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = door.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        anim.SetTrigger("Open");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
