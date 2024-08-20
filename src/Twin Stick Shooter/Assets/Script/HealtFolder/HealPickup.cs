using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 20; // Her sağlık objesinin verdiği can miktarı

    void OnTriggerEnter(Collider other)
    {
        // Oyuncu ile çarpışmayı kontrol et
        if (other.CompareTag("Player"))
        {
            // Oyuncunun PlayerHealth scriptine eriş
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Oyuncunun sağlığını artır
                playerHealth.IncreaseHealth(healthAmount);
                // Sağlık objesini yok et
                Destroy(gameObject);
            }
        }
    }
}
