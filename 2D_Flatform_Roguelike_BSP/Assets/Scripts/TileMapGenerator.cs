using System.Collections.Generic;
using UnityEngine;

public abstract class TileMapGenerator : MapGeneratorFixedBSP
{
    #region 멤버 변수 정의

    [SerializeField]
    private GameObject _tile;   // Dummy Tile Object
    private static Vector3 _worldStart;
    private float _tileSize;
    private int _tileKind;

    #endregion

    #region 프로퍼티 정의
    [field: SerializeField]
    protected List<Sprite> TileSprites { get; set; }

    [field : SerializeField]
    protected GameObject[,] TileMapObjects { get; set; }

    #endregion

    protected virtual void Start()
    {
        // 타일 생성 시작 지점 설정
        _worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
    }

    protected override void InitializeMapData(int X = MapInformation.X, int Y = MapInformation.Y)
    {
        base.InitializeMapData(X, Y);

        if (TileMapObjects == null)
        {
            InitializeTileMapObjects();
        }
        else if (TileMapObjects.GetLength(0) != MapY && TileMapObjects.GetLength(1) != MapX)
        {
            InitializeTileMapObjects();
        }
    }

    private void InitializeTileMapObjects()
    {
        // 타일 크기 설정
        _tileSize = _tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        // 타일맵 배열 할당
        TileMapObjects = new GameObject[MapY, MapX];

        // 타일맵 배열 초기화
        for (var y = 0; y < MapY; y++)
        {
            for (var x = 0; x < MapX; x++)
            {
                // Dummy Tile 생성
                var newTile =
                    Instantiate(_tile,
                                new Vector3(_worldStart.x + (_tileSize * x), _worldStart.y - (_tileSize * y), 0),
                                Quaternion.identity,
                                transform
                                );

                TileMapObjects[y, x] = newTile;
            }
        }
    }

    protected void GenerateTileMapObject()
    {
        // 맵 생성 시작
        GenerateMapBSP();

        // 타일맵 생성
        GenerateTileMap();
    }

    private void GenerateTileMap()
    {
        for(var y = 0; y < MapY; y++)
        {
            for(var x = 0; x < MapX; x++)
            {
                // 해당 위치의 타일맵에 스프라이트 적용
                ApplyTileMapSprite(x, y);
            }
        }
    }

    private GameObject ApplyTileMapSprite(int x, int y)
    {
        // map에서 생성할 타일과 타일의 종류를 가져옴
        _tileKind = Map[y, x];
        var targetTile = TileMapObjects[y, x];

        // 타일 종류에 맞는 Sprite 부여
        targetTile.GetComponent<SpriteRenderer>().sprite = TileSprites[_tileKind];
        return targetTile;
    }
}