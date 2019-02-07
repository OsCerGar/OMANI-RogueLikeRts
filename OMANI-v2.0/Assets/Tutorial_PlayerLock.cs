﻿using System.Collections;
using UnityEngine;

public class Tutorial_PlayerLock : MonoBehaviour
{
    public CharacterMovement movement;
    public OMANINPUT controls;

    [SerializeField] GameObject StartingCamera, SurkaCamera;
    [SerializeField] GameObject SneakySurka;

    [SerializeField] private InverseKinematics leg1, leg2, leg3, leg4;
    bool cameraChanged;
    private void Awake()
    {
        controls.PLAYER.WASD.performed += movement => CameraChange();
        controls.PLAYER.Joystick.performed += Controllermovement => CameraChange();
        controls.PLAYER.LASERZONE.performed += context => CameraChange();
        controls.PLAYER.LASERZONERELEASE.performed += context => CameraChange();
        controls.PLAYER.LASERSTRONGPREPARATION.performed += context => CameraChange();
        controls.PLAYER.LASERSTRONG.performed += context => CameraChange();
        controls.PLAYER.RadialMenuUp.Disable();
        controls.PLAYER.RadialMenuDown.Disable();

    }


    // Start is called before the first frame update
    void Start()
    {
        movement.speed = 0;
    }

    public void LegRelease(int _leg)
    {
        switch (_leg)
        {
            case 1:
                //Activate Leg
                leg1.enabled = true;
                break;
            case 2:
                leg2.enabled = true;
                break;
            case 3:
                leg3.enabled = true;
                break;
            case 4:
                leg4.enabled = true;
                break;
        }

        if (leg1.enabled && leg2.enabled && leg3.enabled && leg4.enabled)
        {
            SurkaEntersTheShow();
            movement.speed = 0.15f;
        }
    }

    private void CameraChange()
    {
        if (!cameraChanged)
        {
            StartingCamera.SetActive(false);
            StartCoroutine("surkaRoutine");
            cameraChanged = true;
        }

    }

    IEnumerator surkaRoutine()
    {
        yield return new WaitForSeconds(20f);
        SurkaEntersTheShow();
    }

    private void SurkaEntersTheShow()
    {
        SneakySurka.SetActive(true);
        SurkaCamera.SetActive(true);
        StartCoroutine("surkaCameraRoutine");

    }

    IEnumerator surkaCameraRoutine()
    {
        yield return new WaitForSeconds(6f);
        SurkaCamera.SetActive(false);
    }
}
