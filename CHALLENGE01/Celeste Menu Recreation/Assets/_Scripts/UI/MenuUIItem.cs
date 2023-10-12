using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class MenuUIItem : MonoBehaviour {
    [SerializeField] TextMeshProUGUI text;
    public void Hover() {
        text.DOColor(Color.cyan, .2f);
    }

    public void UnHover() {
        text.DOColor(Color.white, .2f);
    }
}
