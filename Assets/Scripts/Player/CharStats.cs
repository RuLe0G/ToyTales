using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class CharStats : MonoBehaviour
{
    [SerializeField]
    private int HP;
    [SerializeField]
    private int maxHP;
    [SerializeField]
    private Freezer freezer;

    public event EventHandler onHpChanged;

    public VisualEffect damageParticles;

    public void Awake()
    {
        HP = maxHP;
    }
    public int getHealth()
    {
        return HP;
    }
    public bool isMaxHp()
    {
        if (HP == maxHP)
            return true;
        return false;
    }

    public bool isInvulnerable = false;

    public void takeDamage(int incomingDamage)
    {
        if (!isInvulnerable)
        {

            PlayDamageParticles();

            StartCoroutine(DelayedHitStop(0.1f));

            StartCoroutine(SetInvulnerable(0.7f));

            HP -= incomingDamage;
            if (HP <= 0)
            {
                death();
            }
            if (onHpChanged != null)
            {
                onHpChanged(this, EventArgs.Empty);
            }
        }
    }

    private IEnumerator DelayedHitStop(float delay)
    {
        yield return new WaitForSeconds(delay);
        freezer.Freeze();
    }

    private IEnumerator SetInvulnerable(float duration)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(duration);
        isInvulnerable = false;
    }

    private void PlayDamageParticles()
    {
        var partic = Instantiate(damageParticles, transform.position, Quaternion.identity);
        Destroy(partic.gameObject, 2f);
        partic.Play();
    }

    public void takeHeal(int incomingHeal)
    {
        HP += incomingHeal;
        if (HP > maxHP)
            HP = maxHP;

        if (onHpChanged != null)
        {
            onHpChanged(this, EventArgs.Empty);
        }
    }

    public void death()
    {
        Debug.Log("ялепрэ " + gameObject.name);
    }
}
