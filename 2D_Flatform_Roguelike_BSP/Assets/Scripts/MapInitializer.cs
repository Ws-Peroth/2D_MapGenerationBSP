using UnityEngine;
public struct MapInformation
{
    public const int X = 100;   // Map x 크기
    public const int Y = 50;   // Map y 크기
}
    
public class MapInitializer : MonoBehaviour
{
    #region 멤버 변수 정의

    [SerializeField]
    private bool _isDebugMode;
    private int[,] _map;
    private int _mapX;
    private int _mapY;
    #endregion

    #region 프로퍼티 정의
    protected bool IsDebugMode
    {
        get => _isDebugMode;
        set => _isDebugMode = value;
    }
    protected int[,] Map
    {
        get => _map;
        set => _map = value;
    }
    protected int MapX
    {
        get => _mapX;
        set => _mapX = value;
    }
    protected int MapY
    {
        get => _mapY;
        set => _mapY = value;
    }
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