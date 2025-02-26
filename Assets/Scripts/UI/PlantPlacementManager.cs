using UnityEngine;
using UnityEngine.EventSystems;

public class PlantPlacementManager : MonoBehaviour
{
    private GameObject currentPlantPreview;
    private bool isPlacingPlant = false;

    public void SetCurrentPlantPreview(GameObject plantPreview)
    {
        currentPlantPreview = plantPreview;
    }

    void Update()
    {
        if (isPlacingPlant && currentPlantPreview != null)
        {
            // Update preview to follow the mouse cursor.
            Vector3 mousePos = Input.mousePosition;
            // For 2D, we typically want z=0 in world space.
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
            worldPos.z = 0;
            currentPlantPreview.transform.position = worldPos;

            // When the player clicks (and not on UI), place the plant.
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
            {
                PlacePlant(worldPos);
            }
        }
    }

    // Called by the PlantCard when a card is clicked.
    public void StartPlantPlacement(GameObject plantPrefab)
    {
        // If already placing a plant, cancel the current placement.
        if (isPlacingPlant)
        {
            CancelPlantPlacement();
        }
        
        // Instantiate a preview of the plant.
        // Optionally, you could make a “ghost” version (semi-transparent, unresponsive).
        currentPlantPreview = Instantiate(plantPrefab);
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

    // Finalizes the placement of the plant.
    private void PlacePlant(Vector3 position)
    {
        // Here you might check for valid placement (grid cell, resource availability, etc.)
        // For now, we simply finalize placement.
        isPlacingPlant = false;
        // Optionally, if the preview is a temporary ghost, you might destroy it and instantiate a permanent plant.
        // For this example, we'll assume the preview is now the placed plant:
        // (If you need to initialize it further, do it here.)
    }

    // Utility method to check if the pointer is over a UI element.
    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
