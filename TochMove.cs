using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TochMove : MonoBehaviour
{
    private int auxDirecao;
    private float speed;
    void Start()
    {
        speed = 9f;
    }


    void Update()
    {
        if(auxDirecao != 0)
        {
            transform.Translate(speed * Time.deltaTime * auxDirecao, 0, 0);
        }

        if(auxDirecao < 0)
        {
         GetComponent<SpriteRenderer>().flipX = true;
        }
     if(auxDirecao > 0)
        {
        GetComponent<SpriteRenderer>().flipX = false;
        }

    
    }
    public void TochHorizontal( int direcao)
    {
        auxDirecao = direcao;
    }
    
    
    
}
