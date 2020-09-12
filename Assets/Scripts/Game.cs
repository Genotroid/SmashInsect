﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(LevelBuilder))]
public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private Destroyer _destroyer;
    [SerializeField] private TMP_Text _healthUI;
    [SerializeField] private TMP_Text _scoreUI;
    [SerializeField] private TMP_Text _levelUI;

    private SpawnArea _spawner;
    private LevelBuilder _levelBuilder;
    private int _levelId;
    private int _score;
    private int _health;

    private void Awake()
    {
        PlayerPrefs.SetInt("LevelId", 1);
        _levelBuilder = GetComponent<LevelBuilder>();
        _spawner = _levelBuilder.SpawnArea;
        _levelId = GetLevelIdFromPrefs();
        _score = GetScoreFromPrefs();
        _health = GetHealthFromPrefs();
    }

    private void Start()
    {
        SetUI();
    }

    private void OnEnable()
    {
        _destroyer.InsectMissed += OnDangerInsectTape;
        _spawner.SafeInsectTapped += OnSafeInsectTape;
        _spawner.DangerInsectTapped += OnDangerInsectTape;
        _spawner.AllInsectTapped += LevelComplete;
    }

    private void OnDisable()
    {
        _spawner.SafeInsectTapped -= OnSafeInsectTape;
        _spawner.DangerInsectTapped -= OnDangerInsectTape;
        _spawner.AllInsectTapped -= LevelComplete;
    }

    private void OnSafeInsectTape(Insect insect)
    {
        _score += insect.Score;
        _scoreUI.text = _score.ToString();
    }

    private void OnDangerInsectTape()
    {
        PlayerPrefs.SetInt("PlayerHealth", --_health);
        _healthUI.text = _health.ToString();
        if (_health <= 0)
            GameOver();
    }

    private void LevelComplete()
    {
        _levelUI.text = _levelId.ToString();
        _menuPanel.SetActive(true);
    }

    private void GameOver()
    {
        _spawner.gameObject.SetActive(false);
        _menuPanel.SetActive(true);
        SetPrefs();
    }

    private void SetPrefs(int level = 1, int score = 0, int health = 3)
    {
        PlayerPrefs.SetInt("LevelId", level);
        PlayerPrefs.SetInt("PlayerScore", score);
        PlayerPrefs.SetInt("PlayerHealth", health);
    }

    private int GetLevelIdFromPrefs()
    {
        int levelId = 1;
        if (PlayerPrefs.HasKey("LevelId"))
            levelId = PlayerPrefs.GetInt("LevelId");

        return levelId;
    }

    private int GetScoreFromPrefs()
    {
        int score = 0;
        if (PlayerPrefs.HasKey("PlayerScore"))
            score = PlayerPrefs.GetInt("PlayerScore");

        return score;
    }

    private int GetHealthFromPrefs()
    {
        int health = 3;
        if (PlayerPrefs.HasKey("PlayerHealth"))
            health = PlayerPrefs.GetInt("PlayerHealth");

        return health;
    }

    private void SetUI()
    {
        _healthUI.text = _health.ToString();
        _scoreUI.text = _score.ToString();
        _levelUI.text = _levelId.ToString();
    }

    public void StartLevel()
    {
        _levelBuilder.LoadLevel(_levelId++);
        SetPrefs(_levelId, _score, _health); 
        _menuPanel.SetActive(false);
    }

    public void RestartLevel()
    {
        _score = GetScoreFromPrefs();
        _levelId = GetLevelIdFromPrefs();
        SetUI();
        _levelBuilder.LoadLevel(_levelId);
        _menuPanel.SetActive(false);
    }
}
