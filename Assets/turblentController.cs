using UnityEngine;

public class turbulentController : MonoBehaviour
{
    [Header("Turbulence Settings")]
    [SerializeField] private float baseUpwardForce = 5f;
    [SerializeField] private float turbulenceStrength = 2f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float swayRange = 1.5f;
    
    private Vector3 noiseOffset;
    private float timeOffset;
    private Rigidbody rb;
    private Vector3 startPos;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        
        // Unique starting position in noise space for each object
        noiseOffset = new Vector3(
            Random.Range(0f, 100f),
            Random.Range(0f, 100f),
            Random.Range(0f, 100f)
        );
        
        timeOffset = Random.Range(0f, 100f);
        
        // Add random initial force
        rb.AddForce(new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(0f, 2f),
            Random.Range(-1f, 1f)
        ), ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        float time = Time.time + timeOffset;
        
        // Calculate spiral motion
        float radius = swayRange * (Mathf.PerlinNoise(time * 0.5f + noiseOffset.x, 0) * 0.5f + 0.5f);
        float angle = time * rotationSpeed;
        
        // Create spiral force
        Vector3 spiralForce = new Vector3(
            Mathf.Cos(angle) * radius,
            0,
            Mathf.Sin(angle) * radius
        );

        // Add turbulence using 3D Perlin noise
        Vector3 turbulence = new Vector3(
            GetNoise(time, 0),
            GetNoise(time, 1),
            GetNoise(time, 2)
        ) * turbulenceStrength;

        // Calculate upward force with variation
        float heightFactor = 1f - Mathf.Clamp01((transform.position.y - startPos.y) / 10f);
        float upwardVariation = Mathf.PerlinNoise(time * 0.5f + noiseOffset.y, noiseOffset.z) * 0.5f + 0.5f;
        Vector3 upwardForce = Vector3.up * (baseUpwardForce * heightFactor * upwardVariation);

        // Combine forces
        Vector3 totalForce = spiralForce + turbulence + upwardForce;

        // Apply force
        rb.AddForce(totalForce, ForceMode.Acceleration);

        // Add some rotation for visual interest
        rb.AddTorque(turbulence * 0.1f, ForceMode.Acceleration);

        // Add centering force to prevent straying too far
        Vector3 toCenter = (startPos - transform.position) * 0.5f;
        toCenter.y = 0; // Only center horizontally
        rb.AddForce(toCenter, ForceMode.Acceleration);

        // Add drag to prevent excessive speeds
        rb.AddForce(-rb.linearVelocity * 0.3f, ForceMode.Acceleration);
    }

    private float GetNoise(float time, int offset)
    {
        return (Mathf.PerlinNoise(
            time * 0.5f + noiseOffset[offset],
            noiseOffset[(offset + 1) % 3]
        ) * 2f - 1f);
    }
}