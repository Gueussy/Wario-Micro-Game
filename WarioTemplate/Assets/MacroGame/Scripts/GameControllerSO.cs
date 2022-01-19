using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGameControllerSO", menuName = "MacroGame/GameControllerSO", order = 0)]
public class GameControllerSO : ScriptableObject
{
    [BPMRange]
    public int currentGameSpeed = 50;

    [Range(1, 3)]
    public int currentDifficulty = 1;

}
