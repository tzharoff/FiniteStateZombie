using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [Header("Zombie Wait Settings")]
    [SerializeField] private float minWaitTime = 2.5f;
    [SerializeField] private float maxWaitTime = 4.0f;

    [Header("Zombie Wander Settings")]
    [SerializeField] private float minWanderTime = 5f;
    [SerializeField] private float maxWanderTime = 10f;
    [SerializeField] private float minRotationDistance = 30f;
    [SerializeField] private float maxRotationDistance = 330f;

    public enum ZombieState
    {
        Wait,
        Wander,
        Chase,
        Attack
    }

    private ZombieState zombieState = ZombieState.Wait;

    private bool isWaiting = false;
    private Coroutine waitingCoroutine;

    private bool isWandering = false;
    private Coroutine wanderingCoroutine;

    private ZombieMovement zombieMovement;

    // Start is called before the first frame update
    void Start()
    {
        zombieMovement = GetComponent<ZombieMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (zombieState)
        {
            case ZombieState.Wait:
                ZombieWait();
                break;
            case ZombieState.Wander:
                ZombieWander();
                break;
            case ZombieState.Chase:
                ZombieChase();
                break;
            case ZombieState.Attack:
                ZombieAttack();
                break;
            default:
                break;
        }
    }
    
    private void ZombieWait()
    {
        if (!isWaiting)
        {
            waitingCoroutine = StartCoroutine(GoWandering());
        }
    }

    IEnumerator GoWandering()
    {
        isWaiting = true;
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        zombieState = ZombieState.Wander;
        isWaiting = false;
    }

    private void ZombieWander()
    {
        if (!isWandering)
        {
            isWandering = true;
            transform.Rotate(0f, Random.Range(minRotationDistance, maxRotationDistance), 0f);
            wanderingCoroutine = StartCoroutine(WalkTime());
        }
    }

    IEnumerator WalkTime()
    {
        zombieMovement.IsMoving = true;
        yield return new WaitForSeconds(Random.Range(minWanderTime, maxWanderTime));
        zombieMovement.IsMoving = false;
        isWaiting = false;
        zombieState = ZombieState.Wait;
        isWandering = false;
    }

    private void ZombieChase()
    {

    }

    private void ZombieAttack()
    {

    }
}
