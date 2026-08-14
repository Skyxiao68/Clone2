using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject flowerPrefab;
    [SerializeField] private float maxTime = 1.5f; 
    [SerializeField] private float heightRange = 0.5f; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnFlower();
    }

    // Update is called once per frame
    void SpawnFlower()
    {
        float randomY = Random.Range(-heightRange, heightRange);
        float randomRotation = Random.Range(80f, 145f);
        GameObject flower = Instantiate(flowerPrefab, new Vector3(transform.position.x, transform.position.y + randomY, transform.position.z), Quaternion.Euler(0f, 0f, randomRotation));
        Destroy(flower, 15f);
        Invoke("SpawnFlower", maxTime); 
    }
    
}

