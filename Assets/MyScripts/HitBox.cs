using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Invector.vCharacterController
{
    public class HitBox : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            
            if (other.GetComponent<vThirdPersonController>() != null)
            {
                
                other.GetComponent<vThirdPersonController>().Damaged(1);
            }
        }
        
    }
}
