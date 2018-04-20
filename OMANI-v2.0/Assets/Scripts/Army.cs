﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour
{
    [Space]
    [SerializeField]
    private List<NPC> swordsmans, archers, musketeers, shieldmans, rogues = new List<NPC>();

    [SerializeField]
    private GameObject OrderPositionObject;

    //Adds the boy to the Army and makes it follow the Army commander.
    public void Reclute(NPC barroBoy)
    {
        if (barroBoy.AI_GetTarget() != this.gameObject)
        {
            switch (barroBoy.BoyType)
            {
                case "Swordsman":
                    swordsmans.Add(barroBoy);
                    break;
                case "Archer":
                    archers.Add(barroBoy);
                    break;
                case "Musketeer":
                    musketeers.Add(barroBoy);
                    break;
                case "Shieldman":
                    shieldmans.Add(barroBoy);
                    break;
                case "Rogue":
                    rogues.Add(barroBoy);
                    break;
            }

            barroBoy.Follow(this.gameObject);
        }

    }

    public void Order(string type, Vector3 orderPosition)
    {
        NPC barroBoy = null;

        switch (type)
        {
            case "Swordsman":
                if (swordsmans.Count > 0)
                {
                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = orderPosition;

                    barroBoy = swordsmans[swordsmans.Count - 1];
                    swordsmans.Remove(barroBoy);

                    orderPositionVar.GetComponent<OrderPositionObject>().NPC = barroBoy.gameObject;
                    barroBoy.Order(orderPositionVar);
                }

                break;
            case "Archer":
                if (archers.Count > 0)
                {
                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = orderPosition;

                    barroBoy = archers[archers.Count - 1];
                    archers.Remove(barroBoy);

                    orderPositionVar.GetComponent<OrderPositionObject>().NPC = barroBoy.gameObject;
                    barroBoy.Order(orderPositionVar);

                }
                break;
            case "Musketeer":
                if (musketeers.Count > 0)
                {
                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = orderPosition;

                    barroBoy = musketeers[musketeers.Count - 1];
                    musketeers.Remove(barroBoy);

                    orderPositionVar.GetComponent<OrderPositionObject>().NPC = barroBoy.gameObject;
                    barroBoy.Order(orderPositionVar);

                }
                break;
            case "Shieldman":

                if (shieldmans.Count > 0)
                {

                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = orderPosition;

                    barroBoy = shieldmans[shieldmans.Count - 1];
                    shieldmans.Remove(barroBoy);

                    orderPositionVar.GetComponent<OrderPositionObject>().NPC = barroBoy.gameObject;
                    barroBoy.Order(orderPositionVar);

                }
                break;
            case "Rogue":
                if (rogues.Count > 0)
                {
                    GameObject orderPositionVar = Instantiate(OrderPositionObject);
                    orderPositionVar.transform.position = orderPosition;

                    barroBoy = rogues[rogues.Count - 1];
                    rogues.Remove(barroBoy);

                    orderPositionVar.GetComponent<OrderPositionObject>().NPC = barroBoy.gameObject;
                    barroBoy.Order(orderPositionVar);

                }
                break;
        }
    }
    public void OrderDirect(string type, GameObject orderPosition)
    {
        NPC barroBoy = null;
        switch (type)
        {
            case "Swordsman":
                if (swordsmans.Count > 0)
                {
                    barroBoy = swordsmans[swordsmans.Count - 1];
                    swordsmans.Remove(barroBoy);

                    barroBoy.Order(orderPosition);
                }

                break;
            case "Archer":
                if (archers.Count > 0)
                {

                    barroBoy = archers[archers.Count - 1];
                    archers.Remove(barroBoy);

                    barroBoy.Order(orderPosition);

                }
                break;
            case "Musketeer":
                if (musketeers.Count > 0)
                {

                    barroBoy = musketeers[musketeers.Count - 1];
                    musketeers.Remove(barroBoy);

                    barroBoy.Order(orderPosition);

                }
                break;
            case "Shieldman":

                if (shieldmans.Count > 0)
                {


                    barroBoy = shieldmans[shieldmans.Count - 1];
                    shieldmans.Remove(barroBoy);

                    barroBoy.Order(orderPosition);

                }
                break;
            case "Rogue":
                if (rogues.Count > 0)
                {

                    barroBoy = rogues[rogues.Count - 1];
                    rogues.Remove(barroBoy);

                    barroBoy.Order(orderPosition);

                }
                break;
        }
    }

    public NPC GetBoyArmy(string type)
    {
        NPC barroBoy = null;
        switch (type)
        {
            case "Swordsman":
                barroBoy = swordsmans[swordsmans.Count - 1];
                break;
            case "Archer":
                barroBoy = archers[archers.Count - 1];
                break;
            case "Musketeer":
                barroBoy = musketeers[musketeers.Count - 1];
                break;
            case "Shieldman":
                barroBoy = shieldmans[shieldmans.Count - 1];
                break;
            case "Rogue":
                barroBoy = rogues[rogues.Count - 1];
                break;
        }

        return barroBoy;
    }

    public int ListSize(string _type)
    {
        int size = 0;
        switch (_type)
        {
            case "Swordsman":
                size = swordsmans.Count;
                break;
            case "Archer":
                size = archers.Count;
                break;
            case "Musketeer":
                size = musketeers.Count;
                break;
            case "Shieldman":
                size = shieldmans.Count;
                break;
            case "Rogue":
                size = rogues.Count;
                break;
        }

        return size;
    }

    public void RemoveFromList(NPC barroBoy)
    {

        switch (barroBoy.BoyType)
        {
            case "Swordsman":
                swordsmans.Remove(barroBoy);

                break;
            case "Archer":
                archers.Remove(barroBoy);

                break;
            case "Musketeer":
                musketeers.Remove(barroBoy);
                break;
            case "Shieldman":
                shieldmans.Remove(barroBoy);
                break;
            case "Rogue":
                rogues.Remove(barroBoy);
                break;
        }
    }

    public void GUI_ActivateCircle(string _type)
    {

        switch (_type)
        {
            case "Swordsman":
                if (swordsmans.Count > 0)
                {
                    GUI_ListActivateCircle(swordsmans);
                    GUI_ListDisableCircle(archers);
                }

                break;
            case "Archer":
                if (archers.Count > 0)
                {
                    GUI_ListActivateCircle(archers);
                    GUI_ListDisableCircle(swordsmans);
                }
                break;
        }
    }

    private void GUI_ListActivateCircle(List<NPC> _list)
    {

        foreach (NPC npc in _list)
        {
            npc.EnableCircle();
        }

    }

    private void GUI_ListDisableCircle(List<NPC> _list)
    {

        foreach (NPC npc in _list)
        {
            npc.DisableCircle();
        }

    }
}
