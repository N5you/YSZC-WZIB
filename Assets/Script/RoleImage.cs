using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//角色脚本
public class RoleImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Role role { private set; get; }//角色信息

    private Image icoImage;

    public void OnStart()
    {
        role = new Role();
        icoImage = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    private Coroutine coroutine;

    public void SetRole(int index)
    {
        if (index == role.id) return;
        XmlDocument docRole = new XmlDocument();
        TextAsset textAssetRole = Resources.Load<TextAsset>("Role");
        docRole.LoadXml(textAssetRole.text);
        XmlElement GetXmlRole = docRole.DocumentElement;//获取根节点
        string plot = "role" + index;
        XmlNodeList personNodes = GetXmlRole.GetElementsByTagName(plot);

        string id = index.ToString();
        string ico = string.Empty;
        string name = string.Empty;
        string description = string.Empty;
        foreach (XmlElement node in personNodes)
        {
            //id = node.GetElementsByTagName("id")[0].InnerText;
            description = node.GetElementsByTagName("description")[0].InnerText;
            ico = node.GetElementsByTagName("ico")[0].InnerText;
            name = node.GetElementsByTagName("neme")[0].InnerText;
        }
        role = new Role(id, ico, name, description);

        if (icoImage.sprite == role.ico) return;
        icoImage.sprite = role.ico;
        icoImage.color = new Color(1, 1, 1, 0);
        coroutine = StartCoroutine(Ico());
    }


    private IEnumerator Ico()
    {
        yield return 10;

        icoImage.color = new Color(1, 1, 1, icoImage.color.a + 0.1f);

        StopCoroutine(coroutine);
        if (1f > icoImage.color.a)
        {
            coroutine = StartCoroutine(Ico());
        }
    }

    public void OnPointerDown(PointerEventData eventData)//按下
    {
        icoImage.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        CanvasUI.Instance.SetMessageLined(role.description, true);
    }

    public void OnPointerUp(PointerEventData eventData)//抬起
    {
        icoImage.color = new Color(1f, 1f, 1f, 1f);
        CanvasUI.Instance.SetMessageLined(string.Empty, false);
    }
}