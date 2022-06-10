using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class ArduinoInput : MonoBehaviour
{
    public enum COMPORT
    {
        COM1 = 1,
        COM2,
        COM3,
        COM4,
        COM5,
    }
    public COMPORT port = COMPORT.COM5;
    SerialPort stream;
    public Vector3 arduinoIn = Vector3.zero;
    public bool connected = false;

    private void Awake()
    {
        stream = new SerialPort("COM" + (int)port, 19200);
    }

    void Start()
    {
        stream.Open();
    }

    private void OnApplicationQuit()
    {
        stream.Close();
    }

    void Update()
    {
        if (stream.IsOpen)
        {
            try
            {
                connected = true;
            string line = stream.ReadLine();
            stream.BaseStream.Flush();
            if (line.StartsWith("ROT:"))
            {
                line = line.Replace("ROT:", "");
                string[] rots = line.Split('/');
                
                    arduinoIn = new Vector3(float.Parse(rots[0]), float.Parse(rots[1]), float.Parse(rots[2]));
            }
#pragma warning disable IDE0059 // Unnecessary assignment of a value
#pragma warning disable CS0168 // Variable is declared but never used
        }
            catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
#pragma warning restore IDE0059 // Unnecessary assignment of a value
            {}
        } else
        {
            connected = false;
        }

    }
}
