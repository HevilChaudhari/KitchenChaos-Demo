using System;
using UnityEngine;

public class ContainerCounter : BaseCounter 
{
    public event EventHandler OnPlayerGrabedObject;

    [SerializeField] private KitchenObjectSO _kitchenObjectSO;


    public override void Interect(PlayerController player)
    {
        if (!player.HasKitchenObject())
        {
            Transform kitchenObjectTransform = Instantiate(_kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayerGrabedObject?.Invoke(this, EventArgs.Empty);
        }
            
    }


}
