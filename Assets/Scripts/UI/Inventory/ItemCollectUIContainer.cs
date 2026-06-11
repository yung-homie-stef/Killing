using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class ItemCollectUIContainer : MonoBehaviour
{
    [SerializeField] private GameObject _itemCollectPopupPrefab = null;
    [SerializeField] private int _maxPopups = 7;
    [SerializeField] private float _popupDuration = 5.0f;

    private readonly Queue<GameObject> _activePopups = new();

    // Start is called before the first frame update
    
    public void ShowItemPickup(ItemObject item, bool acquired)
    {
        GameObject popup = Instantiate(_itemCollectPopupPrefab, transform);
        ItemCollectPopup _popupScript = popup.GetComponent<ItemCollectPopup>();
        _popupScript.InitializePopup(item, acquired);

        _activePopups.Enqueue(popup);
        if (_activePopups.Count > _maxPopups)
            Destroy(_activePopups.Dequeue());

        Tween.Scale(target: popup.transform, startValue: 0, endValue: 1, duration: 0.25f);

        StartCoroutine(RemovePopup(popup));
    }

    private IEnumerator RemovePopup(GameObject popup)
    {
        yield return new WaitForSeconds(_popupDuration);
        if (popup == null) yield break;

        Tween.Scale(target: popup.transform, endValue: 0, duration: 0.25f).OnComplete(() => Destroy(popup));

    }
}
