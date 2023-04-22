using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    private CharacterStats _stats;
    private PlayerManager _playerManager;

    private void Start()
    {
        _playerManager = PlayerManager.playerManagerInstance;
        _stats = GetComponent<CharacterStats>();
    }

    public override void Interact()
    {
        base.Interact();
        CharacterCombat playerCombat = _playerManager.player.GetComponent<CharacterCombat>();
        if (playerCombat != null)
            playerCombat.Attack(_stats);
    }
}
