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

    public Animator hotBarAnimator;

    public Image currentImage;
    public Sprite fireImage;
    public Sprite snowFlakeImage;

    void Update()
    {
        slider.value = bonfire.currentSize;

        float normalizedValue = (slider.value - slider.minValue) / (slider.maxValue - slider.minValue);

        fillImage.color = colorGradient.Evaluate(normalizedValue);

        if (slider.value <= (slider.maxValue + slider.minValue) / 2)
        {
            hotBarAnimator.SetBool("IsCold", true);
            currentImage.sprite = snowFlakeImage;
        }
        else
        {
            hotBarAnimator.SetBool("IsCold", false);
            currentImage.sprite = fireImage;
        }
    }
}
