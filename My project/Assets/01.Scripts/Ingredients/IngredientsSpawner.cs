using System.Collections;
using UnityEngine;

public class IngredientsSpawner : MonoBehaviour
{
    [Header("재료 정보")]
    [SerializeField] private GameObject[] _ingredients;
    [SerializeField] private GameObject _firstBun;

    [Header("스폰 정보")]
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _spawnDelay = 1.0f;

    private GameObject _currentIngredient;
    private bool _isSpawning = false;
    private bool _isGameOver = false;

    private void OnEnable()
    {
        FailZoneManager.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        FailZoneManager.OnGameOver -= HandleGameOver;
    }

    private void Start()
    {
        SpawnFirstObject();
    }

    private void SpawnFirstObject()
    {
        _currentIngredient = Instantiate(_firstBun, _spawnPoint.transform.position, Quaternion.identity);

        var move = _currentIngredient.GetComponent<IngredientMove>();
        if (move != null)
        {
            move.SetSkipFailCheck();
        }

        Drop();
    }

    public void Drop()
    {
        if (_currentIngredient == null) return;
        if (_isSpawning) return;
        if (_isGameOver) return;

        var move = _currentIngredient.GetComponent<IngredientMove>();
        if (move != null)
        {
            move.Drop();
        }

        var ingredient = _currentIngredient.GetComponent<Ingredient>();
        if (ingredient != null && ingredient.Data != null)
        {
            ScoreManager.Instance.AddScore(ingredient.Data.Score);
        }

        CameraFollow.Instance.RegisterIngredient(_currentIngredient.GetComponent<Rigidbody>());

        StartCoroutine(SpawnNextRoutine());
    }

    private IEnumerator SpawnNextRoutine()
    {
        _isSpawning = true;
        _currentIngredient = null;

        yield return new WaitForSeconds(_spawnDelay);

        if (_isGameOver) yield break;

        GameObject prefab = _ingredients[Random.Range(0, _ingredients.Length)];
        _currentIngredient = Instantiate(prefab, _spawnPoint.position, Quaternion.identity);

        _isSpawning = false;
    }

    private void HandleGameOver()
    {
        _isGameOver = true;
    }
}
