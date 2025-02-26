using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour, IProduct
{
    [SerializeField] private float speed = 10f;
    private Transform sunCollectionPoint;
    private int sunValue = 25;

    private SunPool pool;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SetPool();
        sunCollectionPoint = SunCollectionPoint.Instance.transform;
    }

    private void SetPool()
    {
        pool = GetComponentInParent<SunPool>();
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
        float angle = Random.Range(0, 360);
        Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
        rb.velocity = direction * speed;
    }

    private void OnMouseDown()
    {
        Collect();
    }

    private void Collect()
    {
        //Shoots the sun to the sun collection point and disables it
        rb.velocity = (sunCollectionPoint.position - transform.position).normalized * speed * 2;
        StartCoroutine(SendSunToCollectionPoint());
        
    }

    private IEnumerator SendSunToCollectionPoint()
    {
        while (Vector2.Distance(transform.position, sunCollectionPoint.position) > 0.1f)
        {
            yield return null;
        }
        SunManager.Instance.AddSun(sunValue);
        pool.ReturnObject(gameObject);
    }
}
