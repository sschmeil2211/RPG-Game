using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    private CharacterStats _stats;
    private float _attackCooldown = 0f;

    public float attackDelay = 0.6f;
    public float attackSpeed = 1f;

    public event System.Action OnAttack;

    private void Start() => _stats = GetComponent<CharacterStats>();

    private void Update() => _attackCooldown -= Time.deltaTime;

    public void Attack(CharacterStats targetStats)
    {
        if(_attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));
            OnAttack?.Invoke();
            _attackCooldown = 1f / attackSpeed;
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(_stats.damage.GetModifiedValue());
    }
}
