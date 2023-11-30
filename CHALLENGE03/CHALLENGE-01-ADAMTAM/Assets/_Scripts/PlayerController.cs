using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, PlayerInputActions.IPlayActions {
    private PlayerInputActions inputActions;
    private Rigidbody rb;
    private Transform cam;
    private CapsuleCollider coll;
    Vector3 rawMove = Vector3.zero, relMove = Vector3.zero;
    Vector2 lookDelta;
    float verticalRotation = 0;
    [SerializeField] CinemachineVirtualCamera vcam;
    [SerializeField] float moveSpeed, jumpSpeed, sens, gravity, interactionRange;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform rotatable;
    bool grounded;

    public Transform PlayerTransform { get { return transform; } }

    private void Awake() {
        inputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();
        cam = vcam.transform;
        Cursor.lockState = CursorLockMode.Locked;
        coll = GetComponent<CapsuleCollider>();
    }
    private void OnEnable() {
        inputActions.Play.SetCallbacks(this);
    }
    void Start() {
        inputActions.Play.Enable();
    }

    void Update() {
        // calculate input relative to camera
        relMove = cam.right * rawMove.x + cam.forward * rawMove.z;
        relMove.y = 0;
        relMove = relMove.normalized;
    }

    private void FixedUpdate() {
        // movement
        Vector3 temp = relMove * moveSpeed;
        temp.y = rb.velocity.y;
        rb.velocity = temp;

        // grounded check
        grounded = Physics.Raycast(coll.bounds.center, Vector3.down, coll.bounds.extents.y + .2f, groundLayer);

        // gravity
        if (!grounded) rb.velocity += Vector3.down * gravity;

        // Check for interactables
        Ray interactableRay = new Ray(cam.position, cam.forward);
        if (Physics.Raycast(interactableRay, out RaycastHit hit, 1)) {
            if (hit.transform is IInteractable) {
                print("whefiwf");
            }
        }
    }
    private void LateUpdate() {
        verticalRotation = Mathf.Clamp(verticalRotation - lookDelta.y * sens * Time.deltaTime, -90, 90);
        cam.eulerAngles = new Vector3(verticalRotation, cam.eulerAngles.y + lookDelta.x * sens * Time.deltaTime);
        transform.eulerAngles = Vector3.up * transform.eulerAngles.y;

        rotatable.rotation = cam.rotation;
        rotatable.eulerAngles = new Vector3(0, rotatable.eulerAngles.y, 0);
    }
    public void OnMove(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            Vector2 input = context.ReadValue<Vector2>();
            rawMove.x = input.x;
            rawMove.z = input.y;
        } else if (context.phase == InputActionPhase.Canceled) {
            rawMove = Vector3.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context) {
        lookDelta = context.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext context) {
        throw new System.NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (grounded) rb.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
    }
}

public interface IInteractable {
    public void Interact(PlayerController controller);
}