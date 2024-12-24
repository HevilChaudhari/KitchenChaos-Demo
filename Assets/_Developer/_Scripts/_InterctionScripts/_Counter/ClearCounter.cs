using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;

    public override void Interect(PlayerController player)
    {
        if (!HasKitchenObject())
        {
            //There is no kitchen object here
            if (player.HasKitchenObject())
            {
                //Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //PLayer not carrying anything
            }
        }
        else
        {
            //There is a kitchen object here
            if (player.HasKitchenObject())
            {
                //Player Carrying Somthing
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }




}
