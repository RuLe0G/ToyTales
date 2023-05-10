using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vaulting : MonoBehaviour
{

    public LayerMask whatVaultWall;

    private float playerHeight;
    private float playerRadius;

    public Transform PObj;
    public new_PlayerMovement pm;

    private ThridPersonAsset playerActionsAsset;
    private InputAction move;

    private new_PlayerMovement mov;

    private void Awake()
    {
        playerActionsAsset = new ThridPersonAsset();
    }
    private void OnEnable()
    {
        playerActionsAsset.Player.Jump.started += PressJump;
        playerActionsAsset.Player.Enable();
    }

    private void OnDisable()
    {
        playerActionsAsset.Player.Jump.started -= PressJump;
        playerActionsAsset.Player.Disable();

        pm = GetComponent<new_PlayerMovement>();
        PObj = pm.PObj;
    }

    private void Start()
    {
        mov = GetComponent<new_PlayerMovement>();
        playerHeight = GetComponent<CapsuleCollider>().height;
    }

    private void PressJump(InputAction.CallbackContext obj)
    {
        if (!mov.grounded) 
        {
            Vault();
        }
    }


    private void Vault()
    {
        Ray ray = new Ray(PObj.transform.position + Vector3.up * 0.25f, PObj.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit firstHit, 1f, whatVaultWall))
        {
            StartCoroutine(LerpVault(firstHit.point + (Vector3.up * playerHeight), 0.5f));
        }
    }
    IEnumerator LerpVault(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = PObj.transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 pos = PObj.transform.position + Vector3.up * 0.25f;
        Vector3 direction = PObj.transform.forward * 5f;
        Gizmos.DrawRay(pos, direction);

    }
}
