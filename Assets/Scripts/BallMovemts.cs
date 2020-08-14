using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovemts : MonoBehaviour
{
    //audio
    AudioSource fuenteDeAudio;
    //Clips de audio
    public AudioClip audioGol, audioRaqueta, audioRebote;
    //velocidad
    public float speed = 30f;

    public float tiempo = 100;
    //contador de los goles
    public int golesi = 0;
    public int golesd = 0;
//Cajas de texto de los contadores
	public Text contadorIzquierda;
	public Text contadorDerecha;
    public Text Resultado;
    public Text Temporizador;

    private string minutos, segundos;



    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);

        fuenteDeAudio = GetComponent<AudioSource>();
        //contadores en 0
        contadorIzquierda.text  = golesi.ToString();
        contadorDerecha.text= golesd.ToString();

        Resultado.enabled = false;

        Time.timeScale = 1;



    }

    void OnCollisionEnter2D(Collision2D micolision){
        if(micolision.gameObject.name == "GamePadFeft"){
            int x =1;
            int y = direccionY(transform.position, micolision.transform.position);
            Vector2 direccion = new Vector2(x, y);
            //GetComponent<Rigidbody2D>().AddForce(direccion * speed);
            GetComponent<Rigidbody2D>().velocity = direccion * speed;
            fuenteDeAudio.clip = audioRaqueta;
			fuenteDeAudio.Play();
        }
        else if (micolision.gameObject.name == "GamePadRinght"){
            int x = -1;
            int y = direccionY(transform.position, micolision.transform.position);
            Vector2 direccion = new Vector2(x,y);
            GetComponent<Rigidbody2D>().velocity = direccion * speed;
            //GetComponent<Rigidbody2D>().AddForce( direccion * speed);
            fuenteDeAudio.clip = audioRaqueta;
			fuenteDeAudio.Play();

        }

        if (micolision.gameObject.name == "WallUp" || micolision.gameObject.name == "WallDown"){

			//Reproduzco el sonido del rebote
			fuenteDeAudio.clip = audioRebote;
			fuenteDeAudio.Play();

		}

    }


    // Update is called once per frame
    void Update()
    {
        tiempo -= Time.deltaTime;
        if (!comprobarFinal()){
			minutosSegundos(tiempo);
		}
		else{
			minutosSegundos(0);
		}
        speed = speed + 0.001f;
        //GetComponent<Rigidbody2D>().velocity = new Vector2( GetComponent<Rigidbody2D>().velocity.x*speed*Time.deltaTime, GetComponent<Rigidbody2D>().velocity.y);
    }

    void minutosSegundos(float tiempo){

		//Minutos
		if (tiempo > 120){
			minutos = "02";
		}
		else if (tiempo >= 60){
			minutos = "01";
		}
		else{
			minutos = "00";
		}

		//Segundos
		int numSegundos = Mathf.RoundToInt(tiempo % 60);
		if (numSegundos > 9){
			segundos = numSegundos.ToString();
		}
		else{
			segundos = "0" + numSegundos.ToString();
		}

		//Escribo en la caja de texto
		Temporizador.text = minutos + ":" + segundos;

	}
    int direccionY(Vector2 posicionBola, Vector2 posicionRaqueta){

		if (posicionBola.y > posicionRaqueta.y){
			return 1;
		}
		else if (posicionBola.y < posicionRaqueta.y){
			return -1;
		}
		else{
			return 0;
		}
	}

    public void reiniciarBola(string direccionn){
        transform.position = Vector2.zero;        
        //speed = 30f;
        //Velocidad y dirección
		if (direccionn == "Derecha"){
			//Incremento goles al de la derecha
			golesd++;
			//Lo escribo en el marcador
			contadorDerecha.text = golesd.ToString();
            //Reinicio la bola (si no ha llegado a 5)
            if (!comprobarFinal()){
                GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            }
			
		}
		else if (direccionn == "Izquierda"){
			//Incremento goles al de la izquierda
			golesi++;
			//Lo escribo en el marcador
			contadorIzquierda.text = golesi.ToString();
			//Reinicio la bola (si no ha llegado a 5)
			if (!comprobarFinal()){
				GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
				//Vector2.right es lo mismo que new Vector2(-1,0)
			}	
		}
        fuenteDeAudio.clip = audioGol;
		fuenteDeAudio.Play();
    }

    void OnTriggerEnter2D(Collider2D bola){        
        if (bola.gameObject.name == "Izquierda"){
            reiniciarBola("Derecha");
        }
        else if (bola.gameObject.name == "Derecha"){			
			reiniciarBola("Izquierda");
		}
        
    }

    bool comprobarFinal(){
		//Compruebo si se ha acabado el tiempo
		if (tiempo <= 0){

			//Compruebo quién ha ganado
			if (golesi > golesd){
				//Escribo y muestro el resultado
				Resultado.text = "¡Jugador Izquierda GANA!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
			}
			else if (golesd > golesi){
				//Escribo y muestro el resultado
				Resultado.text = "¡Jugador Derecha GANA!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
			}
			else{
				//Escribo y muestro el resultado
				Resultado.text = "¡EMPATE!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
			}
			//Muestro el resultado, pauso el juego y devuelvo true
			Resultado.enabled = true;
			Time.timeScale = 0; //Pausa
			return true;

		}

		//Si el de la izquierda ha llegado a 5
		if (golesi == 5){
			//Escribo y muestro el resultado
			Resultado.text = "¡Jugador Izquierda GANA!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
			//Muestro el resultado, pauso el juego y devuelvo true
			Resultado.enabled = true;
			Time.timeScale = 0; //Pausa
			return true;
		}
		//Si el de le aderecha a llegado a 5
		else if (golesd == 5){
			//Escribo y muestro el resultado
			Resultado.text = "¡Jugador Derecha GANA!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
			//Muestro el resultado, pauso el juego y devuelvo true
			Resultado.enabled = true;
			Time.timeScale = 0; //Pausa
			return true;
		}
		//Si ninguno ha llegado a 5, continúa el juego
		else{
			return false;
		}
	}

}
