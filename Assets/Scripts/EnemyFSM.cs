using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ǥ: ���� FSM ���̾�׷��� ���� ���۽�Ű�� �ʹ�.
// �ʿ�Ӽ�1: �� ����

// ��ǥ2: �÷��̾���� �Ÿ��� �����ؼ� Ư�� ���·� ������ش�.
// �ʿ�Ӽ�2: �÷��̾���� �Ÿ�, �÷��̾� Ʈ������

// ��ǥ3: ���� ���°� Move�� ��, �÷��̾���� ���� ���� ���̸� ���� �÷��̾ ���󰣴�.
// �ʿ�Ӽ�3: �̵� �ӵ�, ���� �̵��� ���� ĳ���� ��Ʈ�ѷ�, ���� ����
public class EnemyFSM : MonoBehaviour
{
    // �ʿ�Ӽ�1: �� ����
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    // �ʿ�Ӽ�2: �÷��̾���� �Ÿ�, �÷��̾� Ʈ������
    public float findDistance;
    Transform player;

    EnemyState enemyState;

    // �ʿ�Ӽ�3: �̵� �ӵ�, ���� �̵��� ���� ĳ���� ��Ʈ�ѷ�
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
        // ��ǥ: ���� FSM ���̾�׷��� ���� ���۽�Ű�� �ʹ�.
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

    // ��ǥ3: ���� ���°� Move�� ��, �÷��̾���� ���� ���� ���̸� ���� �÷��̾ ���󰣴�.
    private void Move()
    {
        //�÷��̾���� ���� ���� ���̸� ���� �÷��̾ ���󰣴�.
        float distanceToPlayer = (player.position - transform.position).magnitude;
        if (distanceToPlayer < attackDistance)
        {
            //���� ���� ���� ������ �������� ���¸� �����Ѵ�.
            enemyState = EnemyState.Attack;
            print("���� ��ȯ: Move -> Attack");
            
        }
        else
        {
            Vector3 dir = (player.position - transform.position).normalized;

            //�÷��̾ ���󰣴�.
            characterController.Move(dir * moveSpeed * Time.deltaTime);
        }
    }

    // ��ǥ2: �÷��̾���� �Ÿ��� �����ؼ� Ư�� ���·� ������ش�.
    private void Idle()
    {
        float distanceToPlayer = (player.position - transform.position).magnitude;
        //float tempDist = Vector3.Distance(transform.position, player.position);

        // ���� �÷��̾���� �Ÿ��� Ư�� ���� �� �� ���¸� Move�� �ٲ��ش�.
        if(distanceToPlayer < findDistance)
        {
            enemyState = EnemyState.Move;
            print("���� ��ȯ : Idle -> Move");
        }
    }
}
