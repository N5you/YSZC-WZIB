using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseBgImage : MonoBehaviour
{
    private Text NmeText;
    private Transform tr;

    private void Start()
    {
        NmeText = transform.Find("NmeText").GetComponent<Text>();
        tr = transform.Find("GameObject");
        gameObject.SetActive(false);
    }

    public void SetChoose(string[] index, string Name)//设置选择框 //点击按钮后跳到对应index的剧情 //标题名字和按钮
    {
        string[] bt = Name.Split('-');
        NmeText.text = bt[0];

        GameObject p = Resources.Load<GameObject>("UI/Button-1");

        string[] xz = bt[1].Split(',');
        for (int i = 0; i < xz.Length; i++)
        {
            GameObject go = Instantiate(p);
            go.transform.SetParent(tr);
            go.transform.Find("Text").GetComponent<Text>().text = xz[i];
            int n = int.Parse(index[i]);
            //不能等于 1024 等于就进城
            go.GetComponent<Button>().onClick.AddListener(delegate () { EndConversation(n); });
        }

        gameObject.SetActive(true);
    }

    public void EndConversation(int index)
    {
        Button[] btn = tr.GetComponentsInChildren<Button>();
        foreach (Button bt in btn)
        {
            Destroy(bt.gameObject);
        }

        gameObject.SetActive(false);
        CanvasUI.Instance.NextStepConversation(index);
    }
}
