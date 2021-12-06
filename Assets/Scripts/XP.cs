using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: This class should be renamed to Coins, if it's going to be used
public class XP : MonoBehaviour
{
    private int xp;

    // Start is called before the first frame update
    void Start()
    {
        xp = PlayerPrefs.GetInt("totalXP", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddXP(int amount)
    {
        xp += amount;
    }

    public int GetXP()
    {
        return xp;
    }
}
