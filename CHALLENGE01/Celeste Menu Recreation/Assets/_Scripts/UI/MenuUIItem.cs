using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class MenuUIItem : MonoBehaviour {
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] MenuOverride menuOverride;

    RectTransform rect;
    float anchorY;
    int ID;
    enum MenuOverride {
        None,
        Climb
    }
    private void Awake() {
        rect = GetComponent<RectTransform>();
        anchorY = rect.anchoredPosition.y;
        print($"{rect}, {anchorY}");
        ID = GetInstanceID();
    }
    public void Hover() {
        DOTween.Kill(ID);
        text.DOColor(Color.cyan, .2f);
        if (menuOverride == MenuOverride.None) {
            rect.DOAnchorPosX(230, .25f).SetEase(Ease.OutQuint).SetDelay(.1f).SetId(ID);
            rect.DOAnchorPosY(anchorY, .5f);
        }
    }

    public void UnHover() {
        DOTween.Kill(ID);
        text.DOColor(Color.white, .2f);
        if (menuOverride == MenuOverride.None) {
            rect.DOAnchorPosX(200, .25f).SetEase(Ease.OutQuint).SetId(ID);
        }
    }
}
