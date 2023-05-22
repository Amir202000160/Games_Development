using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ATTACK : MonoBehaviour
{
    public move charMove;
   
    public void PlayerAttack()
    {
        //Debug.Log("");
        charMove.DoAttack();

    }
    public void PlayerDamage()
    {
        transform.GetComponentInParent<enemy_control>().DamagePlayer();

        
    }
}
