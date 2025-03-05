using UnityEngine;
using UnityEngine.EventSystems;

public class PlantPlacementManager : MonoBehaviour, IManager
{
    [SerializeField] private Canvas canvas;
    private GameManager _parent;
    private SunManager _sunManager;
    private GridManager _gridManager;
    private MouseWorld _mouseWorld;
    private GameObject _currentPlantPreview;
    private bool _isPlacingPlant = false;
    private PlantSO _currentPlantSO;


    public void Initialize(GameManager manager)
    {
        _parent = manager;
        _sunManager = _parent.GetSunManager();
        _gridManager = _parent.GetGridManager();
        _mouseWorld = _parent.GetMouseWorld();
    }

    public void SetCurrentPlantPreview(GameObject plantPreview)
    {
        _currentPlantPreview = plantPreview;
    }
    public void SetCurrentPlantSO(PlantSO plantSO)
    {
        _currentPlantSO = plantSO;
    }

    void Update()
    {
        if (_isPlacingPlant && _currentPlantPreview != null)
        {
            _currentPlantPreview.transform.position = _mouseWorld.GetMouseWorldPosition();

            if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
            {
                PlacePlant(_currentPlantPreview.transform.position);
            }
        }
    }

    // Called by the PlantCard when a card is clicked.
    public void StartPlantPlacement(PlantSO plantSO)
    {
        if (_isPlacingPlant)
        {
            CancelPlantPlacement();
        }
        _currentPlantPreview = Instantiate(plantSO.plantPreviewPrefab, _mouseWorld.GetMouseWorldPosition(), Quaternion.identity);
        SetCurrentPlantPreview(_currentPlantPreview);
        SetCurrentPlantSO(plantSO);
        _isPlacingPlant = true;
    }

    // Cancel placement and clean up the preview.
    public void CancelPlantPlacement()
    {
        if (_currentPlantPreview != null)
        {
            Destroy(_currentPlantPreview);
        }
        _isPlacingPlant = false;
    }

    private void PlacePlant(Vector2 position)
    {
        if (!_gridManager.IsWithinBounds(_gridManager.GetGridPosition(position))
        || _gridManager.IsCellOccupiedByPlant(_gridManager.GetGridPosition(position))
        || !_sunManager.CanAfford(_currentPlantSO.sunCost))
        {
            CancelPlantPlacement();
            return;
        }
        _sunManager.SpendSun(_currentPlantSO.sunCost);
        IController plant = GameManager.Instance.GetPlantManager().GetObject(_currentPlantSO.plantName, position);
        CancelPlantPlacement();
    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }


}
