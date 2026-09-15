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

    private const string UpgradeSaveDataKey = "UpgradeSaveData";
    
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
        
        // [1] 각각 저장해주는 경우 (오버헤드가 발생하고, 유지보수가 힘들어짐)
        // for (int i = 0; i < _upgrades.Length; i++)
        // {
        //     PlayerPrefs.SetString($"Upgrade.{i}.Name", _upgrades[i].Name);
        //     PlayerPrefs.SetInt($"Upgrade.{i}.Level", _upgrades[i].Level);
        // }

        // [2]
        UpgradeSaveData saveData = new UpgradeSaveData(_upgrades.Length);
        for (int i = 0; i < _upgrades.Length; i++)
        {
            saveData.Name[i] = _upgrades[i].Name;
            saveData.Level[i] = _upgrades[i].Level;
        }

        // [★]
        // 혹시 게임 데이터 같은 걸 보시면 확장자가 게임별로 다르다.
        // json 포맷으로 문자열 변환으로 키와 값 형태로 저장한 형태
        // 이를 사용하기 위해선 UpgradeSaveData 클래스에 [System.Serializable]를 붙여줘야 한다!
        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(UpgradeSaveDataKey, json);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        if (!PlayerPrefs.HasKey(UpgradeSaveDataKey)) return;
        
        string json = PlayerPrefs.GetString(UpgradeSaveDataKey);
        UpgradeSaveData saveData = JsonUtility.FromJson<UpgradeSaveData>(json);
        
        for (int i = 0; i < _upgrades.Length; i++)
        {
            Debug.Log($"{_upgrades[i].Name} 로드 완료!");
            _upgrades[i].SetLevel(saveData.Level[i]);
        }
    }
}
