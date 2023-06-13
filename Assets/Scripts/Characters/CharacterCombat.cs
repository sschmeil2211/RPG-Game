using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    private bool _inCombat;
    private CharacterStats _opponentStats;
    private CharacterStats _stats;
    [SerializeField] private EnemyType _enemyKind;
    private float _attackCooldown = 0f;
    [SerializeField] private float _attackTime = 10f;
    [SerializeField] private float _combatCooldown = 5f;
    private float _lastAttackTime;

    public float attackDelay = 0.6f;
    public float attackSpeed = 1f;
    public float projectileSpeed = 100;

    public event System.Action OnAttack;

    public EnemyType EnemyKind => _enemyKind;

    public bool InCombat => _inCombat;

    private void Start() => _stats = GetComponent<CharacterStats>();

    private void Update()
    {
        _attackCooldown -= Time.deltaTime;
        if (Time.time - _lastAttackTime > _combatCooldown)
            _inCombat = false;
    }

    public void Attack(CharacterStats targetStats)
    {
        if (_attackCooldown <= 0f)
        {
            _opponentStats = targetStats;
            OnAttack?.Invoke();
            _attackCooldown = 1f / attackSpeed;
            _inCombat = true;
            _lastAttackTime = Time.time;
        }
    }

    public void AttackHitAnimationEvent()
    {
        _opponentStats.TakeDamage(_stats.damage.GetBaseValue());
        if (_opponentStats.Health <= 0)
            _inCombat = false;
    }
}
