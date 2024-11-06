using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public Transform AttackHitbox;

    //Attack
    public float meleeAtkSpd;
    public float meleeCooldown;

    // Update is called once per frame
    void Update()
    {
        var attackInput = Input.GetMouseButtonDown(0);

        if (attackInput && meleeCooldown <= 0f)
        {
            Debug.Log("Attacked");
            anim.SetTrigger("Attack");
            meleeCooldown = meleeAtkSpd;
            AttackHitbox.gameObject.SetActive(true);
        }
        else
        {
            if (meleeCooldown > 0f)
            {
                meleeCooldown -= Time.deltaTime;
            }
        }

    }
}
