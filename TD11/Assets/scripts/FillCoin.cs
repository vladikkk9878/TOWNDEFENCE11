using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FillCoin : MonoBehaviour
{
    [Header("Шкала и золото")]
    [SerializeField] private Image coinImage;
    [SerializeField] private float Time = 1.0f;
    
    private IEnumerator FillingRoutine()
    {
        while (true)
        {
            float timer = 0f;
            while (timer < Time)
            {
                timer += UnityEngine.Time.deltaTime;
                coinImage.fillAmount = timer / Time;
                yield return null;
            }
            coinImage.fillAmount = 0f;
            int earn = 1 + kolvoCivillian;
            Gold += earn;
            UpdateGoldUI();
        }
    }
}
