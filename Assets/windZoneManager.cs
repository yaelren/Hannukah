using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WindZoneManager : MonoBehaviour
{
    [SerializeField] private Vector3 _windDir = new Vector3(0, 0.03f, 0);
    [SerializeField] private GameObject _sofPrefab;

    // [Header("Wind Direction Ranges")] private Vector2 xAxisRange = new Vector2(-0.05f, 0.05f);
    // private Vector2 yAxisRange = new Vector2(0.01f, 0.05f);
    private Vector2 xAxisRange = new Vector2(-0.08f, 0.08f);
    private Vector2 yAxisRange = new Vector2(0f, 0.05f);

    // public int numOfSof;
    public float largeScale;
    public float smallScale;
    public int numfOfLargeSof;
    public int numOfSmallSof;
    
    private float _lastUpdateTime = 0f;
    private float _timeBetweenUpdates = 5f;

    public void Start()
    {
        spawnSof();
    }

    private void FixedUpdate()
    {
        if (Time.time - _lastUpdateTime > _timeBetweenUpdates)
        {
            _lastUpdateTime = Time.time;
            _timeBetweenUpdates = Random.Range(0.5f, 2);
            float xDir = Random.Range(xAxisRange.x, xAxisRange.y);
            float yDir = Random.Range(yAxisRange.x, yAxisRange.y);
            _windDir = new Vector3(xDir, yDir, 0);
        }
    }


    private void spawnSof()
    {
        for (int i = 0; i < numOfSmallSof; i++)
        {
            var sof = Instantiate(_sofPrefab, Vector3.zero, Quaternion.identity);
            sof.transform.localScale = smallScale* sof.transform.localScale;
        }
        
        for (int i = 0; i < numfOfLargeSof; i++)
        {
            var sof = Instantiate(_sofPrefab, Vector3.zero, Quaternion.identity);
            sof.transform.localScale = largeScale* sof.transform.localScale;
        }
     
    }

    private void OnTriggerStay(Collider other)
    {
        var hitObject = other.gameObject;
        if (hitObject != null)
        {
            var rb = hitObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float force = Random.Range(4f, 20);
                rb.AddForce(_windDir * force);
            }
        }
    }
}