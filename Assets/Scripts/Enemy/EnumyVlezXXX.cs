using System.Collections;
using UnityEngine;

public class EnemyVlezXXX : MonoBehaviour
{
    public float emergeDuration = 2f; // ������������ ���������� �� �����
    public float disappearDuration = 2f; // ������������ ������������
    public int damageAmount = 10; // ���������� �����, ���������� ������

    private float originalX;
    private float emergeDistance;
    private bool isEmerging = false;
    private bool isDisappearing = false;
    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        originalX = transform.localPosition.x;
        emergeDistance = boxCollider.bounds.size.x;
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
        while (transform.localPosition.x < originalX + emergeDistance)
        {
            Vector3 newPosition = transform.localPosition;
            newPosition.x += Time.deltaTime / emergeDuration;
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
        while (transform.localPosition.x > originalX)
        {
            Vector3 newPosition = transform.localPosition;
            newPosition.x -= Time.deltaTime / disappearDuration;
            transform.localPosition = newPosition;
            yield return null;
        }

        yield return new WaitForSeconds(2f); // ����� �� ���������� ��������� �����

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
