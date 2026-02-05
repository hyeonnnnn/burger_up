using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance { get; private set; }

    [SerializeField] private float _smoothTime = 0.3f;
    [SerializeField] private float _minHeightToFollow = 1.0f;
    [SerializeField] private float _settleDelay = 1.5f;

    private readonly List<TrackedRigidbody> _ingredients = new List<TrackedRigidbody>();
    private float _initialY;
    private float _velocity;
    private bool _isGameOver;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _initialY = transform.position.y;
    }

    private void OnEnable()
    {
        FailZoneManager.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        FailZoneManager.OnGameOver -= HandleGameOver;
    }

    public void RegisterIngredient(Rigidbody rb)
    {
        _ingredients.Add(new TrackedRigidbody
        {
            Rb = rb,
            RegisterTime = Time.time
        });
    }

    private void LateUpdate()
    {
        if (_isGameOver) return;

        float highestY = 0f;
        float now = Time.time;

        for (int i = _ingredients.Count - 1; i >= 0; i--)
        {
            if (_ingredients[i].Rb == null)
            {
                _ingredients.RemoveAt(i);
                continue;
            }

            if (now - _ingredients[i].RegisterTime < _settleDelay)
                continue;

            float y = _ingredients[i].Rb.position.y;
            if (y > highestY)
            {
                highestY = y;
            }
        }

        float targetY = _initialY + Mathf.Max(0f, highestY - _minHeightToFollow);

        Vector3 pos = transform.position;
        pos.y = Mathf.SmoothDamp(pos.y, targetY, ref _velocity, _smoothTime);
        transform.position = pos;
    }

    private void HandleGameOver()
    {
        _isGameOver = true;
    }

    private struct TrackedRigidbody
    {
        public Rigidbody Rb;
        public float RegisterTime;
    }
}
