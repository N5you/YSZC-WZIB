using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

//框架
public class CanvasUI : MonoBehaviour
{
    public static CanvasUI Instance { private set; get; } //单例模式

    private Image BgPanel;//背景图片
    private RoleImage[] roleImage;

    private Button KillButton;
    private Button SaveButton;

    private ChooseBgImage chooseBgImage;//选择面板
    private Image InitialPanelBgImage;//初始面板
    private MessageBgImage messageBgImage;//消息显示面板
    private Text MessageLined;//介绍显示

    private Coroutine coroutine;

    private void Start()
    {
        BgPanel = transform.Find("BgPanel").GetComponent<Image>();

        KillButton = transform.Find("KillButton").GetComponent<Button>();
        SaveButton = transform.Find("SaveButton").GetComponent<Button>();

        roleImage = GetComponentsInChildren<RoleImage>();
        foreach (RoleImage roleL in roleImage)
        {
            roleL.OnStart();
        }

        InitialPanelBgImage = transform.Find("InitialPanelBgImage").GetComponent<Image>();
        chooseBgImage = transform.Find("ChooseBgImage").GetComponent<ChooseBgImage>();
        messageBgImage = transform.Find("MessageBgImage").GetComponent<MessageBgImage>();
        MessageLined = transform.Find("MessageLined").GetComponent<Text>();
        MessageLined.gameObject.SetActive(false);
    }

    private void AccordingSwitch(bool ba)//显示开关
    {
        KillButton.gameObject.SetActive(ba);
        SaveButton.gameObject.SetActive(ba);
    }

    public void SetMessageLined(string str, bool bz)
    {
        MessageLined.text = str;
        MessageLined.gameObject.SetActive(bz);
    }

    private Plot plot;
    private int indexPlot = 0;//当前剧情进度

    public void OnSaveButton()//保存按钮
    {
        string save = indexPlot + "," + index;
        PlayerPrefs.SetString("N5youCom", save);
    }

    public void OnKillButton()//击杀按钮
    {
    }

    private int index = 1;//当前进一步的进度 (索引)
    public void OnNextStep()//下一步
    {
        if (chooseBgImage.gameObject.activeSelf) return;

        foreach (RoleImage rle in roleImage)
        {
            rle.gameObject.SetActive(false);
        }

        string[] strRole = plot.role[index - 1].Split(':');//角色id
        string[] sLocation = plot.location[index - 1].Split(':');//显示位置
        for (int i = 0; i < strRole.Length; i++)
        {
            int n = int.Parse(sLocation[i]);
            roleImage[n].gameObject.SetActive(true);
            int v = int.Parse(strRole[i]);
            roleImage[n].SetRole(v);
        }

        string[] strPlot = plot.plot[index - 1].Split('*');
        messageBgImage.gameObject.SetActive(true);
        messageBgImage.SetMessage(strPlot[1], strPlot[2], int.Parse(strPlot[0]));

        Sprite sprite = Resources.Load<Sprite>("BG/BG" + plot.bg[index - 1]);
        if (BgPanel.sprite != sprite)
        {
            BgPanel.color = new Color(1, 1, 1, 0);
            BgPanel.sprite = sprite;//设置背景图
            coroutine = StartCoroutine(BG());
        }

        index++;
        if (index > plot.plot.Length) chooseBgImage.SetChoose(plot.chooseIndex, plot.chooseName);//显示选择框
    }

    public void NextStepConversation(int indx)//下一步对话
    {
        index = 1;//归0
        plot = GetPlot(indx);//根据索引获取
        OnNextStep();
    }

    private XmlDocument doc = new XmlDocument();

    private Plot GetPlot(int indexL)//根据进度获取剧情
    {
        XmlElement rootElem = doc.DocumentElement;//获取根节点
        string plot = "MsgGather" + indexL;
        XmlNodeList personNodes = rootElem.GetElementsByTagName(plot); //获取person子节点集合

        int id = 0;
        string[] content = new string[id];
        string bg = string.Empty;
        string role = string.Empty;
        string chooseIndex = string.Empty;
        string chooseName = string.Empty;
        string locationL = string.Empty;

        foreach (XmlElement node in personNodes)
        {
            id = int.Parse(node.GetElementsByTagName("id")[0].InnerText);
            content = new string[id];
            for (int i = 0; i < id; i++)
            {
                string ix = "Plot" + (i + 1);
                string name = node.GetElementsByTagName(ix)[0].InnerText;
                content[i] = name;
            }

            bg = node.GetElementsByTagName("bg")[0].InnerText;
            role = node.GetElementsByTagName("role")[0].InnerText;
            chooseIndex = node.GetElementsByTagName("index")[0].InnerText;
            chooseName = node.GetElementsByTagName("BT")[0].InnerText;
            locationL = node.GetElementsByTagName("location")[0].InnerText;
        }

        Plot plotContent = new Plot(bg, role, content, chooseIndex, chooseName, locationL);
        return plotContent;
    }

    public void NewStart()//新的开始
    {
        plot = GetPlot(indexPlot);
        InitialPanelBgImage.gameObject.SetActive(false);
        OnNextStep();
        AccordingSwitch(true);
    }

    public void Read()//读取
    {
        string[] key = PlayerPrefs.GetString("N5youCom").Split(',');
        indexPlot = int.Parse(key[0]);
        index = int.Parse(key[1]);
        NewStart();
    }

    public void Exit()//退出游戏
    {
        Application.Quit();
    }

    private void Awake()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("plot");
        doc.LoadXml(textAsset.text);

        Instance = this;
    }

    private IEnumerator BG()
    {
        yield return new WaitForSeconds(0.05f);

        BgPanel.color = new Color(1, 1, 1, BgPanel.color.a + 0.1f);

        StopCoroutine(coroutine);
        if (1f > BgPanel.color.a)
        {
            coroutine = StartCoroutine(BG());
        }
    }
}