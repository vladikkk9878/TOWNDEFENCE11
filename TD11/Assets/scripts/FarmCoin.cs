using UnityEngine;
using UnityEngine.UI;


public class FarmCoin : MonoBehaviour
{
    [SerializeField] private Info info;
    [SerializeField] private Image targetImage;
    [SerializeField] private int totalClick = 5;
    private Button click_btn;
    private float click;

    private void Start()
    {
        click_btn = GetComponent<Button>();
        click_btn.onClick.AddListener(FillAndReward);
        
        if (targetImage != null)
        {
            targetImage.fillAmount = 0f;
        }
    }

    private void OnDestroy()
    {
        click_btn.onClick.RemoveAllListeners();
    }
    public void FillAndReward()
    {
        Debug.Log("Заполнение начинается");
        if (targetImage == null) return;

        targetImage.fillAmount += 1f / totalClick;

        if (targetImage.fillAmount >= 1f)
        {
            FullFill();
            info.AddAllGold(10);
        }
    }
    
    public void FullFill()
    {
        targetImage.fillAmount = 0f;
    }
}
