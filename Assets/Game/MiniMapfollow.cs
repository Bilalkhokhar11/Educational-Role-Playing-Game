using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MinimapFollow : MonoBehaviour
{
    public Transform player;
    public float height = 20f;

    void LateUpdate()
    {
        Vector3 newPos = player.position;
        newPos.y += height;
        transform.position = newPos;

        transform.rotation = Quaternion.Euler(90f, 0f, 0f); // NO rotation with player
    }
}
