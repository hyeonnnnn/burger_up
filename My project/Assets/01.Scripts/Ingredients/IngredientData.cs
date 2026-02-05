using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "BurgerUp/IngredientData")]
public class IngredientData : ScriptableObject
{
    [SerializeField] private string _ingredientName;
    [SerializeField] private int _score = 10;

    public string IngredientName => _ingredientName;
    public int Score => _score;
}
