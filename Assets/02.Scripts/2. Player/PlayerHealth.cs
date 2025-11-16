using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerStat _stat;
    private PlayerEffector _effect;
    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
        _effect = GetComponent<PlayerEffector>();
    }

    public void OnHit(float damage)
    {
        _stat.HealthDown(damage);
        if (_stat.IsDead())
        {
            OnDead();
        }
        else
        {
            _effect.PlayHitSound();
        }
    }

    private void OnDead()
    {
        Debug.Log("Player Dead");
        //사망할 경우 Current데이터 초기화
        
        if(SaveManager.IsManagerExist())
        {
            SaveManager save = SaveManager.Instance;
            SaveData saveData = save.GetSaveData();
            saveData.ResetCurrentData();
            save.Save();
        }
        _effect.InstantiateGameOverSoundObject();
        Destroy(gameObject);
    }
}
