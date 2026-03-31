using UnityEngine;

public class IngredientFailDetector : MonoBehaviour
{
    private bool _isActive;
    private bool _skipCheck;
    private float _activateTime;

    public void Activate()
    {
        _isActive = true;
        _activateTime = Time.time;
    }

    public void SetSkipCheck()
    {
        _skipCheck = true;
    }

    private void Update()
    {
        if (!_isActive) return;
        if (_skipCheck) return;
        if (Time.time - _activateTime < FailZoneManager.Instance.CheckDelay) return;

        if (transform.position.y < FailZoneManager.Instance.FailHeight)
        {
            GameManager.Instance.TriggerGameOver();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!_isActive) return;
        if (_skipCheck) return;
        if (Time.time - _activateTime < FailZoneManager.Instance.CheckDelay) return;

        if (collision.gameObject.CompareTag(FailZoneManager.Instance.FloorTag))
        {
            GameManager.Instance.TriggerGameOver();
        }
    }
}
