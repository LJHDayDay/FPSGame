using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// ��ǥ1: ������ ����(Ready, Start, GameOver)�� �����ϰ�, ������ ���۰� ���� TextUI�� ǥ���ϰ� �ʹ�.
// �ʿ�Ӽ�1: ���ӻ��� ������ ����, TextUI

// ��ǥ2: 2�� �� ������ Ready ���¿��� Start ���·� ����Ǹ� ������ ���۵ȴ�.

// ��ǥ3: �÷��̾��� hp�� 0���� ������ ���� �ؽ�Ʈ�� ���¸��� GameOver�� �ٲ��ش�.
// �ʿ�Ӽ�: hp�� ����ִ� playerMove

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    // �ʿ�Ӽ�1: ���ӻ��� ������ ����, TextUI
    public enum GameState
    {
        Ready,
        Start,
        GameOver
    }

    public GameState state = GameState.Ready;
    public TMP_Text stateText;

    // �ʿ�Ӽ�2: hp�� ����ִ� playerMove
    PlayerMove player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        stateText.text = "Ready";
        stateText.color = new Color32(255, 185, 0, 255);

        StartCoroutine(GameStart());

        player = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    // ��ǥ2: 2�� �� ������ Ready ���¿��� Start ���·� ����Ǹ� ������ ���۵ȴ�.
    IEnumerator GameStart()
    {
        //2�ʸ� ��ٸ���.
        yield return new WaitForSeconds(2);

        stateText.text = "Game Start";
        stateText.color = Color.green;

        //0.5�ʸ� ��ٸ���.
        yield return new WaitForSeconds(0.5f);

        //���� text ��Ȱ��
        stateText.gameObject.SetActive(false);

        // ���� ����
        state = GameState.Start;
    }

    void CheckGameOver()
    {
        if(player.hp < 0)
        {
            //���� �ؽ�Ʈ ON
            stateText.gameObject.SetActive(true);

            // ���� �ؽ�Ʈ�� Game Over�� ���
            stateText.text = "Game Over";

            // ���� �ؽ�Ʈ�� ���������� ��
            stateText.color = new Color32(255, 0, 0, 255);

            // ���¸� Game Over�� �ٲ�
            state = GameState.GameOver;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameOver();
    }
}
