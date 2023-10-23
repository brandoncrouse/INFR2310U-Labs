using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentController : MonoBehaviour {
    [SerializeField] NavMeshAgent agent;
    Camera cam;
    void Start() {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButton(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                agent.SetDestination(hit.point);
            }
        }
    }
}