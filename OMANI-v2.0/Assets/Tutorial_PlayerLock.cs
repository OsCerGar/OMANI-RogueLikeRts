﻿using UnityEngine;

public class Tutorial_PlayerLock : MonoBehaviour
{
    public CharacterMovement movement;
    public OMANINPUT controls;

    [SerializeField]
    GameObject StartingCamera;

    [SerializeField]
    private InverseKinematics leg1, leg2, leg3, leg4;

    private void Awake()
    {
        controls.PLAYER.WASD.performed += movement => CameraChange();
        controls.PLAYER.Joystick.performed += Controllermovement => CameraChange();
        controls.PLAYER.LASERZONE.performed += context => CameraChange();
        controls.PLAYER.LASERZONERELEASE.performed += context => CameraChange();
        controls.PLAYER.LASERSTRONGPREPARATION.performed += context => CameraChange();
        controls.PLAYER.LASERSTRONG.performed += context => CameraChange();

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
            Debug.Log("why");
            movement.speed = 0.15f;
        }
    }

    private void CameraChange()
    {
        StartingCamera.SetActive(false);
    }
}
