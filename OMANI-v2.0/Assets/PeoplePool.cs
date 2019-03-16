﻿using EZObjectPools;
using UnityEngine;

public class PeoplePool : MonoBehaviour
{

    EZObjectPool Worker, Warrior;
    GameObject Spawned;

    // Use this for initialization
    void Start()
    {
        var AllPoolers = FindObjectsOfType<EZObjectPool>();
        foreach (EZObjectPool item in AllPoolers)
        {
            if (item.PoolName == "Worker")
            {
                Worker = item;
            }
            if (item.PoolName == "Warrior")
            {
                Warrior = item;
            }
        }

    }

    public GameObject Spawn(Transform tr, Vector3 ps, string _name)
    {
        if (_name == "Worker" || _name == "worker")
        {
            return WorkerSpawn(tr, ps);
        }
        if (_name == "Warrior" || _name == "warrior")
        {
            return WarriorSpawn(tr);
        }
        return null;
    }

    public GameObject WorkerSpawn(Transform tr, Vector3 ps)
    {
        Worker.TryGetNextObject(ps, tr.rotation, out Spawned);
        return Spawned;
    }

    public GameObject WarriorSpawn(Transform tr)
    {
        Warrior.TryGetNextObject(tr.position, tr.rotation, out Spawned);
        return Spawned;

    }

}
