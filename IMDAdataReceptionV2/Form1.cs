using System;
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

        // UDP sense general variables
        CustomLabel fRotLabel = new CustomLabel(-90, -80, "F", 1, LabelMarkStyle.None);
        CustomLabel bRotLabel = new CustomLabel(80, 90, "B", 1, LabelMarkStyle.None);
        CustomLabel rRotLabel = new CustomLabel(80, 90, "R", 1, LabelMarkStyle.None);
        CustomLabel lRotLabel = new CustomLabel(-90, -80, "L", 1, LabelMarkStyle.None);
        CustomLabel rAccLabel = new CustomLabel(13,15,"R", 1, LabelMarkStyle.None);
        CustomLabel lAccLabel = new CustomLabel(-15, -13, "L", 1, LabelMarkStyle.None);
        int senseUDPport;
        Thread senseUDPserverThread;
        IPEndPoint senseUDPserverEP;
        UdpClient senseUDPserver;
        bool senseServerStatus, senseServerStarted = false, senseServerTurnedOff;
        String sensor;

        // Left arm sense variables
        int n = 1;
        List<int> alIndexValues = new List<int>();
        List<double> alXZdataValues = new List<double>();
        List<double> alYZdataValues = new List<double>();
        List<double> alXdataValues = new List<double>();
        List<double> alYdataValues = new List<double>();
        List<double> alZdataValues = new List<double>();

        // Right arm sense variables

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
            //Size = new Size(1920, 830); // Full size without windows taskbar
            alXZchart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
            alXZchart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
            alXZchart.Series[0].BorderWidth = 5;
            alXZchart.ChartAreas["ChartArea1"].AxisY.Maximum = 90;
            alXZchart.ChartAreas["ChartArea1"].AxisY.Interval = 30;
            alXZchart.ChartAreas["ChartArea1"].AxisY.Minimum = -90;
            alXZchart.ChartAreas["ChartArea1"].AxisY.Title = "RotXZ";
            alXZchart.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(fRotLabel);
            alXZchart.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(bRotLabel);

            alYZchart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
            alYZchart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
            alYZchart.Series[0].BorderWidth = 5;
            alYZchart.ChartAreas["ChartArea1"].AxisY.Maximum = 90;
            alYZchart.ChartAreas["ChartArea1"].AxisY.Interval = 30;
            alYZchart.ChartAreas["ChartArea1"].AxisY.Minimum = -90;
            alYZchart.ChartAreas["ChartArea1"].AxisY.Title = "RotYZ";
            alYZchart.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(rRotLabel);
            alYZchart.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(lRotLabel);

            alXchart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
            alXchart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
            alXchart.Series[0].BorderWidth = 3;
            alXchart.ChartAreas["ChartArea1"].AxisY.Maximum = 15;
            alXchart.ChartAreas["ChartArea1"].AxisY.Interval = 5;
            alXchart.ChartAreas["ChartArea1"].AxisY.Minimum = -15;
            alXchart.ChartAreas["ChartArea1"].AxisY.Title = "AccX";
            alXchart.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(rAccLabel);
            alXchart.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(lAccLabel);

            alYchart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
            alYchart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
            alYchart.Series[0].BorderWidth = 3;
            alYchart.ChartAreas["ChartArea1"].AxisY.Maximum = 15;
            alYchart.ChartAreas["ChartArea1"].AxisY.Interval = 5;
            alYchart.ChartAreas["ChartArea1"].AxisY.Minimum = -15;
            alYchart.ChartAreas["ChartArea1"].AxisY.Title = "AccY";

            alZchart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
            alZchart.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
            alZchart.Series[0].BorderWidth = 3;
            alZchart.ChartAreas["ChartArea1"].AxisY.Maximum = 15;
            alZchart.ChartAreas["ChartArea1"].AxisY.Interval = 5;
            alZchart.ChartAreas["ChartArea1"].AxisY.Minimum = -15;
            alZchart.ChartAreas["ChartArea1"].AxisY.Title = "AccZ";

            serverAddress = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString(); // Get server's IP Address
            serverAddressBox.Text = serverAddress; // Display the server's IP Address
        }

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
                        debugTextBox.Text = senseUDPmessageBox.Text.Substring(0,2); // Debug
                        if (senseUDPmessageBox.Text.Length == 50 && !senseUDPmessageBox.Text.Contains("NAN")) { // If a whole valid frame was received
                            sensor = senseUDPmessageBox.Text.Substring(0,2);
                            switch (sensor) {
                                case "AL":
                                    if (n < 11) { // First 10 received messages
                                        alIndexValues.Add(n);
                                        alXZdataValues.Add(Convert.ToDouble(senseUDPmessageBox.Text.Substring(6,6)));
                                        alYZdataValues.Add(Convert.ToDouble(senseUDPmessageBox.Text.Substring(16,6)));
                                        alXdataValues.Add(Convert.ToDouble(senseUDPmessageBox.Text.Substring(25, 6)));
                                        alYdataValues.Add(Convert.ToDouble(senseUDPmessageBox.Text.Substring(34, 6)));
                                        alZdataValues.Add(Convert.ToDouble(senseUDPmessageBox.Text.Substring(43, 6)));
                                        n++;
                                    }
                                    else {
                                        for (int i = 1; i < 10; i++) {
                                            alXZdataValues[i - 1] = alXZdataValues[i];
                                            alYZdataValues[i - 1] = alYZdataValues[i];
                                            alXdataValues[i - 1] = alXdataValues[i];
                                            alYdataValues[i - 1] = alYdataValues[i];
                                            alZdataValues[i - 1] = alZdataValues[i];
                                        }
                                        alXZdataValues[9] = Convert.ToDouble(senseUDPmessageBox.Text.Substring(6,6));
                                        alYZdataValues[9] = Convert.ToDouble(senseUDPmessageBox.Text.Substring(16,6));
                                        alXdataValues[9] = Convert.ToDouble(senseUDPmessageBox.Text.Substring(25, 6));
                                        alYdataValues[9] = Convert.ToDouble(senseUDPmessageBox.Text.Substring(34, 6));
                                        alZdataValues[9] = Convert.ToDouble(senseUDPmessageBox.Text.Substring(34, 6));
                                    }
                                    alXZchart.Series["Series1"].Points.DataBindXY(alIndexValues, alXZdataValues);
                                    alYZchart.Series["Series1"].Points.DataBindXY(alIndexValues, alYZdataValues);
                                    alXchart.Series["Series1"].Points.DataBindXY(alIndexValues, alXdataValues);
                                    alYchart.Series["Series1"].Points.DataBindXY(alIndexValues, alYdataValues);
                                    alZchart.Series["Series1"].Points.DataBindXY(alIndexValues, alZdataValues);
                                    alXZchart.Invalidate();
                                    alYZchart.Invalidate();
                                    alXchart.Invalidate();
                                    alYchart.Invalidate();
                                    alZchart.Invalidate();
                                    break;
                                default:
                                    break;
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
                            byte[] outputXZData = Encoding.ASCII.GetBytes(alXZdataValues[9].ToString());
                            animodUDPserver.Send(outputXZData, outputXZData.Length, animodUDPserverEP);
                            for (int i = 7; i >= 1; i--) {
                                chatHistoryArray[i] = chatHistoryArray[i - 1]; // Move the history up one position
                            }
                            chatHistoryArray[0] = serverAddress + " at " + DateTime.Now.ToString() + ": " + Encoding.ASCII.GetString(outputXZData); // Insert the sent data in the history
                            break;
                        case "y":
                            byte[] outputYZData = Encoding.ASCII.GetBytes(alYZdataValues[9].ToString());
                            animodUDPserver.Send(outputYZData, outputYZData.Length, animodUDPserverEP); // Send data
                            for (int i = 7; i >= 1; i--) {
                                chatHistoryArray[i] = chatHistoryArray[i - 1]; // Move the history up one position
                            }
                            chatHistoryArray[0] = serverAddress + " at " + DateTime.Now.ToString() + ": " + Encoding.ASCII.GetString(outputYZData); // Insert the sent data in the history
                            break;
                        default:
                            break;
                    }
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
