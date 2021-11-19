using UnityEngine;
public struct MapInformation
{
    public const int X = 100;   // Map x 크기
    public const int Y = 50;   // Map y 크기
}
    
public class MapInitializer : MonoBehaviour
{
    #region 프로퍼티 정의
    [field : SerializeField] 
    protected bool IsDebugMode { get; set; }
    protected int[,] Map { get; set; }
    protected int MapX { get; set; }
    protected int MapY { get; set; }
    #endregion

    protected virtual void InitializeMapData(int X = MapInformation.X, int Y = MapInformation.Y)
    {
        Map = new int[Y, X];

        MapX = Map.GetLength(1);
        MapY = Map.GetLength(0);

        // map의 값 초기화
        for (var y = 0; y < MapY; y++)
        {
            for (var x = 0; x < MapX; x++)
            {
                Map[y, x] = 1;
            }
        }
    }
}

/// <summary>
/// [position information class] field : startX = 0, startY = 0, endX = 0, endY = 0
/// </summary>
public class MapLocationPosition
{
    #region 프로퍼티 정의
    public int StartX { get; set; }
    public int EndX { get; set; }
    public int StartY { get; set; }
    public int EndY { get; set; }
    #endregion

    public MapLocationPosition(int startX = 0, int startY = 0, int endX = 0, int endY = 0)
    {
        StartX = startX;
        StartY = startY;
        EndX = endX;
        EndY = endY;
    }
}

public struct Type
{
    public int x;
    public int y;
    public int[,] structure;

    public Type(int[,] structure)
    {
        this.structure = structure;
        x = structure.GetLength(0);
        y = structure.GetLength(1);
    }
}