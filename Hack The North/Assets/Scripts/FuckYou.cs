using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.Networking;

using System.Threading.Tasks;
using TMPro;
public class FuckYou : MonoBehaviour
{
    public RecordingCanvas rc;
    public string baseURL = "https://hack-the-north-deeznuts.herokuapp.com/";
    public string curText;

    public Text translatedDisplay;
    public TMP_Text transcriptText;
    bool running;

    bool send;
    public string response;

    private void Update()
    {
        // transform.position = receivedPos; //assigning receivedPos in SendAndReceiveData()
        // translatedDisplay.text = curText;
        // print(text);
    }

    private void Start()
    {
        string url = baseURL + ConvertURL("translate?q=salut j'le fromage. aimes tu le fromage? non? va te faire foutre");
        print(baseURL);
        // A correct website page.
        StartCoroutine(GetRequest(url, false));
        // ThreadStart ts = new ThreadStart(Main);
        // mThread = new Thread(ts);
        // mThread.Start();
        // RunAsync().Wait();
    }

    string ConvertURL(string s)
    {
        string newS = "";
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == ' ')
            {
                newS += "%20";
            }
            else if (s[i].ToString() == "'")
            {
                newS += "%27";
            }
            else
            {
                newS += s[i];
            }
        }
        return newS;
    }

    // void Main()
    // {
    //     RunAsync().Wait();
    // }

    IEnumerator GetRequest(string uri, bool type)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            print(webRequest.downloadHandler.text);
            response = webRequest.downloadHandler.text;
            translatedDisplay.text += response;

            if (!type)
            {

                transcriptText.text = GameObject.Find("Notes Manager").GetComponent<NotesManager>().transcript;
            }
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    public void translate(string text)
    {

        string url = baseURL + "translate?q=" + ConvertURL(text);
        StartCoroutine(GetRequest(url, false));
    }

    public void AudioEnd(string text)
    {
        if (rc.language != "en-US")
        {
            translate(text);
            translatedDisplay.text += text;
        }
        else
        {
            transcriptText.text = GameObject.Find("Notes Manager").GetComponent<NotesManager>().transcript;
        }

    }
}


