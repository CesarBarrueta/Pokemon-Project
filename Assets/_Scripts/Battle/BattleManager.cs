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

        battleDialogBox.SetPokemonMoves(playerUnit.Pokemon.Moves);

        enemyUnit.SetupPokemon();
        enemyHUD.SetPkmnData(enemyUnit.Pokemon);

        yield return battleDialogBox.SetDialog($"A wild {enemyUnit.Pokemon.Base.Name} appeared!");
        yield return new WaitForSeconds(1.0f);

        //TODO: Comparar speed de EnemyUnit y PlayerUnit para decidir quien ataca primero
        if(enemyUnit.Pokemon.Speed > playerUnit.Pokemon.Speed)
        {
            yield return battleDialogBox.SetDialog($"Enemy {enemyUnit.Pokemon.Base.Name} attacks first");
            EnemyAction();
        }else
        {
            yield return battleDialogBox.SetDialog($"{playerUnit.Pokemon.Base.Name} attacks first");
            PlayerAction();
        }

    }

    public void PlayerAction()
    {
        state = BattleState.PlayerSelectAction;
        StartCoroutine(battleDialogBox.SetDialog("Select an action"));
        
        battleDialogBox.ToggleDialogText(true);
        battleDialogBox.ToggleActionsText(true);
        battleDialogBox.ToggleMoves(false);

        //currentSelectedAction = 0;
        battleDialogBox.SelectAction(currentSelectedAction);
    }

    public void PlayerMovement ()
    {
        state = BattleState.PlayerMove;

        battleDialogBox.ToggleDialogText(false);
        battleDialogBox.ToggleActionsText(false);
        battleDialogBox.ToggleMoves(true);

        currentSelectedMove = 0;
        battleDialogBox.SelectMove(currentSelectedMove, playerUnit.Pokemon.Moves[currentSelectedMove]);
    }

    public void EnemyAction()
    {

    }

    private int currentSelectedAction;
    private float timeSinceLastClick;
    public float timeBetweenClicks = 1.0f;
    private void HandlePlayerActionSelection()
    {
        if(timeSinceLastClick < timeBetweenClicks)
        {
            return;
        }

        if(Input.GetAxisRaw("Vertical") != 0)
        {
            timeSinceLastClick = 0;
            currentSelectedAction = (currentSelectedAction + 1) % 2;
            battleDialogBox.SelectAction(currentSelectedAction);
        }

        if(Input.GetAxisRaw("Submit") != 0)
        {
            timeSinceLastClick = 0;
            if(currentSelectedAction == 0)
            {
                PlayerMovement();
            }else if(currentSelectedAction == 1)
            {
                //TODO: Run
            }
        }
    }

    private int currentSelectedMove;
    private void HandlePlayerMoveSelector()
    {
        if(timeSinceLastClick < timeBetweenClicks)
        {
            return;
        }

        if(Input.GetAxisRaw("Vertical") != 0)
        {
            timeSinceLastClick = 0;
            var oldSelectedMove = currentSelectedMove;
            currentSelectedMove = (currentSelectedMove + 2) % 4;
            //Bloquea que se pueda apuntar a los movimientos vacíos
            if(currentSelectedMove >= playerUnit.Pokemon.Moves.Count)
            {
                currentSelectedMove = oldSelectedMove;
            }
            battleDialogBox.SelectMove(currentSelectedMove, playerUnit.Pokemon.Moves[currentSelectedMove]);
        } else if(Input.GetAxisRaw("Horizontal") != 0)
        {
            timeSinceLastClick = 0;
            var oldSelectedMove = currentSelectedMove;
            
            if(currentSelectedMove <= 1)
            {
                currentSelectedMove = (currentSelectedMove + 1) % 2;
            } else 
            {
                currentSelectedMove = (currentSelectedMove + 1) % 2 + 2;
            }
            //Bloquea que se pueda apuntar a los movimientos vacíos
            if(currentSelectedMove >= playerUnit.Pokemon.Moves.Count)
            {
                currentSelectedMove = oldSelectedMove;
            }
            battleDialogBox.SelectMove(currentSelectedMove, playerUnit.Pokemon.Moves[currentSelectedMove]);
        }
    }

    private void Update() {
        timeSinceLastClick += Time.deltaTime;
        if(state == BattleState.PlayerSelectAction)
        {
            HandlePlayerActionSelection();
        } else if(state == BattleState.PlayerMove)
        {
            HandlePlayerMoveSelector();
        }
    }
}
