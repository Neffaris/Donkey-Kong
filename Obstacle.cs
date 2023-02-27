using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private bool wasJumpedByPlayer;

   public void OnPlayerJumpsOver()
{
    if(!wasJumpedByPlayer)
    {
        // FindObjectOfType<Game_Manager>().Get_100_Points();
        Game_Manager.Instance.Get_100_Points();
    }
    wasJumpedByPlayer = true;
}
}
