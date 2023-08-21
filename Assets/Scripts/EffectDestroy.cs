using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);// (오브젝트, 시간)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
