using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    private PokemonBase _base;
    private int _level;

    public Pokemon (PokemonBase pkmnBase, int pkmnLevel)
    {
        _base = pkmnBase;
        _level = pkmnLevel;
    }

    public int MaxHP => Mathf.FloorToInt((_base.MaxHp * _level)/100.0f) + 1;
    public int Attack => Mathf.FloorToInt((_base.Attack * _level)/100.0f) + 1;
    public int Defense => Mathf.FloorToInt((_base.Defense * _level)/100.0f) + 1;
    public int SpAttack => Mathf.FloorToInt((_base.SpAttack * _level)/100.0f) + 1;
    public int SpDefense => Mathf.FloorToInt((_base.SpDefense * _level)/100.0f) + 1;
    public int Speed => Mathf.FloorToInt((_base.Speed * _level)/100.0f) + 1;

}
