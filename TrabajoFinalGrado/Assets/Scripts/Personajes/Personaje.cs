using System;
using UnityEngine;

public class Personaje:MonoBehaviour
{
        public PersonajeVida PersonajeVida { get; private set; }

        private void Awake()
        {
                PersonajeVida = GetComponent<PersonajeVida>();
        }

        public void RestaurarPersonaje()
        {
                PersonajeVida.RestaurarPersonaje();
        }
}
