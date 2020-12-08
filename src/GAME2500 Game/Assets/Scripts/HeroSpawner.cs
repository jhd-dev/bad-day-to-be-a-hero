using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroSpawner : MonoBehaviour
{
    [System.Serializable]
    public class HeroType
    {
        public string name;
        public GameObject gameobject;
    }

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
    [SerializeField] HeroType[] heroTypes;
    [SerializeField] Wave[] waves;
    [SerializeField] Text waveDisplay;
    [SerializeField] Text waveHeroesDisplay;
    [SerializeField] List<BlockerFade> blockers; // should correspond to index of spawn point
    [SerializeField] List<Transform> spawnPoints;
    int timeTillNextWave;
    List<int> openBlockers = new List<int>();

    void Start() {
        StartCoroutine("Spawn");
        waveDisplay.text = "WAVE 1 APPROACHING - GET READY";
        waveHeroesDisplay.text = JoinStrings(GetWaveEnemyNames(waves[0]));
        UpdateBlockersInWave(waves[0]);
    }

    IEnumerator Spawn() {
        yield return new WaitForSeconds(timeBeforeFirstWave);

        for (int w = 0; w < waves.Length; w++) {
            // Before the wave
            List<GameObject> existingHeroes = new List<GameObject>();

            // Spawn the appropriate heroes on a certain time interval
            for (int h = 0; h < waves[w].heroSpawns.Length; h++){
                int heroType = waves[w].heroSpawns[h].heroID;
                int spawnID = waves[w].heroSpawns[h].spawnPointID;
                // Health bar shiz
                GameObject currentHero = Instantiate(heroTypes[heroType].gameobject, spawnPoints[spawnID]);
                GameObject hb = Instantiate(healthBar);
                hb.transform.parent = currentHero.transform;
                hb.transform.localPosition = new Vector3(1.8f, .8f, 0f);
                hb.GetComponent<HeroHealthBarScript>().hero = currentHero;

                waveDisplay.text = "WAVE " + (w + 1).ToString() + " - " + (waves[w].heroSpawns.Length - h - 1) + " Undeployed";
                existingHeroes.Add(currentHero);
                yield return new WaitForSeconds(timeBetweenHeroes);
            }

            // Wait until all the wave's heroes are dead
            yield return new WaitUntil(() => AllDead(existingHeroes));

            // Timer until next wave (if we aren't on the final wave already)
            if (w != waves.Length - 1) {
                UpdateBlockersInWave(waves[w+1]);
                timeTillNextWave = timeBetweenWaves;
                while (timeTillNextWave > 0) {
                    waveDisplay.text = "Wave " + (w + 2).ToString() + " in " + timeTillNextWave.ToString();
                    waveHeroesDisplay.text = JoinStrings(GetWaveEnemyNames(waves[w + 1]));
                    yield return new WaitForSeconds(1);
                    timeTillNextWave--;
                }
            }
        }

        waveDisplay.text = "You Win!";
        waveHeroesDisplay.text = "";
    }

    bool AllDead(List<GameObject> heroes)
    {
        for (int i = 0; i < heroes.Count; i++)
            if (heroes[i] != null) return false;

        return true;
    }

    List<string> GetWaveEnemyNames(Wave w)
    {
        List<string> accum = new List<string>();

        for (int i = 0; i < w.heroSpawns.Length; i++)
        {
            if (!accum.Contains(heroTypes[w.heroSpawns[i].heroID].name))
            {
                accum.Add(heroTypes[w.heroSpawns[i].heroID].name);
            }
        }

        return accum;
    }

    string JoinStrings (List<string> l)
    {
        string str = "";

        for (int i = 0; i < l.Count; i++)
        {
            str += l[i];
            if (i < l.Count - 1) str += ", ";
        }

        return str;
    }

    void UpdateBlockersInWave(Wave w)
    {
        openBlockers = new List<int>();

        for (int i = 0; i < w.heroSpawns.Length; i++)
        {
            if (!openBlockers.Contains(w.heroSpawns[i].spawnPointID)) // If someone is spawning in a (visually) blocked area
            {
                openBlockers.Add(w.heroSpawns[i].spawnPointID); //(Visually) unblock the area
                blockers[openBlockers[openBlockers.Count - 1]].Disappear();
            }
        }

        for (int i = 0; i < blockers.Count; i++)
        {
            if (!openBlockers.Contains(i))
            {
                blockers[i].Reappear();
            }
        }
    }
}
