using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    //constants
    private static readonly string KEY_HIGHEST_SCORE = "HighestScore";

    //API
    [Header("Audio)")]
    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioSource gameoverSfx;

    [Header("Score")]
    //Score
    [SerializeField] private float score;
    [SerializeField] private int highestScore;

    public List<GameObject> plataformPrefabs;
    public float secondsToRespawnThePlataform;

    public bool isGameOver { get; private set; }

    private void Awake()
    {
        //instance the singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        score = 0;
        highestScore = PlayerPrefs.GetInt(KEY_HIGHEST_SCORE);
    }

    private void Update()
    {
        if (!isGameOver) {
            //Increment Score
            score += Time.deltaTime;

            //Update highest score
            if (GetScore() > getHighestScore())
            {
                highestScore = GetScore();
            }
        }
    }

    public int GetScore()
    {
        return (int)Mathf.Floor(score);
    }

    public int getHighestScore()
    {
        return highestScore;
    }


    public void EndGame()
    {
        if (isGameOver) return;

        //Set Flag
        isGameOver = true;

        //stop music
        musicPlayer.Stop();

        //play sfx
        gameoverSfx.Play();

        //save highest score
        PlayerPrefs.SetInt(KEY_HIGHEST_SCORE, getHighestScore());

        //Reload Scene
        StartCoroutine(ReloadScene(10));
    }

    private IEnumerator ReloadScene(float delay)
    {
        //wait
        yield return new WaitForSeconds(delay);

        //realod Scene

        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void Respawn(Vector3 originalPosition)
    {
        StartCoroutine(RespawnCR(originalPosition));
    }

    public IEnumerator RespawnCR(Vector3 originalPosition)
    {
        yield return new WaitForSeconds(secondsToRespawnThePlataform);

        int plataformPrefabIndex = Random.Range(0, plataformPrefabs.Count);
        Instantiate(plataformPrefabs[plataformPrefabIndex], originalPosition, plataformPrefabs[plataformPrefabIndex].transform.rotation);
    }
}
