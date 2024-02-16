using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHUD playerHUD;

    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHUD enemyHUD;

    [SerializeField] BattleDialogBox battleDialogBox;

    public void Start()
    {
        SetupBattle();
    }

    public void SetupBattle()
    {
        playerUnit.SetupPokemon();
        playerHUD.SetPkmnData(playerUnit.Pokemon);

        enemyUnit.SetupPokemon();
        enemyHUD.SetPkmnData(enemyUnit.Pokemon);

        StartCoroutine(battleDialogBox.SetDialog($"A wild {enemyUnit.Pokemon.Base.Name} appeared!"));
    }
}
