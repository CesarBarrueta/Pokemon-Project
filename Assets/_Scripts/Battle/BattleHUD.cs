using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [SerializeField]
    private Text pokemonName;
    [SerializeField]
    private Text pokemonLvl;
    [SerializeField]
    private HpBar hpBar;

    public void SetPkmnData(Pokemon pokemon)
    {
        pokemonName.text = pokemon.Base.Name;
        pokemonLvl.text = $"Lv: {pokemon.Level}";
        hpBar.SetHP(pokemon.Hp/pokemon.MaxHP);
    }
}
