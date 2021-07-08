using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Invector.vCharacterController
{


    public class HP_Potions : MonoBehaviour
    {
        Text txt;

        // Start is called before the first frame update
        private int amount;
        void Start()
        {
            txt = GetComponentInChildren<Text>();
            amount = 2;
        }

        void Update()
        {
            txt.text = amount.ToString();
        }
        public void usePotion()
        {
            if (amount > 0)
            {
                amount -= 1;
                GameObject.FindGameObjectWithTag("Player").GetComponent<vThirdPersonController>().Damaged(-2);
            }
            
        }
    }
}