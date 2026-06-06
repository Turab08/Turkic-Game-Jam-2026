using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBar : MonoBehaviour
{
    public Slider slider;
    public Bonfire bonfire;

    public Gradient colorGradient;
    public Image fillImage;

    void Update()
    {
        slider.value = bonfire.currentSize;

        float normalizedValue = (slider.value - slider.minValue) / (slider.maxValue - slider.minValue);

        fillImage.color = colorGradient.Evaluate(normalizedValue);
    }


}
