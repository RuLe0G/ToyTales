using System.Collections;
using UnityEngine;

public class EnemyVlezYYY : MonoBehaviour
{
    public float emergeDuration = 2f; // Длительность выдвигания из земли
    public float disappearDuration = 2f; // Длительность исчезновения
    public int damageAmount = 10; // Количество урона, наносимого игроку

    public float timeToAtk = 1f;

    private float originalY;
    private float emergeDistance;
    private bool isEmerging = false;
    private bool isDisappearing = false;
    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        originalY = transform.localPosition.y;
        emergeDistance = boxCollider.bounds.size.y;
    }

    private void Update()
    {
        if (!isEmerging && !isDisappearing)
        {
            StartCoroutine(Emerge());
        }
    }

    private IEnumerator Emerge()
    {
        isEmerging = true;

        // Выдвигание из земли
        while (transform.localPosition.y < originalY + emergeDistance)
        {
            Vector3 newPosition = transform.localPosition;
            newPosition.y += Time.deltaTime / emergeDuration;
            transform.localPosition = newPosition;
            yield return null;
        }

        yield return new WaitForSeconds(1f); // Время, в котором враг находится над землей

        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        isDisappearing = true;

        // Исчезновение
        while (transform.localPosition.y > originalY)
        {
            Vector3 newPosition = transform.localPosition;
            newPosition.y -= Time.deltaTime / disappearDuration;
            transform.localPosition = newPosition;
            yield return null;
        }

        yield return new WaitForSeconds(timeToAtk); // Время до следующего появления врага

        isEmerging = false;
        isDisappearing = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharStats>().takeDamage(damageAmount);
        }
    }
}
