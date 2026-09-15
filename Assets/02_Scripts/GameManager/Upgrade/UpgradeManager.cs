using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // 업그레이드 관리자 : 업그레이드들에 대한 무결성과 생성, 조회, 수정, 삭제 등과 관련된 게임 로직
    
    private static UpgradeManager _instance = null;
    public static UpgradeManager Instance => _instance;

    [SerializeField] private Upgrade[] _upgrades;
    public Upgrade[] Upgrades => _upgrades;
    
    // 업그레이드 UI들
    [SerializeField] private UI_UpgradeButton[] _uiUpgradeButtons;
    
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        Load();
        RefreshUI();
    }

    public void LevelUp(int index)
    {
        // ToDo : 묻지말고 시켜라!
        // 골드 매니저에게 돈이 있는지 물어보고 돈이 있다면 차감 후 업그레이드 호출
        Upgrade upgrade = _upgrades[index];
        if (ScoreManager.Instance.GetScore() < upgrade.Cost)
        {
            return;
        }

        ScoreManager.Instance.SpendScore(upgrade.Cost);
        _upgrades[index].LevelUp();
        
        Save();
        RefreshUI();
    }

    private void RefreshUI()
    {
        foreach (UI_UpgradeButton uiUpgradeButton in _uiUpgradeButtons)
        {
            uiUpgradeButton.Refresh();
        }
    }

    private void Save()
    {
        // 데이터 저장은 유의미한 정보만 저장한다.
        // 그래서 레벨만 저장한다.
        for (int i = 0; i < _upgrades.Length; i++)
        {
            PlayerPrefs.SetInt($"Upgrade.{i}.Level", _upgrades[i].Level);
        }
        PlayerPrefs.Save();
    }

    private void Load()
    {
        for (int i = 0; i < _upgrades.Length; i++)
        {
            int level = PlayerPrefs.GetInt($"Upgrade.{i}.Level", 1);
            _upgrades[i].SetLevel(level);
        }
    }
}
