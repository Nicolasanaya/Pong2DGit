using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PadMovement : MonoBehaviour
{
    //[Header("Separacion del sprite con el borde de la pantalla en Y abajo:")]
    //public float yOffsetInf;

    //[Header("Separacion del sprite con el borde de la pantalla en Y arriba:")]
   // public float yOffsetSup;
    //Velocidad de movimiento
    [Tooltip("Velocidad")]
    [SerializeField] private float velocity = 0f;

    [Header("Controles para el game pad: ")]
    [SerializeField] private KeyCode upControl;
    [SerializeField] private KeyCode downControl;

    //private Rigidbody2D _rigibdody2D;
    // Start is called before the first frame update
    void Start()
    {
        //_rigibdody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // leer el control
        // aplicar el movimiento
        // mover para ariiba
        if(Input.GetKey(upControl))
            transform.Translate(x:0f,y:velocity,z:0f);
        else if(Input.GetKey(downControl))
            transform.Translate(x:0f,y:-velocity,z:0f);

        // if (_rigibdody2D.Equals(other: null))
        //     _rigibdody2D.AddForce(Vector2.up,ForceMode2D.Impulse);
        // else{
        //     Debug.LogWarning(message: "El objeto no tiene rigibdody");
        // }
        //transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y,yOffsetInf,yOffsetSup));
    }
}
