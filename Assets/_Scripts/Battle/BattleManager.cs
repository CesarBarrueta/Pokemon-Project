using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState 
{
    StartBattle,
    PlayerSelectAction,
    PlayerMove,
    EnemyMove,
    Busy
};

public class BattleManager : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHUD playerHUD;

    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHUD enemyHUD;

    [SerializeField] BattleDialogBox battleDialogBox;

    public BattleState state;

    public void Start()
    {
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        state = BattleState.StartBattle;

        playerUnit.SetupPokemon();
        playerHUD.SetPkmnData(playerUnit.Pokemon);

        enemyUnit.SetupPokemon();
        enemyHUD.SetPkmnData(enemyUnit.Pokemon);

        yield return battleDialogBox.SetDialog($"A wild {enemyUnit.Pokemon.Base.Name} appeared!");
        yield return new WaitForSeconds(1.0f);

        //TODO: Comparar speed de EnemyUnit y PlayerUnit para decidir quien ataca primero}
        if(enemyUnit.Pokemon.Speed > playerUnit.Pokemon.Speed)
        {
            StartCoroutine(battleDialogBox.SetDialog($"Enemy {enemyUnit.Pokemon.Base.Name} attacks first"));
            EnemyAction();
        }else
        {
            StartCoroutine(battleDialogBox.SetDialog($"{playerUnit.Pokemon.Base.Name} attacks first"));
            PlayerAction();
        }

    }

    public void PlayerAction()
    {
        state = BattleState.PlayerSelectAction;
        StartCoroutine(battleDialogBox.SetDialog("Select an action"));
        battleDialogBox.ToggleActionsText(true);
    }

    public void EnemyAction()
    {

    }
}
