using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    private float hitboxDur = 0.1f;
    // Start is called before the first frame update
    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            StartCoroutine(DisableHitBox());
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<EnemyBehavior>(out var curHP))
        {
            curHP.Damage(damage:50);
            Debug.Log("Enemy Hit");
        }
    }

    private IEnumerator DisableHitBox()
    {
        yield return new WaitForSeconds(hitboxDur);
        gameObject.SetActive(false);
        Debug.Log("AttackEnd");
    }
}
