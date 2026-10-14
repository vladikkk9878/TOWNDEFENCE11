using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FillCoin : MonoBehaviour
{
    [Header("Ссылки на фичи")]
    [SerializeField] private GameData gameData;
    [SerializeField] private BattleSystem battleSystem; // Появится поле в инспекторе

    [Header("Шкала монетки")]
    [SerializeField] private Image coinImage;           // Появится поле в инспекторе
    [SerializeField] private float coinFillTime = 2.0f;

    [Header("Шкала атаки")]
    [SerializeField] private Slider attackSlider;       // Появится поле в инспекторе
    [SerializeField] private float timeToAttack = 30.0f;

    private void Start()
    {
        if (coinImage != null) coinImage.fillAmount = 0f;

        if (attackSlider != null)
        {
            attackSlider.minValue = 0f;
            attackSlider.maxValue = timeToAttack;
            attackSlider.value = 0f;
        }

        StartCoroutine(CoinFillingRoutine());
        StartCoroutine(AttackTimerRoutine());
    }

    private IEnumerator CoinFillingRoutine()
    {
        while (true)
        {
            float timer = 0f;
            while (timer < coinFillTime)
            {
                timer += Time.deltaTime;
                if (coinImage != null) coinImage.fillAmount = timer / coinFillTime;
                yield return null;
            }
            if (coinImage != null) coinImage.fillAmount = 0f;

            AddGoldFromCycle();
        }
    }

    private IEnumerator AttackTimerRoutine()
    {
        while (true)
        {
            float timer = 0f;
            while (timer < timeToAttack)
            {
                timer += Time.deltaTime;
                if (attackSlider != null) attackSlider.value = timer;
                yield return null;
            }
            if (attackSlider != null) attackSlider.value = 0f;

            if (battleSystem != null)
            {
                battleSystem.ResolveBattle();
            }
        }
    }

    private void AddGoldFromCycle()
    {
        if (gameData == null) return;

        int earn = 1 + gameData.kolvoCivillian;
        gameData.allGold += earn;

        if (gameData.uiManager != null) gameData.uiManager.UpdateAllUI();
    }
}