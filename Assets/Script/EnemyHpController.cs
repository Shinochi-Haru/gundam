using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpController : MonoBehaviour
{
    [SerializeField] int _hp = 0;
    [SerializeField] int _maxHp = 0;


    public int MaxHp
    {
        get { return _maxHp; }
        set { _maxHp = value; }
    }

    public int CurrentHp
    {
        get { return _hp; }
        private set { _hp = value; }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Damage(int damage)
    {
        CurrentHp -= damage;
        if (CurrentHp <= 0)
        {
            CurrentHp = 0;
            //animator.SetTrigger("Death");
            Destroy(gameObject, 1f);
        }
        //UpdateUI(CurrentHp);
    }
}
