using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMovimiento : MonoBehaviour
{   
    [SerializeField] private float _velocidad;

    public bool enMovimiento => _direcciondeMovimiento.magnitude > 0f;
    public Vector2 DireccionMovimiento => _direcciondeMovimiento;
    
    private PersonajeVida personajeVida;
    private Rigidbody2D _rigibody2d;
    private Vector2 _input;
    private Vector2 _direcciondeMovimiento;



    private void Awake()
    {
        personajeVida = GetComponent<PersonajeVida>();
        _rigibody2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
    }

    
    private void Update()
    {
        if (personajeVida.Derrotado)
        {
            _direcciondeMovimiento = Vector2.zero;
            
            return;
        }
        

        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //X
       
            
        
            if ( _input.x > 0.1f)
            {
                _direcciondeMovimiento.x = 1f;
            }
            else if (_input.x < 0f)
            {
                _direcciondeMovimiento.x = -1f;
            }
            else
            {
                _direcciondeMovimiento.x = 0f;
            }

            //Y
            if (_input.y > 0.1f)
            {
                _direcciondeMovimiento.y = 1f;
            }
            else if (_input.y < 0f)
            {
                _direcciondeMovimiento.y = -1f;
            }
            else
            {
                _direcciondeMovimiento.y = 0f;
            }
    }
    private void FixedUpdate()
    {
        _rigibody2d.MovePosition(_rigibody2d.position + _direcciondeMovimiento * _velocidad * Time.deltaTime);
    }
}
