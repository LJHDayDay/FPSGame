using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 목표1 : 마우스 오른쪽 버튼을 눌러 폭탄을 특정방향으로 발사하고 싶다.
// 필요속성: 폭탄 게임 오브젝트, 발사 위치, 방향
// 1-1. 마우스 오른쪽 버튼을 누른다.
// 1-2. 폭탄 게임오브젝트를 생성하고 firePostion에 위치시킨다.
// 1-3. 폭탄 오브젝트의 rigidBody를 가져와서 힘을 가한다.

// 목표2 : 마우스 왼쪽 버튼을 누르면 시선 방향으로 총을 발사하고 싶다.
// 2-1. 마우스 왼쪽 버튼을 누른다.
// 2-2. 레이를 생성하고 발사 위치와 발사 방향을 설정한다.
// 2-3. 레이가 부딪힌 대상의 정보를 저장할 수 있는 변수를 만든다.
// 2-4. 레이를 발사하고, 부딪힌 물체가 있으면 그 위치에 피격 효과를 만든다.
// 필요속성: 피격효과 게임오브젝트, 이펙트의 파티클 시스템

public class PlayerFire : MonoBehaviour
{
    public GameObject bomb;
    public GameObject firePosition;
    public float power;
    private PlayerFire playerFire;
    

    // 필요속성: 피격효과 게임오브젝트, 이펙트의 파티클 시스템
    public GameObject hitEffect;
    new ParticleSystem particleSystem;

    void Awake()
    {
        playerFire = GameObject.Find("Player").GetComponent<PlayerFire>();
    }

    private void Start()
    {
        particleSystem = hitEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //순서1. 마우스 오른쪽 버튼을 누른다.
        if (Input.GetMouseButtonDown(1))//왼쪽(0), 오른쪽(1), 휠(2)
        {
            //순서2. 폭탄 게임오브젝트를 생성하고 firePostion에 위치시킨다.
            GameObject bombGo = Instantiate(bomb);
            bombGo.transform.position = firePosition.transform.position;

            //순서3. 폭탄 오브젝트의 rigidBody를 가져와서 힘을 가한다.
            Rigidbody rigidbody = bombGo.GetComponent<Rigidbody>();
            rigidbody.AddForce(Camera.main.transform.forward * power, ForceMode.VelocityChange );
        }

        // 2-1. 마우스 왼쪽 버튼을 누른다.
        if(Input.GetMouseButtonDown(0)) {
            // 2-2. 레이를 생성하고 발사 위치와 발사 방향을 설정한다.
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            // 2-3. 레이가 부딪힌 대상의 정보를 저장할 수 있는 변수를 만든다.
            RaycastHit hitInfo = new RaycastHit();

            // 2-4. 레이를 발사하고, 부딪힌 물체가 있으면 그 위치에 피격 효과를 만든다.
            if(Physics.Raycast(ray, out hitInfo)) //ref : 주소를 복사한다 out : 
            {
                print("충돌체와의 거리는 : " + hitInfo.distance);

                //부딪힌 물체가 있으면 그 위치에 피격 효과를(법선 벡터 방향으로) 만든다.
                hitEffect.transform.position = hitInfo.point;
                hitEffect.transform.forward = hitInfo.normal;

                // 피격 이펙트를 재생한다.
                particleSystem.Play();
            }

        }


    }
}
