using UnityEngine;

public class StoveCounter : BaseCounter {
    private enum State {
        Idle,
        Frying,
        Fried,
        Burned,
    }
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    
    private State state;
    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;
    private float burningTimer;
    private BurningRecipeSO burningRecipeSO;


    private void Start() {
        state = State.Idle;
    }
    private void Update() {
        switch (state) {
            case State.Idle:
                break;
            case State.Frying:
                fryingTimer += Time.deltaTime;
                if (fryingTimer > fryingRecipeSO.fryingTimerMax) {
                    // Fried
                    GetKitchenObject().DestroySelf();
                    
                    KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                    
                    burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Fried;
                    burningTimer = 0f;
                }
                break;
            case State.Fried:
                fryingTimer += Time.deltaTime;
                if (burningTimer > burningRecipeSO.burningTimerMax) {
                    // Fried
                    GetKitchenObject().DestroySelf();
                    
                    KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                    
                    state = State.Burned;
                }
                break;
            case State.Burned:
                break;
        }
    }

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // There is no KO here
            if (player.HasKitchenObject()) {
                //Player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    // Player carrying something with a recipe
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    
                    state = State.Frying;
                    fryingTimer = fryingTimer = 0f;
                }
            }
            else {
                // Player not carrying anything
            }
        }else {
            // There is a KO here
            if (player.HasKitchenObject()) {
                //Player is carrying something
            } else {
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null) {
            return fryingRecipeSO.output;
        } else {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
            if (fryingRecipeSO.input == inputKitchenObjectSO) {
                return fryingRecipeSO;
            }
        }
        return null;
    }
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray) {
            if (burningRecipeSO.input == inputKitchenObjectSO) {
                return burningRecipeSO;
            }
        }
        return null;
    }
}
