using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Treasure : MonoBehaviour
{
    [SerializeField] int maxWealth = 100;
    [SerializeField] Slider slider;
    int wealth = 100;

    void Start()
    {
        wealth = maxWealth;
    }

    public void GetPillaged(int amountStolen) {
        wealth -= amountStolen;
        slider.value = (float)wealth / (float)maxWealth;
        if (wealth <= 0) {
            print("Game Over");
        }
    }
}
