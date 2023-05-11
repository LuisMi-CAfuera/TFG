using UnityEngine;

[CreateAssetMenu(menuName = "Item/Pocion Mana")]
public class PocionMana : InventarioItem
{
    [Header("Pocion Info")]
    public float RestauracionMana;

    public override bool UsarItem()
    {
        if (Inventario.Instance.Personaje.PersonajeMana.PuedeRestauar)
        {
            Inventario.Instance.Personaje.PersonajeMana.RestauarMana(RestauracionMana);
            return true;
        }
        return false;
    }
}
