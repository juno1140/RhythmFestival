using UnityEngine;

/// <summary>
/// Playerの操作を管理するクラス
/// </summary>
public class UserController : MonoBehaviour
{
    private StageUIController ui;

    // 評価判定位置
    public float[,] iconPositionJudg = new float[4, 2]
    {
        {6.7f, 7.1f}, // Excellent
        {6.4f, 7.4f}, // Good
        {6.0f, 7.8f}, // Nice
        {5.2f, 8.6f}  // Bad
    };

    private void Start()
    {
        ui = StageUIController.Instance;
    }

    public void OnClick(string position)
    {
        Judgment(position);
    }

    void Judgment(string position)
    {
        GameObject rhythmIcon = GameObject.Find(position);

        // 対象のリズムアイコンがなければreturn
        if (rhythmIcon == null) { return; }

        RhythmIconController icon = rhythmIcon.GetComponent<RhythmIconController>();

        for (int i = 0; i <= 3; i++)
        {
            if (iconPositionJudg[i, 0] <= icon.PositionVector && icon.PositionVector <= iconPositionJudg[i, 1])
            {
                switch (i)
                {
                    case 0:
                        ui.SetJudgmentText(StageUIController.JUDGE_PERFECT);
                        break;
                    case 1:
                        ui.SetJudgmentText(StageUIController.JUDGE_GOOD);
                        break;
                    case 2:
                        ui.SetJudgmentText(StageUIController.JUDGE_NICE);
                        break;
                    case 3:
                        ui.SetJudgmentText(StageUIController.JUDGE_BAD);
                        break;
                    default:
                        break;
                }
                icon.IconDestroy();
                break;
            }
        }
    }
}
