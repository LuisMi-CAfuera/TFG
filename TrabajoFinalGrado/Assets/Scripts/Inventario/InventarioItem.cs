using UnityEngine;

public enum TiposDeItem
{
    Armas,
    Pociones,
    Pergaminos,
    Ingredientes,
    Tesoros
}
public class InventarioItem : ScriptableObject
{
    [Header("Parametros")] 
    public string ID;
    public string Nombre;
    public Sprite Icono;
    [TextArea]public string Descripcion;

    [Header("Informacion")] 
    public TiposDeItem tipo;
    public bool EsConsumible;
    public bool EsAcumulable;
    public int AcumulacionMax;

     public int Cantidad;




    public InventarioItem CopiarItem()
    {
        InventarioItem nuevaInstancia = Instantiate(this);
        return nuevaInstancia;
    }

    public virtual bool UsarItem()
    {
        return true;
    }
    
    public virtual bool EquiparItem()
    {
        return true;
    }

    public virtual bool BorrarItem()
    {
        return true;
    }




}
