using System;
using System.Collections.Generic;
using UnityEngine;

public enum EFactoryType
{
    Bullet,
    Enemy,
    Item
}

public class FactoryManager : SimpleSingleton<FactoryManager>
{
    [Header("Factories")]
    [SerializeField] private List<FactoryBase> factoryList = new List<FactoryBase>();
    private Dictionary<Type, FactoryBase> factoryMap;

    protected override void Awake()
    {
        base.Awake();
        SetFactoryMap();
    }

    private void SetFactoryMap()
    {
        factoryMap = new Dictionary<Type, FactoryBase>();

        foreach (FactoryBase factory in factoryList)
        {
            if (factory == null) 
                continue;

            Type t = factory.GetType();
            if (!factoryMap.ContainsKey(t))
            {
                factoryMap.Add(t, factory);
            }
        }
    }

    public T GetFactory<T>() where T : FactoryBase
    {
        Type key = typeof(T);
        if (factoryMap.TryGetValue(key, out FactoryBase value))
            return value as T;

        Debug.LogError($"FactoryManager: {key.Name} not registered!");
        return null;
    }

}
