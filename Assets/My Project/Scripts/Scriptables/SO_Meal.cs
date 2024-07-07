using UnityEngine;


[CreateAssetMenu(fileName = "SO_Meal", menuName = "SO/Meal", order = 0)]
public class SO_Meal : ScriptableObject
{
    public string Name;
    public SO_Ingredient[] ingredients;
}
    

