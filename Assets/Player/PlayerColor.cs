using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    [SerializeField] Color[] colors = new Color[4]; 
    [SerializeField] private SpriteRenderer torsoSprite;


    public void SetPlayerColor(int playerIndex){
        if(playerIndex < 0) return;
        torsoSprite.color = colors[playerIndex];
    }

    

}
