using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorBSP : MapInitializer
{
    #region 멤버 변수 정의
    private const int BorderSizeX = 2;
    private const int BorderSizeY = 2;
    #endregion

    #region 프로퍼티 정의
    private int RoomDivideRatio => Random.Range(4, 7);
    private int GetTileNumber => IsDebugMode ? (int)Tile.FilledTile : (int)Tile.BlankedTile;

    #endregion


    protected override void InitializeMapData(int X = MapInformation.X, int Y = MapInformation.Y)
    {
        base.InitializeMapData(X, Y);
    }

    protected void GenerateMapBSP()
    {
        // 맵 생성 시작
        DivideMap(5, 0, 0, MapX, MapY);
    }

    private MapLocationPosition DivideMap(int depth, int startX, int startY, int endX, int endY)
    {
        var lengthX = endX - startX;
        var lengthY = endY - startY;

        if (depth == 0 || lengthX <= 10 || lengthY <= 10)
        {
            return GenerateRoom(startX, startY, endX, endY);
        }

        MapLocationPosition firstRoom, secondRoom;
        int dividenum;

        // X의 길이가 더 길 경우 세로방향으로 분할
        if (lengthX > lengthY)
        {
            dividenum = RoomDivideRatio * lengthX / 10;
            firstRoom = DivideMap(depth - 1, startX, startY, startX + dividenum, endY);
            secondRoom = DivideMap(depth - 1, startX + dividenum, startY, endX, endY);
            ConnectVerticalRoom(firstRoom.EndX, secondRoom.StartX, firstRoom.EndY);
        }
        else    // 가로 방향으로 분할
        {
            dividenum = RoomDivideRatio * lengthY / 10;
            firstRoom = DivideMap(depth - 1, startX, startY, endX, startY + dividenum);
            secondRoom = DivideMap(depth - 1, startX, startY + dividenum, endX, endY);
            ConnectHorizonRoom(firstRoom.EndY, secondRoom.StartY, firstRoom.StartX);
        }

        return new MapLocationPosition(firstRoom.StartX, firstRoom.StartY, firstRoom.EndX, firstRoom.EndY);
    }

    private MapLocationPosition GenerateRoom(int startX, int startY, int endX, int endY)
    {
        // (startX, startY) ~ (endX, endY) 의 구역 중, border만큼을 띄우고 방을 생성
        for (var y = startY + BorderSizeY; y < endY - BorderSizeY; y++)
        {
            for (var x = startX + BorderSizeX; x < endX - BorderSizeX; x++)
            {
                Map[y, x] = 0;
            }
        }

        // 실제 방의 위치를 반환함
        // 방이 생성된 위치는 border의 안쪽임으로, border의 크기만큼 연산을 하여 반환
        return new MapLocationPosition
            (
                startX + BorderSizeX,
                startY + BorderSizeY,
                endX - BorderSizeX - 1,
                endY - BorderSizeY - 1
            );
    }

    private void ConnectVerticalRoom(int start, int end, int lockedPosition)
    {
        var tileNum = GetTileNumber;

        for (var x = start; x < end; x++)
        {
            Map[lockedPosition, x] = tileNum;
        }
    }

    private void ConnectHorizonRoom(int start, int end, int lockedPosition)
    {
        var tileNum = GetTileNumber;

        for (var y = start; y < end; y++)
        {
            Map[y, lockedPosition] = tileNum;
        }
    }
}
