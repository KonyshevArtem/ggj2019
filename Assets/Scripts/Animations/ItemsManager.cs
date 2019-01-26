using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public Transform ItemsContainer;
    public bool IsHoldingItem;
    
    public void PickUpItem(string itemName)
    {
        if (IsHoldingItem) return;
        ItemsContainer.Find(itemName).gameObject.SetActive(true);
        IsHoldingItem = true;
    }

    public void PutDownItem()
    {
        IsHoldingItem = false;
        for (int i = 0; i < ItemsContainer.childCount; ++i)
            ItemsContainer.GetChild(i).gameObject.SetActive(false);
    }
}