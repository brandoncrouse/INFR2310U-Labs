using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    private State currentState;
    private StateManager stateManager;

    public GameObject[] TitleObjects;
    public GameObject[] MenuObjects;

    public event Action<int> MenuInteractEvent;
    private void Awake() {
        if (Instance != null && Instance != this) Destroy(this); else Instance = this;
        stateManager = new StateManager(this);
    }
    private void Start() {
        currentState = stateManager.Title();
        currentState.Enter();
    }

    public void SwitchState(State state) {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    private void Update() {
        currentState.Update();
    }

    public void Interact(int index) {
        MenuInteractEvent?.Invoke(index);
    }
}
