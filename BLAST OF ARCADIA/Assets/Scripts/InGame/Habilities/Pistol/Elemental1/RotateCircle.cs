using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCircle : MonoBehaviour
{
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rotation());
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * speed);

    }
    IEnumerator Rotation()
    {
        speed = -100;
        yield return new WaitForSeconds(0.2f); 
        speed = -500;
        yield return new WaitForSeconds(0.4f);
        speed = -700;
        yield return new WaitForSeconds(0.3f);
        speed = -1000;
        yield return new WaitForSeconds(0.4f);
        speed = -2000;
        yield return new WaitForSeconds(0.3f); 
        speed = -4000;
        yield return new WaitForSeconds(0.2f);

    }
}
