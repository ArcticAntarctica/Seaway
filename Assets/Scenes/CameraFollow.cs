using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    private void Awake()
    {
        StartCoroutine(ZoomOut());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0f, 27.430000608f, -34.9300003f);
        //Vector3(0, 27.1760006, -34.9300003)
            //Vector3(0, -0.254000008, 0)
    }

    IEnumerator ZoomOut()
    {
        
        
        yield return null;
    }

}
