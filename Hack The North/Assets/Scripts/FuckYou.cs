using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
public class FuckYou : MonoBehaviour
{
    Thread mThread;
    public string connectionIP = "127.0.0.1";
    public int connectionPort = 25001;
    IPAddress localAdd;
    TcpListener listener;
    TcpClient client;
    Vector3 receivedPos = Vector3.zero;
    public string curText;

    public Text translatedDisplay;
    bool running;

    bool send;
    string inputText;

    private void Update()
    {
        // transform.position = receivedPos; //assigning receivedPos in SendAndReceiveData()
        translatedDisplay.text = curText;
        // print(text);
    }

    private void Start()
    {
        ThreadStart ts = new ThreadStart(GetInfo);
        mThread = new Thread(ts);
        mThread.Start();

        run_cmd();
    }

    public void TextFinished(string text)
    {
        send = true;
        inputText = text;
    }
    void GetInfo()
    {
        localAdd = IPAddress.Parse(connectionIP);
        listener = new TcpListener(IPAddress.Any, connectionPort);
        listener.Start();

        client = listener.AcceptTcpClient();

        running = true;
        while (running)
        {
            // SendAndReceiveData();
            if (send)
            {
                SendAndReceiveData(inputText);
                send = false;
            }
            SendAndReceiveData("Bonjour, J'aimerais un croissant, s'il vous plait.");
        }
        listener.Stop();
    }

    void SendAndReceiveData(string text)
    {
        NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];

        //---Sending Data to Host----
        byte[] myWriteBuffer = Encoding.ASCII.GetBytes(text); //Converting string to byte data
        nwStream.Write(myWriteBuffer, 0, myWriteBuffer.Length); //Sending the data in Bytes to Python

        //---receiving Data from the Host----
        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize); //Getting data in Bytes from Python
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead); //Converting byte data to string

        if (dataReceived != null)
        {

            //---Using received data---
            curText = dataReceived; //<-- assigning receivedPos value from Python
            print(curText);


        }
    }

    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }
    /*
    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
    */

    private void run_cmd()
    {

        string fileName = @"C:/Personal/Coding/Hack-The-North/Hack The North/Assets/Scripts/Python/translator.py";

        Process p = new Process();
        p.StartInfo = new ProcessStartInfo(@"C:\Python27\python.exe", fileName)
        {
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        p.Start();

        string output = p.StandardOutput.ReadToEnd();
        p.WaitForExit();

        Console.WriteLine(output);

        Console.ReadLine();

    }
}