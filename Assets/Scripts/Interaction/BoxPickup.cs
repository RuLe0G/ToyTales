using UnityEngine;
using UnityEngine.InputSystem;

public class BoxPickup : MonoBehaviour
{
    [Header("Pickup Setting")]
    public float pickupRange = 3f;
    public LayerMask boxLayer;

    private GameObject currentBox = null;
    private bool isHoldingBox = false;

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
        playerActionsAsset.Player.Pickup.started += PressPickup;
        playerActionsAsset.Player.Enable();
    }

    private void OnDisable()
    {
        playerActionsAsset.Player.Pickup.started -= PressPickup;
        playerActionsAsset.Player.Disable();
    }
    RaycastHit hit;
    private void PressPickup(InputAction.CallbackContext obj)
    {
        if (!isHoldingBox)
        {

            if (Physics.Raycast(transform.position, PObj.forward, out hit, pickupRange, boxLayer))
            {
                currentBox = hit.collider.gameObject;
                currentBox.GetComponent<Rigidbody>().isKinematic = true;
                currentBox.transform.parent = transform;
                isHoldingBox = true;
            }
        }
        else
        {
            currentBox.transform.parent = null;
            currentBox.GetComponent<Rigidbody>().isKinematic = false;
            currentBox = null;
            isHoldingBox = false;
        }
        if (currentBox != null)
        {
            currentBox.transform.position += -hit.normal.normalized * 0.2f;
            currentBox.transform.position += new Vector3(0, 0.2f, 0);
        }
    }
}
