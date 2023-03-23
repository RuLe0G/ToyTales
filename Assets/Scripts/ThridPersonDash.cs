using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThridPersonDash : MonoBehaviour
{
    private Rigidbody rb;
    ThridPersonController player;
    private ThridPersonAsset playerActionsAsset;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float maxDashYSpeed;
    public float dashDuration;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    private void Awake()
    {
        playerActionsAsset = new ThridPersonAsset();
        rb = GetComponent<Rigidbody>();
        player = GetComponent<ThridPersonController>();
    }

    private void OnEnable()
    {
        playerActionsAsset.Player.Dash.started += DoDash;
        playerActionsAsset.Player.Enable();
    }

    private void OnDisable()
    {
        playerActionsAsset.Player.Dash.started -= DoDash;
        playerActionsAsset.Player.Disable();
    }

    private void DoDash(InputAction.CallbackContext obj)
    {
        Dash();
        if (dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;
    }

    private void Dash()
    {
        //if (dashCdTimer > 0) return;
        //else dashCdTimer = dashCd;
        player.maxSpeed = 15;
        Vector3 v = new Vector3(rb.velocity.normalized.x, 0, rb.velocity.normalized.z);
        Vector3 forceToApply = v * dashForce + Vector3.up * dashUpwardForce;

        rb.AddForce(forceToApply, ForceMode.Impulse);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private void ResetDash()
    {
        player.maxSpeed = 5;
    }
}
