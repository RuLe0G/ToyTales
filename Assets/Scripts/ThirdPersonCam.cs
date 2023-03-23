using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("Setup")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;
    private ThridPersonAsset playerActionsAsset;
    private InputAction move;

    public float rotationSpeed;

    public Transform combatLookAt;

    public GameObject thirdPersonCam;
    public GameObject fightCam;
    public GameObject topDownCam;


    public CameraStyle curStyle;
    public enum CameraStyle
    {
        Basic,
        Fight,
        Topdown
    }

    private void Awake()
    {
        playerActionsAsset = new ThridPersonAsset();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnEnable()
    {
        move = playerActionsAsset.Player.Move;
        playerActionsAsset.Player.Enable();
    }

    private void OnDisable()
    {
        playerActionsAsset.Player.Disable();
    }

    private void Update()
    {
        ///TEMP CAM SWICH
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwichCameraStyle(CameraStyle.Basic);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwichCameraStyle(CameraStyle.Fight);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwichCameraStyle(CameraStyle.Topdown);

        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        if (curStyle == CameraStyle.Basic || curStyle == CameraStyle.Topdown)
        {
            float horizontalInput = move.ReadValue<Vector2>().x;
            float verticalInput = move.ReadValue<Vector2>().y;
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
        else if (curStyle == CameraStyle.Fight)
        {
            Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;

            playerObj.forward = dirToCombatLookAt.normalized;
        }
    }

    private void SwichCameraStyle(CameraStyle inStyle)
    { 
        fightCam.SetActive(false);
        topDownCam.SetActive(false);
        thirdPersonCam.SetActive(false);

        if (inStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if (inStyle == CameraStyle.Topdown) topDownCam.SetActive(true);
        if (inStyle == CameraStyle.Fight) fightCam.SetActive(true);

        curStyle= inStyle;
    }
}
