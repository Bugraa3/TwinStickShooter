using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için
using UnityEngine.UI; // UI bileşenleri için

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Duraklatma ekranı paneliniz
    public Button resumeButton; // Resume butonu
    public Button restartButton; // Restart butonu

    private bool isPaused = false; // Oyun duraklatma durumu

    void Start()
    {
        // Butonlara işlev ekleyin
        resumeButton.onClick.AddListener(() => Debug.Log("Resume Button Clicked"));
        restartButton.onClick.AddListener(() => Debug.Log("Restart Button Clicked"));

        // Duraklatma menüsünü gizle
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        // ESC tuşuna basıldığında menüyü açma veya kapama
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true); // Paneli göster
        Time.timeScale = 0f; // Oyunun zamanını duraklat
        isPaused = true; // Duraklatma durumunu güncelle

        // Oyun nesneleriyle etkileşimi engellemek için
        ToggleGameObjects(false);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Paneli gizle
        Time.timeScale = 1f; // Oyunun zamanını devam ettir
        isPaused = false; // Duraklatma durumunu güncelle

        // Oyun nesneleriyle etkileşimi yeniden etkinleştir
        ToggleGameObjects(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f; // Oyunun zamanını devam ettir
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Mevcut sahneyi yeniden yükle
    }

    private void ToggleGameObjects(bool state)
    {
        // Tüm oyuncu ve ateş etme nesnelerini etkinleştir veya devre dışı bırak
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject gun = GameObject.FindGameObjectWithTag("Gun");

        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = state;
        }

        if (gun != null)
        {
            gun.GetComponent<Gun>().enabled = state;
        }
    }
}
