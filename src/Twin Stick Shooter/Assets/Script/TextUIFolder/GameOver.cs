using UnityEngine;
using UnityEngine.SceneManagement; // Sahneyi yeniden yüklemek için

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel; // Oyun bitti paneli
    public GameObject[] uiElements; // Gizlenecek UI öğeleri (skor vb.)

    void Start()
    {
        // Oyun başladığında paneli gizle ve UI öğelerini göster
        gameOverPanel.SetActive(false);
        SetUIElementsActive(true);
    }

    public void ShowGameOverScreen()
    {
        // Oyun bitti ekranını göster
        gameOverPanel.SetActive(true);
        SetUIElementsActive(false); // Skor ve diğer UI öğelerini gizle
        Time.timeScale = 0f; // Oyunu duraklat (isteğe bağlı)
    }

    public void RestartGame()
    {
        // Yeni oyuna başla
        Time.timeScale = 1f; // Oyunun zamanını devam ettir
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Mevcut sahneyi yeniden yükle
    }

    private void SetUIElementsActive(bool isActive)
    {
        foreach (GameObject uiElement in uiElements)
        {
            uiElement.SetActive(isActive);
        }
    }
}
