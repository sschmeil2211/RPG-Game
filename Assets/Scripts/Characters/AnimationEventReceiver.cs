using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
    public CharacterCombat characterCombat;

    public void AttackHitEvent() => characterCombat.AttackHitAnimationEvent();
}
