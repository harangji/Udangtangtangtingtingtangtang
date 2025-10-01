using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableCharacterIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public SummonableCharacterData CharacterData { get; private set; }

    private Image _iconImage;
    private static GameObject _draggedIcon; // A static reference to the icon being dragged
    private Transform _originalParent;
    private Canvas _mainCanvas;

    private void Awake()
    {
        _iconImage = GetComponent<Image>();
        _mainCanvas = GetComponentInParent<Canvas>();
    }

    public void Initialize(SummonableCharacterData data)
    {
        CharacterData = data;
        if (_iconImage != null)
        {
            _iconImage.sprite = data.uiIcon;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CharacterData == null) return;

        // Create a temporary icon to drag around
        _draggedIcon = new GameObject("DraggedIcon");
        _draggedIcon.transform.SetParent(_mainCanvas.transform, false);
        _draggedIcon.transform.SetAsLastSibling(); // Ensure it renders on top

        var image = _draggedIcon.AddComponent<Image>();
        image.sprite = CharacterData.uiIcon;
        image.raycastTarget = false; // So it doesn't interfere with drop detection

        // Make it semi-transparent
        var color = image.color;
        color.a = 0.7f;
        image.color = color;
        
        UpdateDraggedIconPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_draggedIcon == null) return;
        UpdateDraggedIconPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_draggedIcon == null)
        {
            Destroy(_draggedIcon);
            return;
        }

        // Convert screen position to world position
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
        worldPos.z = 0; // Assuming a 2D game

        // Check if the drop is outside the UI area (simple check)
        // A more robust solution would use a dedicated "drop zone"
        if (!EventSystem.current.IsPointerOverGameObject(eventData.pointerId))
        {
            // Summon the character
            if (CharacterData.characterPrefab != null)
            {
                Instantiate(CharacterData.characterPrefab, worldPos, Quaternion.identity);
                Debug.Log($"{CharacterData.characterName} summoned at {worldPos}");
            }
        }
        else
        {
            Debug.Log("Cancelled summon because it was dropped on UI.");
        }

        // Clean up the dragged icon
        Destroy(_draggedIcon);
        _draggedIcon = null;
    }

    private void UpdateDraggedIconPosition(PointerEventData eventData)
    {
        if (_draggedIcon != null && _mainCanvas != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _mainCanvas.transform as RectTransform,
                eventData.position,
                _mainCanvas.worldCamera,
                out Vector2 localPoint);
            
            _draggedIcon.transform.localPosition = localPoint;
        }
    }
}
