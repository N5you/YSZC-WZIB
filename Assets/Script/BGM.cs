using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public static BGM Instance { private set; get; } //单例模式

    private AudioSource Bgm;

    private void Start()
    {
        Bgm = GetComponent<AudioSource>();
    }

    public void SetBgm()//场景（章节）索引，剧情索引
    {
    }

    private void Awake()
    {
        Instance = this;
    }
}
