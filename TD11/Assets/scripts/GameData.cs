using UnityEngine;

public class GameData : MonoBehaviour
{

    [Header("Менеджеры")]
    public UIManager uiManager;
    public GameManager gameManager;

    [Header("Базовые значения ресурсов")]
    public int allGold = 0;
    public int kolvoCivillian = 0;
    public int kolvoWarrior = 0;
    public int currentWaveEnemies = 3;

    [Header("Цены найма")]
    public int priceCivillian = 5;
    public int priceWarrior = 10;

    [Header("Настройки голода и волн")]
    public float consumptionInterval = 4.0f;
    public int goldPerWarrior = 8;
    public int waveMultiplier = 2; 
}
