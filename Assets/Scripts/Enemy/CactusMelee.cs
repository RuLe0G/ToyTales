using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Unity.Burst.Intrinsics.X86;

public class CactusMelee : MonoBehaviour
{
    [Header("Base")]
    private AudioSource aud;
    public Cactus enm;
    private NavMeshAgent nma;
    private Animator anim;
    public LayerMask lmask;
    private Rigidbody rb;
    private SwingCheck swingCheck;

    private float defaultCoolDown = 0.5f;

    [Header("Fight")]
    public float coolDown;


    private void Start()
    {
        enm = GetComponent<Cactus>();
        nma = enm.nma;
        anim = enm.anim;

        TrackTick();
    }
    private void OnEnable()
    {
        if (enm == null)
        {
            enm = GetComponent<Cactus>();
        }
        CancelAttack();
        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }

    private void Update()
    {
        if(coolDown == 0f)
        {
            if (enm.target != null && Vector3.Distance(enm.target.position, transform.position) < 2f)
            {
                Swing();
            }
            return;
        }
        if (coolDown - Time.deltaTime > 0f)
        {
            coolDown -= Time.deltaTime / 2.5f;
            return;
        }
        coolDown = 0f;
    }
    public void Swing()
    {
        if (aud == null)
        {
            aud = GetComponentInChildren<SwingCheck>().GetComponent<AudioSource>();
        }
        if (nma == null)
        {
            nma = enm.nma;
        }
        enm.stopped = true;
        anim.speed = 1f;
        coolDown = defaultCoolDown;
        if (nma.enabled)
        {
            nma.isStopped = true;
        }
        anim.SetTrigger("Swing");
    }
    public void SwingEnd()
    {
        if (nma.isOnNavMesh)
        {
            nma.isStopped = false;
        }
        enm.stopped = false;
        anim.SetTrigger("StopSwing");
    }
    public void DamageStart()
    {
        aud.Play();

        if(swingCheck == null)
        {
            swingCheck = GetComponentInChildren<SwingCheck>(); 
        }
        swingCheck.damage = 30;
        swingCheck.DamageStart();
    }
    public void DamageEnd()
    {
        if(rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        enm.attacking= false;
        if (swingCheck = null)
        {
            swingCheck = GetComponentInChildren<SwingCheck>();
        }
        swingCheck.DamageStop();
    }
    public void CancelAttack()
    {
        enm.attacking = false;
        enm.stopped = false;
        coolDown = defaultCoolDown;
        if (swingCheck == null)
        {
            swingCheck = GetComponentInChildren<SwingCheck>();
        }
        swingCheck.DamageStop();
    }

    public void TrackTick()
    {
        if (gameObject.activeInHierarchy)
        {
            if (nma == null)
            {
                nma = enm.nma;
            }
            if (nma != null && nma.enabled && nma.isOnNavMesh && enm.target != null)
            {
                RaycastHit raycastHit;
                if (Physics.Raycast(enm.target.position + Vector3.up * 0.1f, Vector3.down, out raycastHit, float.PositiveInfinity, lmask))
                {
                    nma.SetDestination(raycastHit.point);
                }
                else
                {
                    nma.SetDestination(enm.target.position);
                }
            }
        }
        Invoke("TrackTick", 0.1f);
    }
}
