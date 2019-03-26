﻿using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource combatMusic, combatStop, mainTheme;
    bool isPlaying;
    int numberOfEnemies = 0;
    float timeToStop;

    float lerpTime = 12f;
    float currentLerpTime;


    void OnEnable()
    {
        Enemy.OnDie += enemyDied;
        WorldSpawnPos.OnSummon += enemySummoned;
    }


    void OnDisable()
    {
        Enemy.OnDie -= enemyDied;
        WorldSpawnPos.OnSummon -= enemySummoned;
    }

    void enemyDied(Enemy died)
    {
        numberOfEnemies--;
        timeToStop = Time.time;
        StartCoroutine("stopMusic");

    }

    void enemySummoned()
    {
        if (!isPlaying)
        {
            currentLerpTime = 0;
            combatMusic.Play(); mainTheme.volume = 0;
            isPlaying = true;
        }
        numberOfEnemies++;
    }

    void Update()
    {
        //stops after 30 seconds
        if (Time.time - timeToStop > 30f)
        {
            timeToStop = 999;
            StopCombatMusic();
        }

        if (isPlaying)
        {
            currentLerpTime += Time.deltaTime;
            float t = currentLerpTime / lerpTime;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            combatMusic.volume = 1f;
            mainTheme.volume = Mathf.Lerp(mainTheme.volume, 0f, t);
        }
        else
        {
            currentLerpTime += Time.deltaTime;
            float t = currentLerpTime / lerpTime;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            combatMusic.volume = 0f;
            mainTheme.volume = Mathf.Lerp(mainTheme.volume, 1f, t);

            if (combatMusic.isPlaying && combatMusic.volume <= 0.1f)
            {
                combatMusic.Stop();
                combatStop.Play();
                numberOfEnemies = 0;
                currentLerpTime = 0f;
            }
        }
    }

    private void StopCombatMusic()
    {
        isPlaying = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        combatMusic = transform.Find("CombatMusic").GetComponent<AudioSource>();
        combatStop = transform.Find("CombatMusicStop").GetComponent<AudioSource>();
        mainTheme = transform.Find("MainTheme").GetComponent<AudioSource>();
    }

    private IEnumerator stopMusic()
    {
        if (numberOfEnemies == 0)
        {
            yield return new WaitForSeconds(4.5f);
            StopCombatMusic();
        }
    }

}
