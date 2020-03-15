using System;
using UnityEngine;

/// <summary>
/// リズムアイコンを管理するクラス
/// </summary>
public class RhythmIconController : MonoBehaviour
{
    private const float GENERATION_POINT_X = 0;
    private const float GENERATION_POINT_Y = 3.3f;
    public int ButtonCode { get; private set; } // 対応するボタン番号
    public double PositionVector { get; private set; } // 発生源からの現在の移動距離
    private StageUIController ui;

    // リズムアイコンの移動方向
    float[,] iconMoveDir = new float[9, 2]
    {
        {-0.05f, 0},          // L4
        {-0.046f, -0.019f},   // L3
        {-0.0354f, -0.0354f}, // L2
        {-0.019f, -0.046f},   // L1
        {0, -0.05f},          // Center
        {0.019f, -0.046f},    // R1
        {0.0354f, -0.0354f},  // R2
        {0.046f, -0.019f},    // R3
        {0.05f, 0}            // R4
    };

    private void Awake()
    {
        transform.position = new Vector2(GENERATION_POINT_X, GENERATION_POINT_Y); // センターに配置
        ui = StageUIController.Instance;
    }

    // 必ず定義する
    public void Initialize(string bp)
    {
        switch (bp)
        {
            case "L4":
                ButtonCode = 0;
                name = "L4";
                break;
            case "L3":
                ButtonCode = 1;
                name = "L3";
                break;
            case "L2":
                ButtonCode = 2;
                name = "L2";
                break;
            case "L1":
                ButtonCode = 3;
                name = "L1";
                break;
            case "C":
                ButtonCode = 4;
                name = "C";
                break;
            case "R1":
                ButtonCode = 5;
                name = "R1";
                break;
            case "R2":
                ButtonCode = 6;
                name = "R2";
                break;
            case "R3":
                ButtonCode = 7;
                name = "R3";
                break;
            case "R4":
                ButtonCode = 8;
                name = "R4";
                break;
            default:
                break;
        }
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(iconMoveDir[ButtonCode, 0], iconMoveDir[ButtonCode, 1], 0);
        PositionVector =
            Math.Sqrt(Math.Pow(GENERATION_POINT_X - transform.position.x, 2) + Math.Pow(GENERATION_POINT_Y - transform.position.y, 2));
        if (PositionVector > 8.6f)
        {
            name = "miss";
            MissIcon();
        }
    }

    private void MissIcon()
    {
        ui.SetJudgmentText(StageUIController.JUDGE_MISS);
        IconDestroy();
    }

    // 画面外に出た時デリート
    //public void OnBecameInvisible()
    //{
        
    //    IconDestroy();
    //}

    public void IconDestroy()
    {
        Destroy(this.gameObject);
    }
}
