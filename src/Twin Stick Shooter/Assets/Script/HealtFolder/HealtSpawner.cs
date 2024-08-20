using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtSpawner : MonoBehaviour
{
    public Camera mainCameraa;
    public GameObject[] healtPrefab;
    public float healtSpawnInterval = 50f;
    public float healtSpawnDistance;
    public float healtFixedYPosition;

    private float healtSpawnTimer = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        healtSpawnTimer += Time.deltaTime;

        if (healtSpawnTimer >= healtSpawnInterval)
        {
            SpawnCubeOutsideCameraView();
            healtSpawnTimer = 0f;
        }
    }

    void SpawnCubeOutsideCameraView()
    {
        // Kameranın frustum köşelerini hesaplayın
        Vector3[] frustumCorners = new Vector3[4];
        mainCameraa.CalculateFrustumCorners(
            new Rect(0, 0, 1, 1),
            mainCameraa.nearClipPlane,
            Camera.MonoOrStereoscopicEye.Mono,
            frustumCorners
        );

        // Görüş alanının dışına rastgele bir konum belirleyin
        Vector3 randomDirection = Random.onUnitSphere;
        randomDirection.y = Mathf.Abs(randomDirection.y); // Y eksenini yukarıda tutun

        // Kamera merkezinden uzaklık
        Vector3 spawnPosition = mainCameraa.transform.position + randomDirection * healtSpawnDistance;
        spawnPosition.y = healtFixedYPosition;

        // Kamera frustum'u içinde olup olmadığını kontrol edin
        if (!IsPositionWithinCameraView(spawnPosition))
        {
            // Rastgele bir prefab seçin
            int randomIndex = Random.Range(0, healtPrefab.Length);
            GameObject selectedPrefab = healtPrefab[randomIndex];

            // Seçilen prefab'ı oluştur
            Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
        }
    }

    bool IsPositionWithinCameraView(Vector3 position)
    {
        // Kameranın görsel alanında olup olmadığını kontrol edin
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCameraa);
        return GeometryUtility.TestPlanesAABB(planes, new Bounds(position, Vector3.one));
    }


    
}
