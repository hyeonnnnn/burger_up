using System;
using System.Collections.Generic;
using UnityEngine;

public class FailZoneManager : MonoBehaviour
{
    public static FailZoneManager Instance { get; private set; }

    public static event Action OnGameOver;

    [SerializeField] private float _failHeight = -2f;
    [SerializeField] private float _checkDelay = 2f;

    private readonly List<TrackedIngredient> _trackedIngredients = new List<TrackedIngredient>();
    private bool _isGameOver;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RegisterIngredient(Transform ingredient)
    {
        _trackedIngredients.Add(new TrackedIngredient
        {
            Transform = ingredient,
            RegisterTime = Time.time
        });
    }

    private void Update()
    {
        if (_isGameOver) return;

        float now = Time.time;

        for (int i = _trackedIngredients.Count - 1; i >= 0; i--)
        {
            var tracked = _trackedIngredients[i];

            if (tracked.Transform == null)
            {
                _trackedIngredients.RemoveAt(i);
                continue;
            }

            if (now - tracked.RegisterTime < _checkDelay) continue;

            if (tracked.Transform.position.y < _failHeight)
            {
                _isGameOver = true;
                OnGameOver?.Invoke();
                return;
            }
        }
    }

    private struct TrackedIngredient
    {
        public Transform Transform;
        public float RegisterTime;
    }
}
