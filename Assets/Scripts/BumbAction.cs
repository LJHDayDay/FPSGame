using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 목표: 폭탄이 물체에 부딪히면 파괴된다.
public class BumbAction : MonoBehaviour
{
    public GameObject bombEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject bombEffGO = Instantiate(bombEffect);

        bombEffGO.transform.position = transform.position;

        Destroy(gameObject);
    }
}
