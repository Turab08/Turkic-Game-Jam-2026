using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Food", fileName = "New Food")]
public class FoodData : ScriptableObject
{
    public string foodName;
    public Sprite foodImage;
}
