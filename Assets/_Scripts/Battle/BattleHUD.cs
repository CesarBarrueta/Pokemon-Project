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
    [SerializeField]
    private Text hpNumber;

    private Pokemon _pokemon;

    public void SetPkmnData(Pokemon pokemon)
    {
        _pokemon = pokemon;

        pokemonName.text = pokemon.Base.Name;
        pokemonLvl.text = $"Lv {pokemon.Level}";
        UpdatePkmnData();
    }

    public void UpdatePkmnData()
    {
        hpBar.SetHP(_pokemon.Hp/(float)_pokemon.MaxHP);
        hpNumber.text = $"{_pokemon.Hp}/{_pokemon.MaxHP}";
    }
}
