using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Invector.vCharacterController
{
    public class BulletHit : MonoBehaviour
    {
        public float dmgRate;
        private Animator anim;
        private float timer;
        private int _playerAttackStateHash;
        AnimatorStateInfo info;
        public bool DamagedAnEnemy = false;


        void Start()
        {
            anim = GetComponentInParent<Animator>();
            _playerAttackStateHash = Animator.StringToHash("Attack");
    }
        private void OnTriggerEnter(Collider other)
        {

            if (other.GetComponentInParent<MyAI>() != null && info.tagHash == _playerAttackStateHash)
            {
                if (timer >= dmgRate)
                {
                
                    other.GetComponentInParent<MyAI>().Damaged(1);
                    DamagedAnEnemy = true;
                    timer = 0f;
                }
                
            }
            else if(other.GetComponentInParent<vThirdPersonController>() != null && info.tagHash == _playerAttackStateHash)
            {
                if (timer >= dmgRate)
                {
                    other.GetComponentInParent<vThirdPersonController>().Damaged(1);
                    timer = 0f;
                }
                    
            }

            //Destroy(gameObject);
        }

        private void Update()
        {
            timer += Time.deltaTime;
            info = anim.GetCurrentAnimatorStateInfo(0);
            
        }
    }
}
