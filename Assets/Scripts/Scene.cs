using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {

    public string nextSceneName;
    public string NomalSceneName;
    public GUIStyle wButton;    //スタイル変数
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Application.LoadLevel(nextSceneName);//次のシーンへ移行
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Application.LoadLevel(NomalSceneName);//ノーマルシーンへの移行
        }
    }
    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 90), "Start Menu");
        if (GUI.Button(new Rect(20, 40, 80, 20), "Easy"))
        {
            Application.LoadLevel(nextSceneName);//次のシーンへ移行
        }
        if (GUI.Button(new Rect(20, 70, 80, 20), "Nomal"))
        {
            Application.LoadLevel(NomalSceneName);//ノーマルシーンへの移行
        }
    }
}
