using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Invector.vCharacterController
{
    public class musicSystemControl : MonoBehaviour
    {
        // Start is called before the first frame update
        FMODUnity.StudioEventEmitter emitter;
        GameObject player;
        vThirdPersonController playerController;
        public BulletHit sword;
        int numberOfEnemies;
        int temp;
        public float enemyPresenceDistance;

        void Start()
        {
            
            emitter = GetComponent<FMODUnity.StudioEventEmitter>();
            player = GameObject.FindGameObjectWithTag("Player");
            playerController = player.GetComponent<vThirdPersonController>();
        }

        // Update is called once per frame
        void Update()
        {
            temp = 0;
            emitter.SetParameter("Health", playerController.currentHP);
            emitter.SetParameter("Stamina", playerController.currentStamina);

            GameObject[] list = GameObject.FindGameObjectsWithTag("enemy");
            
            foreach (GameObject enemy in list)
            {
                if ((enemy.transform.position - player.transform.position).magnitude <= enemyPresenceDistance)
                {
                    temp += 1;
                }
            }

            numberOfEnemies = temp;
            Debug.Log(numberOfEnemies);

            emitter.SetParameter("Enemies", numberOfEnemies);

            if(numberOfEnemies == 0)
            {
                sword.DamagedAnEnemy = false;
            }

            emitter.SetParameter("currentHeight", playerController.height.position.y);

            if(sword.DamagedAnEnemy)
            {
                emitter.SetParameter("PlayerAttacked", 1f);
            }

        }
    }
}