using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float moveSpeed = 2;
    [SerializeField ] private float rotationSpeed = 5;
    private Vector2 _moveInput;
    private Vector3 _camForward;
    private Vector3 _camRight;
    private Vector3 _moveDirection;
    private CharacterController _characterController;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        _characterController.Move(_moveDirection * moveSpeed * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
      _moveInput = value.Get<Vector2>();
    }

    private void CalculateMovement()
    {
        _camForward = playerCamera.transform.forward;
        _camRight = playerCamera.transform.right;
        _camForward.y = 0;
        _camRight.y = 0;
        _camForward.Normalize();
        _camRight.Normalize();
        
        _moveDirection = _camRight * _moveInput.x + _camForward * _moveInput.y;

        Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    
}
