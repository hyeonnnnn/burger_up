using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    private void Start()
    {
        _panel.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.OnGameOver += ShowPanel;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= ShowPanel;
    }

    private void ShowPanel()
    {
        _panel.SetActive(true);
    }

    public void Retry()
    {
        GameManager.Instance.RestartGame();
    }
}
