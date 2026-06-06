using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MenuItem", fileName = "New Item")]
public class MenuItem : ScriptableObject
{
    public string menuItemName;
    public FoodData food1;
    public FoodData food2;
    public FoodData food3;
}
