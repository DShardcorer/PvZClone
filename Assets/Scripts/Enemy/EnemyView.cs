using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyView : MonoBehaviour, IProduct
{
    private Rigidbody2D _rb;
    private Vector2 _offset;
    private Enemy _controller;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _offset = new Vector2(6, 0.5f);
    }

    public void Initialize(Enemy controller)
    {
        _controller = controller;
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
        transform.position += (Vector3)_offset;
        ResetSpeed();
    }

    public void ResetSpeed()
    {
        _rb.velocity = _controller.GetProperties().speed * Vector2.left;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _controller.OnTriggerEnter2D(other);
    }

    public void SetSpeedToZero()
    {
        _rb.velocity = Vector2.zero;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _controller.OnTriggerExit2D(other);

    }



}
