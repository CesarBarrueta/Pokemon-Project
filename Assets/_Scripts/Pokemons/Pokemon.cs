using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    private PokemonBase _base;
    private int _level;

    public PokemonBase Base => _base;
    public int Level
    {
        get => _level;
        set => _level = value;
    } 


    private List<Move> _moves;
    public List<Move> Moves
    {
        get => _moves;
        set => _moves = value;
    }

    //Actual Pokemon health value
    private int _hp;
    public int Hp
    {
        get => _hp;
        set => _hp = value;
    }

    public Pokemon (PokemonBase pkmnBase, int pkmnLevel)
    {
        _base = pkmnBase;
        _level = pkmnLevel;
        _hp = _base.MaxHp;

        _moves = new List<Move>();
        foreach (var lMove in _base.LearnableMoves)
        {
            if(lMove.Level <= _level)
            {
                _moves.Add(new Move(lMove.Move));
            }

            if(_moves.Count >= 4)
            {
                break;
            }
        }
    }

    public int MaxHP => Mathf.FloorToInt((_base.MaxHp * _level)/100.0f) + 1;
    public int Attack => Mathf.FloorToInt((_base.Attack * _level)/100.0f) + 1;
    public int Defense => Mathf.FloorToInt((_base.Defense * _level)/100.0f) + 1;
    public int SpAttack => Mathf.FloorToInt((_base.SpAttack * _level)/100.0f) + 1;
    public int SpDefense => Mathf.FloorToInt((_base.SpDefense * _level)/100.0f) + 1;
    public int Speed => Mathf.FloorToInt((_base.Speed * _level)/100.0f) + 1;

    

}
