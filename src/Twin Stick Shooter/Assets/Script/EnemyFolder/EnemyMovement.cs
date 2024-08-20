using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5f;

    private void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        if (target != null)
        {
            // Hedef pozisyonu ile mevcut pozisyon arasındaki farkı hesapla
            Vector3 direction = (target.position - transform.position).normalized;

            // Hareket vektörünü oluştur
            Vector3 move = direction * moveSpeed * Time.deltaTime;

            // Y eksenini sabit tutarak yeni pozisyonu hesapla
            Vector3 newPosition = transform.position + move;
            newPosition.y = transform.position.y; // Y eksenini sabit tut

            // Nesneyi yeni pozisyona yerleştir
            transform.position = newPosition;
        }
    }
}
