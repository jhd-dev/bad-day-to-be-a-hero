using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroSpawner : MonoBehaviour
{
    [System.Serializable]
    public class HeroSpawn
    {
        public int heroID;
        public int spawnPointID;
    }

    [System.Serializable]
    public class Wave
    {
        public HeroSpawn[] heroSpawns;
    }

    [SerializeField] int timeBeforeFirstWave;
    [SerializeField] int timeBetweenHeroes;
    [SerializeField] int timeBetweenWaves;
    [SerializeField] GameObject[] heroTypes;
    [SerializeField] Wave[] waves;
    [SerializeField] Text waveDisplay;
    [SerializeField] List<Transform> spawnPoints;

    void Start() {
        StartCoroutine("Spawn");
        waveDisplay.text = "PREPARE FOR ATTACK";
    }

    IEnumerator Spawn() {
        yield return new WaitForSeconds(timeBeforeFirstWave);

        for (int i = 0; i < waves.Length; i++)
        {
            // During a wave

            waveDisplay.text = "WAVE " + (i + 1).ToString();

            for (int j = 0; j < waves[i].heroSpawns.Length; j++)
            {
                int heroType = waves[i].heroSpawns[j].heroID;
                int spawnID = waves[i].heroSpawns[j].spawnPointID;
                Instantiate(heroTypes[heroType], spawnPoints[spawnID]);
                yield return new WaitForSeconds(timeBetweenHeroes);
            }

            // After the wave

            if (i == waves.Length - 1) waveDisplay.text = "Waves Completed!";
            else waveDisplay.text = "Next Wave in " + timeBetweenWaves.ToString() + " sec";

            // Before the next wave

            yield return new WaitForSeconds(timeBetweenWaves);
        }

        print("All Waves Concluded.");
    }
}
