using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject Player;
    public float speed = 5f;
    public float rotationSpeed = 5f;
    private float distance;
    private float chaseDis = 30f;

    private int maxHP = 100;
    public int curHP;

    public UnityEvent<int> Damaged;

    void Start()
    {
        
    }

    private void Awake()
    {
        curHP = maxHP;
    }

    public int HP
    {
        get => curHP;
        private set
        {
            var isDamaged = value > curHP;
            if (isDamaged)
            {
                Damaged?.Invoke(curHP);
            }
        }
    }

    public void Damage(int damage)
    {
        curHP -= damage;
    }

    void Update()
    {
        if (curHP <= 0)
        {
            Die();
        }

        distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance < chaseDis)
        {
            Vector3 direction = Player.transform.position - transform.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(this.transform.position, Player.transform.position, speed * Time.deltaTime);
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
