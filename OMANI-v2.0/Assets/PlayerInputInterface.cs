﻿using Rewired;
using Rewired.ControllerExtensions;
using UnityEngine;

public class PlayerInputInterface : MonoBehaviour
{

    //Rewired
    public Rewired.Player inputs; // The Rewired Player
    public int playerId = 0;
    public IDualShock4Extension ds4;


    //Input variables
    //Movement
    Vector2 movementAxis, movementAxisController, lookAxis, robotQuickSelection;
    bool laser, summon, radialMenu;

    public Vector2 MovementAxis { get => movementAxis; set => movementAxis = value; }
    public Vector2 MovementAxisController { get => movementAxisController; set => movementAxisController = value; }
    public bool Laser { get => laser; set => laser = value; }
    public Vector2 LookAxis { get => lookAxis; set => lookAxis = value; }
    public bool Summon { get => summon; set => summon = value; }
    public bool RadialMenu { get => radialMenu; set => radialMenu = value; }
    public Vector2 RobotQuickSelection { get => robotQuickSelection; set => robotQuickSelection = value; }

    //Pointer

    //Actions


    // Start is called before the first frame update
    void Start()
    {
        inputs = ReInput.players.GetPlayer(playerId);
        ds4 = inputs.controllers.Joysticks[0].GetExtension<IDualShock4Extension>();
    }

    // Update is called once per frame
    void Update()
    {
        RestartControllerAxis();
        ControllerLookAxis();
        Movement();
        Actions();
        laser = inputs.GetButton("FireLaser");
    }

    private void RobotSelection()
    {
        robotQuickSelection.x = inputs.GetAxis("RobotSelectionHorizontal");
        robotQuickSelection.y = inputs.GetAxis("RobotSelectionVertical");
    }
    private void Actions()
    {
        RobotSelection();
    }

    private void Movement()
    {
        movementAxisController.x = inputs.GetAxis("HorizontalGamepad");
        movementAxisController.y = inputs.GetAxis("VerticalGamepad");
        movementAxis.x = inputs.GetAxis("HorizontalKeyboard");
        movementAxis.y = inputs.GetAxis("VerticalKeyboard");
    }

    void RestartControllerAxis()
    {
        movementAxisController = new Vector2(0, 0);
        movementAxis = new Vector2(0, 0);
        robotQuickSelection = new Vector2(0, 0);
        lookAxis = new Vector2(0, 0);
        laser = false;
        summon = false;
    }

    void ControllerLookAxis()
    {
        lookAxis.x = inputs.GetAxis("HorizontalRightGamepad");
        lookAxis.y = inputs.GetAxis("VerticalRightGamepad");
    }

    public void SetVibration(int _motor, float _amount, float _time, bool _stops)
    {
        inputs.SetVibration(_motor, _amount, _time);

    }

    public void SetDS4Lights()
    {

    }
}
