using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPet : MonoBehaviour
{
    private PlayerInput input;
    [SerializeField] private List<Pet> _spawnedPet;

    [SerializeField] private GameObject _petPrefab;
    [SerializeField] private int _maxSpawnCount = 5;

    private void Awake()
    {
        input = transform.GetComponent<PlayerInput>();
        _spawnedPet = new List<Pet>();
    }

    private void Update()
    {
        if(input.IsInputSpawnPet)
        {
            SpawnPet();
        }
        if(input.IsInputDestroyPet)
        {
            DestroyPet();
        }
    }

    private void DestroyPet()
    {
        if(_spawnedPet.Count == 0)
        {
            return;
        }

        Pet target = _spawnedPet[_spawnedPet.Count - 1];
        _spawnedPet.Remove(target);
        Destroy(target.gameObject);
    }

    private void SpawnPet()
    {
        if (_spawnedPet.Count >= _maxSpawnCount)
        {
            return;
        }

        Pet pet = Instantiate(_petPrefab, transform.position, Quaternion.identity)
            .GetComponent<Pet>();

        if (_spawnedPet.Count - 1 < 0)
            pet.SetInit(transform);
        else
            pet.SetInit(_spawnedPet[_spawnedPet.Count - 2].transform);

        _spawnedPet.Add(pet);
    }
}
