using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public List<AtckSO> combo;
    float lastClicketTime;
    float lastComboEnd;
    int comboCounter;

    Animator anim;
    [SerializeField]
    SwingCheck swing;

    private ThridPersonAsset playerActionsAsset;
    private InputAction attck;

    private void Awake()
    {
        playerActionsAsset = new ThridPersonAsset();
    }
    private void OnEnable()
    {
        playerActionsAsset.Player.Attack.started += PressAttck;
        playerActionsAsset.Player.Enable();
    }

    private void OnDisable()
    {
        playerActionsAsset.Player.Attack.started -= PressAttck;
        playerActionsAsset.Player.Disable();
    }

    private void PressAttck(InputAction.CallbackContext obj)
    {
        Attack();
    }

    private void Start()
    {
        anim= GetComponentInChildren<Animator>();
    }
    private void Update()
    {        
        ExitAttack();
    }
    void Attack()
    {
        if(Time.time - lastComboEnd > 0.01f && comboCounter < combo.Count) 
        {
            CancelInvoke("EndCombo");

            if(Time.time - lastClicketTime >= 0.2f)
            {
                anim.runtimeAnimatorController = combo[comboCounter].animationOV;
                anim.Play("Attack", 0, 0);
                swing.damage = combo[comboCounter].damage; 
                comboCounter++;
                lastClicketTime= Time.time;

                if(comboCounter >= combo.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }
    void ExitAttack()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 1);
        }
    }
    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }
}
