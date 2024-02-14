using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Pokemon/New Move")]

public class MoveBase : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private PokemonType type;
    [SerializeField] private MoveCategory category;
    [SerializeField] private int pp;
    [SerializeField] private int power;
    [SerializeField] private int accuracy;
    [TextArea][SerializeField] private string description;

    //getters
    public string Name => name;
    public PokemonType Type => type;
    public MoveCategory Category => category;
    public int Pp => pp;
    public int Power => power;
    public int Accuracy => accuracy;
    public string Description => description; 
}

public enum MoveCategory
{
    Special,
    Physical,
    Status
}