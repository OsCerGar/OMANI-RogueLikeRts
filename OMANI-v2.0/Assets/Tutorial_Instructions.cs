﻿using UnityEngine;

public class Tutorial_Instructions : MonoBehaviour
{
    public GameObject parent, pcImage, controllerImage;
    [SerializeField]
    Transform look;


    private void Awake()
    {
        if (look == null) { look = FindObjectOfType<LookDirectionsAndOrder>().pointerDirection.transform; }
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            PCVersion();
        }
        else
        {
            parent.SetActive(false);
        }
        /*if()
        {
            ControllerVersion();
        }*/
    }

    void PCVersion()
    {
        parent.SetActive(true);

        pcImage.SetActive(true);
        controllerImage.SetActive(false);
    }
    void ControllerVersion()
    {
        parent.SetActive(true);
        pcImage.SetActive(false);
        controllerImage.SetActive(true);
    }
}
