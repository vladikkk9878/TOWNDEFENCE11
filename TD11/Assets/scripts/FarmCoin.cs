using UnityEngine;
using UnityEngine.UI;

public class FarmCoin : MonoBehaviour
{
    [Header("Ссылки на базы данных")]
    [SerializeField] private GameData gameData;

    [Header("Настройки заполнения")]
    [SerializeField] private Image targetImage;
    [SerializeField] private int totalClick = 6;
    [SerializeField] private int rewardGold = 5;  

    private Button click_btn;

    private void Start()
    {
        click_btn = GetComponent<Button>();

        if (click_btn != null)
        {
            click_btn.onClick.AddListener(FillAndReward);
        }
        if (targetImage != null)
        {
            targetImage.fillAmount = 0f;
        }
    }

    private void OnDestroy()
    {
        if (click_btn != null)
        {
            click_btn.onClick.RemoveAllListeners();
        }
    }

    public void FillAndReward()
    {
        if (targetImage == null) return;
        targetImage.fillAmount += 1f / totalClick;
        Debug.Log($"Алмаз заполнен на: {targetImage.fillAmount * 100}%");

        if (targetImage.fillAmount >= 1f)
        {
            FullFill();
            if (gameData != null)
            {
                gameData.allGold += rewardGold;
                if (gameData.uiManager != null)
                {
                    gameData.uiManager.UpdateAllUI();
                }
            }
            else
            {
                Debug.LogError("Забыли перетащить объект GameData в скрипт FarmCoin!");
            }
        }
    }

    public void FullFill()
    {
        targetImage.fillAmount = 0f;
    }
}
