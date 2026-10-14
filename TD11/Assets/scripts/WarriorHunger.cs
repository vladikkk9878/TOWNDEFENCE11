using UnityEngine;
using System.Collections;

public class WarriorHunger : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    private void Start()
    {
        StartCoroutine(ConsumptionRoutine());
    }

    private IEnumerator ConsumptionRoutine()
    {
        while (true)
        {
            if (gameData == null) { yield return null; continue; }

            yield return new WaitForSeconds(gameData.consumptionInterval);
            if (gameData.kolvoWarrior <= 0) continue;

            int totalRequiredGold = gameData.kolvoWarrior * gameData.goldPerWarrior;
            if (gameData.allGold >= totalRequiredGold)
            {
                gameData.allGold -= totalRequiredGold;
            }
            else
            {
                int availableGold = gameData.allGold;
                gameData.allGold = 0;
                int deficit = totalRequiredGold - availableGold;
                int deadWarriors = Mathf.CeilToInt((float)deficit / gameData.goldPerWarrior);

                if (deadWarriors > gameData.kolvoWarrior) deadWarriors = gameData.kolvoWarrior;
                gameData.kolvoWarrior -= deadWarriors;
            }
            if (gameData.uiManager != null) gameData.uiManager.UpdateAllUI();
        }
    }
}