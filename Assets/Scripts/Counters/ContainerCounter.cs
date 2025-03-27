using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(Player player) {
        if(!player.HasKitchenObject()) {
            // Player is not carrying something
            KitchenObject.SpawnKitchenObject(kitchenObjectSO,player);
            
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        } else {
            
        }
    }
    
}
