using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // Takip edilecek hedef (örneğin oyuncu)
    public Vector3 offset;            // Kamera ile hedef arasındaki mesafe
    public float smoothSpeed = 0.125f; // Kamera hareketinin yumuşatma hızı

    void LateUpdate()
    {
        // Hedef pozisyonuna offset ekleyerek yeni bir pozisyon hesapla
        Vector3 desiredPosition = target.position + offset;

        // Yumuşak bir geçiş ile hedef pozisyona doğru hareket et
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Kamerayı yeni pozisyona yerleştir
        transform.position = smoothedPosition;

        // Eğer kameranın bakış açısını da hedefe döndürmek isterseniz, şu satırı ekleyebilirsiniz:
        // transform.LookAt(target);
    }
}
