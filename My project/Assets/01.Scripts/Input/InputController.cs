using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private IngredientsSpawner _ingredientsSpawner;

    private bool _isGameOver;

    private void OnEnable()
    {
        FailZoneManager.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        FailZoneManager.OnGameOver -= HandleGameOver;
    }

    private void Update()
    {
        GetPlayerInput();
    }

    private void GetPlayerInput()
    {
        if (_isGameOver) return;

        if (Input.GetMouseButtonDown(0))
        {
            _ingredientsSpawner.Drop();
        }
    }

    private void HandleGameOver()
    {
        _isGameOver = true;
    }
}
