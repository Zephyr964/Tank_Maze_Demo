using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//网格类
public class Cell
{
    public int x, y;

    public bool up, down, right, left;

    public bool visited;

    public Cell(int x, int y)
    {
        this.x = x; this.y = y;
        up = true;
        down = true;
        right = true;
        left = true;
        visited = false;
    }
}
public class MImi : MonoBehaviour
{


    [Header("迷宫大小")]
    public int width;
    public int height;

    [Header("格子大小")]
    public float cellSize;

    [Header("墙壁预设体")]
    public GameObject wall;

    [Header("得分预设体")]
    public GameObject star;
    [Header("道具预设体")]
    public GameObject box1;
    public GameObject box2;
    public GameObject box3;

    //炮台预制体位置集合
    private HashSet<Vector2Int> monsterList = new HashSet<Vector2Int>();

    public Cell[,] grids;
    private Vector3 cellPos;//格子的位置
    public Vector3 CellPos
    {
        get { return cellPos; }
    }

    //用stack来实现回溯
    private Stack<Cell> stack = new Stack<Cell>();

    void Awake()
    {
        InitGrid(width, height);
        Dfs(grids[0, 0]);
        CollectMonster();
        DrawMimi();
    }

    //初始化网格
    public void InitGrid(int width, int height)
    {
        grids = new Cell[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                grids[i, j] = new Cell(i, j);
            }
        }

    }


    //寻找邻居函数
    public List<Cell> SeekNeighbour(Cell cell)
    {
        List<Cell> neighbours = new List<Cell>();
        if (cell.x - 1 >= 0)
        {
            Cell c = grids[cell.x - 1, cell.y];
            if (!c.visited)
                neighbours.Add(c);
        }

        if (cell.x + 1 < width)
        {
            Cell c = grids[cell.x + 1, cell.y];
            if (!c.visited)
                neighbours.Add(c);
        }

        if (cell.y - 1 >= 0)
        {
            Cell c = grids[cell.x, cell.y - 1];
            if (!c.visited)
                neighbours.Add(c);
        }

        if (cell.y + 1 < height)
        {
            Cell c = grids[cell.x, cell.y + 1];
            if (!c.visited)
                neighbours.Add(c);
        }
        return neighbours;
    }

    //拆除墙壁
    public void RemoveWall(Cell cell, Cell neighbour)
    {
        int dx, dy;
        //判断邻居的方位
        dx = cell.x - neighbour.x;
        dy = cell.y - neighbour.y;
        if (dx == 1)//邻居在左边
        {
            cell.left = false;
            neighbour.right = false;
        }
        if (dx == -1)
        {
            cell.right = false;
            neighbour.left = false;
        }
        if (dy == 1)//邻居在上方
        {
            cell.up = false;
            neighbour.down = false;
        }
        if (dy == -1)
        {
            cell.down = false;
            neighbour.up = false;
        }

    }

    //dfs深度算法
    public void Dfs(Cell cell)
    {
        cell.visited = true;
        stack.Push(cell);

        while (stack.Count > 0)
        {
            //取出它来找它的邻居
            Cell Scell = stack.Pop();
            List<Cell> neighbours = SeekNeighbour(Scell);

            if (neighbours.Count > 0)
            {
                //如果它有邻居就再放回去 用来重塑 把邻居存入栈顶
                stack.Push(Scell);
                int random = Random.Range(0, neighbours.Count);
                Cell neighbour = neighbours[random];
                neighbour.visited = true;
                RemoveWall(Scell, neighbour);

                stack.Push(neighbour);
            }

        }





    }

    //画出迷宫地图
    public void DrawMimi()
    {
        if (wall == null)
        {
            return;
        }


        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Cell cell = grids[i, j];
                //设置x轴为宽，z轴为高
                cellPos = new Vector3(i * cellSize, 0, j * cellSize);

                //内边界
                if (cell.down)//下墙
                {
                    Vector3 posUp = cellPos + new Vector3(0, 0, cellSize * 0.5f);
                    GameObject objUp = Instantiate(wall, posUp, Quaternion.identity, transform);
                    objUp.transform.localScale = new Vector3(cellSize /2, 2, 0.2f);
                    // 因为墙壁预设体的缩放跟普通设置的不一样，好像是标准的两倍，面板上我不会改，直接在代码缩放里/2
                }
                if (cell.right)
                {
                    Vector3 posR = cellPos + new Vector3(cellSize * 0.5f, 0, 0);
                    GameObject objR = Instantiate(wall, posR, Quaternion.identity, transform);
                    objR.transform.localScale = new Vector3(0.2f, 2, cellSize /2 );
                }

                #region 生成游戏道具
                //判断格子上是否有炮台预设体
                Vector2Int vector2 = new Vector2Int(i,j);
                if (monsterList.Contains(vector2))
                {
                    continue;
                }
                //奖励箱子的坐标
                if (i == 2 && j == 0)
                {
                    continue;
                }
                //创建奖励星星
                int randomBox = Random.Range(0, 100);
                if(randomBox < 90)
                {
                    GameObject stars = Instantiate(star, cellPos, Quaternion.identity, transform);
                }
                else if(randomBox < 93)
                {
                    GameObject stars = Instantiate(box1, cellPos, Quaternion.identity, transform);
                }
                else if(randomBox < 96)
                {
                    GameObject stars = Instantiate(box2, cellPos, Quaternion.identity, transform);
                }
                else
                {
                    GameObject stars = Instantiate(box3, cellPos, Quaternion.identity, transform);
                }


                #endregion

            }
        }


        //外边界
        for (int x = 0; x < width; x++)
        {
            Vector3 posX = new Vector3(cellSize * x, 0, cellSize * -0.5f);
            GameObject objX = Instantiate(wall, posX, Quaternion.identity, transform);
            objX.transform.localScale = new Vector3(cellSize / 2, 2, 0.2f);
        }
        for (int y = 0; y < height; y++)
        {
            Vector3 posY = new Vector3(cellSize * -0.5f, 0, cellSize * y);
            GameObject objY = Instantiate(wall, posY, Quaternion.identity, transform);
            objY.transform.localScale = new Vector3(0.2f, 2, cellSize / 2);
        }
    }

    //收集地图上的炮台预设体
    public void CollectMonster()
    {
        monsterList.Clear();
        //查找地图上所有的炮台预设体
        MonsterObj[] monsterObjs = FindObjectsOfType<MonsterObj>();
        foreach(MonsterObj monsterObj in monsterObjs)
        {
            Cell cell = PosToGrid(monsterObj.transform.position);
            monsterList.Add(new Vector2Int(cell.x, cell.y));
        }

    }

    #region Monster相关方法
    //把怪物当前坐标转换为地图的格子
    public Cell PosToGrid(Vector3 vector)
    {
        int i = Mathf.RoundToInt(vector.x / cellSize);
        int j = Mathf.RoundToInt(vector.z / cellSize);
        i = Mathf.Clamp(i, 0, width - 1);
        j = Mathf.Clamp(j, 0, height - 1);

        return grids[i, j];
    }

    //让怪物获取当前可以访问的邻居
    public List<Cell> GetNeighbor(Cell cell)
    {
        List<Cell> Mneighbor = new List<Cell>();
        if (cell.up == false && cell.y >= 0)
        {
            Mneighbor.Add(grids[cell.x, cell.y - 1]);
        }
        if (cell.down == false && cell.y < height - 1)
        {
            Mneighbor.Add(grids[cell.x, cell.y + 1]);
        }
        if (cell.right == false && cell.x < width - 1)
        {
            Mneighbor.Add(grids[cell.x + 1, cell.y]);
        }
        if (cell.left == false && cell.x >= 0)
        {
            Mneighbor.Add(grids[cell.x - 1, cell.y]);
        }

        return Mneighbor;

    }

    //输入地图格子转换为世界坐标中心
    public Vector3 GridToPos(Cell cell)
    {
        return new Vector3(cell.x * cellSize, 0, cell.y * cellSize);
    }

    #endregion
}
