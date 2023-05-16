using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class CharStats : MonoBehaviour
{
    [SerializeField]
    private int coins;
    [SerializeField]
    private int HP;
    [SerializeField]
    private int maxHP;
    [SerializeField]
    private Freezer freezer;

    public HealthBar healthBar;

    public event EventHandler onHpChanged;

    public VisualEffect damageParticles;

    private void Start()
    {
        healthBar.Setup(this);
    }

    public void Awake()
    {
        HP = maxHP;

        freezer = FindObjectOfType<Freezer>();
    }
    public void addCoin(int i)
    {
        coins += i;
    }
    public int getHealth()
    {
        return HP;
    }
    public float getHealthPercent()
    {
        return (float)HP / maxHP;
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
        var partic = Instantiate(damageParticles, transform.position + Vector3.up, Quaternion.identity);
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
