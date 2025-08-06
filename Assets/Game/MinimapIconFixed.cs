using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIconFixed : MonoBehaviour
{
    public RectTransform icon; // the player icon

    void Update()
    {
        // Keep the icon fixed in place (e.g., -150 x position)
        icon.anchoredPosition = Vector2.zero;

        // Do NOT rotate the icon — it should stay upright
        //icon.localRotation = Quaternion.identity;
    }
}