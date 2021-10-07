using System.Collections;
using UnityEngine;

public class TileMapModifier : TileMapGenerator
{
    protected override void Start()
    {
        base.Start();

        if (IsDebugMode)
        {
            StartCoroutine(RepeatGenerateTileMap());
        }
        else
        {
            InitializeMapData();
            GenerateTileMapObject();
        }
    }

    private IEnumerator RepeatGenerateTileMap()
    {
        int mapNumber = 1;
        while (true)
        {
            InitializeMapData();
            GenerateTileMapObject();

            yield return new WaitForSeconds(1.5f);

            if (IsDebugMode)
            {
                ModifyFilledTiles();
            }
            print($"End [{mapNumber}]");
            mapNumber++;

            yield return new WaitForSeconds(2f);
        }
    }

    private void ModifyFilledTiles()
    {
        var blankTile = (int)Tile.BlankedTile;
        var filledTile = (int)Tile.FilledTile;

        for (var y = 0; y < MapY; y++)
        {
            for (var x = 0; x < MapX; x++)
            {
                var tileKind = Map[y, x];

                if (tileKind == filledTile)
                {
                    Map[y, x] = blankTile;
                    TileMapObjects[y, x].GetComponent<SpriteRenderer>().sprite = TileSprites[blankTile];
                }
            }
        }
    }
}

