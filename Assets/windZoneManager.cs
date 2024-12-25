using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using Random = UnityEngine.Random;

public class WindZoneManager : MonoBehaviour
{
    [SerializeField] private Vector3 _windDir = new Vector3(0, 0.03f, 0);
    [SerializeField] private GameObject _sofPrefab;

    // [Header("Wind Direction Ranges")] private Vector2 xAxisRange = new Vector2(-0.05f, 0.05f);
    // private Vector2 yAxisRange = new Vector2(0.01f, 0.05f);
    private Vector2 xAxisRange = new Vector2(-1f, 1f);
    private Vector2 yAxisRange = new Vector2(-6f, 0.05f);

    // public int numOfSof;
    // public float largeScale;
    public float smallScale;
    // public int numfOfLargeSof;
    public int numOfSmallSof;
    
    private float _lastXUpdateTime = 0f;
    private float _timeXBetweenUpdates = 5f;
    private float _lastYUpdateTime = 0f;
    private float _timeYBetweenUpdates = 5f;
    public float xForce = 5f;
    public int yForce = 10;
    public void Start()
    {
        spawnSof();
    }

    private void FixedUpdate()
    {
        StickToSides();
    }

    private void StickToSides()
    {
        if (Time.time - _lastXUpdateTime > _timeXBetweenUpdates)
        {
            _lastXUpdateTime = Time.time;
            _timeXBetweenUpdates = Random.Range(1f, 5);
          
            float xDir = Random.Range(xAxisRange.x, xAxisRange.y)*xForce;
            float oldY = _windDir.y;
            _windDir = new Vector3(xDir, oldY, 0);
        }
        
        if (Time.time - _lastYUpdateTime > _timeYBetweenUpdates)
        {
            _lastYUpdateTime = Time.time;
            _timeYBetweenUpdates = 20;
            int yDir = Random.Range(0, 2) * yForce;
            _windDir = new Vector3(_windDir.x, yDir, 0);
        }
 
    }
    
    // private void RandomWind()
    // {
    //     if (Time.time - _lastXUpdateTime > _timeXBetweenUpdates)
    //     {
    //         _lastXUpdateTime = Time.time;
    //         _timeXBetweenUpdates = Random.Range(3f, 15);
    //         float xDir = Random.Range(xAxisRange.x, xAxisRange.y);
    //         float yDir = Random.Range(yAxisRange.x, yAxisRange.y);
    //         _windDir = new Vector3(xDir, yDir, 0);
    //     }
    // }

    private void spawnSof()
    {
        for (int i = 0; i < numOfSmallSof; i++)
        {
            var sof = Instantiate(_sofPrefab, Vector3.zero, Quaternion.identity);
            sof.transform.localScale = smallScale* sof.transform.localScale;
            if (i % 3 == 1)
            {
                sof.tag = "C";
            }
            else if (i % 2 == 0)
            {
                sof.tag ="B" ;
            }
            else
            {
                sof.tag = "A";
            }
        }

        // for (int i = 0; i < numfOfLargeSof; i++)
        // {
        //     GameObject sof = Instantiate(_sofPrefab, Vector3.zero, Quaternion.identity);
        //     sof.transform.localScale = largeScale * sof.transform.localScale;
        //
        // }
    }

    public float Aforce = 1f;
    public float Bforce = 0.8f;
    public float Cforce = 0.5f;
    private void OnTriggerStay(Collider other)
    {
        var hitObject = other.gameObject;
        if (hitObject != null)
        {
            var rb = hitObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float otherForce = 1;
                if(other.tag == "A")
                    otherForce = Aforce;
                else if(other.tag == "B")
                    otherForce = Bforce;
                else if (other.tag == "C")
                    otherForce = Cforce;
                rb.AddForce(otherForce*_windDir);
            }
        }
    }
}