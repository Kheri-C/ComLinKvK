﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using System.Net;
using System.Net.Sockets;

namespace IMDAdataReceptionV2 {
    public partial class Form1 : Form {

        // General variables
        String serverAddress;

        // UDP sense variables
        List<int> indexValues = new List<int>();
        List<double> XZdataValues = new List<double>();
        List<double> YZdataValues = new List<double>();
        int n = 1, senseUDPport;
        Thread senseUDPserverThread;
        IPEndPoint senseUDPserverEP;
        UdpClient senseUDPserver;
        bool senseServerStatus, senseServerStarted = false, senseServerTurnedOff;

        // UDP animod variables
        int animodUDPport;
        String[] chatHistoryArray = new String[10];
        String chatHistory;
        Thread animodUDPserverThread;
        IPEndPoint animodUDPserverEP;
        UdpClient animodUDPserver;
        bool animodServerStatus, animodServerStarted = false, animodServerTurnedOff;

        public Form1() {
            InitializeComponent();
            // Change chart appearance
            XZchart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
            XZchart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
            XZchart.Series[0].BorderWidth = 5;
            YZchart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
            YZchart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
            YZchart.Series[0].BorderWidth = 5;
            serverAddress = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString(); // Get server's IP Address
            serverAddressBox.Text = serverAddress; // Display the server's IP Address
        }

        /*private void OnDataReceived(object sender, SerialDataReceivedEventArgs e) {
            this.Invoke((MethodInvoker)delegate {
                textBox2.Text = serialPort1.ReadLine();
                if (textBox2.Text.Length == 18 && !textBox2.Text.Contains("NAN")) { // If a whole valid frame was received
                    if (n < 11) { // First 10 received messages
                        xValues.Add(n);

                        yValues.Add(Convert.ToDouble(textBox2.Text.Substring(2, 5)));
                        yValues2.Add(Convert.ToDouble(textBox2.Text.Substring(textBox2.Text.IndexOf('y') + 2, 5)));
                        n++;
                        chart1.Series["Series1"].Points.DataBindXY(xValues, yValues);
                        chart2.Series["Series1"].Points.DataBindXY(xValues, yValues2);
                        chart1.Invalidate();
                        chart2.Invalidate();
                    }
                    else {
                        for (int i = 1; i < 10; i++) {
                            yValues[i - 1] = yValues[i];
                        }
                        yValues[9] = Convert.ToDouble(textBox2.Text.Substring(2, 5));
                        for (int i = 1; i < 10; i++) {
                            yValues2[i - 1] = yValues2[i];
                        }
                        yValues2[9] = Convert.ToDouble(textBox2.Text.Substring(textBox2.Text.IndexOf('y') + 2, 5));
                        chart1.Series["Series1"].Points.DataBindXY(xValues, yValues);
                        chart2.Series["Series1"].Points.DataBindXY(xValues, yValues2);
                        chart1.Invalidate();
                        chart2.Invalidate();
                    }
                }
            });
        }*/

        void listenSense() {
            while (true) {
                byte[] inputData = senseUDPserver.Receive(ref senseUDPserverEP); // Receive the informations
                if (senseServerTurnedOff) { // If the server was turned off and is now turned on
                    inputData = Encoding.ASCII.GetBytes(""); // Delete what was stored in the buffer when the server was off
                    senseServerTurnedOff = false;
                }
                else {
                    this.Invoke((MethodInvoker)delegate {
                        senseUDPmessageBox.Text = Encoding.ASCII.GetString(inputData);
                        debugTextBox.Text = senseUDPmessageBox.Text.Length.ToString(); // Debug
                        if (senseUDPmessageBox.Text.Length == 19 && !senseUDPmessageBox.Text.Contains("NAN")) { // If a whole valid frame was received
                            if (n < 11) { // First 10 received messages
                                indexValues.Add(n);
                                XZdataValues.Add(Convert.ToDouble(senseUDPmessageBox.Text.Substring(2, 7)));
                                YZdataValues.Add(Convert.ToDouble(senseUDPmessageBox.Text.Substring(senseUDPmessageBox.Text.IndexOf('y') + 2, 7)));
                                n++;
                                XZchart.Series["Series1"].Points.DataBindXY(indexValues, XZdataValues);
                                YZchart.Series["Series1"].Points.DataBindXY(indexValues, YZdataValues);
                                XZchart.Invalidate();
                                YZchart.Invalidate();
                            }
                            else {
                                for (int i = 1; i < 10; i++) {
                                    XZdataValues[i - 1] = XZdataValues[i];
                                }
                                XZdataValues[9] = Convert.ToDouble(senseUDPmessageBox.Text.Substring(2, 7));
                                for (int i = 1; i < 10; i++) {
                                    YZdataValues[i - 1] = YZdataValues[i];
                                }
                                YZdataValues[9] = Convert.ToDouble(senseUDPmessageBox.Text.Substring(senseUDPmessageBox.Text.IndexOf('y') + 2, 7));
                                XZchart.Series["Series1"].Points.DataBindXY(indexValues, XZdataValues);
                                YZchart.Series["Series1"].Points.DataBindXY(indexValues, YZdataValues);
                                XZchart.Invalidate();
                                YZchart.Invalidate();
                            }
                        }
                    });
                }
            }
        }

        void listenAnimod() {
            while (true) {
                byte[] inputData = animodUDPserver.Receive(ref animodUDPserverEP); // Receive the information
                if (animodServerTurnedOff) { // If the server was turned off and is now turned on
                    inputData = Encoding.ASCII.GetBytes(""); // Delete what was stored in the buffer when the server was off
                    animodServerTurnedOff = false;
                }
                else {
                    for (int i = 9; i >= 1; i--) {
                        chatHistoryArray[i] = chatHistoryArray[i - 1]; // Move the history up one position 
                    }
                    chatHistoryArray[0] = animodUDPserverEP.Address.ToString() + " at " + DateTime.Now.ToString() + ": " + Encoding.ASCII.GetString(inputData); // Insert the received information in the history
                    switch (Encoding.ASCII.GetString(inputData)) {
                        case "x":
                            byte[] outputXZData = Encoding.ASCII.GetBytes(XZdataValues[9].ToString());
                            animodUDPserver.Send(outputXZData, Encoding.ASCII.GetString(outputXZData).Length, animodUDPserverEP);
                            for (int i = 7; i >= 1; i--) {
                                chatHistoryArray[i] = chatHistoryArray[i - 1]; // Move the history up one position
                            }
                            chatHistoryArray[0] = serverAddress + " at " + DateTime.Now.ToString() + ": " + Encoding.ASCII.GetString(outputXZData); // Insert the sent data in the history
                            break;
                        case "y":
                            byte[] outputYZData = Encoding.ASCII.GetBytes(YZdataValues[9].ToString());
                            animodUDPserver.Send(outputYZData, Encoding.ASCII.GetString(outputYZData).Length, animodUDPserverEP);
                            for (int i = 7; i >= 1; i--) {
                                chatHistoryArray[i] = chatHistoryArray[i - 1]; // Move the history up one position
                            }
                            chatHistoryArray[0] = serverAddress + " at " + DateTime.Now.ToString() + ": " + Encoding.ASCII.GetString(outputYZData); // Insert the sent data in the history
                            break;
                        default:
                            break;
                    }
                    /*if (Encoding.ASCII.GetString(inputData) == "r") { // If a request is received
                        // Send data
                        byte[] outputData = Encoding.ASCII.GetBytes(XZdataValues[9].ToString());
                        animodUDPserver.Send(outputData, Encoding.ASCII.GetString(outputData).Length, animodUDPserverEP);
                        for (int i = 7; i >= 1; i--) {
                            chatHistoryArray[i] = chatHistoryArray[i - 1]; // Move the history up one position
                        }
                        chatHistoryArray[0] = serverAddress + " at " + DateTime.Now.ToString() + ": " + Encoding.ASCII.GetString(outputData); // Insert the sent data in the history
                    }*/
                    chatHistory = "";
                    for (int i = 9; i >= 0; i--) {
                        chatHistory += chatHistoryArray[i] + "\r\n\r\n"; // Append the history in a single String
                    }
                    this.Invoke((MethodInvoker)delegate {
                        animodUDPchat.Text = chatHistory; // Update the history
                    });
                }
            }
        }

        private void senseUDPpowerButton_Click(object sender, EventArgs e) {
            if (!senseServerStarted) { // If the initial configuration hasn't been done
                if (int.TryParse(senseUDPportBox.Text, out senseUDPport)) {
                    senseUDPserverEP = new IPEndPoint(IPAddress.Any, senseUDPport);
                    senseUDPserver = new UdpClient(senseUDPserverEP);
                    // Start the thread
                    senseUDPserverThread = new Thread(() => listenSense());
                    senseUDPserverThread.Start();
                    senseUDPstatusIndicator.Value = 100;
                    senseServerStatus = true;
                    senseServerStarted = true;
                    senseUDPmessageBox.Text = "";
                }
                else {
                    senseUDPmessageBox.Text = "Please input a correct port";
                }
            }
            else { // Once the initial configuration has been done
                if (senseServerStatus) { // If the server's turned off
                    senseUDPserverThread.Abort();
                    senseUDPstatusIndicator.Value = 0;
                    senseServerStatus = false;
                    senseServerTurnedOff = true;
                }
                else { // If the server's turned on
                    senseUDPserverThread = new Thread(() => listenSense());
                    senseUDPserverThread.Start();
                    senseUDPstatusIndicator.Value = 100;
                    senseServerStatus = true;
                }
            }
        }

        private void animodUDPpowerButton_Click(object sender, EventArgs e) {
            if (!animodServerStarted) { // If the initial configuration hasn't been done
                if (int.TryParse(animodUDPportBox.Text, out animodUDPport)) {
                    animodUDPserverEP = new IPEndPoint(IPAddress.Any, animodUDPport);
                    animodUDPserver = new UdpClient(animodUDPserverEP);
                    // Start the thread
                    animodUDPserverThread = new Thread(() => listenAnimod());
                    animodUDPserverThread.Start();
                    animodUDPstatusIndicator.Value = 100;
                    animodServerStatus = true;
                    animodServerStarted = true;
                    animodUDPchat.Text = "";
                }
                else {
                    animodUDPchat.Text = "Please input a correct port";
                }
            }
            else { // Once the initial configuration has been done
                if (animodServerStatus) { // If the server's turned off
                    animodUDPserverThread.Abort();
                    animodUDPstatusIndicator.Value = 0;
                    animodServerStatus = false;
                    animodServerTurnedOff = true;
                }
                else { // If the server's turned on
                    animodUDPserverThread = new Thread(() => listenAnimod());
                    animodUDPserverThread.Start();
                    animodUDPstatusIndicator.Value = 100;
                    animodServerStatus = true;
                }
            }
        }
    }
}
