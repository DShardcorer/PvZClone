using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour, IController
{
    [SerializeField] private float _speed = 10f;
    private SunManager _parent;

    private Vector2 _sunCollectionPoint;
    private int sunValue = 25;

    private Rigidbody2D _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sunCollectionPoint = StageManager.Instance.GetSunManager().GetSunCollectionPointOnWorldSpace();
    }




    public void Initialize()
    {
        _parent = StageManager.Instance.GetSunManager();
        gameObject.SetActive(true);
        float angle = Random.Range(0, 360);
        Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
        _rb.velocity = direction * _speed;
    }

    private void OnMouseDown()
    {
        Collect();
    }

    private void Collect()
    {
        //Shoots the sun to the sun collection point
        _rb.velocity = (_sunCollectionPoint - (Vector2)transform.position).normalized * _speed * 4;
        StartCoroutine(SendSunToCollectionPoint());

    }

    private IEnumerator SendSunToCollectionPoint()
    {
        yield return new WaitForSeconds(0.5f);
        _parent.AddSun(sunValue);
        
    }

    public void Dispose()
    {
        _parent = null;
        gameObject.SetActive(false);
    }
}
