using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    private CharacterCombat _playerCombat;
    private CharacterStats _stats;
    private PlayerManager _playerManager;

    private void Start()
    {
        _playerManager = PlayerManager.playerManagerInstance;
        _stats = GetComponent<CharacterStats>();
        _playerCombat = _playerManager.player.GetComponent<CharacterCombat>();
    }

    public override void Interact()
    {
        base.Interact();
        if(_playerCombat != null)
            _playerCombat.Attack(_stats);
    }
}
