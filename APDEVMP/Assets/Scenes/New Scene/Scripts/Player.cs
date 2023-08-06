using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        /*if(transform.position.x >= GameManager.topRight.x && moveHorizontal > 0)
        {
            moveHorizontal = 0;

        }
        if(transform.position.x <= GameManager.bottomLeft.x && moveHorizontal < 0)
        {
            moveHorizontal = 0;
        }*/
        transform.Translate(moveHorizontal * Vector2.right);
        
    }
}
