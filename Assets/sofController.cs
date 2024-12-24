using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class sofController : MonoBehaviour
{
    [SerializeField] private Vector3 _windDir = Vector3.up;

    [Header("Wind Direction Ranges")] 
    private Vector2 xAxisRange = new Vector2(-0.8f, 0.8f);
    private Vector2 yAxisRange = new Vector2(1.2f, 3f);
    private Vector2 zAxisRange = new Vector2(-1f, 1f);

    [Header("Oscillation Speeds (in degrees per second)")] [SerializeField]
    private float xAxisSpeed = 10f;
    [SerializeField] private float yAxisSpeed = 5f;
    [SerializeField] private float zAxisSpeed = 15f;

    private float xValue;
    private float yValue;
    private float zValue;

    private bool xIncreasing = true;
    private bool yIncreasing = true;
    private bool zIncreasing = true;

    private Rigidbody rb;
    public void Start()
    {
        xValue = Random.Range(xAxisRange.x, xAxisRange.y);
        yValue = Random.Range(yAxisRange.x, yAxisRange.y);
        zValue = Random.Range(zAxisRange.x, zAxisRange.y);
        xAxisSpeed = Random.Range(5, 15);
        yAxisSpeed = Random.Range(8, 20);
        zAxisSpeed = Random.Range(5, 15);
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // updateWindDirection();
        // float force = Random.Range(0, 10);
        // rb.AddForce(_windDir * force);
    }

    void updateWindDirection()
    {
        // Update X axis
        if (xIncreasing)
        {
            xValue += xAxisSpeed * Time.deltaTime;
            if (xValue >= xAxisRange.y)
            {
                xValue = xAxisRange.y;
                xIncreasing = false;
            }
        }
        else
        {
            xValue -= xAxisSpeed * Time.deltaTime;
            if (xValue <= xAxisRange.x)
            {
                xValue = xAxisRange.x;
                xIncreasing = true;
            }
        }

        // Update Y axis
        if (yIncreasing)
        {
            yValue += yAxisSpeed * Time.deltaTime;
            if (yValue >= yAxisRange.y)
            {
                yValue = yAxisRange.y;
                yIncreasing = false;
            }
        }
        else
        {
            yValue -= yAxisSpeed * Time.deltaTime;
            if (yValue <= yAxisRange.x)
            {
                yValue = yAxisRange.x;
                yIncreasing = true;
            }
        }

        // Update Z axis
        if (zIncreasing)
        {
            zValue += zAxisSpeed * Time.deltaTime;
            if (zValue >= zAxisRange.y)
            {
                zValue = zAxisRange.y;
                zIncreasing = false;
            }
        }
        else
        {
            zValue -= zAxisSpeed * Time.deltaTime;
            if (zValue <= zAxisRange.x)
            {
                zValue = zAxisRange.x;
                zIncreasing = true;
            }
        }

        // Create the wind direction vector using the current axis values
        _windDir = new Vector3(xValue, yValue, zValue);
    }

    private void OnCollisionEnter(Collision other)
    {
        float bounceForce = 1.2f;
        rb.AddForce(other.contacts[0].normal * bounceForce, ForceMode.Impulse);
        
    }
    
}