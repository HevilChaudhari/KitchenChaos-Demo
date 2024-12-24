using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    private Animator _animator;

    private int _idleAnimationHashId;
    private int _walkAnimationHashId;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _idleAnimationHashId = Animator.StringToHash("Idle");
        _walkAnimationHashId = Animator.StringToHash("Walk");
    }

    private void Update()
    {
        if(!_playerController)
            return;

        if (_playerController._isWalking)
            _animator.Play(_walkAnimationHashId);
        else
            _animator.Play(_idleAnimationHashId);
    }
}
