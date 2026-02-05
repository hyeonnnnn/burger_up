using System;
using UnityEngine;

public class FailZoneManager : MonoBehaviour
{
    public static FailZoneManager Instance { get; private set; }

    public static event Action OnGameOver;

    [SerializeField] private float _failHeight = -2f;
    [SerializeField] private float _checkDelay = 2f;
    [SerializeField] private string _floorTag = "Floor";

    private bool _isGameOver;

    public float FailHeight => _failHeight;
    public float CheckDelay => _checkDelay;
    public string FloorTag => _floorTag;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void TriggerGameOver()
    {
        if (_isGameOver) return;

        _isGameOver = true;
        Time.timeScale = 0f;
        OnGameOver?.Invoke();
    }
}
