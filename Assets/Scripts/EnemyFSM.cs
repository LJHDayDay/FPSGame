using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 목표: 적을 FSM 다이어그램에 따라 동작시키고 싶다.
// 필요속성1: 적 상태

// 목표2: 플레이어와의 거리를 측정해서 특정 상태로 만들어준다.
// 필요속성2: 플레이어와의 거리, 플레이어 트랜스폼

// 목표3: 적의 상태가 Move일 때, 플레이어와의 공격 범위 밖이면 적이 플레이어를 따라간다.
// 필요속성3: 이동 속도, 적의 이동을 위한 캐릭터 컨트롤러, 공격 범위
public class EnemyFSM : MonoBehaviour
{
    // 필요속성1: 적 상태
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    // 필요속성2: 플레이어와의 거리, 플레이어 트랜스폼
    public float findDistance;
    Transform player;

    EnemyState enemyState;

    // 필요속성3: 이동 속도, 적의 이동을 위한 캐릭터 컨트롤러
    public float moveSpeed;
    CharacterController characterController;
    public float attackDistance = 10f;



    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.Idle;

        player = GameObject.Find("Player").transform;

        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // 목표: 적을 FSM 다이어그램에 따라 동작시키고 싶다.
        switch (enemyState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
            case EnemyState.Die:
                Die();
                break;
        }
    }

    private void Die()
    {
        throw new NotImplementedException();
    }

    private void Damaged()
    {
        throw new NotImplementedException();
    }

    private void Return()
    {
        throw new NotImplementedException();
    }

    private void Attack()
    {
        
    }

    // 목표3: 적의 상태가 Move일 때, 플레이어와의 공격 범위 밖이면 적이 플레이어를 따라간다.
    private void Move()
    {
        //플레이어와의 공격 범위 밖이면 적이 플레이어를 따라간다.
        float distanceToPlayer = (player.position - transform.position).magnitude;
        if (distanceToPlayer < attackDistance)
        {
            //공격 범위 내로 들어오면 공격으로 상태를 변경한다.
            enemyState = EnemyState.Attack;
            print("상태 전환: Move -> Attack");
            
        }
        else
        {
            Vector3 dir = (player.position - transform.position).normalized;

            //플레이어를 따라간다.
            characterController.Move(dir * moveSpeed * Time.deltaTime);
        }
    }

    // 목표2: 플레이어와의 거리를 측정해서 특정 상태로 만들어준다.
    private void Idle()
    {
        float distanceToPlayer = (player.position - transform.position).magnitude;
        //float tempDist = Vector3.Distance(transform.position, player.position);

        // 현재 플레이어와의 거리가 특정 범위 내 면 상태를 Move로 바꿔준다.
        if(distanceToPlayer < findDistance)
        {
            enemyState = EnemyState.Move;
            print("상태 전환 : Idle -> Move");
        }
    }
}
