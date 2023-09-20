using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour {
    [SerializeField] PathManager pathManager;
    [SerializeField] Animator animator;

    readonly string WALKING = "isWalking";

    List<Waypoint> path;
    Waypoint target;

    public float moveSpeed;
    public float rotateSpeed;
    bool isWalking;
    int index;

    void Start() {
        isWalking = false;
        animator.SetBool(WALKING, false);
        path = pathManager.GetPath();
        if (path != null && path.Count > 0) target = path[0];
    }

    void RotateTowardsTarget() {
        float stepSize = rotateSpeed * Time.deltaTime;
        Vector3 targetDirection = target.Position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, stepSize, 0);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void MoveForward() {
        float stepSize = moveSpeed * Time.deltaTime;
        float distanceToTarget = Vector3.Distance(transform.position, target.Position);
        if (distanceToTarget < stepSize) {
            return;
        }

        Vector3 moveDirection = Vector3.forward;
        transform.Translate(moveDirection * stepSize);
    }

    void Update() {
        if (Input.anyKeyDown) {
            isWalking = !isWalking;
            animator.SetBool(WALKING, isWalking);
        }

        if (!isWalking) return;
        RotateTowardsTarget();
        MoveForward();
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Waypoint") && other.name == $"{index}") return;
        index++;
        target = pathManager.GetNext(); 
    }
}
