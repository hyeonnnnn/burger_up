using UnityEngine;

public class IngredientSpawnPosition : MonoBehaviour
{
    [SerializeField] private float _viewportY = 0.8f;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 viewportPoint = new Vector3(0.5f, _viewportY, _camera.nearClipPlane);
        Vector3 worldPoint = _camera.ViewportToWorldPoint(viewportPoint);

        Vector3 pos = transform.position;
        pos.y = worldPoint.y;
        transform.position = pos;
    }
}
