using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Invector.vCharacterController
{


    public class SimpleMusicSystemControl : MonoBehaviour
    {
        vThirdPersonController playerController;
        GameObject player;
        public GameObject music1;
        public GameObject music2;
        FMODUnity.StudioEventEmitter emitter1;
        FMODUnity.StudioEventEmitter emitter2;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            emitter1 = music1.GetComponent<FMODUnity.StudioEventEmitter>();
            emitter2 = music2.GetComponent<FMODUnity.StudioEventEmitter>();
            playerController = player.GetComponent<vThirdPersonController>();
        }

        // Update is called once per frame
        void Update()
        {
            if(playerController.height.position.y <= 7 && music1.activeSelf)
            {
                music1.SetActive(false);
                music2.SetActive(true);
            }
        }
    }
}