using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{

    public const int kInitialTrapsCount = 5;
    public const int kTrapsIncreaseLevel = 10;
    public const int kEntranceLength = 3;
    public const float kWallsDelta = 0.1f;
    public const int kCenterIOffset = 2;
    public const int kCenterJOffset = 2;
    public const float kCamYOffset = 4;
    public const string kPrefsFilename = "MazeRunner";
    public static readonly int[] kEntrancesPerLevels = { 8, 7, 6, 5, 4, 3, 2, 1 };
    public static readonly Vector3 kNoVelocityEps = new Vector3(0.01f, 0.01f, 0.01f);

}
