using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private float spawnRate = 5.0f;
    [SerializeField] private int quantity = 5;
    [SerializeField] private float minRadius = 5.0f;
    [SerializeField] private float maxRadius = 15.0f;

    private float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (!(timer >= spawnRate)) return;
        timer = 0f;
            
        for (int i = 0; i < quantity; i++)
        {
            // Generate a random position within the radius
            float randomRadius = Random.Range(minRadius, maxRadius);
            float randomAngle = Random.Range(0f, 360f);
            Vector3 spawnPosition = transform.position + Quaternion.Euler(0f, randomAngle, 0f) * (Vector3.forward * randomRadius);

            // Spawn the object at the position
            Instantiate(spawnObject, spawnPosition, Quaternion.identity);
        }
    }
}
