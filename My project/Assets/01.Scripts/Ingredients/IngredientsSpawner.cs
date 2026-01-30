using UnityEngine;

public class IngredientsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _ingredient;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _minXPosition = -2;
    [SerializeField] private float _maxXPosition = 2;

    private void Update()
    {
        Move();
    }

    private void Move()
    {

    }
}
