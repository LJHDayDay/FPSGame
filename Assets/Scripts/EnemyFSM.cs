using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ��ǥ: ���� FSM ���̾�׷��� ���� ���۽�Ű�� �ʹ�.
// �ʿ�Ӽ�1: �� ����

// ��ǥ2: �÷��̾���� �Ÿ��� �����ؼ� Ư�� ���·� ������ش�.
// �ʿ�Ӽ�2: �÷��̾���� �Ÿ�, �÷��̾� Ʈ������

// ��ǥ3: ���� ���°� Move�� ��, �÷��̾���� ���� ���� ���̸� ���� �÷��̾ ���󰣴�.
// �ʿ�Ӽ�3: �̵� �ӵ�, ���� �̵��� ���� ĳ���� ��Ʈ�ѷ�, ���� ����

// ��ǥ4: �÷��̾ ���ݹ��� ���� ������ Ư�� �ð��� �ѹ��� attackPower�� ������ �����Ѵ�.
// �ʿ�Ӽ�4: ����ð�, Ư������ ������ , attackPower

// ��ǥ5: �÷��̾ ���󰡴ٰ� �ʱ� ��ġ���� ���� �Ÿ��� ����� Return ���·� ��ȯ�Ѵ�.
// �ʿ�Ӽ�5: �ʱ���ġ, �̵� ���� ����

// ��ǥ6: �ʱ� ��ġ�� ���ƿ´�. Ư�� �Ÿ� �̳���, Idle ���·� ��ȯ�Ѵ�.
// �ʿ�Ӽ�6: Ư�� �Ÿ�

// ��ǥ7: �÷��̾��� ������ ������ hitDamage��ŭ ���׹��� hp�� ���ҽ�Ų��.
// �ʿ�Ӽ�7: hp

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

    // �ʿ�Ӽ�4: ����ð�, Ư������ ������ 
    float currentTime = 0;
    public float attackTime;
    public int attackPower = 1;

    // �ʿ�Ӽ�5: �ʱ���ġ, �̵� ���� ����
    Vector3 originPos;
    public float moveDistance = 20f;

    // �ʿ�Ӽ�6: Ư�� �Ÿ�
    float returnDistance = 0.3f;

    // �ʿ�Ӽ�7: hp
    public int hp = 3;

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.Idle;

        player = GameObject.Find("Player").transform;

        characterController = GetComponent<CharacterController>();

        originPos = transform.position;
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
                //Damaged();
                break;
            case EnemyState.Die:
                //Die();
                break;
        }
    }

    private void Die()
    {
        StopAllCoroutines();

        StartCoroutine(DieProcess());
    }

    // ����: 2���Ŀ� �� �ڽ��� �����ϰڴ�.
    IEnumerator DieProcess()
    {
        yield return new WaitForSeconds(2);

        print("���");
        Destroy(gameObject);
    }

    // ��ǥ7: �÷��̾��� ������ ������ damage��ŭ ���׹��� hp�� ���ҽ�Ų��.
    // ��ǥ8: ���׹��� ü���� 0���� ũ�� �ǰ� ���·� ��ȯ
    // ��ǥ9: �׷��� ������ ���� ���·� ��ȯ
    public void DamageAction(int damage)
    {
        // ����, �̹� ���׹̰� �ǰݵưų�, ��� ���¶�� �������� ���� �ʴ´�.
        if(enemyState == EnemyState.Damaged || enemyState == EnemyState.Die)
        {
            return;
        }

        // �÷��̾��� ���ݷ� ��ŭ hp�� ����
        hp -= damage;

        // ��ǥ8: ���׹��� ü���� 0���� ũ�� �ǰ� ���·� ��ȯ
        if (hp > 0)
        {
            enemyState = EnemyState.Damaged;
            print("���� ��ȯ : Any State -> Damaged");
            Damaged();
        }

        // ��ǥ9: �׷��� ������ ���� ���·� ��ȯ
        else
        {
            enemyState = EnemyState.Die;
            print("���� ��ȯ : Any State -> Die");
            Die();
        }
    }

    private void Damaged()
    {
        // �ǰ� ��� 0.5

        // �ǰ� ���� ó���� ���� �ڷ�ƾ ����
        StartCoroutine(DamageProcess());
    }



    // ������ ó����
    IEnumerator DamageProcess()
    {
        // �ǰ� ��� �ð���ŭ ��ٸ���.
        yield return new WaitForSeconds(0.5f);

        //���� ���¸� �̵� ���·� ��ȯ�Ѵ�
        enemyState = EnemyState.Move;
        print("���� ��ȯ : Damaged -> Move");
    }

    // ��ǥ6: �ʱ� ��ġ�� ���ƿ´�. Ư�� �Ÿ� �̳���, Idle ���·� ��ȯ�Ѵ�.
    private void Return()
    {
        float distanceToOrginPos = (originPos - transform.position).magnitude;
        // �ʱ� ��ġ�� ���ƿ´�.
        if(distanceToOrginPos > returnDistance)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            characterController.Move(dir * moveSpeed * Time.deltaTime);
        }
        //Ư�� �Ÿ� �̳���, Idle ���·� ��ȯ�Ѵ�.
        else
        {
            enemyState = EnemyState.Idle;
            print("���� ��ȯ : Return -> Idle");
        }

    }

    private void Attack()
    {
        // ��ǥ4: �÷��̾ ���ݹ��� ���� ������ Ư�� �ð��� �ѹ��� �����Ѵ�.
        float distanceToPlayer = (player.position - transform.position).magnitude;
        if (distanceToPlayer < attackDistance)
        {
            currentTime += Time.deltaTime;
            //Ư�� �ð��� �ѹ��� �����Ѵ�.

            if (currentTime > attackTime)
            {
                player.GetComponent<PlayerMove>().DamageAction(attackPower);
                print("����!");
                currentTime = 0;
            }

        }
        else
        {
            //�׷��� ������ Move�� ���¸� ��ȯ�Ѵ�.
            enemyState = EnemyState.Move;
            print("���� ��ȯ:Attack -> Move");
            currentTime = 0;
        }
    }

    // ��ǥ3: ���� ���°� Move�� ��, �÷��̾���� ���� ���� ���̸� ���� �÷��̾ ���󰣴�.
    private void Move()
    {
        //�÷��̾���� ���� ���� ���̸� ���� �÷��̾ ���󰣴�.
        float distanceToPlayer = (player.position - transform.position).magnitude;

        // ��ǥ5: �÷��̾ ���󰡴ٰ� �ʱ� ��ġ���� ���� �Ÿ��� ����� �ʱ� ��ġ�� ���ƿ´�.
        float distanceToOriginPos = (originPos - transform.position).magnitude;

        if(distanceToOriginPos > moveDistance)
        {
            enemyState = EnemyState.Return;
            print("���� ��ȯ: Move -> Return");
        }

        else if (distanceToPlayer < attackDistance)
        {
            //���� ���� ���� ������ �������� ���¸� �����Ѵ�.
            enemyState = EnemyState.Attack;
            print("���� ��ȯ: Move -> Attack");
            currentTime = attackTime;
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
