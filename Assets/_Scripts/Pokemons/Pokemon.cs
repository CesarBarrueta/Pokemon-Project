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
        _hp = MaxHP;

        _moves = new List<Move>();
        foreach (var lMove in _base.LearnableMoves)
        {
            if(lMove.Level <= _level)
            {
                _moves.Add(new Move(lMove.Move));
            }

            if(_moves.Count >= 4)
            {
                break; //TODO: Comportamiento de qué hará si el pkmn tiene > 4 movimientos
            }
        }
    }

    public int MaxHP => Mathf.FloorToInt((_base.MaxHp * _level)/20.0f) + 1;
    public int Attack => Mathf.FloorToInt((_base.Attack * _level)/100.0f) + 1;
    public int Defense => Mathf.FloorToInt((_base.Defense * _level)/100.0f) + 1;
    public int SpAttack => Mathf.FloorToInt((_base.SpAttack * _level)/100.0f) + 1;
    public int SpDefense => Mathf.FloorToInt((_base.SpDefense * _level)/100.0f) + 1;
    public int Speed => Mathf.FloorToInt((_base.Speed * _level)/100.0f) + 1;


    public bool RecieveDamage(Pokemon attacker, Move move)
    {
        float modifiers = UnityEngine.Random.Range(0.85f, 1.0f);
        float baseDamage = ((((2 * attacker.Level/5f)+2f) * move.Base.Power * (float)(attacker.Attack / this.Defense))/50f)+2f;
        int totalDamage = Mathf.FloorToInt(baseDamage * modifiers);
        Debug.Log(totalDamage);
        this.Hp -= totalDamage;

        if(this.Hp <=0 )
        {
            this.Hp = 0;
            return true;  
        }

        return false;
            
    }

    public Move RandomMove()
    {
        int randId = UnityEngine.Random.Range(0,Moves.Count);
        return Moves[randId]; 
    }
    

}
