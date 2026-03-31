using UnityEngine;

public class IngredientMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;

    private Rigidbody _rb;
    private bool _isDrop;
    private Vector3 _startPosition;
    private int _direction = 1;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (_isDrop) return;

        _rb.isKinematic = true;
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (_isDrop) return;

        Move();
    }

    private void Move()
    {
        Vector3 position = transform.position;
        position.x += _direction * _speed * Time.deltaTime;

        if (position.x >= _startPosition.x + _distance)
        {
            position.x = _startPosition.x + _distance;
            _direction = -1;
        }
        else if (position.x <= _startPosition.x - _distance)
        {
            position.x = _startPosition.x - _distance;
            _direction = 1;
        }

        transform.position = position;
    }

    public void Drop()
    {
        _isDrop = true;
        _rb.isKinematic = false;
    }
}
