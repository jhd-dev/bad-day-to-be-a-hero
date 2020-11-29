using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start() {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn() {
        yield return new WaitForSeconds(timeBeforeFirstWave);

        for (int i = 0; i < waves.Length; i++) {
            for (int j = 0; j < waves[i].heroes.Length; j++) {
                int heroType = waves[i].heroes[j];
                Instantiate(heroTypes[heroType], transform);
                yield return new WaitForSeconds(timeBetweenHeroes);
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }

        print("All Waves Concluded.");
    }
}
