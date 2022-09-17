using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KKSpeech;
public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetLanguages();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetLanguages()
    {
        SpeechRecognizer.GetSupportedLanguages();
    }
}
