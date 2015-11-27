using UnityEngine;
using System.Collections;
using System.IO.Ports; //Add the system.IO.Ports so we can use the SerialPort Class.

public class SerialPortUnity : MonoBehaviour {
    //This is the most important Class inside the project.
    //This Class handles the arduino serial prints and converts them to readable values inside unity.
    
    //First we are going to add some event handlers wich we are going to use later.
    public delegate void IntDelegate(int value);
    public event IntDelegate HorizontalAxis;
    public event IntDelegate VerticalAxis;
    public event IntDelegate HorizontalAxis2;
    public event IntDelegate VerticalAxis2;

    //Now we have to set up a stream that listens to the port assigned in the arduino code.
    SerialPort stream;
    string streamData = "";
    void Start()
    {
        //Here we are going to set the serialport it needs to listen to.
        //9600 is the value that you have also read in the arduino code.
        stream = new SerialPort("COM5", 9600); //COM5 is a port my pc uses, this is different on every pc.
        stream.ReadTimeout = 100; //define the timeout
        //stream.Open(); //Open the Serial Stream.
    }

    void Update () {
        //Here we are checking if the stream is open.
        if (stream.IsOpen)
        {
            streamData = stream.ReadLine(); //Here we read the strings that the arduino is printing.
            if (streamData != "")
                SplitSerialInformation(streamData); //We are going to split this data if a message is send.
        }
        if (Input.GetKeyDown(KeyCode.Escape) && stream.IsOpen) //Closing the stream when you press escape
        {
            stream.Close();
        }
        else if(Input.GetKeyDown(KeyCode.Space) && !stream.IsOpen) //Opening the stream when you press space
        {
            stream.Open();
        }
    }

    // Here we are going to split the information that was send by the ardiuno.
    void SplitSerialInformation(string value)
    {
        //First off we are going to split by the char ","
        string[] XY = value.Split(','); //This will give back an array of strings
        int xAxis1 = int.Parse(XY[0]); //The first string as said in the arduino code has the Horizontal Axis of the first 4-way jostick.
        int yAxis1 = int.Parse(XY[1]); //The second string as said in the arduino code has the Vertical Axis of the first 4-way jostick.
        int xAxis2 = int.Parse(XY[2]); //The third string as said in the arduino code has the Horizontal Axis of the second 4-way jostick.
        int yAxis2 = int.Parse(XY[3]); //The fourth string as said in the arduino code has the Vertical Axis of the second 4-way jostick.
        xAxis1 -= 512; //The 4-way joystick has a maximum value of 1024
        yAxis1 -= 512; //The standard value is 512
        xAxis2 -= 512; //We want this to be 0 so we are subtracting 512
        yAxis2 -= 512;
        
        if (HorizontalAxis != null) //Here we are checking if the event handler has a function attached to it.
            HorizontalAxis(xAxis1); //If it has a function attached to it, then start the function and give the first horizontal axis
        if (VerticalAxis != null) //We are repeating this action with all the axis
            VerticalAxis(yAxis1);
        if (HorizontalAxis2 != null)
            HorizontalAxis2(xAxis2);
        if (VerticalAxis2 != null)
            VerticalAxis2(yAxis2);

        //Flush the stream so it can read the new values without the old values still being serialised.
        stream.BaseStream.Flush();
    }
}
