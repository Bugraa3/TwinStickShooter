using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Camera mainCamera;                 // Kamerayı atayın
    public GameObject[] cubePrefabs;          // Küp prefab'larını içeren dizi
    public float spawnInterval = 2f;          // Küp oluşturma aralığı (saniye cinsinden)
    public float spawnDistance = 50f;         // Küplerin kamera görüş alanı dışına yerleştirileceği mesafe
    public float fixedYPosition = 1.50f;      // Küplerin sabit Y pozisyonu

    private float spawnTimer = 0f;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnCubeOutsideCameraView();
            spawnTimer = 0f;
        }
    }

    void SpawnCubeOutsideCameraView()
    {
        // Kameranın frustum köşelerini hesaplayın
        Vector3[] frustumCorners = new Vector3[4];
        mainCamera.CalculateFrustumCorners(
            new Rect(0, 0, 1, 1),
            mainCamera.nearClipPlane,
            Camera.MonoOrStereoscopicEye.Mono,
            frustumCorners
        );

        // Görüş alanının dışına rastgele bir konum belirleyin
        Vector3 randomDirection = Random.onUnitSphere;
        randomDirection.y = Mathf.Abs(randomDirection.y); // Y eksenini yukarıda tutun

        // Kamera merkezinden uzaklık
        Vector3 spawnPosition = mainCamera.transform.position + randomDirection * spawnDistance;
        spawnPosition.y = fixedYPosition;

        // Kamera frustum'u içinde olup olmadığını kontrol edin
        if (!IsPositionWithinCameraView(spawnPosition))
        {
            // Rastgele bir prefab seçin
            int randomIndex = Random.Range(0, cubePrefabs.Length);
            GameObject selectedPrefab = cubePrefabs[randomIndex];

            // Seçilen prefab'ı oluştur
            Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
        }
    }

    bool IsPositionWithinCameraView(Vector3 position)
    {
        // Kameranın görsel alanında olup olmadığını kontrol edin
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        return GeometryUtility.TestPlanesAABB(planes, new Bounds(position, Vector3.one));
    }
}
