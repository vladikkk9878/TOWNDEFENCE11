using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Resource : MonoBehaviour
{
    [Header("Ссылки на Менеджеры")]
    [SerializeField] private GameManager gameManager;

    [Header("Шкала и золото")]
    [SerializeField] private Image coinImage;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private float Time = 1.0f;

    [Header("Крестьяне")]
    [SerializeField] private TextMeshProUGUI civiliansText;
    [SerializeField] private Button buyCivillianBtn;
    [SerializeField] private int priceCivillian = 5;

    [Header("Воины")]
    [SerializeField] private TextMeshProUGUI warriorsText;
    [SerializeField] private Button buyWarriorBtn;
    [SerializeField] private int priceWarrior = 10;

    [Header("Настройки Потребления")]
    [SerializeField] private float consumptionInterval = 4.0f;
    [SerializeField] private int goldPerWarrior = 8;

    [Header("Таймер Атаки и Враги")]
    [SerializeField] private Slider attackSlider;
    [SerializeField] private TextMeshProUGUI waveInfoText;
    [SerializeField] private float timeToAttack = 30.0f;
    [SerializeField] private int waveMultiplier = 2;

    private int kolvoWarrior = 0;
    private int allGold = 0;
    private int kolvoCivillian = 0;

    private int currentWaveEnemies = 3;

    private void Start()
    {
        if (coinImage != null) coinImage.fillAmount = 0f;

        if (attackSlider != null)
        {
            attackSlider.minValue = 0f;
            attackSlider.maxValue = timeToAttack;
            attackSlider.value = 0f;
        }

        UpdateGoldUI();
        UpdateCiviliansUI();
        UpdateWarriorsUI();
        UpdateWaveUI();

        StartCoroutine(FillingRoutine());
        StartCoroutine(ConsumptionRoutine());
        StartCoroutine(AttackTimerRoutine());
    }

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
            allGold += earn;
            UpdateGoldUI();
        }
    }

    private IEnumerator ConsumptionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(consumptionInterval);
            if (kolvoWarrior <= 0) continue;

            int totalRequiredGold = kolvoWarrior * goldPerWarrior;
            if (allGold >= totalRequiredGold)
            {
                allGold -= totalRequiredGold;
            }
            else
            {
                int availableGold = allGold;
                allGold = 0;
                int deficit = totalRequiredGold - availableGold;
                int deadWarriors = Mathf.CeilToInt((float)deficit / goldPerWarrior);

                if (deadWarriors > kolvoWarrior) deadWarriors = kolvoWarrior;

                kolvoWarrior -= deadWarriors;
                UpdateWarriorsUI();
            }
            UpdateGoldUI();
        }
    }

    private IEnumerator AttackTimerRoutine()
    {
        while (true)
        {
            float timer = 0f;

            while (timer < timeToAttack)
            {
                timer += UnityEngine.Time.deltaTime;
                if (attackSlider != null)
                {
                    attackSlider.value = timer;
                }
                yield return null;
            }

            ResolveBattle();

            currentWaveEnemies *= waveMultiplier;
            UpdateWaveUI();

            if (attackSlider != null) attackSlider.value = 0f;
        }
    }

    private void ResolveBattle()
    {
        Debug.LogWarning($"НАПАДЕНИЕ! Деревню атакуют {currentWaveEnemies} врагов!");

        int remainingEnemies = currentWaveEnemies;

        if (kolvoWarrior >= remainingEnemies)
        {
            kolvoWarrior -= remainingEnemies;
            remainingEnemies = 0;
            Debug.Log("Наши воины отбили атаку! Все враги уничтожены");
        }
        else
        {
            remainingEnemies -= kolvoWarrior;
            kolvoWarrior = 0;
            Debug.Log("Все наши воины пали в бою! Враги прорвались к крестьянам");
        }
        UpdateWarriorsUI();

        if (remainingEnemies > 0)
        {
            int killedCivilians = remainingEnemies * 3;

            if (killedCivilians > kolvoCivillian)
            {
                killedCivilians = kolvoCivillian;
            }

            kolvoCivillian -= killedCivilians;
            Debug.LogError($"Враги устроили резню! Погибло крестьян: {killedCivilians}");
            UpdateCiviliansUI();

            if (kolvoCivillian <= 0)
            {
                if (gameManager != null)
                {
                    gameManager.TriggerGameOver();
                }
                else
                {
                    Debug.LogError("Забыли перетащить объект GameManager в инспектор RESOURCESCRIPT!");
                }
            }
        }
    }

    public void BuyCivillians()
    {
        if (allGold >= priceCivillian)
        {
            allGold -= priceCivillian;
            kolvoCivillian += 1;
            UpdateGoldUI();
            UpdateCiviliansUI();
        }
    }

    public void BuyWarrior()
    {
        if (allGold >= priceWarrior)
        {
            allGold -= priceWarrior;
            kolvoWarrior += 1;
            UpdateGoldUI();
            UpdateWarriorsUI();
        }
    }

    private void UpdateGoldUI()
    {
        if (goldText != null) goldText.text = $"Всего золота: {allGold}";
        if (buyCivillianBtn != null) buyCivillianBtn.interactable = (allGold >= priceCivillian);
        if (buyWarriorBtn != null) buyWarriorBtn.interactable = (allGold >= priceWarrior);
    }

    private void UpdateCiviliansUI()
    {
        if (civiliansText != null) civiliansText.text = kolvoCivillian.ToString();
    }

    private void UpdateWarriorsUI()
    {
        if (warriorsText != null) warriorsText.text = kolvoWarrior.ToString();
    }

    private void UpdateWaveUI()
    {
        if (waveInfoText != null)
        {
            waveInfoText.text = $"Кол-во врагов: {currentWaveEnemies}";
        }
    }
}