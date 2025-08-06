using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symblrotaion : MonoBehaviour
{
    public Transform player;
    //public float height = 20f;

    void LateUpdate()
    {
        transform.position = player.position + Vector3.up;// * height;

        // Rotate with player’s Y angle
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}