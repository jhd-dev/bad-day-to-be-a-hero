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

    public GameObject healthBar;

    [SerializeField] int timeBeforeFirstWave;
    [SerializeField] int timeBetweenHeroes;
    [SerializeField] int timeBetweenWaves;
    [SerializeField] GameObject[] heroTypes;
    [SerializeField] Wave[] waves;
    [SerializeField] Text waveDisplay;
    [SerializeField] List<Transform> spawnPoints;
    int timeTillNextWave;

    void Start() {
        StartCoroutine("Spawn");
        waveDisplay.text = "PREPARE FOR ATTACK";
    }

    IEnumerator Spawn() {
        yield return new WaitForSeconds(timeBeforeFirstWave);

        for (int w = 0; w < waves.Length; w++) {
            // Before the wave
            waveDisplay.text = "WAVE " + (w + 1).ToString();
            List<GameObject> existingHeroes = new List<GameObject>();

            // Spawn the appropriate heroes on a certain time interval
            for (int h = 0; h < waves[w].heroSpawns.Length; h++){
                int heroType = waves[w].heroSpawns[h].heroID;
                int spawnID = waves[w].heroSpawns[h].spawnPointID;
                // Health bar shiz
                GameObject currentHero = Instantiate(heroTypes[heroType], spawnPoints[spawnID]);
                GameObject hb = Instantiate(healthBar);
                hb.transform.parent = currentHero.transform;
                hb.transform.localPosition = new Vector3(1.8f, .8f, 0f);
                hb.GetComponent<HeroHealthBarScript>().hero = currentHero;

                existingHeroes.Add(currentHero);
                yield return new WaitForSeconds(timeBetweenHeroes);
            }

            // Wait until all the wave's heroes are dead
            yield return new WaitUntil(() => AllDead(existingHeroes));

            // Timer until next wave (if we aren't on the final wave already)
            if (w != waves.Length - 1) {
                timeTillNextWave = timeBetweenWaves;
                while (timeTillNextWave > 0) {
                    waveDisplay.text = "Wave " + (w + 2).ToString() + " in " + timeTillNextWave.ToString();
                    yield return new WaitForSeconds(1);
                    timeTillNextWave--;
                }
            }
        }

        waveDisplay.text = "You Win!";
    }

    bool AllDead(List<GameObject> heroes)
    {
        for (int i = 0; i < heroes.Count; i++)
            if (heroes[i] != null) return false;

        return true;
    }

}
