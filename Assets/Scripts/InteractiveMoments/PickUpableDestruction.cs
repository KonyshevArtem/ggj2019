public class PickUpableDestruction : Destruction
{
    protected override bool CanInteract()
    {
        return !PlayerAgent.Instance.GetComponent<ItemsManager>().IsHoldingItem;
    }
}