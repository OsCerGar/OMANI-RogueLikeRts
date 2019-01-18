﻿using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{
    [Space]
    [SerializeField]
    private List<ArmyCell> armyCell = new List<ArmyCell>();

    [SerializeField]
    private GameObject OrderPositionObject;

    private List<GameObject> positions = new List<GameObject>();
    [SerializeField] private GameObject ShootingPosition;
    LookDirectionsAndOrder look;

    //ArmyCellSelection
    private RadialMenu_GUI radialMenu;
    private int ArmyCellSelected;

    private Robot currentFighter;

    PW_SlowMotion slowMo;

    private void Start()
    {
        look = FindObjectOfType<LookDirectionsAndOrder>();
        radialMenu = FindObjectOfType<RadialMenu_GUI>();
        slowMo = FindObjectOfType<PW_SlowMotion>();

    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(2))
        {
            radialMenuPopUp();
        }
        if (Input.GetMouseButtonUp(2))
        {
            radialMenuPopDown();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Order();
        }
    }

    //Makes the RadialMenu Visible
    private void radialMenuPopUp()
    {
        radialMenu.PopUp();
        slowMo.CastPower();
    }

    //Makes the RadialMenu invisible and gets the selected menu item.
    private void radialMenuPopDown()
    {
        int newArmyCellSelected = radialMenu.PopDown();
        slowMo.CastPower();

        // Debería deseleccionar.
        //If currentFighter tiene toda la energia, debería desmaterializarse y volver.
        //Si no, debería desconectarse.

        string robotTypes = armyCell[newArmyCellSelected].getRobotType();

        if (robotTypes == null)
        {
            ArmyCellSelected = 4;

            if (currentFighter != null)
            {
                Robot _transitionStateRobot = currentFighter;
                Remove(currentFighter);
                Reclute(_transitionStateRobot);
            }

        }

        else if (currentFighter != null && robotTypes != currentFighter.boyType)
        {
            ArmyCellSelected = newArmyCellSelected;

            Robot _transitionStateRobot = currentFighter;
            Remove(currentFighter);
            Reclute(_transitionStateRobot);
        }

        else
        {
            ArmyCellSelected = newArmyCellSelected;
        }


    }

    public ArmyCell checkArmyCellAvailable(Robot _newRobot)
    {
        ArmyCell availableCell = null;
        bool cellFound = false; //If there are cells with already a robot on it == true

        //Checks if there is already an ArmyCell with RobotType, or if there is space for a new one
        foreach (ArmyCell cell in armyCell)
        {
            if (cell.getRobotType() != null && cell.getRobotType().Equals(_newRobot.boyType))
            {
                if (checkArmyCellsSpace(cell))
                {

                    availableCell = cell;
                    cellFound = true;
                }
            }
        }

        //Checks if there are empty Cells
        if (!cellFound)
        {
            foreach (ArmyCell cell in armyCell)
            {
                if (cell.availableEmptySpace() && availableCell == null) //Means that the cell is empty.
                {
                    availableCell = cell;
                }
            }
        }
        return availableCell;
    }
    public bool checkArmyCellsSpace(ArmyCell _cell)
    {
        //Checks if there is space in available ArmyCells of the RobotType
        if (_cell.availableArmyCellSpace())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<ArmyCell> getCells()
    {
        return armyCell;
    }

    //Adds the boy to the Army and makes it follow the Army commander.
    public void Reclute(Robot _robot)
    {

        ArmyCell cellToSaveRobot;
        cellToSaveRobot = checkArmyCellAvailable(_robot);

        if (cellToSaveRobot != null)
        {
            cellToSaveRobot.addRobot(_robot);

            //Disable robot
            _robot.Dematerialize();
        }
        else
        {
            //no space left sound or whatever
        }
    }

    public void Order()
    {
        //Makes a Robot Appear if there is no other robot doing stuff.

        if (currentFighter != null)
        {
            //Attacks
            currentFighter.FighterAttack(look.pointerDirection.gameObject);
        }

        else
        {
            //If something is selected
            if (ArmyCellSelected != 4)
            {
                //Materialice the next one
                currentFighter = armyCell[ArmyCellSelected].GetRobot();
                currentFighter.Materialize(ShootingPosition, look.pointerDirection.gameObject);

                //Removes from list to update UI, still currentFighter
                RemoveWithoutFighter(currentFighter);
                armyCell[ArmyCellSelected].Transaction();
            }
        }

    }

    public void Remove(Robot _robot)
    {
        if (_robot == currentFighter)
        {
            currentFighter = null;
        }
        foreach (ArmyCell cell in armyCell)
        {
            cell.removeRobot(_robot);
        }
    }
    public void RemoveWithoutFighter(Robot _robot)
    {
        foreach (ArmyCell cell in armyCell)
        {
            cell.removeRobot(_robot);
        }
    }
}