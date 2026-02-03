using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private IngredientsSpawner _ingredientsSpawner;

    private void Update()
    {
        GetPlayerInput();
    }

    private void GetPlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ingredientsSpawner.Drop();
        }
    }
}
