using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFiller : MonoBehaviour
{

    private enum TileType
    {
        Empty,
        Wall,
        Entrance,
        Trap
    }

    [SerializeField]
    private GameObject wallPrefab = null;
    [SerializeField]
    private GameObject entranceWallPrefab = null;
    [SerializeField]
    private GameObject trapPrefab = null;
    [SerializeField]
    private GameObject playerPrefab = null;
    [SerializeField]
    private Palette palette = null;

    private System.Random generator;

    // Start is called before the first frame update
    void Start()
    {
        generator = new System.Random(GetSeed());
        GenerateMaze();
        wallPrefab.GetComponent<Renderer>().sharedMaterial.color = palette.colors[PlayerData.Instance.currentLevel % 10];
        LevelController.Instance.Resfresh();
    }

    private int GetSeed()
    {
        return PlayerData.Instance.currentLevel;
    }

    private void GenerateMaze()
    {
        Terrain ter = gameObject.GetComponent<Terrain>();
        if (ter == null)
        {
            return;
        }
        ter.terrainData.size = new Vector3(LevelController.Instance.LevelWidth, 100, LevelController.Instance.LevelLength);
        int width = Mathf.RoundToInt(ter.terrainData.size.x); //from terrain
        int length = Mathf.RoundToInt(ter.terrainData.size.z); //from terrain
        int[,,] entrance = GetEntrances(width, length);
        int[,] traps = GetTraps(width, length);
        TileType[,] walls = GetWalls(width, length, entrance, traps);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                GameObject tile = null;
                switch (walls[i, j])
                {
                    case TileType.Entrance:
                        tile = Instantiate(entranceWallPrefab, this.transform);
                        break;
                    case TileType.Trap:
                        tile = Instantiate(trapPrefab, this.transform);
                        break;
                    case TileType.Wall:
                        tile = Instantiate(wallPrefab, this.transform);
                        break;
                    case TileType.Empty:
                    default:
                        break;
                }
                if(tile != null)
                {
                    tile.transform.position =
                            new Vector3(
                                i + (tile.transform.localScale.x / 2),
                                (tile.transform.localScale.y / 2),
                                j + (tile.transform.localScale.z / 2)
                            );
                }
            }
        }
        GameObject player = Instantiate(playerPrefab);
        Point center = GetCenter(width, length);
        player.transform.position = new Vector3(center.x + (player.transform.localScale.x / 2), (player.transform.localScale.y / 4 * 3), center.y + (player.transform.localScale.z / 2));
    }

    private Point GetCenter(int width, int length)
    {
        return new Point(width / 2, length / 2);
    }

    private bool IsCenter(Point curPos, int width, int length)
    {
        Point center = GetCenter(width, length);
        bool goodForX = (curPos.x >= (center.x - Constants.kCenterIOffset)) && (curPos.x <= (center.x + Constants.kCenterIOffset));
        bool goodForY = (curPos.y >= (center.y - Constants.kCenterJOffset)) && (curPos.y <= (center.y + Constants.kCenterJOffset));
        return goodForX && goodForY;
    }

    private bool IsOuterWall(Point curPos, int[,,] entrance, int width, int length)
    {
        for (int i = 0; i < LevelController.Instance.EntrancesCount; i++)
        {
            for (int j = 0; j < Constants.kEntranceLength; j++)
            {
                if ((curPos.x == entrance[i, j, 0]) && (curPos.y == entrance[i, j, 1]))
                {
                    return false;
                }
            }
        }
        return (curPos.x == 0) || (curPos.x == (width - 1)) || (curPos.y == 0) || (curPos.y == (length - 1));
    }

    private int[,,] GetEntrances(int width, int length)
    {
        int[,,] entrance = new int[LevelController.Instance.EntrancesCount, Constants.kEntranceLength, 2];
        for (int cur = 0; cur < LevelController.Instance.EntrancesCount; cur++)
        {
            int enterIndex = 0;
            int enterDirection = generator.Next(4);
            if ((enterDirection % 2) == 0)
            {
                int iIndex = enterDirection == 0 ? 0 : width - 1;
                enterIndex = generator.Next(length);
                enterIndex = enterIndex <= 0 ? 1 : enterIndex;
                enterIndex = enterIndex >= (length - 1) ? length - 2 : enterIndex;
                for (int i = 0; i < Constants.kEntranceLength; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (j == 0)
                        {
                            entrance[cur, i, j] = iIndex;
                        }
                        else
                        {
                            entrance[cur, i, j] = enterIndex + i - 1;
                        }
                    }
                }
            }
            else
            {
                int jIndex = enterDirection == 3 ? 0 : length - 1;
                enterIndex = generator.Next(width);
                enterIndex = enterIndex <= 0 ? 1 : enterIndex;
                enterIndex = enterIndex >= (width - 1) ? width - 2 : enterIndex;
                for (int i = 0; i < Constants.kEntranceLength; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (j == 1)
                        {
                            entrance[cur, i, j] = jIndex;
                        }
                        else
                        {
                            entrance[cur, i, j] = enterIndex + i - 1;
                        }
                    }
                }
            }
        }
        return entrance;
    }

    private TileType[,] GetWalls(int width, int length, int[,,] entrance, int[,] traps)
    {
        TileType[,] walls = new TileType[width, length];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                walls[i, j] = TileType.Empty;
            }
        }
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                Point curPos = new Point(i, j);
                if (IsOuterWall(curPos, entrance, width, length))
                {
                    walls[i, j] = TileType.Wall;
                    continue;
                }
                if (IsCenter(curPos, width, length))
                {
                    continue;
                }
                if (IsEntrance(i, j, entrance))
                {
                    walls[i, j] = TileType.Entrance;
                    continue;
                }
                if (IsTrap(i, j, traps))
                {
                    walls[i, j] = TileType.Trap;
                    continue;
                }
                if (i % 2 == 0 && j % 2 == 0)
                {
                    double val = generator.NextDouble();
                    if (val > Constants.kWallsDelta)
                    {
                        if (IsEntranceNearby(i, j, width, length, entrance) || IsTrap(i, j, traps))
                        {
                            continue;
                        }
                        walls[i, j] = TileType.Wall;
                        int a = val < 0.5 ? 0 : (val < 0.5 ? -1 : 1);
                        int b = a != 0 ? 0 : (val < 0.5 ? -1 : 1);
                        int indI = i + a;
                        indI = indI < 0 ? 0 : indI >= width ? width - 1 : indI;
                        int indJ = j + b;
                        indJ = indJ < 0 ? 0 : indJ >= length ? length - 1 : indJ;
                        if (!(IsEntranceNearby(i, j, width, length, entrance) || IsTrap(indI, indJ, traps)))
                        {
                            walls[indI, indJ] = TileType.Wall;
                        }
                    }
                }
            }
        }
        return walls;
    }

    private bool IsEntrance(int x, int y, int[,,] entrance)
    {
        for (int i = 0; i < LevelController.Instance.EntrancesCount; i++)
        {
            for (int j = 0; j < Constants.kEntranceLength; j++)
            {
                if ((x == entrance[i, j, 0]) && (y == entrance[i, j, 1]))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private int[,] GetTraps(int width, int length)
    {
        int[,] traps = new int[LevelController.Instance.TrapsCount, 2];
        for (int cur = 0; cur < LevelController.Instance.TrapsCount; cur++)
        {
            bool found;
            int nextX;
            int nextY;
            do
            {
                found = false;
                //we don't want to trap any entrance
                nextX = generator.Next(width - 2) + 1;
                nextY = generator.Next(length - 2) + 1;
                for (int i = 0; i < cur; i++)
                {
                    if((nextX == traps[i,0]) && (nextY == traps[i, 1]))
                    {
                        found = true;
                        break;
                    }
                }
            } while (found);
            traps[cur, 0] = nextX;
            traps[cur, 1] = nextY;
        }
        return traps;
    }

    private bool IsTrap(int x, int y, int[,] traps)
    {
        for (int i = 0; i < LevelController.Instance.TrapsCount; i++)
        {
            if ((x == traps[i, 0]) && (y == traps[i, 1]))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsEntranceNearby(int x, int y, int width, int length, int[,,] entrance)
    {
        for (int i = 0; i < LevelController.Instance.EntrancesCount; i++)
        {
            for (int j = 0; j < Constants.kEntranceLength; j++)
            {
                for (int xShift = (x - 1); xShift <= (x + 1); xShift++)
                {
                    if((xShift < 0) || (xShift > width))
                    {
                        continue;
                    }
                    for (int yShift = (y - 1); yShift <= (y + 1); yShift++)
                    {
                        if ((yShift < 0) || (yShift > length))
                        {
                            continue;
                        }
                        if ((xShift == entrance[i, j, 0]) && (yShift == entrance[i, j, 1]))
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

}
