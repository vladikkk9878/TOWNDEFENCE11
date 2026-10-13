using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [Serialaizefield] private image targetImage;
    [Serialaizefield] private int totalClick = 5;
    [Serialaizefield] private int
    private float click;

    private void Start()
    {
        if (targetImage != null) 
        {
            targetImage.fillAmount = 0f;
        }

        step = 1f / totalClicksRequired;
    }

    public void FillAndReward()
    {
        if (fillImage == null) return;

        fillImage.fillAmount += step;

        if (fillImage.fillAmount >= 1f)
        {
            OnFullFill();
        }


    }
    public void FullFill()
    {
        fillImage.fillAmount = 0f;
    }
