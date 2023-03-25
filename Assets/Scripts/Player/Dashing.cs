using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThridPersonDash : MonoBehaviour
{
    [Header("Setup")]
    public Transform orientation;
    private Rigidbody rb;
    private new_PlayerMovement pm;
    public Transform PObj;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    [Header("Input")]
    private ThridPersonAsset playerActionsAsset;
    private InputAction dashKey;
    private InputAction move;

    private void Awake()
    {
        playerActionsAsset = new ThridPersonAsset();
    }
    private void OnEnable()
    {
        move = playerActionsAsset.Player.Move;
        playerActionsAsset.Player.Dash.started += DashKey;
        playerActionsAsset.Player.Enable();
    }

    private void OnDisable()
    {
        playerActionsAsset.Player.Dash.started -= DashKey;
        playerActionsAsset.Player.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<new_PlayerMovement>();
    }
    private void DashKey(InputAction.CallbackContext obj)
    {
        Dash();
    }

    private void Update()
    {
        if(dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;
    }

    private void Dash()
    {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;
        pm.dashing = true;

        Vector3 forceToApply = PObj.forward * dashForce + PObj.up * dashUpwardForce;

        delayedForceToAplly = forceToApply;
        Invoke(nameof(DelayDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private Vector3 delayedForceToAplly;
    private void DelayDashForce()
    {
        rb.AddForce(delayedForceToAplly, ForceMode.Impulse);
    }

    private void ResetDash()
    {
        pm.dashing = false;
    }
}
