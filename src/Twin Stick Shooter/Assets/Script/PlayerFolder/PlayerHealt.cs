using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Sahneyi yeniden yüklemek için

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Text healthText;
    public GameOver gameOverManager; // GameOverManager referansı

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            TakeDamage(10);
            Destroy(other.gameObject); // Çarpan küpü yok et
        }
        else if (other.CompareTag("HealthPickup"))
        {
            HealthPickup healthPickup = other.GetComponent<HealthPickup>();
            if (healthPickup != null)
            {
                IncreaseHealth(healthPickup.healthAmount); // Can artır
                Destroy(other.gameObject); // Sağlık objesini yok et
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            HandleGameOver(); // Can sıfır olduğunda oyun bitti ekranını göster
        }
        UpdateHealthText();
    }

    public void IncreaseHealth(int healthAmount)
    {
        currentHealth += healthAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Maksimum sağlığı aşmamak için
        }
        UpdateHealthText();
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString();
        }
    }

    private void HandleGameOver()
    {
        // Oyun bitti ekranını göster
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOverScreen();
        }
    }
}
