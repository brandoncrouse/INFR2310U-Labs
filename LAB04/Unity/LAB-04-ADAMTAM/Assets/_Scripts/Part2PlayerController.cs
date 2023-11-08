using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part2PlayerController : MonoBehaviour {
    Rigidbody rb;
    Collider col;
    [SerializeField] float gravity, moveSpeed, acceleration, rotateSpeed;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform model;
    [SerializeField] Vector3 offset;
    Transform cam;
    bool grounded;
    Vector3 input, direction, camVel;
    private void Awake() {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        cam = Camera.main.transform;
    }
    void Start() {

    }

    void Update() {
        CalculateInputAndDirection();
    }

    private void FixedUpdate() {
        CheckGrounded();
        Move();
        AddGravity();
        HandleRotation();
    }

    private void LateUpdate() {
        cam.position = Vector3.SmoothDamp(cam.position, transform.position + offset, ref camVel, .3f);
    }
    void CalculateInputAndDirection() {
        input = (Vector3.right * Input.GetAxisRaw("Horizontal") + Vector3.forward * Input.GetAxisRaw("Vertical")).normalized;

        Vector3 camForward = input.z * cam.forward;
        Vector3 camRight = input.x * cam.right;
        direction = camForward + camRight;
    }
    void Move() {
        Vector3 maxVelocity = direction * moveSpeed;
        float maxSpeedChange = acceleration * Time.fixedDeltaTime;

        Vector3 newPosition = Vector3.Lerp(rb.velocity, maxVelocity, maxSpeedChange);

        rb.velocity += newPosition - rb.velocity;
    }
    void AddGravity() {
        if (!grounded) rb.velocity += Vector3.down * gravity;
    }

    void CheckGrounded() {
        Ray ray = new Ray(col.bounds.center, Vector3.down);
        if (Physics.Raycast(ray, col.bounds.extents.y + .1f, groundLayer)) {
            grounded = true;
        } else {
            grounded = false;
        }
    }

    private void HandleRotation() {
        Quaternion playerRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.identity;

        if (input != Vector3.zero) {
            targetRotation = Quaternion.LookRotation(direction);
        }

        if (input.sqrMagnitude != 0) {
            transform.rotation = Quaternion.Slerp(playerRotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

    }
}
