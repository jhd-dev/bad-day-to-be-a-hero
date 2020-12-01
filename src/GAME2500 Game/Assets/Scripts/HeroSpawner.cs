using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave {
        public int[] heroes;
    }

    [SerializeField] int timeBeforeFirstWave;
    [SerializeField] int timeBetweenHeroes;
    [SerializeField] int timeBetweenWaves;
    [SerializeField] GameObject[] heroTypes;
    [SerializeField] Wave[] waves;
    [SerializeField] Text waveDisplay;

    void Start() {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn() {
        waveDisplay.text = "PREPARE FOR ATTACK";
        yield return new WaitForSeconds(timeBeforeFirstWave);

        for (int i = 0; i < waves.Length; i++) {
            waveDisplay.text = "WAVE " + (i + 1).ToString();
            for (int j = 0; j < waves[i].heroes.Length; j++) {
                int heroType = waves[i].heroes[j];
                Instantiate(heroTypes[heroType], transform);
                yield return new WaitForSeconds(timeBetweenHeroes);
            }
            if (i == waves.Length - 1) waveDisplay.text = "Waves Completed!";
            else waveDisplay.text = "Next Wave in " + timeBetweenWaves.ToString() + " sec";
            yield return new WaitForSeconds(timeBetweenWaves);
        }

        print("All Waves Concluded.");
    }
}
