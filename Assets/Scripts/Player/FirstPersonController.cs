using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.EventSystems;

public class FirstPersonController : MonoBehaviour
{
    // variable initialization
    [SerializeField] private bool _canMove = true;

    #region Variable Declarations
    // with this syntax _isXing is only true when _canX is true and the proper button is pressed
    private bool _isSprinting => _canSprint && Input.GetKey(_sprintKeyCode);
    private bool _shouldCrouch => Input.GetKeyDown(_crouchKeyCode) && !_isInCrouchAnimation && _characterControllerComponent.isGrounded;

    [Header("Toggles")]
    [SerializeField] private bool _canSprint = true;
    [SerializeField] private bool _canCrouch = true;
    [SerializeField] private bool _canInteract = true;

    [Header("Controls")]
    [SerializeField] private KeyCode _sprintKeyCode = KeyCode.LeftShift;
    [SerializeField] private KeyCode _crouchKeyCode = KeyCode.LeftControl;
    [SerializeField] private KeyCode _interactKeyCode = KeyCode.E;

    [Header("Base Movement Parameters")]
    [SerializeField] private float _walkSpeed = 3.0f;
    [SerializeField] private float _sprintSpeed = 6.0f;
    [SerializeField] private float _crouchSpeed = 1.75f;
    [SerializeField] private float _gravity = 30.0f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float _lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float _lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float _upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float _lowerLookLimit = 80.0f;

    [Header("Crouch Parameters")]
    [SerializeField] private float _crouchHeight = 0.5f;
    [SerializeField] private float _standingHeight = 2.0f;
    [SerializeField] private float _crouchTime = 0.25f;
    [SerializeField] private Vector3 _crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector3 _standingCenter = Vector3.zero;
    private bool _isCrouching = false;
    private bool _isInCrouchAnimation = false;

    [Header("Interaction")]
    [SerializeField] private Vector3 _interactionRayPoint = default;
    [SerializeField] private float _interactionRayDistance = default;
    [SerializeField] private LayerMask _interactionLayer = default;
    [SerializeField] private Interactable _currentInteractable = null;
    private bool _isInteracting = false;

    private Camera _cameraComponent;
    private CinemachineVirtualCamera _virtualCameraComponent;
    private CharacterController _characterControllerComponent;

    private Vector3 _moveDirection;
    private Vector2 _currentMoveInput;

    private float _xRotation = 0;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        _cameraComponent = GetComponentInChildren<Camera>();
        _virtualCameraComponent = GetComponentInChildren<CinemachineVirtualCamera>();
        _characterControllerComponent = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        GameEventsManager.instance.playerEvents.onEnablePlayerMovement += EnablePlayerMovement;
        GameEventsManager.instance.playerEvents.onDisablePlayerMovement += DisablePlayerMovement;
        GameEventsManager.instance.playerEvents.onPlayerTeleportation += Teleport;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onEnablePlayerMovement -= EnablePlayerMovement;
        GameEventsManager.instance.playerEvents.onDisablePlayerMovement -= DisablePlayerMovement;
    }

    // Update is called once per frame
    void Update()
    {
       if (_canMove)
       {
            HandleMovementInput();
            HandleMouseLook();

            if (_canCrouch)
                HandleCrouch();

            if (_canInteract)
            {
                HandleInteractionCheck();
                HandleInteractionInput();
            }

            ApplyFinalMovements();
        }
    }
    
    private void HandleMovementInput()
    {
        // if _isSprinting is true, multiply movement by sprint speed, otherwise use walk speed
        _currentMoveInput = new Vector2((_isCrouching ? _crouchSpeed :_isSprinting ? _sprintSpeed : _walkSpeed) * Input.GetAxis("Vertical"),
                                        (_isCrouching ? _crouchSpeed : _isSprinting ? _sprintSpeed : _walkSpeed) * Input.GetAxis("Horizontal"));

        float moveInputY = _moveDirection.y;
        _moveDirection = (transform.TransformDirection(Vector3.forward) * _currentMoveInput.x) + (transform.TransformDirection(Vector3.right) * _currentMoveInput.y);
        _moveDirection = _moveDirection.normalized * Mathf.Clamp(_moveDirection.magnitude, 0, _isSprinting ? _sprintSpeed : _walkSpeed);
        _moveDirection.y = moveInputY;

    }

    private void HandleCrouch()
    {
        if (_shouldCrouch)
            StartCoroutine(CrouchStand());
    }

    private void HandleMouseLook()
    {
        _xRotation -= Input.GetAxis("Mouse Y") * _lookSpeedY;
        _xRotation = Mathf.Clamp(_xRotation, -_upperLookLimit, _lowerLookLimit);
        _virtualCameraComponent.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _lookSpeedX, 0);
    }

    private void HandleInteractionCheck()
    {
        if (Physics.Raycast(_cameraComponent.ViewportPointToRay(_interactionRayPoint), out RaycastHit hit, _interactionRayDistance))
        {
            // this allows the player to look at a new interactable even if focus is not broken from current one
            if (hit.collider.gameObject.layer == 3 && (_currentInteractable == null || hit.collider.gameObject.GetInstanceID() != _currentInteractable.GetInstanceID()))
            {
                hit.collider.gameObject.TryGetComponent(out _currentInteractable);
                if (_currentInteractable != null && !_isInteracting)
                {
                    _currentInteractable.Focus();
                    _isInteracting = true;
                }
            }
        }
        else if (_currentInteractable)
        {
            _currentInteractable.LoseFocus();
            _currentInteractable = null;
            _isInteracting = false;
        }
    }

    private void HandleInteractionInput()
    {
        if (Input.GetKeyDown(_interactKeyCode) && _currentInteractable != null && Physics.Raycast(_cameraComponent.ViewportPointToRay(_interactionRayPoint), out RaycastHit hit, _interactionRayDistance, _interactionLayer))
        {
            _currentInteractable.Interact();
        }
    }

    private void ApplyFinalMovements()
    {
        if (!_characterControllerComponent.isGrounded)
            _moveDirection.y -= _gravity * Time.deltaTime;

        _characterControllerComponent.Move(_moveDirection * Time.deltaTime);
    }

    private IEnumerator CrouchStand()
    {
        // if anything is directly above player, don't get back up
        if (_isCrouching && Physics.Raycast(_cameraComponent.transform.position, Vector3.up, 1f))
            yield break;

        _isInCrouchAnimation = true;

        float timeElapsed = 0;
        float targetHeight = _isCrouching ? _standingHeight : _crouchHeight;
        float currentHeight = _characterControllerComponent.height;
        Vector3 targetCenter = _isCrouching ? _standingCenter : _crouchingCenter;
        Vector3 currentCenter = _characterControllerComponent.center;

        while (timeElapsed < _crouchTime)
        {
            _characterControllerComponent.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / _crouchTime);
            _characterControllerComponent.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / _crouchTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _characterControllerComponent.height = targetHeight;
        _characterControllerComponent.center = targetCenter;

        _isCrouching = !_isCrouching;

        _isInCrouchAnimation = false;
    }

    private void EnablePlayerMovement()
    {
        _canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void DisablePlayerMovement()
    {
        _canMove = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Teleport()
    {
        transform.position = PlayerWorldInfo.GetTeleportToLocation().position;
        transform.rotation = PlayerWorldInfo.GetTeleportToLocation().rotation;
    }
}
