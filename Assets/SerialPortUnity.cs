using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class SerialPortUnity : MonoBehaviour {
    public delegate void IntDelegate(int value);
    public event IntDelegate HorizontalAxis;
    public event IntDelegate VerticalAxis;
    public event IntDelegate HorizontalAxis2;
    public event IntDelegate VerticalAxis2;

    SerialPort stream;
    string streamData = "";
    void Start()
    {
        stream = new SerialPort("COM5", 9600);
        stream.ReadTimeout = 100; //define the timeout
        //stream.Open(); //Open the Serial Stream.
    }

    void Update () {
        if (stream.IsOpen)
        {
            streamData = stream.ReadLine();
            if (streamData != "")
                SplitSerialInformation(streamData);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && stream.IsOpen) //closing the stream when you press escape
        {
            stream.Close();
        }
        else if(Input.GetKeyDown(KeyCode.Space) && !stream.IsOpen) //opening the stream when you press space
        {
            stream.Open();
        }
    }

    // Here we are going to split the information that was send by the ardiuno.
    void SplitSerialInformation(string value)
    {
        string[] XY = value.Split(',');
        int xAxis1 = int.Parse(XY[0]);
        int yAxis1 = int.Parse(XY[1]);
        int xAxis2 = int.Parse(XY[2]);
        int yAxis2 = int.Parse(XY[3]);
        xAxis1 -= 512;
        yAxis1 -= 512;
        xAxis2 -= 512;
        yAxis2 -= 512;
        if (HorizontalAxis != null)
            HorizontalAxis(xAxis1);
        if (VerticalAxis != null)
            VerticalAxis(yAxis1);
        if (HorizontalAxis2 != null)
            HorizontalAxis2(xAxis2);
        if (VerticalAxis2 != null)
            VerticalAxis2(yAxis2);

        stream.BaseStream.Flush();
    }
}
