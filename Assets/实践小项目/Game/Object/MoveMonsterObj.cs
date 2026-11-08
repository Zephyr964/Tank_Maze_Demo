using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public enum E_TurnOrMove 
{
    Turn,
    Move,
}


public class MoveMonsterObj : TankBaseObj
{
    #region 实现移动功能
    [Header("地图")]
    public MImi map;

    private Rigidbody rb;//刚体组件

    private Cell targetCell;//目标格子
    private Vector3 targetPos;//目标位置
    private Vector3 startPos;//起点位置

    private Quaternion targetRota;//目标旋转角度
    private Quaternion startRota;//初始旋转角度

    private E_TurnOrMove action;//移动和转向状态
    private Vector3 lastDir;//判断是继续转向还是移动


    private Cell lastCell;//上一格

    private float t;//插值运算的t


    //设置每个敌人的巡逻范围
    public Vector2 areaMax;
    public Vector2 areaMin;
    #endregion

    //检查对象开火
    public Transform lookAtObject;
    public float atkRange;
    public GameObject bullet;
    public Transform[] bulletPos;

    //间隔开火
    public int intervalTime = 1;//间隔时间
    private float nowTime = 0;//世界时间


    //设置怪物血条
    public Texture maxHpTex;
    public Texture hpTex;
    private Rect maxHpRect;
    private Rect hpRect;
    //血条刷新时间
    private float flashTime = 0;

    //消灭坦克加分大小
    public int AddScore;


    void Start()
    {
        #region 移动相关
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        targetCell = map.PosToGrid(transform.position);
        targetPos = map.GridToPos(targetCell);
        startPos = targetPos;

        //初始旋转角度
        targetRota = transform.rotation;
        startRota = transform.rotation;

        //开始先选下一格,然后转向
        // 开局先选格子
        bool needTurn = PickNextCell();
        if (needTurn)
        {
            action = E_TurnOrMove.Turn;
        }
        else
        {
            action = E_TurnOrMove.Move;
            startPos = transform.position;
        }
        t = 0;
        #endregion
    }

    void Update()
    {
        if (isDead) return;
        #region 移动相关

        if (action == E_TurnOrMove.Move)
        {
            t += Time.deltaTime * MoveSpeed;
            Mmoving();
        }
        else if(action == E_TurnOrMove.Turn)
        {
            t += Time.deltaTime * RotateSpeed;
            Mturing();
        }


        #endregion

        #region 炮口锁定玩家，并且进入领地自动攻击
        //锁头

        if (Vector3.Distance(this.transform.position, lookAtObject.transform.position) < atkRange)
        {
            tankHead.transform.LookAt(lookAtObject);
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

    public override void Dead()
    {
        base.Dead();
        GamePanel.Instance.AddScore(AddScore);
    }

    public override void Hurt(TankBaseObj obj)
    {
        base.Hurt(obj);
        flashTime = 3;
    }

    //判断怪物下一个格走哪里
    public bool PickNextCell()
    {
        List<Cell> Mneighbor = map.GetNeighbor(targetCell);
        if (Mneighbor.Count <= 0)
        {
            return true;
        }

        //把不在领地内的邻居进一步过滤
        List<Cell> MAneighbor = new List<Cell>();
        foreach (Cell neighbor in Mneighbor)
        {
            if (IsArea(neighbor))
            {
                MAneighbor.Add(neighbor);
            }
        }

        if (MAneighbor.Count == 0)
        { return true; }

        if (lastCell != null && MAneighbor.Count > 1)
        {
            //如果可走的路有2条（包括回头路），就把回头路删除，不让他走回头路
            MAneighbor.Remove(lastCell);
        }
        //如果走到尽头了，就把回头路加回来吧
        if (MAneighbor.Count == 0 && lastCell != null)
        {
            MAneighbor.Add(lastCell);
        }

        int nextCell = Random.Range(0, MAneighbor.Count);
        lastCell = targetCell;
        targetCell = MAneighbor[nextCell];
        targetPos = map.GridToPos(targetCell);

        //改变旋转角度
        Vector3 nextDir = (targetPos - this.transform.position).normalized;

        nextDir.y = 0;//水平方向旋转
        if (nextDir.magnitude < 0.01f)
        {
            return true;
        }
        targetRota = Quaternion.LookRotation(nextDir);
        startRota = transform.rotation;


        float dot = Vector3.Dot(lastDir, nextDir);
        lastDir = nextDir; // 保存本次方向，给下一轮判断

        // 点积接近1 = 同向，不需要转向
        if (dot > 0.99f)
        {
            return false;
        }
        return true;
    }

    //怪物移动方法
    public void Mmoving()
    {
        transform.position = Vector3.Lerp(startPos, targetPos, Mathf.Clamp01(t));

        if (t >= 1f)
        {
            transform.position = targetPos;
            // 先拿到当前位置，这就是本段终点
            Vector3 arrivePos = transform.position;

            bool need = PickNextCell();
            if (need)
            {
                action = E_TurnOrMove.Turn;
                t = 0f;
            }
            else
            {
                action = E_TurnOrMove.Move;
                t = 0f;
                startPos = arrivePos; // 用刚到达的位置作为下一段起点
            }
        }
    }
    public void Mturing()
    {
        transform.rotation = Quaternion.Slerp(startRota, targetRota, Mathf.Clamp01(t));
        if (t >= 1f)
        {
            transform.rotation = targetRota;
            //转向完成，切成移动
            action = E_TurnOrMove.Move;
            t = 0f;
            startPos = this.transform.position;
        }
    }

    //判断邻居格子是否在自己的巡逻范围里面
    public bool IsArea(Cell cell)
    {
        return cell.x <= areaMax.x && cell.y <= areaMax.y && cell.x >= areaMin.x && cell.y >= areaMin.y;
    }

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

            if(SettingPanel.Instance.gameObject.activeSelf == false)
            {
                GUI.DrawTexture(maxHpRect, maxHpTex);
                GUI.DrawTexture(hpRect, hpTex);
            }

        }
    }
}
