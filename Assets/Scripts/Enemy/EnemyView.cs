
using UnityEngine;

public class EnemyView : MonoBehaviour, IView
{
    private Rigidbody2D _rb;
    private Vector2 _offset;
    private Enemy _parent;

    public void Initialize(IController controller)
    {
        _parent = (Enemy)controller;
        _rb = GetComponent<Rigidbody2D>();
        _offset = new Vector2(6, 0.5f);
        gameObject.SetActive(true);
        transform.position += (Vector3)_offset;
        ResetSpeed();
    }
    public void Dispose()
    {
        _parent = null;
    }

    public Enemy GetParent()
    {
        return _parent;
    }

    public void ResetSpeed()
    {
        _rb.velocity = _parent.GetProperties().Speed * Vector2.left;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _parent.OnTriggerEnter2D(other);
    }

    public void SetSpeedToZero()
    {
        _rb.velocity = Vector2.zero;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _parent.OnTriggerExit2D(other);

    }



}
