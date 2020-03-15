using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトル画面を管理するクラス
/// </summary>
public class TitleController : MonoBehaviour
{

    IEnumerator Start()
    {
        enabled = false;
        yield return new WaitForSeconds(2);
        enabled = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            SceneManager.LoadScene("StageScene");
        }
    }

}
