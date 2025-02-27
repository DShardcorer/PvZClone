using System.Collections;
using UnityEngine;

public class Zombie : MonoBehaviour, IProduct
{
    [SerializeField] private ZombieSO zombieSO;
    [SerializeField] private LayerMask plantLayer;

    [SerializeField] private LayerMask gameOverLayer;
    private Rigidbody2D rb;

    private int health;
    private int damage;
    private float speed;
    private float attackSpeed;
    private bool isAttacking;

    private Vector2 offset;
    private ObjectPool pool;

    private Plant targetPlant;
    private Coroutine attackPlantCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = zombieSO.health;
        damage = zombieSO.damage;
        speed = zombieSO.speed;
        attackSpeed = zombieSO.attackSpeed;
        SetPool();
        offset = new Vector2(6, 0.5f);
    }

    public void SetPool()
    {
        pool = GetComponentInParent<ObjectPool>();
    }

    private void Start()
    {
        ResetSpeed();
    }

    private void ResetSpeed()
    {
        rb.velocity = new Vector2(-speed, 0);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        pool.ReturnObject(gameObject);
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
        gameObject.transform.position += (Vector3)offset;
        ResetSpeed();
    }

    private void SetSpeedToZero()
    {
        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & plantLayer) != 0)
        {
            targetPlant = other.gameObject.GetComponent<Plant>();
            if (targetPlant != null && !isAttacking)
            {
                isAttacking = true;
                SetSpeedToZero();
                attackPlantCoroutine = StartCoroutine(AttackPlantCoroutine());
            }
        }else if (((1 << other.gameObject.layer) & gameOverLayer) != 0)
        {
            //Trigger gameover
            StageManager.Instance.GameOver();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & plantLayer) != 0 && targetPlant != null)
        {
            if (attackPlantCoroutine != null)
            {
                StopCoroutine(attackPlantCoroutine);
                attackPlantCoroutine = null;
            }
            isAttacking = false;
            targetPlant = null;
            ResetSpeed();
        }
    }

    private IEnumerator AttackPlantCoroutine()
    {
        while (targetPlant != null)
        {
            targetPlant.TakeDamage(damage);
            yield return new WaitForSeconds(1f / attackSpeed);
        }

        isAttacking = false;
        ResetSpeed();
    }
}
