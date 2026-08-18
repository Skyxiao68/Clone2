using UnityEngine;

public class ObstcaleSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject obsticalePrefab;

    [SerializeField]
    private float spawnInterval = 1.5f;

    [SerializeField]
    private float heightRange = 0.5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 0, spawnInterval);
    }

    void SpawnObstacle()
    {
        for (int attempt = 0; attempt < 5; attempt++)
        {
            float randomY = Random.Range(-heightRange, heightRange);
            Vector3 spawnPos = new Vector3(
                transform.position.x,
                transform.position.y + randomY,
                transform.position.z
            );

            float checkRadius = 2f;
            Collider2D hit = Physics2D.OverlapCircle(spawnPos, checkRadius);
            if (hit == null)
            {
                Instantiate(obsticalePrefab, spawnPos, Quaternion.identity);
                return;
            }
        }

        Debug.Log("skip spawn no space ");
    }
}
