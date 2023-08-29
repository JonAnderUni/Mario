using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public float vidaMaxima;
    public float vidaActual;

    private void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDanio(float cantidad){
        vidaActual -= cantidad;

        if(vidaActual <= 0){
            //TODO cuando los personajes mueren, aliados y enemigos
            //Quitar control al personaje
            //Animacion
        }
    }
   
}
