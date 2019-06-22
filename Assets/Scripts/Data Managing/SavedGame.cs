using System;
using System.Collections.Generic;

[Serializable]
public class SavedGame
{
    public int finalScore;
    
    public float xPositionOfBallLauncher;   // ball launcher x position

    #region Bricks Rows informations
    public List<float> BrickRowYPos;

    #region Bricks and score balls in each row
    public List<int> valueOfHealth;
    public List<bool> isBrickEnableInRow;
    public List<int> indexOfEnableScoreBallsInRow;
    #endregion
    #endregion
}