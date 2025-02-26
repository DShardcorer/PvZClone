using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantCard : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PlantSO plantSO;
    private CanvasGroup canvasGroup;
    // Reference to the PlantPlacementManager in the scene:
    private PlantPlacementManager placementManager;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Called when the card is clicked.
    private void Start()
    {
        placementManager = FindObjectOfType<PlantPlacementManager>();
        SunManager.Instance.OnSunChanged += SunManager_OnSunChanged;
    }

    private void SunManager_OnSunChanged(object sender, EventArgs e)
    {
        if (SunManager.Instance.CanAfford(plantSO.sunCost))
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;

        }
        else
        {
            canvasGroup.alpha = 0.5f;
            canvasGroup.interactable = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        placementManager.StartPlantPlacement(plantSO);
    }
}
