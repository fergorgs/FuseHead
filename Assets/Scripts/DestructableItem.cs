using UnityEngine;

[CreateAssetMenu(fileName = "Destructable", menuName = "ScriptableObjects/DestructableItem")]
public class DestructableItem : ScriptableObject
{
    public float blastResistence = 10;
    public Sprite fullLifeSprite, middleLifeSprite, lowLifeSprite;
}
