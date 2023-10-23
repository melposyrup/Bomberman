using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// EXAMPLE
// in Player1P Death(), call the public function in UIManager.instance like following:
//      UIManager.instance.PlayerDeath(playerNumber);
// then all the objects that subscribe this Action will respond.
// example for subscription:
// in xxxManager.cs Start(),
//      UIManager.instance.OnPlayerDeath += ManagerFuncName;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Action<int> OnPlayerDeath;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }

    public void PlayerDeath(int playerNumber) 
    { OnPlayerDeath?.Invoke(playerNumber); }

}
