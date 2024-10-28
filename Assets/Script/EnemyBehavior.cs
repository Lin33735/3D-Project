using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBehavior : MonoBehaviour
{
    public int curHP;

    public UnityEvent<int> Damaged;
    // Start is called before the first frame update
    void Start()
    {
        
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
