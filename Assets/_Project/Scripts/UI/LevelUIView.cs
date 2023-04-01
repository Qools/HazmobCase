using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIView : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private void Update()
    {
        if (GameManager.Instance.isGameStarted) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            EventSystem.CallGameStarted();
            Utility.EnablePanel(canvasGroup, false);
        }
    }
}
