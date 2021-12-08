using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDoneButton : MonoBehaviour
{
    public int reward;

    public void Done()
    {
        FindObjectOfType<GameManager>().AddCoins(reward);
    }
}
