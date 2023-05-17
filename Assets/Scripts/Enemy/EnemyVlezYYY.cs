using System.Collections;
using UnityEngine;

public class EnemyVlezYYY : MonoBehaviour
{
    public float emergeDuration = 2f; // ������������ ���������� �� �����
    public float disappearDuration = 2f; // ������������ ������������
    public int damageAmount = 10; // ���������� �����, ���������� ������

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

        // ���������� �� �����
        while (transform.localPosition.y < originalY + emergeDistance)
        {
            Vector3 newPosition = transform.localPosition;
            newPosition.y += Time.deltaTime / emergeDuration;
            transform.localPosition = newPosition;
            yield return null;
        }

        yield return new WaitForSeconds(1f); // �����, � ������� ���� ��������� ��� ������

        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        isDisappearing = true;

        // ������������
        while (transform.localPosition.y > originalY)
        {
            Vector3 newPosition = transform.localPosition;
            newPosition.y -= Time.deltaTime / disappearDuration;
            transform.localPosition = newPosition;
            yield return null;
        }

        yield return new WaitForSeconds(timeToAtk); // ����� �� ���������� ��������� �����

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
