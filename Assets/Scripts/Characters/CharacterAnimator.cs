using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    protected Animator animator;
    protected CharacterCombat characterCombat;
    protected List<AnimationClip> currentAttackAnimationSet;

    private float _animSmoothTime = 0.1f;
    private NavMeshAgent _agent;

    public AnimatorOverrideController overrideController;
    public AnimationClip repaceableAttackAnimation;
    public List<AnimationClip> defaultAttackAnimationSet;

    protected virtual void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        characterCombat = GetComponent<CharacterCombat>();
        if(overrideController == null)
            overrideController = new(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;
        currentAttackAnimationSet = defaultAttackAnimationSet;
        characterCombat.OnAttack += OnAttack;
    }

    protected virtual void Update()
    {
        float speedPercent = _agent.velocity.magnitude / _agent.speed;
        animator.SetFloat("speedPercent", speedPercent, _animSmoothTime, Time.deltaTime);
        animator.SetBool("inCombat", characterCombat.InCombat);
    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger("attack");
        int attckIndex = Random.Range(0, currentAttackAnimationSet.Count);
        overrideController[repaceableAttackAnimation.name] = currentAttackAnimationSet[attckIndex];
    }
}
