﻿using EZObjectPools;
using System.Collections.Generic;
using UnityEngine;
public class NumberPool : MonoBehaviour
{
    EZObjectPool damagenumber;
    GameObject Spawned; // list
    NumberScript text;

    List<NumberScript> texts = new List<NumberScript>();
    Transform camera;
    // Start is called before the first frame update
    void Awake()
    {
        var AllPoolers = FindObjectsOfType<EZObjectPool>();
        camera = Camera.main.transform;
        foreach (EZObjectPool item in AllPoolers)
        {
            if (item.PoolName == "DamageNumber")
            {
                damagenumber = item;
            }
        }

    }

    public void NumberSpawn(Transform tr, float damage_value, Color _type, GameObject numberOwner, bool _restoring)
    {
        bool alreadyOwned = false;

        foreach (NumberScript txt in texts)
        {
            if (txt.GetNumberOwner() == numberOwner)
            {
                txt.transform.position = numberOwner.transform.position;
                txt.transform.LookAt(camera);
                txt.numberUpdate(damage_value, _type, _restoring);
                alreadyOwned = true;
            }

        }
        if (alreadyOwned == false)
        {
            damagenumber.TryGetNextObject(tr.position, damagenumber.gameObject.transform.rotation, out Spawned);
            text = Spawned.transform.GetComponentInChildren<NumberScript>();
            text.SetNumberOwner(numberOwner);
            text.numberUpdate(damage_value, _type, _restoring);

            text.transform.LookAt(camera);

            texts.Add(text);
        }
    }

    public void RemoveText(NumberScript _textToRemove)
    {
        texts.Remove(_textToRemove);
    }

}
