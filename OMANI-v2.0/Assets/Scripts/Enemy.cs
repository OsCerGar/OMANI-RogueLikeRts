﻿
using UnityEngine;

public class Enemy : NPC
{
    public delegate void DieEvent(Enemy _robot);
    public static event DieEvent OnDie;
    public Collider col;
    public EnnuiSpawnerManager ennuis;
    public Transform laserTarget;

    private void Awake()
    {
        laserTarget = transform.FindDeepChild("LaserObjective");
        laserTarget.gameObject.SetActive(true);

    }

    public override void Update()
    {

        if (Nav != null)
        {
            TPC.Move(Nav.desiredVelocity);
        }
        /*
        if (anim != null)
        {
            if (!RootMotion)
            {
                anim.SetFloat("AnimSpeed", Nav.velocity.magnitude);
            }
        }
        */
    }
    public override void AttackHit()
    {
        Attackzone.SetActive(true);
    }

    public virtual void OnEnable()
    {
        if (laserTarget != null)
        {
            laserTarget.gameObject.SetActive(true);
        }
        else
        {
            laserTarget = transform.FindDeepChild("LaserObjective");
        }
        if (col != null) { col.enabled = true; }

    }

    /*
    private void OnDisable()
    {
        if (OnDie != null)
        {
            OnDie(gameObject);
        }
    }
    */
    public override void Die()
    {
        base.Die();

        int random = Random.Range(0, 10);

        if (random < 3)
        {
            EnnuiSpawnerManager.EnnuiSpawner.SpawnEnnui(laserTarget);
        }


        col.enabled = false;
        laserTarget.gameObject.SetActive(false);
        GamemasterController.GameMaster.Money += 1;
        GamemasterController.GameMaster.Save();

        Debug.Log(GamemasterController.GameMaster.Money);

        if (OnDie != null)
        {
            OnDie(this);
        }

    }

    public void StepSound()
    {
        if (SM != null)
        {
            SM.Step();
        }
    }
}
