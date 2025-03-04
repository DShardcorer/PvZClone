using UnityEngine;
using UnityEngine.EventSystems;

public class PlantPlacementManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private GameObject currentPlantPreview;
    private bool isPlacingPlant = false;
    private PlantSO currentPlantSO;

    public void SetCurrentPlantPreview(GameObject plantPreview)
    {
        currentPlantPreview = plantPreview;
    }
    public void SetCurrentPlantSO(PlantSO plantSO)
    {
        currentPlantSO = plantSO;
    }

    void Update()
    {
        if (isPlacingPlant && currentPlantPreview != null)
        {
            currentPlantPreview.transform.position = MouseWorld.Instance.GetMouseWorldPosition();

            if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
            {
                PlacePlant(currentPlantPreview.transform.position);
            }
        }
    }

    // Called by the PlantCard when a card is clicked.
    public void StartPlantPlacement(PlantSO plantSO)
    {
        if (isPlacingPlant)
        {
            CancelPlantPlacement();
        }
        currentPlantPreview = Instantiate(plantSO.plantPreviewPrefab, MouseWorld.Instance.GetMouseWorldPosition(), Quaternion.identity);
        SetCurrentPlantPreview(currentPlantPreview);
        SetCurrentPlantSO(plantSO);
        isPlacingPlant = true;
    }

    // Cancel placement and clean up the preview.
    public void CancelPlantPlacement()
    {
        if (currentPlantPreview != null)
        {
            Destroy(currentPlantPreview);
        }
        isPlacingPlant = false;
    }

    private void PlacePlant(Vector2 position)
    {
        if (!GridManager.Instance.IsWithinBounds(GridManager.Instance.GetGridPosition(position)) 
        || GridManager.Instance.IsCellOccupiedByPlant(GridManager.Instance.GetGridPosition(position))
        || !SunManager.Instance.CanAfford(currentPlantSO.sunCost))
        {
            CancelPlantPlacement();
            return;
        }
        SunManager.Instance.SpendSun(currentPlantSO.sunCost);
        IController plant = StageManager.Instance.GetPlantManager().GetProduct(currentPlantSO.plantName, position);
        CancelPlantPlacement();
    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
