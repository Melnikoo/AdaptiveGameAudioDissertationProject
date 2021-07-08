using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckForVictory : MonoBehaviour
{
    public GameObject victoryUI;
    FMOD.Studio.Bus MasterBus;
    // Start is called before the first frame update
    void Start()
    {
        MasterBus = FMODUnity.RuntimeManager.GetBus("Bus:/");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            victoryUI.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
