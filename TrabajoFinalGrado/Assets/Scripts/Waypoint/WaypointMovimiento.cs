 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public enum DireccionMovimiento
 {
     Horizontal,
     Vertical
 }
public class WaypointMovimiento : MonoBehaviour
{
    [SerializeField] protected float velocidad;
    
    public  Vector3 puntoPorMoverse => _waypoint.ObtenerPosMovimiento(puntoActualIndex);
    
    protected Waypoint _waypoint;
    protected Animator _animator;
    protected int puntoActualIndex;
    protected Vector3 ultimaPos;
    
    
    // Start is called before the first frame update
    void Start()
    {
        puntoActualIndex = 0;
        _animator = GetComponent<Animator>();
        _waypoint = GetComponent<Waypoint>();
    }

    // Update is called once per frame
    void Update()
    {
        MoverNPC();
        RotarNPC();
        RotarVertical();
        if (ComprobarPuntoActualAlcanzado())
        {
            ActualizarIndexMovimiento();
        }
    }


    private void MoverNPC()
    {
        transform.position = Vector3.MoveTowards(transform.position, puntoPorMoverse, velocidad * Time.deltaTime);
    }

    private bool ComprobarPuntoActualAlcanzado()
    {
        float distanciaHastaPuntoActual = (transform.position - puntoPorMoverse).magnitude;
        if(distanciaHastaPuntoActual < 0.1f)
        {
            ultimaPos = transform.position;
            return true;
        }
        
        return false;
    }

    private void ActualizarIndexMovimiento()
    {
        //Esto sirve para que los NPCS se muevan de forma infinita
        if(puntoActualIndex == _waypoint.Puntos.Length - 1)
        {
            puntoActualIndex = 0;
        }
        else
        {
            if(puntoActualIndex < _waypoint.Puntos.Length - 1)
            {
                puntoActualIndex ++;
            }
        }
    }

    protected virtual void RotarNPC()
    {

    }

    protected virtual void RotarVertical()
    {
        
    }
}
