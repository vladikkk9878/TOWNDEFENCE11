using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Ссылка на базу данных")]
    [SerializeField] private GameData gameData;

    [Header("Текстовые элементы")]
    [SerializeField] private TextMeshProUGUI goldText;       // Сюда перетащи текст 'resource' (золото)
    [SerializeField] private TextMeshProUGUI civiliansText;  // Сюда перетащи левый текст 'clv/wer' (жители)
    [SerializeField] private TextMeshProUGUI warriorsText;   // Сюда перетащи правый текст 'clv/wer' (воины)
    [SerializeField] private TextMeshProUGUI waveInfoText;   // Сюда перетащи текст 'kolvovragov' (враги)

    [Header("Кнопки найма")]
    [SerializeField] private Button buyCivillianBtn;         // Сюда перетащи кнопку 'PlusCivilian'
    [SerializeField] private Button buyWarriorBtn;           // Сюда перетащи кнопку 'PlusVoin'

    private void Start()
    {
        // При старте принудительно выводим начальные цифры на экран
        UpdateAllUI();
    }

    // Этот метод вызывают скрипты GoldIncome, UnitRecruiter, WarriorHunger и BattleSystem
    // каждый раз, когда меняют какую-то цифру в GameData
    public void UpdateAllUI()
    {
        if (gameData == null) return;

        // Берем чистые данные из GameData и превращаем в текст на экране
        if (goldText != null) goldText.text = $"Всего золота: {gameData.allGold}";
        if (civiliansText != null) civiliansText.text = gameData.kolvoCivillian.ToString();
        if (warriorsText != null) warriorsText.text = gameData.kolvoWarrior.ToString();
        if (waveInfoText != null) waveInfoText.text = $"Кол-во врагов: {gameData.currentWaveEnemies}";

        // Проверяем, хватает ли золота на кнопки, и включаем/выключаем их
        if (buyCivillianBtn != null) buyCivillianBtn.interactable = (gameData.allGold >= gameData.priceCivillian);
        if (buyWarriorBtn != null) buyWarriorBtn.interactable = (gameData.allGold >= gameData.priceWarrior);
    }
}