using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] Text dialogText;
    [SerializeField] GameObject actionSelect;
    [SerializeField] GameObject moveSelect;
    [SerializeField] GameObject moveDescription;

    [SerializeField] List<Text> actionsTexts;
    [SerializeField] List<Text> movesTexts;
    [SerializeField] Text ppText;
    [SerializeField] Text typeText;

    public float charactersPerSecond = 10.0f;

    [SerializeField] Color selectedColor = Color.blue;


    public IEnumerator SetDialog(string message)
    {
        dialogText.text = "";
        foreach (var character in message)
        {
            dialogText.text += character;
            yield return new WaitForSeconds(1/charactersPerSecond);
        }
    }

    public void ToggleDialogText(bool activated)
    {
        dialogText.enabled = activated;
    }

    public void ToggleActionsText(bool activated)
    {
        actionSelect.SetActive(activated);
    }

    public void ToggleMoves(bool activated)
    {
        moveSelect.SetActive(activated);
        moveDescription.SetActive(activated);
    }

    public void SelectAction(int selectedAction)
    {
        for (int i = 0; i < actionsTexts.Count; i++)
        {
            actionsTexts[i].color = (i==selectedAction ? selectedColor : Color.black);
        }
    }

    public void SetPokemonMoves(List<Move> moves)
    {
        for(int i = 0; i<movesTexts.Count; i++)
        {
            if (i< moves.Count)
            {
                movesTexts[i].text = moves[i].Base.Name;
            }
        } 
    }
}
