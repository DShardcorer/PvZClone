using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour, IProduct
{
    [SerializeField] private float speed = 10f;

    private Vector2 sunCollectionPoint;
    private int sunValue = 25;

    private ObjectPool pool;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SetPool();
    }

    private void Start()
    {
        sunCollectionPoint = SunManager.Instance.GetSunCollectionPointOnWorldSpace();
    }

    public void SetPool()
    {
        pool = GetComponentInParent<ObjectPool>();
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
        //Shoots the sun to the sun collection point
        rb.velocity = (sunCollectionPoint - (Vector2)transform.position).normalized * speed * 4;
        StartCoroutine(SendSunToCollectionPoint());

    }

    private IEnumerator SendSunToCollectionPoint()
    {
        yield return new WaitForSeconds(0.5f);
        SunManager.Instance.AddSun(sunValue);
        pool.ReturnObject(gameObject);
    }




}
