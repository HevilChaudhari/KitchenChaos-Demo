using UnityEngine;
using System;

public class PlayerController : MonoBehaviour , IKitchenObjectParent
{
    public static PlayerController Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;


    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    } 


    [SerializeField] 
    private GameInput _gameInput;

    [SerializeField] 
    private float _movSpeed = 10f;

    [SerializeField] 
    private float _rotateSpeed = 10f;

    [SerializeField] 
    private Transform _kitchenObjectHoldPoint;

    [SerializeField] 
    private LayerMask _counterLayer;

    private Vector3 _lastInterectionDirection;

    private BaseCounter _selectedCounter;

    private KitchenObject _kitchenObject;

    public bool _isWalking { get; private set; }


    private void Awake()
    {
        if(Instance == null)
        {  
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


    }


    private void OnEnable()
    {
        _gameInput.OnInterect += Interect;
    }

    private void Interect(object sender, System.EventArgs e)
    {
        if(_selectedCounter != null)
        {
            _selectedCounter.Interect(this);
        }
    }

    private void Update()
    {
        PlayerMovement();
        HandleInteraction();
    }



    #region Private Mathods

    //Handle Player Movement
    private void PlayerMovement()
    {
        Vector2 inputVector = _gameInput.InputVectorNormalized();

        Vector3 moveDir = new(inputVector.x, 0f, inputVector.y);

        float movDistance = _movSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, movDistance);

        if (!canMove)
        {
            //Cannot Move towrds movDirection
            //Attempt only x Movement

            Vector3 moveDirX = new(moveDir.x, 0f, 0f);
            moveDirX = moveDirX.normalized;
            
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, movDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                //Cannot Move towrds moveDirX
                //Attempt only Z Movement
                Vector3 moveDirZ = new(0, 0, moveDir.z);
                moveDirZ = moveDirZ.normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, movDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }

        }

        if (canMove)
        {
            _isWalking = moveDir != Vector3.zero;
            transform.position += moveDir * Time.deltaTime * _movSpeed;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * _rotateSpeed);
    }

    //Handle the Interection With Objects
    private void HandleInteraction()
    {
        Vector2 inputVector = _gameInput.InputVectorNormalized();

        Vector3 moveDir = new(inputVector.x, 0f, inputVector.y);

        float interectDistance = 2f;

        if(moveDir != Vector3.zero)
        {
            _lastInterectionDirection = moveDir;
        }


        if(Physics.Raycast(transform.position, _lastInterectionDirection, out RaycastHit raycastHit, interectDistance,_counterLayer))
        {
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if(baseCounter != _selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }

        //Debug.Log(_selectedCounter);
    }


    //Change the Selected Counter
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        _selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = _selectedCounter
        });
    }


    #endregion


    //INterface
    #region IkitchenObjectInterface

    public Transform GetKitchenObjectFollowTransform()
    {
        return _kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }


    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }

    #endregion
}
