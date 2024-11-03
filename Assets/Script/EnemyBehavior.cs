using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject Player;
    public float BaseSpeed = 5f;
    public float rotationSpeed = 5f;

    [SerializeField] private float speed;
    private float distance;
    private float chaseDis = 30f;
    [SerializeField]private float chargeAtPlayerCD;
    private Vector3 direction;
    private Vector3 playerPosition;
    private Quaternion targetRotation;

    private int maxHP = 100;
    public int curHP;

    public UnityEvent<int> Damaged;

    private void Awake()
    {
        curHP = maxHP;
        speed = BaseSpeed;
    }

    void Start()
    {
        
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, Player.transform.position);

    }

    void FixedUpdate()
    {
        if (curHP <= 0)
        {
            Die();
        }

        if (distance < chaseDis)
        {
            speed = BaseSpeed;
            chargeAtPlayerCD = 0f;

            MoveTowardsPlayer();
        }
        else
        {
            chargeAtPlayerCD++;
        } 

        if (chargeAtPlayerCD > 50f)
        {
            Charge();
        }
        if (chargeAtPlayerCD == 50f)
        {
            GetPlayerPos();
        }
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

    void MoveTowardsPlayer()
    {
        direction = Player.transform.position - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        targetRotation = Quaternion.Euler(0, angle, 0);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(this.transform.position, Player.transform.position, speed * Time.deltaTime);
    }

    public void Damage(int damage)
    {
        curHP -= damage;
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

    void Charge()
    {
        speed = 25f;

        Vector3 playerPositionXZ = new Vector3 (playerPosition.x, (this).transform.position.y, playerPosition.z);

        transform.position = Vector3.MoveTowards(this.transform.position, playerPositionXZ, speed * Time.deltaTime);
        Debug.Log("charging");
    }

    void GetPlayerPos()
    {
        playerPosition = Player.transform.position;
    }
}
