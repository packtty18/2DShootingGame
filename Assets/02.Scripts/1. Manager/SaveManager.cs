using Unity.VisualScripting;
using UnityEngine;

public class SaveManager : SimpleSingleton<SaveManager>
{
    private const string SAVE_KEY = "SaveData";
    private SaveData _saveData;

    private void Start()
    {
        _saveData = Load();
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

