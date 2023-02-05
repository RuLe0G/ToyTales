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

    private void Awake()
    {
        controller = CharacterMesh.GetComponent<ThridPersonController>();

    }

    private void LateUpdate()
    {

        ghostTransform.position = new Vector3(CharacterMesh.position.x, ghostPositionY, CharacterMesh.position.z);
}
    void OnLeaveGround()
    {
        ghostPositionY = CharacterMesh.position.y;
    }

}
