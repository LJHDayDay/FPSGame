using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    //필요속성: 마우스 입력 X, Y, 속도
    public float speed = 10.0f;

    private void Start()
    {
        //마우스를 화면 가운데에다가 고정시킨 후 마우스 이미지를 삭제
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        //순서1. 사용자의 마우스 입력(X,Y)을 받는다.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //순서2. 마우스 입력에 따라 회전 방향을 설정한다.
        Vector3 dir = new Vector3(0, mouseX, 0);

        //순서3. 물체를 회전시킨다.
        //r = r0 + vt
        transform.eulerAngles = transform.eulerAngles + dir * speed;

    }
}
