using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ステージのUIを管理するクラス
/// SingletonClass
/// </summary>
public sealed class StageUIController : MonoBehaviour
{
    private static StageUIController _instance;

    // 各種テキストオブジェクト
    private GameObject judgmentObject;
    private Text judgmentText;
    private GameObject comboObject;
    private Text comboText;
    private GameObject clearObject;
    private Text clearText;

    // ボタン
    private GameObject returnTitleButtonObject;
    private Button returnTitleButton;

    // スコアゲージ
    public GameObject scoreGaugeObject;
    public Slider scoreGauge;
    public GameObject lifeGaugeObject;
    public Slider lifeGauge;

    // 判定種別
    public const string JUDGE_PERFECT = "Perfect";
    public const string JUDGE_GOOD = "Good";
    public const string JUDGE_NICE = "Nice";
    public const string JUDGE_BAD = "Bad";
    public const string JUDGE_MISS = "Miss";

    // 色の定義用変数
    private Color P_COLOR;
    private Color G_COLOR;
    private Color N_COLOR;
    private Color B_COLOR;
    private Color M_COLOR;

    // テキストのスタイル設定
    private const string comboTextStyleS = "<color=#ffdead><size=60>";
    private const string comboTextStyleE = "</size> Combo</color>";

    private int comboNum;

    private void Awake()
    {
        comboNum = 0;
        // 判定テキストの設定
        judgmentObject = GameObject.Find("JudgmentText");
        if (judgmentObject)
        {
            judgmentText = judgmentObject.GetComponent<Text>();
            judgmentText.text = "";
        }
        else
        {
            Debug.LogError("JudgmentTextオブジェクトが存在しません");
        }
        // コンボテキストの設定
        comboObject = GameObject.Find("ComboText");
        if (comboObject)
        {
            comboText = comboObject.GetComponent<Text>();
            comboText.text = "";
        }
        else
        {
            Debug.LogError("ComboTextオブジェクトが存在しません");
        }
        // クリアテキストの設定
        clearObject = GameObject.Find("ClearText");
        if (clearObject)
        {
            clearObject.SetActive(false);
        }
        else
        {
            Debug.LogError("ClearTextオブジェクトが存在しません");
        }
        // スコアゲージの設定
        scoreGaugeObject = GameObject.Find("ScoreGauge");
        if (scoreGaugeObject)
        {
            scoreGauge = scoreGaugeObject.GetComponent<Slider>();
            scoreGauge.value = 0;
        }
        // ライフゲージの設定
        lifeGaugeObject = GameObject.Find("LifeGauge");
        if (lifeGaugeObject)
        {
            lifeGauge = lifeGaugeObject.GetComponent<Slider>();
            lifeGauge.value = 1;
        }
        // タイトルボタンへの設定
        returnTitleButtonObject = GameObject.Find("ReturnTitleButton");
        if (returnTitleButtonObject)
        {
            returnTitleButton = returnTitleButtonObject.GetComponent<Button>();
            returnTitleButtonObject.SetActive(false);
        }

        // 色の生成
        ColorUtility.TryParseHtmlString("#ff00ff", out P_COLOR);
        ColorUtility.TryParseHtmlString("#ff8c00", out G_COLOR);
        ColorUtility.TryParseHtmlString("#ffff00", out N_COLOR);
        ColorUtility.TryParseHtmlString("#00008b", out B_COLOR);
        ColorUtility.TryParseHtmlString("#ff0000", out M_COLOR);
    }

    public static StageUIController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<StageUIController>();
            }
            return _instance;
        }
    }

    public void SetJudgmentText(string judgement)
    {
        switch (judgement)
        {
            case JUDGE_PERFECT:
                judgmentText.text = JUDGE_PERFECT;
                judgmentText.color = P_COLOR;
                CounterComboNum(true);
                comboText.text = comboTextStyleS + comboNum.ToString() + comboTextStyleE;
                scoreGauge.value += 0.1f;
                break;
            case JUDGE_GOOD:
                judgmentText.text = JUDGE_GOOD;
                judgmentText.color = G_COLOR;
                CounterComboNum(true);
                comboText.text = comboTextStyleS + comboNum.ToString() + comboTextStyleE;
                scoreGauge.value += 0.05f;
                break;
            case JUDGE_NICE:
                judgmentText.text = JUDGE_NICE;
                judgmentText.color = N_COLOR;
                CounterComboNum(false);
                comboText.text = "";
                scoreGauge.value += 0.01f;
                break;
            case JUDGE_BAD:
                judgmentText.text = JUDGE_BAD;
                judgmentText.color = B_COLOR;
                CounterComboNum(false);
                comboText.text = "";
                // ライフゲージが素直に0にならないため、小数点第3位以下四捨五入
                lifeGauge.value = (float)(Math.Round(lifeGauge.value - 0.1f, 2, MidpointRounding.AwayFromZero));
                break;
            case JUDGE_MISS:
                judgmentText.text = JUDGE_MISS;
                judgmentText.color = M_COLOR;
                CounterComboNum(false);
                comboText.text = "";
                // ライフゲージが素直に0にならないため、小数点第3位以下四捨五入
                lifeGauge.value = (float)(Math.Round(lifeGauge.value - 0.2f, 2, MidpointRounding.AwayFromZero));
                break;
            default:
                break;
        }
    }

    private void CounterComboNum(bool plusFlag)
    {
        if (plusFlag)
        {
            comboNum++;
        }
        else
        {
            comboNum = 0;
        }
    }

    public void GameFailure()
    {
        var icons = GameObject.FindGameObjectsWithTag("RhythmIcon");
        foreach (var icon in icons)
        {
            icon.GetComponent<RhythmIconController>().enabled = false;
        }

        //Time.timeScale = 0;
        judgmentText.enabled = false;
        clearObject.SetActive(true);
        clearText = clearObject.GetComponent<Text>();
        clearText.text = "Game Failure";
        clearText.color = Color.red;
        returnTitleButtonObject.SetActive(true);
    }

    public void DisplayClearText()
    {
        judgmentText.enabled = false;
        comboText.enabled = false;
        clearObject.SetActive(true);
    }

    public void ReturnTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
