using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ステージを管理するクラス
/// </summary>
public class StageContoller : MonoBehaviour
{
    [SerializeField] private Transform rhythmIconParent; // リズムアイコンの親オブジェクト
    [SerializeField] private RhythmIconController icon;  // リズムアイコン
    private StageUIController ui;

    private float endTime = 15f;
    private Dictionary<float, string> generateIcons;
    private List<float> generateTime;
    private int iconCount;

    // Start is called before the first frame update
    void Start()
    {
        ui = StageUIController.Instance;
        iconCount = 0;
        generateIcons = new Dictionary<float, string>()
        {
            {1f, "C"},
            {2f, "L4"},
            {3f, "L3"},
            {4f, "L2"},
            {5f, "L1"},
            {6f, "C"},
            {7f, "R1"},
            {8f, "R2"},
            {9f, "R3"},
            {10f, "R4"},
        };
        generateTime = new List<float>(generateIcons.Keys); // Keyを配列に変換
    }

    // Update is called once per frame
    void Update()
    {
        if (endTime - Time.timeSinceLevelLoad > 0 && generateTime.Count > iconCount && ui.lifeGauge.value != 0)
        {
            if (generateTime[iconCount] < Time.timeSinceLevelLoad)
            {
                RhythmIconController _instance;
                _instance = Instantiate(icon, Vector3.zero, Quaternion.identity, rhythmIconParent);
                _instance.Initialize(generateIcons[generateTime[iconCount]]);
                iconCount++;
            }
        }
        else if (ui.lifeGauge.value <= 0)
        {
            ui.GameFailure();
        }
        else if (endTime < Time.timeSinceLevelLoad)
        {
            ui.DisplayClearText();
            Invoke("LoadTitleScene" , 3.0f);
        }
    }

    void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
