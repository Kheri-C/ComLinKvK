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
        int index = 1;
        List<double> dataValues = new List<double>();

        // Left arm sense variables
        /*int alIndex = 1;
        List<int> alIndexValues = new List<int>();
        List<double> alXZdataValues = new List<double>();
        List<double> alYZdataValues = new List<double>();
        List<double> alXdataValues = new List<double>();
        List<double> alYdataValues = new List<double>();
        List<double> alZdataValues = new List<double>();*/

        // Right arm sense variables
        /*int arIndex = 1;
        List<int> arIndexValues = new List<int>();
        List<double> arXZdataValues = new List<double>();
        List<double> arYZdataValues = new List<double>();
        List<double> arXdataValues = new List<double>();
        List<double> arYdataValues = new List<double>();
        List<double> arZdataValues = new List<double>();*/

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
            serverAddress = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString(); // Get server's IP Address
            serverAddressBox.Text = serverAddress; // Display the server's IP Address
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            // borrar datos lista
            switch (comboBox1.SelectedItem.ToString()) {
                case "None":
                    break;
                case "Left Arm XZ Rotation":
                    chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.Series[0].BorderWidth = 5;
                    chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 90;
                    chart1.ChartAreas["ChartArea1"].AxisY.Interval = 30;
                    chart1.ChartAreas["ChartArea1"].AxisY.Minimum = -90;
                    chart1.ChartAreas["ChartArea1"].AxisY.Title = "alRotXZ";
                    chart1.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(fRotLabel);
                    chart1.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(bRotLabel);
                    break;
                case "Left Arm YZ Rotation":
                    chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.Series[0].BorderWidth = 5;
                    chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 90;
                    chart1.ChartAreas["ChartArea1"].AxisY.Interval = 30;
                    chart1.ChartAreas["ChartArea1"].AxisY.Minimum = -90;
                    chart1.ChartAreas["ChartArea1"].AxisY.Title = "alRotYZ";
                    chart1.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(rRotLabel);
                    chart1.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(lRotLabel);
                    break;
                case "Left Arm X Acceleration":
                    chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.Series[0].BorderWidth = 3;
                    chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 15;
                    chart1.ChartAreas["ChartArea1"].AxisY.Interval = 5;
                    chart1.ChartAreas["ChartArea1"].AxisY.Minimum = -15;
                    chart1.ChartAreas["ChartArea1"].AxisY.Title = "alAccX";
                    chart1.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(rAccLabel);
                    chart1.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(lAccLabel);
                    break;
                case "Left Arm Y Acceleration":
                    chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.Series[0].BorderWidth = 3;
                    chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 15;
                    chart1.ChartAreas["ChartArea1"].AxisY.Interval = 5;
                    chart1.ChartAreas["ChartArea1"].AxisY.Minimum = -15;
                    chart1.ChartAreas["ChartArea1"].AxisY.Title = "alAccY";
                    break;
                case "Left Arm Z Acceleration":
                    chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.Series[0].BorderWidth = 3;
                    chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 15;
                    chart1.ChartAreas["ChartArea1"].AxisY.Interval = 5;
                    chart1.ChartAreas["ChartArea1"].AxisY.Minimum = -15;
                    chart1.ChartAreas["ChartArea1"].AxisY.Title = "alAccZ";
                    break;
                case "Right Arm XZ Rotation":
                    chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.Series[0].BorderWidth = 5;
                    chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 90;
                    chart1.ChartAreas["ChartArea1"].AxisY.Interval = 30;
                    chart1.ChartAreas["ChartArea1"].AxisY.Minimum = -90;
                    chart1.ChartAreas["ChartArea1"].AxisY.Title = "arRotXZ";
                    chart1.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(fRotLabel);
                    chart1.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(bRotLabel);
                    break;
                case "Right Arm YZ Rotation":
                    chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.Series[0].BorderWidth = 5;
                    chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 90;
                    chart1.ChartAreas["ChartArea1"].AxisY.Interval = 30;
                    chart1.ChartAreas["ChartArea1"].AxisY.Minimum = -90;
                    chart1.ChartAreas["ChartArea1"].AxisY.Title = "arRotYZ";
                    chart1.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(rRotLabel);
                    chart1.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(lRotLabel);
                    break;
                case "Right Arm X Acceleration":
                    chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.Series[0].BorderWidth = 3;
                    chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 15;
                    chart1.ChartAreas["ChartArea1"].AxisY.Interval = 5;
                    chart1.ChartAreas["ChartArea1"].AxisY.Minimum = -15;
                    chart1.ChartAreas["ChartArea1"].AxisY.Title = "arAccX";
                    chart1.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(rAccLabel);
                    chart1.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(lAccLabel);
                    break;
                case "Right Arm Y Acceleration":
                    chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.Series[0].BorderWidth = 3;
                    chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 15;
                    chart1.ChartAreas["ChartArea1"].AxisY.Interval = 5;
                    chart1.ChartAreas["ChartArea1"].AxisY.Minimum = -15;
                    chart1.ChartAreas["ChartArea1"].AxisY.Title = "arAccY";
                    break;
                case "Right Arm Z Acceleration":
                    chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
                    chart1.Series[0].BorderWidth = 3;
                    chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 15;
                    chart1.ChartAreas["ChartArea1"].AxisY.Interval = 5;
                    chart1.ChartAreas["ChartArea1"].AxisY.Minimum = -15;
                    chart1.ChartAreas["ChartArea1"].AxisY.Title = "arAccZ";
                    break;
            }
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
                        if (senseUDPmessageBox.Text.Length == 50 && !senseUDPmessageBox.Text.Contains("NAN")) { // If a whole valid frame was received
                            sensor = senseUDPmessageBox.Text.Substring(0,2);
                            switch (sensor) {
                                case "AL":
                                    alRotXZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(6, 6)).ToString();
                                    alRotYZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(16, 6)).ToString();
                                    alAccXtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(25, 6)).ToString();
                                    alAccYtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(34, 6)).ToString();
                                    alAccZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(43, 6)).ToString();
                                    /*if (alIndex < 11) { // First 10 received messages
                                        alIndexValues.Add(alIndex);
                                        alXZdataValues.Add(Convert.ToDouble(senseUDPmessageBox.Text.Substring(6,6)));
                                        alYZdataValues.Add(Convert.ToDouble(senseUDPmessageBox.Text.Substring(16,6)));
                                        alXdataValues.Add(Convert.ToDouble(senseUDPmessageBox.Text.Substring(25, 6)));
                                        alYdataValues.Add(Convert.ToDouble(senseUDPmessageBox.Text.Substring(34, 6)));
                                        alZdataValues.Add(Convert.ToDouble(senseUDPmessageBox.Text.Substring(43, 6)));
                                        alIndex++;
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
                                    alZchart.Invalidate();*/
                                    break;
                                case "AR":
                                    arRotXZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(6, 6)).ToString();
                                    arRotYZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(16, 6)).ToString();
                                    arAccXtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(25, 6)).ToString();
                                    arAccYtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(34, 6)).ToString();
                                    arAccZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(43, 6)).ToString();
                                    /*if (arIndex < 11) { // First 10 received messages
                                        arIndexValues.Add(arIndex);
                                        arXZdataValues.Add(Convert.ToDouble(senseUDPmessageBox.Text.Substring(6, 6)));
                                        arYZdataValues.Add(Convert.ToDouble(senseUDPmessageBox.Text.Substring(16, 6)));
                                        arXdataValues.Add(Convert.ToDouble(senseUDPmessageBox.Text.Substring(25, 6)));
                                        arYdataValues.Add(Convert.ToDouble(senseUDPmessageBox.Text.Substring(34, 6)));
                                        arZdataValues.Add(Convert.ToDouble(senseUDPmessageBox.Text.Substring(43, 6)));
                                        arIndex++;
                                    }
                                    else {
                                        for (int i = 1; i < 10; i++) {
                                            arXZdataValues[i - 1] = arXZdataValues[i];
                                            arYZdataValues[i - 1] = arYZdataValues[i];
                                            arXdataValues[i - 1] = arXdataValues[i];
                                            arYdataValues[i - 1] = arYdataValues[i];
                                            arZdataValues[i - 1] = arZdataValues[i];
                                        }
                                        arXZdataValues[9] = Convert.ToDouble(senseUDPmessageBox.Text.Substring(6, 6));
                                        arYZdataValues[9] = Convert.ToDouble(senseUDPmessageBox.Text.Substring(16, 6));
                                        arXdataValues[9] = Convert.ToDouble(senseUDPmessageBox.Text.Substring(25, 6));
                                        arYdataValues[9] = Convert.ToDouble(senseUDPmessageBox.Text.Substring(34, 6));
                                        arZdataValues[9] = Convert.ToDouble(senseUDPmessageBox.Text.Substring(34, 6));
                                    }
                                    arXZchart.Series["Series1"].Points.DataBindXY(arIndexValues, arXZdataValues);
                                    arYZchart.Series["Series1"].Points.DataBindXY(arIndexValues, arYZdataValues);
                                    arXchart.Series["Series1"].Points.DataBindXY(arIndexValues, arXdataValues);
                                    arYchart.Series["Series1"].Points.DataBindXY(arIndexValues, arYdataValues);
                                    arZchart.Series["Series1"].Points.DataBindXY(arIndexValues, arZdataValues);
                                    arXZchart.Invalidate();
                                    arYZchart.Invalidate();
                                    arXchart.Invalidate();
                                    arYchart.Invalidate();
                                    arZchart.Invalidate();*/
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
                        case "alxz":
                            byte[] outputALXZData = Encoding.ASCII.GetBytes(alRotXZtextbox.Text);
                            animodUDPserver.Send(outputALXZData, outputALXZData.Length, animodUDPserverEP);
                            for (int i = 7; i >= 1; i--) {
                                chatHistoryArray[i] = chatHistoryArray[i - 1]; // Move the history up one position
                            }
                            chatHistoryArray[0] = serverAddress + " at " + DateTime.Now.ToString() + ": " + Encoding.ASCII.GetString(outputALXZData); // Insert the sent data in the history
                            break;
                        case "alyz":
                            byte[] outputALYZData = Encoding.ASCII.GetBytes(alRotYZtextbox.Text);
                            animodUDPserver.Send(outputALYZData, outputALYZData.Length, animodUDPserverEP);
                            for (int i = 7; i >= 1; i--) {
                                chatHistoryArray[i] = chatHistoryArray[i - 1]; // Move the history up one position
                            }
                            chatHistoryArray[0] = serverAddress + " at " + DateTime.Now.ToString() + ": " + Encoding.ASCII.GetString(outputALYZData); // Insert the sent data in the history
                            break;
                        case "alx":
                            byte[] outputALXData = Encoding.ASCII.GetBytes(alAccXtextbox.Text);
                            animodUDPserver.Send(outputALXData, outputALXData.Length, animodUDPserverEP);
                            for (int i = 7; i >= 1; i--) {
                                chatHistoryArray[i] = chatHistoryArray[i - 1]; // Move the history up one position
                            }
                            chatHistoryArray[0] = serverAddress + " at " + DateTime.Now.ToString() + ": " + Encoding.ASCII.GetString(outputALXData); // Insert the sent data in the history
                            break;
                        case "aly":
                            byte[] outputALYData = Encoding.ASCII.GetBytes(alAccYtextbox.Text);
                            animodUDPserver.Send(outputALYData, outputALYData.Length, animodUDPserverEP);
                            for (int i = 7; i >= 1; i--) {
                                chatHistoryArray[i] = chatHistoryArray[i - 1]; // Move the history up one position
                            }
                            chatHistoryArray[0] = serverAddress + " at " + DateTime.Now.ToString() + ": " + Encoding.ASCII.GetString(outputALYData); // Insert the sent data in the history
                            break;
                        case "alz":
                            byte[] outputALZData = Encoding.ASCII.GetBytes(alAccZtextbox.Text);
                            animodUDPserver.Send(outputALZData, outputALZData.Length, animodUDPserverEP);
                            for (int i = 7; i >= 1; i--) {
                                chatHistoryArray[i] = chatHistoryArray[i - 1]; // Move the history up one position
                            }
                            chatHistoryArray[0] = serverAddress + " at " + DateTime.Now.ToString() + ": " + Encoding.ASCII.GetString(outputALZData); // Insert the sent data in the history
                            break;
                        case "arxz":
                            byte[] outputARXZData = Encoding.ASCII.GetBytes(arRotXZtextbox.Text);
                            animodUDPserver.Send(outputARXZData, outputARXZData.Length, animodUDPserverEP); // Send data
                            for (int i = 7; i >= 1; i--) {
                                chatHistoryArray[i] = chatHistoryArray[i - 1]; // Move the history up one position
                            }
                            chatHistoryArray[0] = serverAddress + " at " + DateTime.Now.ToString() + ": " + Encoding.ASCII.GetString(outputARXZData); // Insert the sent data in the history
                            break;
                        case "aryz":
                            byte[] outputARYZData = Encoding.ASCII.GetBytes(arRotYZtextbox.Text);
                            animodUDPserver.Send(outputARYZData, outputARYZData.Length, animodUDPserverEP); // Send data
                            for (int i = 7; i >= 1; i--) {
                                chatHistoryArray[i] = chatHistoryArray[i - 1]; // Move the history up one position
                            }
                            chatHistoryArray[0] = serverAddress + " at " + DateTime.Now.ToString() + ": " + Encoding.ASCII.GetString(outputARYZData); // Insert the sent data in the history
                            break;
                        case "arx":
                            byte[] outputARXData = Encoding.ASCII.GetBytes(arAccXtextbox.Text);
                            animodUDPserver.Send(outputARXData, outputARXData.Length, animodUDPserverEP);
                            for (int i = 7; i >= 1; i--) {
                                chatHistoryArray[i] = chatHistoryArray[i - 1]; // Move the history up one position
                            }
                            chatHistoryArray[0] = serverAddress + " at " + DateTime.Now.ToString() + ": " + Encoding.ASCII.GetString(outputARXData); // Insert the sent data in the history
                            break;
                        case "ary":
                            byte[] outputARYData = Encoding.ASCII.GetBytes(arAccYtextbox.Text);
                            animodUDPserver.Send(outputARYData, outputARYData.Length, animodUDPserverEP);
                            for (int i = 7; i >= 1; i--) {
                                chatHistoryArray[i] = chatHistoryArray[i - 1]; // Move the history up one position
                            }
                            chatHistoryArray[0] = serverAddress + " at " + DateTime.Now.ToString() + ": " + Encoding.ASCII.GetString(outputARYData); // Insert the sent data in the history
                            break;
                        case "arz":
                            byte[] outputARZData = Encoding.ASCII.GetBytes(arAccZtextbox.Text);
                            animodUDPserver.Send(outputARZData, outputARZData.Length, animodUDPserverEP);
                            for (int i = 7; i >= 1; i--) {
                                chatHistoryArray[i] = chatHistoryArray[i - 1]; // Move the history up one position
                            }
                            chatHistoryArray[0] = serverAddress + " at " + DateTime.Now.ToString() + ": " + Encoding.ASCII.GetString(outputARZData); // Insert the sent data in the history
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
