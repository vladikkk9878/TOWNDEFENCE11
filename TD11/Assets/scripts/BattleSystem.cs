using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    public void ResolveBattle()
    {
        if (gameData == null) return;

        int remainingEnemies = gameData.currentWaveEnemies;

        int waveSizeCheck = gameData.currentWaveEnemies;

        if (gameData.kolvoWarrior >= remainingEnemies)
        {
            gameData.kolvoWarrior -= remainingEnemies;
            remainingEnemies = 0;
            Debug.Log("Наши воины отбили атаку!");
        }
        else
        {
            remainingEnemies -= gameData.kolvoWarrior;
            gameData.kolvoWarrior = 0;
            Debug.Log("Воины пали, враги прорвались к крестьянам!");
        }

        if (remainingEnemies > 0)
        {
            int killedCivilians = remainingEnemies * 3;
            if (killedCivilians > gameData.kolvoCivillian) killedCivilians = gameData.kolvoCivillian;

            gameData.kolvoCivillian -= killedCivilians;

            if (gameData.kolvoCivillian <= 0 && gameData.gameManager != null)
            {
                gameData.gameManager.TriggerGameOver();
                return;
            }
        }

        if (waveSizeCheck == 192 && gameData.kolvoCivillian > 0)
        {
            if (gameData.gameManager != null)
            {
                gameData.gameManager.TriggerGameWin();
                return;
            }
        }

        gameData.currentWaveEnemies *= gameData.waveMultiplier;
        if (gameData.uiManager != null) gameData.uiManager.UpdateAllUI();
    }
}