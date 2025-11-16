using Unity.VisualScripting;
using UnityEngine;

//오로지 세이브 데이터의 저장과 로드 및 세이브 파일의 전달만 담당
public class SaveManager : SimpleSingleton<SaveManager>
{
    private const string SAVE_KEY = "SaveData";
    [SerializeField]private SaveData _saveData;

    protected override void Awake()
    {
        base.Awake();
        _saveData = Load();
        InitSave();
    }

    private void InitSave()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerStat stat = player.GetComponent<PlayerStat>();

        // 초기값 세팅
        stat.Initialize(
            _saveData.CurrentPlayerHealth,
            _saveData.CurrentPlayerMoveSpeed,
            _saveData.CurrentPlayerFireCooltime,
            _saveData.CurrentPlayerDamageLevel
        );

        // 이벤트 구독
        stat.OnHealthChanged += hp => _saveData.SetCurrentPlayerHealth(hp);
        stat.OnSpeedChanged += speed => _saveData.SetCurrentPlayerMoveSpeed(speed);
        stat.OnFireCooltimeChanged += cooltime => _saveData.SetCurrentPlayerFireCooltime(cooltime);
        stat.OnDamageLevelChanged += level => _saveData.SetCurrentPlayerDamageLevel(level);
    }

    public SaveData GetSaveData()
    {
        if (_saveData == null)
            _saveData = Load();
        return _saveData;
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(_saveData);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
        Debug.Log($"Save All Data");
    }


    public SaveData Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            Debug.LogWarning("There's no SaveFile");
            _saveData = new SaveData();
            _saveData.ResetCurrentData();
            return _saveData;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        _saveData = JsonUtility.FromJson<SaveData>(json);
        Debug.Log($"Load All Data");
        return _saveData;
    }

    [ContextMenu("Delete Save File")]
    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey(SAVE_KEY);
        Debug.Log("Save data deleted.");
    }
}

