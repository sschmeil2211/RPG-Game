using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    private Dictionary<EquipmentSO, List<AnimationClip>> _weaponAnimationsDictionary;

    public List<WeaponAnimations> weaponAnimations;

    protected override void Start()
    {
        base.Start();
        EquipmentManager.equipmentManagerInstance.onEquipmentChanged += OnEquipmentChanged;
        _weaponAnimationsDictionary = new();
        foreach (WeaponAnimations weaponAnim in weaponAnimations)
            _weaponAnimationsDictionary.Add(weaponAnim.weapon, weaponAnim.clips);
    }

    private void OnEquipmentChanged(EquipmentSO newEquip, EquipmentSO oldEquip)
    {
        if (newEquip != null && newEquip.equipmentType == EquipmentType.WEAPON)
        {
            animator.SetLayerWeight(1, 1);
            if(_weaponAnimationsDictionary.ContainsKey(newEquip))
                currentAttackAnimationSet = _weaponAnimationsDictionary[newEquip];
        }
        else if (newEquip == null && oldEquip != null && oldEquip.equipmentType == EquipmentType.WEAPON)
        {
            animator.SetLayerWeight(1, 0);
            currentAttackAnimationSet = defaultAttackAnimationSet; 
        }
        if (newEquip != null && newEquip.equipmentType == EquipmentType.SHIELD)
            animator.SetLayerWeight(2, 1);
        else if(newEquip == null && oldEquip != null && oldEquip.equipmentType == EquipmentType.SHIELD)
            animator.SetLayerWeight(2, 0);
    }

    [System.Serializable]
    public struct WeaponAnimations
    {
        public EquipmentSO weapon;
        public List<AnimationClip> clips;
    }
}
