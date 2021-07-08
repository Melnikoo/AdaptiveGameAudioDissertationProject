using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
   
    private Animator anim;
    public KeyCode attack = KeyCode.Mouse0;
    FMODUnity.StudioEventEmitter swordSound;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        swordSound = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if(Input.GetKeyDown(attack))
        {
            Debug.Log("attacked");
            anim.SetTrigger("Attack");
            
        }
    }

    void PlaySound()
    {
        swordSound.Play();
    }
}
