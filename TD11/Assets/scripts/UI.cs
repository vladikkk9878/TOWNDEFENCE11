using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI civiliansText;
    [SerializeField] private TextMeshProUGUI warriorsText;
    [SerializeField] private TextMeshProUGUI goldText;
    
    private void UpdateGoldUI()
    {
        if (goldText != null) goldText.text = $"Всего золота: {allGold}";
        //if (buyCivillianBtn != null) buyCivillianBtn.interactable = (allGold >= priceCivillian);
        //if (buyWarriorBtn != null) buyWarriorBtn.interactable = (allGold >= priceWarrior);
    }
}
