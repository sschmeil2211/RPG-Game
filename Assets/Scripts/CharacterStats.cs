using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    private int _health;

    public Stat armor;
    public Stat damage;

    public int Health { 
        set => _health = value; 
        get => _health; 
    }

    private void Awake() => _health = _maxHealth;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            TakeDamage(10);
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetModifiedValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        _health -= damage;
        Debug.Log(transform.name + " recibio " + damage + " damage.");
        if (_health <= 0)
            Die();
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + "murio");
    }
}
