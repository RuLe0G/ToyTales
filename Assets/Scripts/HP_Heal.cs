using UnityEngine;

public class HP_Heal : MonoBehaviour
{
    [SerializeField]
    private int amountHP;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharStats cs = other.GetComponent<CharStats>();
            if (cs.isMaxHp())
            {
                return;
            }
            else
            {
                cs.takeHeal(amountHP);
                Destroy(this.gameObject);
            }
        }
    }
}
