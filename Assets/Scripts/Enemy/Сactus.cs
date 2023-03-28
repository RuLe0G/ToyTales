using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Сactus : MonoBehaviour
{
    public Transform target; // Ссылка на объект главного героя
    public float attackRange = 1.0f; // Расстояние, на котором противник начинает атаку
    public float viewRange = 5.0f; 
    public float moveSpeed = 3.0f; // Скорость движения противника
    public float attackDelay = 3.0f; // Задержка между атаками

    public enum State { Idle, MoveToTarget, Attack }; // Состояния противника
    public State currentState; // Текущее состояние

    private float lastAttackTime = 0f; // Время последней атаки
    private float idleTime = 0f; // Время, которое противник должен просто стоять на месте после атаки

    void Start()
    {
        currentState = State.Idle; // Начинаем в статичном состоянии
        lastAttackTime = Time.time - attackDelay; // Начальное значение времени последней атаки

        initialScale = transform.localScale;
    }
    
    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                if (Vector3.Distance(transform.position, target.position) < viewRange)
                {
                    currentState = State.MoveToTarget;
                }
                break;

            case State.MoveToTarget:
                transform.LookAt(target.position);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;

                if (Vector3.Distance(transform.position, target.position) < attackRange)
                {
                    currentState = State.Attack;
                }
                break;

            case State.Attack:
                Attack();
                if (Time.time - lastAttackTime > attackDelay)
                {                    
                    lastAttackTime = Time.time;
                    idleTime = attackDelay;
                    currentState = State.Idle; // После атаки переходим в состояние "Idle"
                }
                else
                {
                    idleTime = attackDelay - (Time.time - lastAttackTime);
                }
                break;
        }
    }

    private Vector3 initialScale;
    public float scaleFactor = 3f;
    private float returnTime = 1f;
    public GameObject Ship;
    private void Attack()
    {
        Ship.transform.localScale = initialScale * scaleFactor;

        StartCoroutine(ReturnToOriginalSize());
    }
    IEnumerator ReturnToOriginalSize()
    {

        yield return new WaitForSeconds(returnTime);


        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / returnTime;
            Ship.transform.localScale = Vector3.Lerp(Ship.transform.localScale, initialScale, t);
            yield return null;
        }


        Ship.transform.localScale = initialScale;
    }
}
