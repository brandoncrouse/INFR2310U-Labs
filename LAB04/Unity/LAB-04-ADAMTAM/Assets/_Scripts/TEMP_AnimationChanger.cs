using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_AnimationChanger : MonoBehaviour {
    Animator animator;
    private void Awake() {
        animator = GetComponent<Animator>();
    }
    void Start() {
        animator.SetFloat("Blend", .2f);
    }

    void Update() {

    }
}
