using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapplayericon : MonoBehaviour
{ 
    [Header("Player")]
    public Transform player;                  // Player in world
    public RectTransform minimapRect;         // The minimap UI RawImage RectTransform
    public RectTransform icon;                // The icon on the minimap
    public Vector2 terrainOrigin = Vector2.zero; // Bottom-left world position of terrain
    public Vector2 terrainSize = new Vector2(50f, 50f); // Width/length of terrain in world units

    [Header("Shop POI")]
    public Transform shopWorld;               // World position of the shop
    public RectTransform shopIcon;            // The shop icon on the minimap
    public float radarRange = 50f;            // Range of radar in world units
    public float snapInDistance = 30f;        // Distance where icon starts appearing inside

    void Update()
    {
        // -------- Player icon positioning --------
        float relX = player.position.x - terrainOrigin.x;
        float relZ = player.position.z - terrainOrigin.y;
        icon.anchoredPosition = new Vector2(-150f, -150f);

        float normX = Mathf.Clamp01(relX / terrainSize.x);
        float normY = Mathf.Clamp01(relZ / terrainSize.y);

        float uiX = (normX - 0.5f) * minimapRect.rect.width;
        float uiY = (normY - 0.5f) * minimapRect.rect.height;

        icon.anchoredPosition = new Vector2(uiX, uiY);
        icon.localRotation = Quaternion.Euler(0, 0, -player.eulerAngles.y);

        // -------- Shop icon positioning --------
        Vector2 playerPos = new Vector2(player.position.x, player.position.z);
        Vector2 shopPos = new Vector2(shopWorld.position.x, shopWorld.position.z);
        Vector2 offset = shopPos - playerPos;
        float distance = offset.magnitude;

        float halfMapRadius = Mathf.Min(minimapRect.rect.width, minimapRect.rect.height) * 0.5f;
        Vector2 mapOffset = offset / radarRange * halfMapRadius;

        if (distance > snapInDistance)
        {
            // Clamp to circular edge
            mapOffset = mapOffset.normalized * halfMapRadius;
        }
        else
        {
            // Freely move inside circular range
            mapOffset = Vector2.ClampMagnitude(mapOffset, halfMapRadius);
        }

        shopIcon.anchoredPosition = mapOffset;
        // 🔒 No rotation applied to shopIcon
    }
}
