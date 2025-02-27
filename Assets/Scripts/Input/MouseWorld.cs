using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    public static MouseWorld Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Vector2 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public GridPosition GetMouseGridPosition()
    {
        return GridManager.Instance.GetGridPosition(GetMouseWorldPosition());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Right-clicked on: " + hit.collider.gameObject.name);
                //if is plant
                if (hit.collider.gameObject.GetComponent<Plant>() != null)
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
