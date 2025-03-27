using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // There is no KO here
            if (player.HasKitchenObject()) {
                //Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else {
                // Player not carrying anything
            }
        }else {
            // There is a KO here
            if (player.HasKitchenObject()) {
                //Player is carrying somethings
            } else {
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    
}
