using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPushBtn : MonoBehaviour
{
    [Header("Interact Setting")]
    public float interactRange = 3f;
    public LayerMask btnLayer;

    [Header("Setup")]
    private Transform PObj;
    private ThridPersonAsset playerActionsAsset;

    private void Start()
    {
        new_PlayerMovement pm = GetComponent<new_PlayerMovement>();
        PObj = pm.PObj;
    }
    private void Awake()
    {
        playerActionsAsset = new ThridPersonAsset();
    }
    private void OnEnable()
    {
        playerActionsAsset.Player.Pickup.started += PressBtn;
        playerActionsAsset.Player.Enable();
    }

    private void OnDisable()
    {
        playerActionsAsset.Player.Pickup.started -= PressBtn;
        playerActionsAsset.Player.Disable();
    }
    private Switch currentBtn = null;
    private void PressBtn(InputAction.CallbackContext obj)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, PObj.forward, out hit, interactRange, btnLayer))
        {
            currentBtn = hit.collider.gameObject.GetComponent<Switch>();
            currentBtn.Action();
            currentBtn = null;
        }

    }
}
