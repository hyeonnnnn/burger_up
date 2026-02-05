using UnityEngine;

public class IngredientMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;


    private Rigidbody _rb;
    private bool _isDrop;
    private bool _skipFailCheck;
    private float _dropTime;
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
        if (!_isDrop)
        {
            Move();
            return;
        }

        CheckFail();
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
        _dropTime = Time.time;
        _rb.isKinematic = false;
    }

    private void CheckFail()
    {
        if (_skipFailCheck) return;
        if (Time.time - _dropTime < FailZoneManager.Instance.CheckDelay) return;

        if (transform.position.y < FailZoneManager.Instance.FailHeight)
        {
            FailZoneManager.Instance.TriggerGameOver();
        }
    }

    public void SetSkipFailCheck()
    {
        _skipFailCheck = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!_isDrop) return;
        if (_skipFailCheck) return;
        if (Time.time - _dropTime < FailZoneManager.Instance.CheckDelay) return;

        if (collision.gameObject.CompareTag(FailZoneManager.Instance.FloorTag))
        {
            FailZoneManager.Instance.TriggerGameOver();
        }
    }
}
