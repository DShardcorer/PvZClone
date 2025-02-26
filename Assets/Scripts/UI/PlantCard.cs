using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantCard : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PlantSO plantSO;
    // Reference to the PlantPlacementManager in the scene:
    [SerializeField] private PlantPlacementManager placementManager;

    // Called when the card is clicked.
    public void OnPointerClick(PointerEventData eventData)
    {
        // Tell the placement manager to start placing this plant.
        placementManager.SetCurrentPlantPreview(plantSO.plantPrefab);
        placementManager.StartPlantPlacement(plantSO.plantPrefab);
    }
}
