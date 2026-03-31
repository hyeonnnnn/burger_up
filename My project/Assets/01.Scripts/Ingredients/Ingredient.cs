using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private IngredientData _data;

    public IngredientData Data => _data;
}
