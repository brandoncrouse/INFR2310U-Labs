using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DitzelGames.FastIK;

public class Part2_PlayerController : MonoBehaviour {
    [SerializeField] FastIKFabric[] iks;
    void Start() {

    }

    void Update() {

    }

    void SetIKs(bool active) {
        foreach (FastIKFabric ik in iks) {
            ik.enabled = active;
        }
    }
}
