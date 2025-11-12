using UnityEngine;

public class PlayerPrefController : MonoBehaviour
{
    [ContextMenu("Delete All Pref")]
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("모든 데이터 삭제");
    }

    [ContextMenu("Save")]
    public void Save()
    {
        PlayerPrefs.Save();
        Debug.Log("데이터 영구 저장");
    }
}
