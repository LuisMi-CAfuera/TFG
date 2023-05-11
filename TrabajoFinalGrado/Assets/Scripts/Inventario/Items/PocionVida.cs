using UnityEngine;

[CreateAssetMenu(menuName = "Item/Pocion Vida")]
public class PocionVida : InventarioItem
{
    [Header("Pocion Info")] 
    public float RestauracionVida;

    public override bool UsarItem()
    {
        if (Inventario.Instance.Personaje.PersonajeVida.PuedeSerCurado)
        {
            Inventario.Instance.Personaje.PersonajeVida.RestaurarVida(RestauracionVida);
            return true;
        }
        return false;
    }
}
