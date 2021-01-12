using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用来存放读取的剧情数据
public class Plot
{
    public string[] bg { private set; get; }//背景图ID
    public string[] role { private set; get; } //角色ID
    public string[] plot { private set; get; } //剧情内容
    public string[] chooseIndex { private set; get; } //点击按钮后跳到对应index的剧情
    public string chooseName { private set; get; } //标题名字和按钮
    public string[] location { private set; get; } //显示位置
    public string[] bgm { private set; get; } //背景音乐

    public Plot(string bgL, string roleL, string[] plotL, string index, string name, string locationL)
    {
        bg = bgL.Split(',');
        role = roleL.Split(',');
        plot = plotL;
        chooseIndex = index.Split(',');
        chooseName = name;
        location = locationL.Split(',');
    }
}

public class Role//角色信息存储
{
    public int id { private set; get; }//id
    public Sprite ico { private set; get; }//图像
    public string name { private set; get; }//名字
    public string description { private set; get; }//描述

    public Role(){}

    public Role(string idL, string icoL, string nameL, string descriptionL)
    {
        id = int.Parse(idL);
        ico = Resources.Load<Sprite>("RoleImage/role" + icoL);
        name = nameL;
        description = descriptionL;
    }
}