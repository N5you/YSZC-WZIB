using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBgImage : MonoBehaviour
{
    private Text[] NameText;
    private Text dText;
    private Button button;

    private void Start()
    {
        NameText = transform.Find("GameObject").GetComponentsInChildren<Text>();
        dText = transform.Find("105Text").GetComponent<Text>();
        button = transform.Find("Button").GetComponent<Button>();
        gameObject.SetActive(false);
    }

    private char[] strCharAr;
    private int index;
    private Coroutine coroutine;

    public void SetMessage(string name, string describe, int ind)
    {
        foreach (Text text in NameText)
        {
            text.text = string.Empty;
        }

        NameText[ind].text = name;
        strCharAr = describe.ToCharArray();

        index = 0;
        dText.text = string.Empty;
        button.interactable = false;

        coroutine = StartCoroutine(Message());
    }

    private IEnumerator Message()
    {
        yield return new WaitForSeconds(0.10f);

        dText.text = dText.text + strCharAr[index];
        index++;

        StopCoroutine(coroutine);
        if (index < strCharAr.Length)
        {
            coroutine = StartCoroutine(Message());
        }
        else
        {
            button.interactable = true;
        }
    }
}
