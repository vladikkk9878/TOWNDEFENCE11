using UnityEngine;

public class UnitRecruiter : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    public void BuyCivillians()
    {
        if (gameData == null) return;

        if (gameData.allGold >= gameData.priceCivillian)
        {
            gameData.allGold -= gameData.priceCivillian;
            gameData.kolvoCivillian += 1;
            if (gameData.uiManager != null) gameData.uiManager.UpdateAllUI();
        }
    }

    public void BuyWarrior()
    {
        if (gameData == null) return;

        if (gameData.allGold >= gameData.priceWarrior)
        {
            gameData.allGold -= gameData.priceWarrior;
            gameData.kolvoWarrior += 1;
            if (gameData.uiManager != null) gameData.uiManager.UpdateAllUI();
        }
    }
}