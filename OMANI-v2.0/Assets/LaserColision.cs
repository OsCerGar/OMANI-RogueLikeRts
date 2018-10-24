﻿using UnityEngine;

public class LaserColision : MonoBehaviour
{
    [SerializeField]
    public bool laserEnabled { get; set; }
    Power_Laser powerLaser;
    [SerializeField] float rad = 0.5f;
    ParticleSystem PSArea;

    private void Awake()
    {
        powerLaser = FindObjectOfType<Power_Laser>();
        PSArea = GetComponentInChildren<ParticleSystem>();
    }
    private void Update()
    {
        if (laserEnabled)
        {
            LaserCollisions();

            rad = Mathf.Clamp(rad + 0.1f, 0.5f, 3f);
            //emit effect of zone
            if (PSArea != null)
            {
                var main = PSArea.main;
                main.startSize = rad * 3f;
                PSArea.Play();
            }
        }
        else
        {
            rad = Mathf.Clamp(rad - 0.1f, 0.5f, 3f);
            PSArea.Stop();

        }


    }

    private void LaserCollisions()
    {
        Enemy enemy, closestEnemyTarget = null;
        Interactible interactible, closestBUTarget = null;
        Robot ally;
        bool somethingHitted = false;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, rad);
        foreach (Collider other in targetsInViewRadius)
        {
            if (other.CompareTag("Building"))
            {
                interactible = other.GetComponent<Interactible>();

                if (interactible != null)
                {
                    interactible.Action();
                    if (interactible.actionBool)
                    {
                        powerLaser.setWidth(interactible.linkPrice);
                        somethingHitted = true;
                    }

                }
            }

            else if (other.CompareTag("Enemy"))
            {

                enemy = other.GetComponent<Enemy>();

                if (enemy != null)
                {
                    Transform target = other.transform;

                    //Distance to target
                    float dstToTarget = Vector3.Distance(transform.position, target.position);
                    //If the closestTarget is null he is the closest target.
                    // If the distance is smaller than the distance to the closestTarget.
                    if (closestBUTarget == null || dstToTarget < Vector3.Distance(transform.position, target.position))
                    {
                        closestEnemyTarget = enemy;
                    }
                }
            }

            else if (other.CompareTag("People"))
            {

                ally = other.GetComponent<Robot>();

                if (ally != null)
                {
                    ally.robot_energy.Action();
                    somethingHitted = true;

                }
            }

            else if (other.CompareTag("Inactive"))
            {

                ally = other.GetComponent<Robot>();

                if (ally != null)
                {
                    ally.robot_energy.Action();
                    somethingHitted = true;

                }
            }
        }

        if (somethingHitted == true)
        {
            powerLaser.setWidth(1);
        }

    }
}

