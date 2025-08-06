using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class L2C : MonoBehaviour
{
    public CanvasGroup bgGroup;     // Background frame
    public CanvasGroup imageGroup;  // Foreground image
    public float fadeDuration = 1f;
    public float delayBetween = 0.3f;
    [SerializeField] private float delaySeconds = 7f;
    [SerializeField] GameObject delayedObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Cursor.visible = true;
        
    }
} 
    