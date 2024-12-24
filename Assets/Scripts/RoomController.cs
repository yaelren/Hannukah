using UnityEngine;

public class RotatingRoomController : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Speed of room rotation")]
    public float rotationSpeed = 30f;

    [Tooltip("Axis of rotation")]
    public Vector3 rotationAxis = Vector3.up;

    [Header("Ball Spawn Settings")]
    [Tooltip("Prefab of the ball to spawn")]
    public GameObject ballPrefab;

    [Tooltip("Number of balls to spawn")]
    public int numberOfBalls = 10;

    [Tooltip("Area where balls will spawn")]
    public Vector3 spawnArea = new Vector3(5f, 5f, 5f);

    private void Start()
    {
        // Spawn balls at the start
        SpawnBalls();
    }

    private void Update()
    {
        // Rotate the entire room
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }

    private void SpawnBalls()
    {
        for (int i = 0; i < numberOfBalls; i++)
        {
            // Calculate random spawn position within the spawn area
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                spawnArea.y / 2, // Start from top half of the area
                Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
            );

            // Instantiate the ball
            GameObject ball = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);

            // Ensure the ball has a Rigidbody for physics
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
            if (ballRigidbody == null)
            {
                ballRigidbody = ball.AddComponent<Rigidbody>();
            }

            // Optional: Add some initial randomness to ball physics
            ballRigidbody.AddForce(
                Random.Range(-10f, 10f), 
                Random.Range(0f, 10f), 
                Random.Range(-10f, 10f), 
                ForceMode.Impulse
            );
        }
    }
}