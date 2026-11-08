using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterObj : TankBaseObj
{
    public GameObject bullet;
    public Transform[] bulletPos;

    #region 角度旋转相关

    //角度
    public GameObject player;//玩家
    private float index;


    private bool myCellUp;//存储自己格子信息
    private bool myCellDown;
    private bool myCellLeft;
    private bool myCellRight;


    #endregion


    #region 攻击相关
    [Header("视野相关")]
    public float viewValue = 8f;
    public Transform eyePos;
    private bool IsCanSeePlayer = false;

    //间隔开火
    public float intervalTime = 1;//间隔时间
    private float nowTime = 0;//世界时间
    #endregion

    #region 地图位置相关
    public MImi map;

    private Cell myCell;//自己的格子位置
    private List<Cell> myNeighbors;//格子周围的邻居
    private Dictionary<Cell, bool> CellValue = new Dictionary<Cell, bool>();//把格子信息存在字典里

    #endregion

    //设置怪物血条
    public Texture maxHpTex;
    public Texture hpTex;
    private Rect maxHpRect;
    private Rect hpRect;
    //血条刷新时间
    private float flashTime = 0;

    void Start()
    {
        #region 把自身周围的墙机制上封锁
        //先获取自己和邻居的格子信息
        myCell = map.PosToGrid(this.transform.position);
        myNeighbors = map.GetNeighbor(myCell);
        myCellUp = myCell.up;
        myCellDown = myCell.down;
        myCellLeft = myCell.left;
        myCellRight = myCell.right;
        foreach(Cell neighbor in myNeighbors)
        {
            bool value = GetWall(myCell, neighbor);
            CellValue[neighbor] = value;
            SetWall(myCell, neighbor, true);
        }


        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        TurretRotateLogic();
        #region 判断玩家距离然后开火
        if (player == null)
        {
            IsCanSeePlayer =false;
            return;
        }
        Vector3 dis = player.transform.position - this.transform.position;
        if (dis.magnitude > viewValue)
        {
            IsCanSeePlayer = false;
            return;
        }
        RaycastHit hit;//发射射线，检测墙体
        if(Physics.Raycast(eyePos.position, dis.normalized, out hit, dis.magnitude))
        {
            if (hit.collider.CompareTag("Player"))
            {
                IsCanSeePlayer = true;
            }
        }


        if(IsCanSeePlayer)
        {
            //看到玩家后开火
            nowTime += Time.deltaTime;
            if (nowTime > intervalTime)
            {
                Fire();
                nowTime = 0;
            }

        }

        #endregion
    }


    public override void Fire()
    {
        for (int i = 0; i < bulletPos.Length; i++)
        {
            GameObject obj = Instantiate(bullet, bulletPos[i].position, bulletPos[i].rotation);
            BulletObj bulletObj = obj.GetComponent<BulletObj>();
            bulletObj.SetFather(this);
        }

    }

    public override void Hurt(TankBaseObj obj)
    {
        base.Hurt(obj);
        flashTime = 3;
    }
    public override void Dead()
    {
        if (isDead) return;
        foreach (var val in CellValue)
        {
            Cell neighbor = val.Key;
            bool value = val.Value;
            SetWall(myCell, neighbor, value);
        }
        CellValue.Clear();
        base.Dead();
        GamePanel.Instance.AddScore(30);
    }

    private bool GetWall(Cell a, Cell b)
    {
        int dx = a.x - b.x;
        int dy = a.y - b.y;
        if (dx == 1) return a.left;
        if (dx == -1) return a.right;
        if (dy == 1) return a.up;
        if (dy == -1) return a.down;
        return true;
    }

    /// <summary>
    /// 设置两个相邻格子之间的墙状态（双向同步）
    /// </summary>
    private void SetWall(Cell a, Cell b, bool wallOn)
    {
        int dx = a.x - b.x;
        int dy = a.y - b.y;
        if (dx == 1)
        {
            a.left = wallOn;
            b.right = wallOn;
        }
        if (dx == -1)
        {
            a.right = wallOn;
            b.left = wallOn;
        }
        if (dy == 1)
        {
            a.up = wallOn;
            b.down = wallOn;
        }
        if (dy == -1)
        {
            a.down = wallOn;
            b.up = wallOn;
        }

    }


    #region 扇形转向核心
    /// <summary>
    /// 判断 0~360 的角度是否在环形区间内
    /// min和max都是0~360
    /// 例：315~45 是跨0点的区间，函数自动识别
    /// </summary>
    bool AngleInSector(float angle, float min, float max)
    {
        if (min < max)
        {
            return angle >= min && angle <= max;
        }
        else
        {
            // 跨0度的情况：315 ~ 45
            return angle >= min || angle <= max;
        }
    }

    void TurretRotateLogic()
    {
        // 当前炮台Y欧拉角（0~360）
        float currentY = tankHead.transform.eulerAngles.y;
        float targetAngle = currentY;

        // 玩家方向角度 0~360
        Vector3 dirToPlayer = player.transform.position - tankHead.transform.position;
        float playerAngle = Mathf.Atan2(dirToPlayer.x, dirToPlayer.z) * Mathf.Rad2Deg;
        if (playerAngle < 0) playerAngle += 360;

        // 开放通路（false=没有墙，可以朝这个扇形）
        bool canUp = !myCellUp;
        bool canRight = !myCellRight;
        bool canDown = !myCellDown;
        bool canLeft = !myCellLeft;
        int canNumber = 0;
        if (canUp)
        {
            canNumber += 1;
        }
        if (canRight)
        {
            canNumber += 1;
        }
        if (canDown)
        {
            canNumber += 1;
        }
        if (canLeft)
        {
            canNumber += 1;
        }


        bool playerInValid = false;
        // 判断玩家是否在开放扇形内
        if (canUp && AngleInSector(playerAngle, 315, 45)) playerInValid = true;
        if (canRight && AngleInSector(playerAngle, 45, 135)) playerInValid = true;
        if (canDown && AngleInSector(playerAngle, 135, 225)) playerInValid = true;
        if (canLeft && AngleInSector(playerAngle, 225, 315)) playerInValid = true;

        if (playerInValid && IsCanSeePlayer)
        {
            //玩家在合法扇形 + 射线能看见 → 转向玩家
            targetAngle = playerAngle;
        }
        else
        {
            // ========== 全部巡逻逻辑，限制在这个else里面 ==========
            float[] sectorStart = { 315, 45, 135, 225 };
            bool[] open = { canUp, canRight, canDown, canLeft };
            List<int> openIdxList = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                if (open[i]) openIdxList.Add(i);
            }

            if (openIdxList.Count == 0)
            {
                targetAngle = currentY;
            }
            else
            {
                //❌ 看不见玩家 / 玩家不在允许扇形 → 在开放扇形内来回巡逻
                if(canNumber == 1)
                {
                    if (canUp)
                    {
                        targetAngle = 315 + Mathf.PingPong(Time.time * RotateSpeed, 90f);
                    }
                    else if (canRight)
                    {
                        targetAngle = 45 + Mathf.PingPong(Time.time * RotateSpeed, 90f);
                    }
                    else if (canDown)
                    {
                        targetAngle = 135 + Mathf.PingPong(Time.time * RotateSpeed, 90f);
                    }
                    else if (canLeft)
                    {
                        targetAngle = 225 + Mathf.PingPong(Time.time * RotateSpeed, 90f);
                    }
                }
                else if(canNumber == 2)
                {
                    if(canUp && canDown)
                    {
                        targetAngle = 315 + Mathf.PingPong(Time.time * RotateSpeed, 270f);
                    }
                    else if( canRight && canLeft)
                    {
                        targetAngle = 45 + Mathf.PingPong(Time.time * RotateSpeed, 270f);
                    }
                    else if(canUp && canRight)//右上  从代码层面看是没问题，但是00具体好像左右弄反了
                    {
                        //targetAngle = 315 + Mathf.PingPong(Time.time * RotateSpeed, 180f);
                        targetAngle = 45 + Mathf.PingPong(Time.time * RotateSpeed, 180f);
                    }
                    else if (canUp && canLeft)//左上
                    {
                        //targetAngle = 225 + Mathf.PingPong(Time.time * RotateSpeed, 180f);
                        targetAngle = 135 + Mathf.PingPong(Time.time * RotateSpeed, 180f);
                    }
                    else if (canDown && canRight)//右下
                    {
                        //targetAngle = 45 + Mathf.PingPong(Time.time * RotateSpeed, 180f);
                        targetAngle = 315 + Mathf.PingPong(Time.time * RotateSpeed, 180f);
                    }
                    else if (canDown && canLeft)//左下
                    {
                        //targetAngle = 135 + Mathf.PingPong(Time.time * RotateSpeed, 180f);
                        targetAngle = 225 + Mathf.PingPong(Time.time * RotateSpeed, 180f);
                    }

                }
                else
                {
                    if (canUp && canDown && canRight)
                    {
                        targetAngle = 315 + Mathf.PingPong(Time.time * RotateSpeed, 270f);
                    }
                    else if(canUp && canDown && canLeft)
                    {
                        targetAngle = 135 + Mathf.PingPong(Time.time * RotateSpeed, 270f);
                    }
                    else if(canRight && canLeft && canDown)
                    {
                        targetAngle = 45 + Mathf.PingPong(Time.time * RotateSpeed, 270f);
                    }
                    else if (canRight && canLeft && canUp)
                    {
                        targetAngle = 225 + Mathf.PingPong(Time.time * RotateSpeed, 270f);
                    }
                }



            }
        }

        // 四元数平滑旋转
        Quaternion targetRot = Quaternion.Euler(0, targetAngle, 0);
        tankHead.transform.rotation = Quaternion.Lerp(tankHead.transform.rotation, targetRot, Time.deltaTime * RotateSpeed);
    }
    #endregion

    private void OnGUI()
    {
        if (isDead) return;


        if (flashTime > 0)
        {
            flashTime -= Time.deltaTime;

            //先把世界坐标转换为屏幕坐标
            Vector3 screen = Camera.main.WorldToScreenPoint(this.transform.position);
            //屏幕坐标转换为GUI坐标
            screen.y = Screen.height - screen.y;


            maxHpRect.width = 100;
            maxHpRect.height = 15;
            maxHpRect.x = screen.x - 50;
            maxHpRect.y = screen.y - 50;


            hpRect.width = (float)Hp / MaxHp * 100f;
            hpRect.height = 15;
            hpRect.x = screen.x - 50;
            hpRect.y = screen.y - 50;

            if (SettingPanel.Instance.gameObject.activeSelf == false)
            {
                GUI.DrawTexture(maxHpRect, maxHpTex);
                GUI.DrawTexture(hpRect, hpTex);
            }

        }
    }
}
