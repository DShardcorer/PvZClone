using System;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantCard : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PlantSO plantSO;
    private SunManager _sunManager;
    private CanvasGroup _canvasGroup;
    // Reference to the PlantPlacementManager in the scene:
    private PlantPlacementManager _placementManager;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        
    }

    // Called when the card is clicked.
    private void Start()
    {
        _sunManager = GameManager.Instance.GetSunManager();
        _placementManager = GameManager.Instance.GetPlantPlacementManager();
        _sunManager.OnSunChanged += SunManager_OnSunChanged;
    }

    private void SunManager_OnSunChanged(object sender, EventArgs e)
    {
        if (_sunManager.CanAfford(plantSO.sunCost))
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;

        }
        else
        {
            _canvasGroup.alpha = 0.5f;
            _canvasGroup.interactable = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _placementManager.StartPlantPlacement(plantSO);
    }
}
