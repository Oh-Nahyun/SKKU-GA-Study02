using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // static(정적)
    private static ScoreManager _instance = null;
    public static ScoreManager Instance => _instance;

    // 관리 : 특정 데이터에 대한 무결성과 생성, 읽기, 수정, 삭제 등과 관련된 로직
    private int _bestScore;
    private int _currentScore = 0;
    private int _lastRefreshScore = -1;

    // UI 책임 추가
    [SerializeField] private TextMeshProUGUI _bestScoreTextUI;
    [SerializeField] private TextMeshProUGUI _currentScoreTextUI;

    private void Awake()
    {
        // 늦게 태어난 매니저는 나는 늦었네~ 하면서 삭제
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void Update()
    {
        Refresh();
    }

    private void Refresh()
    {
        if (_lastRefreshScore == _currentScore) return;

        _bestScoreTextUI.text = $"BestScore: {_bestScore}";
        _currentScoreTextUI.text = $"BestScore: {_currentScore}";

        _lastRefreshScore = _currentScore;
    }

    public int GetScore()
    {
        return _currentScore;
    }

    public void AddScore(int score)
    {
        if (score <= 0) return;

        _currentScore += score;
        if (_currentScore > _bestScore)
        {
            _currentScore = _bestScore;
        }
    }
}