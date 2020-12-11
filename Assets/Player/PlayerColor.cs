using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    [SerializeField] Color[] colors = new Color[4]; 
    [SerializeField] private SpriteRenderer[] sprites;


    public void SetPlayerColor(int playerIndex){
        if(playerIndex < 0) return;
        foreach(var sprite in sprites){
            sprite.color = colors[playerIndex];
        }
    }

    

}
