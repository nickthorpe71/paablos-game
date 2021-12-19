using Calc;
using Data;
using System.Collections.Generic;
using UnityEngine;

public class Graphics : MonoBehaviour
{
    public Material blackMat;
    public Material whiteMat;
    public TileGraphic[][] tileGraphics;
    private Dictionary<char, string> elementResourceMap = new Dictionary<char, string>()
    {
        ['G'] = "Elements/Green",
        ['R'] = "Elements/Red",
        ['D'] = "Elements/Dark",
        ['W'] = "Elements/White",
        ['B'] = "Elements/Blue",
        ['Y'] = "Elements/Yellow"
    };
    private List<GameObject> activePieces = new List<GameObject>();

    // --- Init ---
    public void CollectTileGraphics()
    {
        tileGraphics = SortTileGraphics(GameObject.FindGameObjectsWithTag("Tile"));
    }

    public TileGraphic[][] SortTileGraphics(GameObject[] collectedGraphics)
    {
        TileGraphic[][] result = new TileGraphic[Const.BOARD_HEIGHT][];

        for (int i = 0; i < Const.BOARD_HEIGHT; i++)
            result[i] = new TileGraphic[Const.BOARD_WIDTH];

        foreach (GameObject graphic in collectedGraphics)
        {
            int x = (int)graphic.transform.position.x;
            int y = (int)graphic.transform.position.z;
            result[y][x] = graphic.GetComponent<TileGraphic>();
        }

        return result;
    }

    // --- Instantiation ---
    public void InstantiateInitialBoard(Data.Board board)
    {
        BoardC.LoopTiles(board.tiles, InitTileOccupant);
    }

    private void InitTileOccupant(Tile tile)
    {
        if (tile.contents == TileContents.Piece)
            InstantiatePiece(tile.piece, tile.x, tile.y);
        else if (tile.contents == TileContents.Element)
            InstantiateElement(tile.element, tile.x, tile.y);
    }

    private void InstantiatePiece(Piece piece, int x, int y)
    {
        string path = PieceC.GetPathByLabel(piece.label);
        Vector3 pos = new Vector3(x, 0, y);
        int rotationY = piece.color == PieceColor.White ? 180 : 0;
        Vector3 rotation = new Vector3(0, rotationY, 0);

        GameObject newPiece = Instantiate(Resources.Load(path) as GameObject);
        newPiece.transform.position = pos;
        newPiece.transform.eulerAngles = rotation;

        newPiece.GetComponent<SetBaseColor>().SetColor(piece.color == PieceColor.White ? whiteMat : blackMat);
    }

    private void InstantiateElement(char element, int x, int y)
    {
        string path = "Elements/" + element;
        Vector3 pos = new Vector3(x, 0.5f, y);
        GameObject newPiece = Instantiate(Resources.Load(path) as GameObject);
        newPiece.transform.position = pos;
    }

    // --- Update ---
    public void UpdateTileGraphics(Tile[][] tiles)
    {
        GraphicsC.LoopTileGraphicsWithIndexes(tileGraphics, (graphic, x, y) =>
        {
            Tile currentTile = tiles[y][x];

            if (currentTile.isClicked)
            {
                graphic.Click();
                return;
            }
            else if (currentTile.isHovered)
            {
                graphic.Hover();
                return;
            }
            else if (currentTile.isHighlighted)
            {
                graphic.Highlight();
                return;
            }
            else if (currentTile.isAOE)
            {
                graphic.AOE();
                return;
            }
            else
            {
                graphic.Reset();
            }
        });
    }
}