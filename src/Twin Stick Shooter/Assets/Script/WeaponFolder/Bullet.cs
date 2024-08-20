using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float life = 3;
    public int scoreValue = 5; // Küp yok edildiğinde eklenecek puan
    public GameObject explosionEffect; // Partikül prefabı için referans
    private ScoreManager scoreManager; // ScoreManager referansı

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void Start()
    {
        // ScoreManager referansını bul
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreValue); // Skoru güncelle
            }

            // Partikül efektini yarat
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, other.transform.position, Quaternion.identity);
            }

            Destroy(other.gameObject); // Küpü yok et
            Destroy(gameObject); // Mermiyi yok et
        }
    }



}
