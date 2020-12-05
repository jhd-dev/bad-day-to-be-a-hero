using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Treasure : MonoBehaviour
{
    [SerializeField] int maxWealth = 100;
    [SerializeField] Slider slider;
    [SerializeField] Sprite lowDamageSpr;
    [SerializeField] Sprite medDamageSpr;
    [SerializeField] Sprite hiDamageSpr;
    SpriteRenderer spriteRenderer;
    int wealth = 100;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        wealth = maxWealth;
        ChooseSprite(1f);
    }

    public void GetPillaged(int amountStolen) {
        wealth -= amountStolen;
        float relativeWealth = (float)wealth / (float)maxWealth;
        ChooseSprite(relativeWealth);
        slider.value = relativeWealth;
        if (wealth <= 0) {
            GameStatus.isGameOver = true;
        }
    }

    void ChooseSprite(float ratio)
    {
        if (ratio >= 2f / 3f)
            spriteRenderer.sprite = lowDamageSpr;
        else if (ratio >= 1f / 3f)
            spriteRenderer.sprite = medDamageSpr;
        else
            spriteRenderer.sprite = hiDamageSpr;
    }
}
