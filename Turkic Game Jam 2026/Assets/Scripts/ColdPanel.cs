using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColdPanel : MonoBehaviour
{
    public HotBar hotBar;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        Color tempColor = image.color;
        
        float t = Mathf.InverseLerp(0.6f, 0.2f, hotBar.slider.value);

        float maxAlpha = 15f / 255f; 
        tempColor.a = Mathf.Lerp(0.0f, maxAlpha, t);
        
        image.color = tempColor;
    }
}
