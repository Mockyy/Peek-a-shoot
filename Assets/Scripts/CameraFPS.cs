﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCVForUnityExample;
using OpenCVForUnity.CoreModule;

public class CameraFPS : MonoBehaviour
{
    [SerializeField] private FaceDetectionWebCamTextureExample webcam;
    [SerializeField] private Transform[] pos;

    private Camera cam;
    [SerializeField] private Transform playerBody;
    [SerializeField] private float sensitivity = 100f;
    private float xRotation = 0f;

    private bool peekUp = false;
    private bool peekLeft = false;
    private bool peekRight = false;

    // Start is called before the first frame update
    void Start()
    {
        //Veérouillage de la souris et assignation de la position de la caméra
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main;
        cam.transform.position = pos[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        //Permet de déplacer la caméra selon le placement dans la webcam
        peekFixed();

        //Récupération des mouvements de la souris
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Rotation de la caméra
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    //Si le face tracking atteint certaines zones de l'écran, on y déplace la caméra
    public void peekFixed()
    {
        if (webcam.rect.y < 100)
        {
            peekUp = true;
        }
        else
        {
            peekUp = false;
        }

        if (webcam.rect.x > 350)
        {
            peekRight = true;
        }
        else if (webcam.rect.x < 150)
        {
            peekLeft = true;
        }
        else
        {
            peekLeft = false;
            peekRight = false;
        }

        if (!peekLeft && !peekRight && !peekUp)
        {
            cam.transform.position = pos[0].position;
        }
        if (peekUp && !peekRight && !peekLeft)
        {
            cam.transform.position = pos[1].position;
        }
        if (peekUp && peekLeft)
        {
            cam.transform.position = pos[4].position;
        }
        if (peekUp && peekRight)
        {
            cam.transform.position = pos[5].position;
        }
        if (peekRight)
        {
            cam.transform.position = pos[2].position;
        }
        if (peekLeft)
        {
            cam.transform.position = pos[3].position;
        }
    }

    //Tentative de suivre le face tracking sans passer par des positions pré définies
    public void peekFree()
    {
        Vector3 movement = new Vector2(0f, 0f);

        if (webcam.rect.x > 350)
        {
            movement.x = 1f;
        }
        else if (webcam.rect.x < 150)
        {
            movement.x = -1f;
        }

        if (webcam.rect.y < 100)
        {
            movement.y = 1f;
        }
        else
        {
            movement.y = -1f;
        }

        Mathf.Clamp(cam.transform.position.x, -1f, 1f);
        Mathf.Clamp(cam.transform.position.y, 0.57f, 1.27f);

        cam.transform.position += movement;
    }
}
