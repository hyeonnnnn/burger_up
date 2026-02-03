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

    private void Start()
    {
        SpawnFirstObject();
    }

    private void SpawnFirstObject()
    {
        _currentIngredient = Instantiate(_firstBun, _spawnPoint.transform.position, Quaternion.identity);
        Drop();
    }

    public void Drop()
    {
        if (_currentIngredient == null) return;
        if (_isSpawning) return;

        var move = _currentIngredient.GetComponent<IngredientMove>();
        if (move != null)
        {
            move.Drop();
        }

        StartCoroutine(SpawnNextRoutine());
    }

    private IEnumerator SpawnNextRoutine()
    {
        _isSpawning = true;
        _currentIngredient = null;

        yield return new WaitForSeconds(_spawnDelay);

        GameObject prefab = _ingredients[Random.Range(0, _ingredients.Length)];
        _currentIngredient = Instantiate(prefab, _spawnPoint.position, Quaternion.identity);

        _isSpawning = false;
    }
}
