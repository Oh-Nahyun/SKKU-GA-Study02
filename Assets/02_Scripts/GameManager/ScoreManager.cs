using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // 싱글톤 : static(정적)
    private static ScoreManager _instance = null;
    public static ScoreManager Instance => _instance;

    // 관리 : 특정 데이터에 대한 무결성과 생성, 읽기, 수정, 삭제 등과 관련된 로직
    private int _bestScore;
    private int _currentScore;

    private const string SaveKey = "BestScore";

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

    private void Start()
    {
        // 입력 : Input
        // 저장/불러오기 : PlayerPrefs
        // [방안 1]
        // if (PlayerPrefs.HasKey("BestScore"))
        // {
        //     _bestScore = PlayerPrefs.GetInt("BestScore");
        // }
        // [방안 2]
        _bestScore = PlayerPrefs.GetInt(SaveKey, 0);

        Refresh();
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
            _bestScore = _currentScore;

            // 저장 : PlayerPrefs.Set~ 시리즈를 이용해 int/float/string을 저장 가능하다.
            // 내 컴퓨터 어딘가에 저장이 된다... (빈번한 저장은 렉 유발)
            PlayerPrefs.SetInt(SaveKey, _bestScore);
            PlayerPrefs.Save();
        }

        Refresh();
    }

    private void Refresh()
    {
        _bestScoreTextUI.text = $"Best Score : {_bestScore}";
        _currentScoreTextUI.text = $"Current Score : {_currentScore}";
    }
}