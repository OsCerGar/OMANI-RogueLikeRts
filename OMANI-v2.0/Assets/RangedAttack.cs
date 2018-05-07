﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class RangedAttack : MonoBehaviour {

    [SerializeField] string TagToAttack;
    [SerializeField] float PushBack;


    void OnParticleCollision(GameObject other)
    {
        if (other.tag == TagToAttack)
        {
            var EnemyNPC = other.GetComponent<NPC>();
            var EnemyNavMesh = other.GetComponent<UnityEngine.AI.NavMeshAgent>();
            EnemyNPC.Life -= transform.parent.GetComponent<NPC>().Damage;
            if (EnemyNavMesh != null)
            {
                EnemyNavMesh.velocity = (other.transform.position - transform.position).normalized * PushBack;
            }

            var targetVariable = (SharedGameObject)other.gameObject.GetComponent<BehaviorTree>().GetVariable("Target");
            targetVariable.Value = transform.parent.gameObject;
        }
    }
}
