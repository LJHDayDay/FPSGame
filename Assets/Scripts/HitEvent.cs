using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ǥ: �÷��̾�� �������� ������.

public class HitEvent : MonoBehaviour
{
    public EnemyFSM eFsm;

    public void HitPlayer()
    {
        eFsm.AttackAction();
    }
}
