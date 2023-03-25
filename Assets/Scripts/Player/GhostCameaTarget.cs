using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class GhostCameaTarget : MonoBehaviour
{
    [SerializeField]
    private Transform CharacterMesh;
    [SerializeField]
    private Transform ghostTransform;
    [SerializeField]
    float ghostPositionY;
    private ThridPersonController controller;
    private float characterVelocity;
    [SerializeField]
    private Camera cam; 

    private void Awake()
    {
        controller = CharacterMesh.GetComponent<ThridPersonController>();

    }
    void OnLeaveGround()
    {
        // update Y for behavior 3
        ghostPositionY = CharacterMesh.position.y;
    }
    void LateUpdate()
    {
        //Vector3 characterViewPos = cam.WorldToViewportPoint(CharacterMesh.position + characterVelocity * Time.deltaTime);

        //// behavior 2
        //if (viewPos.y > 0.85f || viewPos.y < 0.3f)
        //{
        //    ghostPositionY = CharacterMesh.position.y;
        //}
        //// behavior 4
        //else if (controller.IsGrounded)
        //{
        //    ghostPositionY = CharacterMesh.position.y;
        //}
        // behavior 5
        var desiredPosition = new Vector3(CharacterMesh.position.x, ghostPositionY, CharacterMesh.position.z);
        ghostTransform.position = desiredPosition;
    }
}
