using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager playerManagerInstance;

    private void Awake() => playerManagerInstance = this;
    #endregion

    public GameObject player;
}
