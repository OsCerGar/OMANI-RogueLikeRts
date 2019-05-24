﻿using UnityEngine;

public class Worker : Robot
{
    public GameObject Scrap;
    [SerializeField]
    private ParticleSystem TrailEffect;

    [SerializeField]
    private GameObject RollHillBox;
    LookDirectionsAndOrder LookDAO;
    public bool animationRollAttack;

    public override void AttackHit()
    {
        base.AttackHit();
        RollDisableCheck();
        TrailEffect.Stop();
    }
    public override void FighterAttack(GameObject _position)
    {
        if (anim.GetBool("Roll"))
        {
            RollAttackFinished();

        }
        else
        {
            base.FighterAttack(_position);
            StartRollAttack();
            //enableTree("Attack");
        }
    }

    public override void Awake()
    {
        base.Awake();
        boyType = "Worker";
        LookDAO = FindObjectOfType<LookDirectionsAndOrder>();
    }
    public override void Start()
    {
        base.Start();
        damage = Mathf.RoundToInt(damage + (damage * (float.Parse(GamemasterController.GameMaster.getCsvValues("RobotBaseDamageLevel", GamemasterController.GameMaster.RobotBaseDamageLevel)[2]) / 100)));
        maxpowerPool = Mathf.RoundToInt(maxpowerPool + (maxpowerPool * (float.Parse(GamemasterController.GameMaster.getCsvValues("RobotMaxLifeLevel", GamemasterController.GameMaster.RobotMaxLifeLevel)[2]) / 100)));
    }
    public void Trail()
    {
        TrailEffect.gameObject.SetActive(true);
        TrailEffect.Play();
    }
    public void FlipSound()
    {
        workerSM.Flip();
    }
    public void StartRollAttack()
    {
        enableTree("Attack");
        anim.SetBool("Roll", true);
        Trail();
        workerSM.Flip();
        RollHillBox.SetActive(true);
    }
    public void RollAttackFinished()
    {
        enableTree("Follow");
        anim.SetBool("Roll", false);
        RollHillBox.SetActive(false);
    }
    public void RollCollision()
    {
        anim.SetTrigger("AttackCollision");
        Fired();
    }

    public override void CoolDown()
    {
        RollDisableCheck();
        base.CoolDown();
    }

    public override void Die()
    {
        RollDisableCheck();
        base.Die();
    }
    public override void Dematerialize()
    {
        RollDisableCheck();
        base.Dematerialize();
    }
    private void RollDisableCheck()
    {
        anim.SetBool("Roll", false);
        RollHillBox.SetActive(false);
        LookDAO.AlternativeCenter(null);
    }

}
