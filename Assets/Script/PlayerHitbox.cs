using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public Transform Player;

    void Update()
    {
        Vector3 targetPosition = Player.position;
        transform.position = targetPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("You Lose");
            PlayerDead();
        }
    }

    private void PlayerDead()
    {
        Player.gameObject.SetActive(false);
    }
}
