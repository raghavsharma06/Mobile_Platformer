using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;               // gives access to the [Serialazble] attribute

/// <summary>
/// The Data Model for your game data
/// </summary>

[Serializable]
public class GameData
{
    
    public int coinCount;           // tracks the number of coins collected
    public int lives;               // tracks the player lives
    public int score;               // for tracking the score
    public bool[] keyFound;         // for tracking which keys have been found
    public LevelData[] levelData;   // for tracking level data like level unlocked, stars awarded, level number
    public bool isFirstBoot;        // for initializing data when game is started for the first time
    public bool PlayMusic, PlaySound;
}
