using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    private void Start()
    {
        _panel.SetActive(false);
    }

    private void OnEnable()
    {
        FailZoneManager.OnGameOver += ShowPanel;
    }

    private void OnDisable()
    {
        FailZoneManager.OnGameOver -= ShowPanel;
    }

    private void ShowPanel()
    {
        _panel.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
