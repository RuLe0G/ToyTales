using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ThridPersonDash : MonoBehaviour
{
    [Header("Setup")]
    private Transform orientation;
    private Rigidbody rb;
    private new_PlayerMovement pm;
    private Transform PObj;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    private ThridPersonAsset playerActionsAsset;
    private InputAction dashKey;
    private InputAction move;

    [Header("VFX")]
    public float activeTime = 2f;
    public float meshRefreshRate;
    public float meshDestroyDelay = 1f;
    public Transform alterPObj;
    public Material mat;
    public string shaderVarRef;
    public float shaderVarRate = 0.1f;
    public float shaderVarRefreshRate = 0.05f;
    private bool isTrailActive;
    private SkinnedMeshRenderer[] skinnedMeshRenderers;

    public Volume dashVolume;

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
        orientation = pm.orientation;
        PObj = pm.PObj;
    }
    private void DashKey(InputAction.CallbackContext obj)
    {
        Dash();
    }

    private void Update()
    {
        if (dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;
    }

    private void Dash()
    {

        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;
        pm.dashing = true;

        dashVolume.weight = 1;

        isTrailActive = true;
        StartCoroutine(ActivateTrails(activeTime));

        Vector3 forceToApply = PObj.forward * dashForce + PObj.up * dashUpwardForce;

        StartCoroutine(VolumeReducing());

        delayedForceToAplly = forceToApply;
        Invoke(nameof(DelayDashForce), dashDuration);

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

    IEnumerator VolumeReducing()
    {
        float elapsedTime = 0f;
        float startWeight = dashVolume.weight;

        while (elapsedTime < dashDuration)
        {
            elapsedTime += Time.deltaTime;
            float weight = Mathf.Lerp(startWeight, 0, elapsedTime / dashDuration);
            dashVolume.weight = weight;
            yield return null;
        }
        dashVolume.weight = 0;
    }

    IEnumerator ActivateTrails(float timeActive)
    {
        while (timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            if (skinnedMeshRenderers == null)
                skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                GameObject gObj = new GameObject();
                gObj.transform.SetPositionAndRotation(alterPObj.position, alterPObj.rotation);
                MeshRenderer mr = gObj.AddComponent<MeshRenderer>();
                MeshFilter mf = gObj.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                skinnedMeshRenderers[i].BakeMesh(mesh);
                mf.mesh = mesh;
                mr.material = mat;

                StartCoroutine(AnimateMaterial(mr.material, 0, shaderVarRate, shaderVarRefreshRate));

                Destroy(gObj, meshDestroyDelay);
            }

            yield return new WaitForSeconds(meshRefreshRate);
        }
        isTrailActive = false;
    }
    IEnumerator AnimateMaterial(Material mat, float goal, float rate, float refreshRate)
    {
        float valueToAnimate = mat.GetFloat(shaderVarRef);

        while (valueToAnimate > goal)
        {
            valueToAnimate -= rate;
            mat.SetFloat(shaderVarRef, valueToAnimate);
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
