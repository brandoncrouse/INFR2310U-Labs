using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuState : State {
    Transform rotator;
    RectTransform menuBar;
    GameObject cam;
    float rotateSpeed = 10;
    public MenuState(GameObject[] gameObjects, StateManager manager) : base(gameObjects, manager) { }
    public override void Setup() {
        rotator = gameObjects[0].transform;
        cam = rotator.GetChild(0).gameObject;
        menuBar = gameObjects[1].GetComponent<RectTransform>();
        menuBar.anchoredPosition = Vector3.left * 500;
    }
    public override void Enter() {
        cam.SetActive(true);
        menuBar.DOAnchorPosX(0, .4f).SetDelay(1.2f);
    }
    public override void Update() {
        rotator.Rotate(Vector3.up * -Time.deltaTime * rotateSpeed);
    }
    public override void Exit() {
        cam.SetActive(false);
    }
}
