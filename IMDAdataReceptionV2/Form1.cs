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

        // UDP ESPsense variables
        Int32 senseUDPport = 5332;
        CustomLabel fRotLabel = new CustomLabel(-90, -80, "F", 1, LabelMarkStyle.None);
        CustomLabel bRotLabel = new CustomLabel(80, 90, "B", 1, LabelMarkStyle.None);
        CustomLabel rRotLabel = new CustomLabel(80, 90, "R", 1, LabelMarkStyle.None);
        CustomLabel lRotLabel = new CustomLabel(-90, -80, "L", 1, LabelMarkStyle.None);
        CustomLabel rAccLabel = new CustomLabel(13,15,"R", 1, LabelMarkStyle.None);
        CustomLabel lAccLabel = new CustomLabel(-15, -13, "L", 1, LabelMarkStyle.None);
        CustomLabel bAccLabel = new CustomLabel(13, 15, "B", 1, LabelMarkStyle.None);
        CustomLabel fAccLabel = new CustomLabel(-15, -13, "F", 1, LabelMarkStyle.None);
        CustomLabel dAccLabel = new CustomLabel(13, 15, "D", 1, LabelMarkStyle.None);
        CustomLabel uAccLabel = new CustomLabel(-15, -13, "U", 1, LabelMarkStyle.None);
        bool changeChartFormat = false;
        bool senseServerStatus, senseServerStarted = false, senseServerTurnedOff;
        //int senseUDPport;
        Thread senseUDPserverThread;
        IPEndPoint senseUDPserverEP;
        UdpClient senseUDPclient;
        Queue<double> data = new Queue<double>(10);

        // TCP ESPsense variables
        Int32 senseTCPport = 5332;
        IPEndPoint alSenseEP, arSenseEP, ccSenseEP, flSenseEP, frSenseEP, senseTCPserverEP;
        TcpClient senseTCPclient;
        NetworkStream senseTCPstream;

        // UDP animod variables
        Int32 animodUDPport = 42069;
        Thread animodUDPserverThread;
        IPEndPoint animodUDPserverEP;
        UdpClient animodUDPserver;
        bool animodServerStatus, animodServerStarted = false, animodServerTurnedOff;
        Queue<String> chat = new Queue<string>(10);

        public Form1() {
            InitializeComponent();
            //Size = new Size(1920, 830); // Full size without windows taskbar
            serverAddress = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString(); // Get server's IP Address
            serverAddressBox.Text = serverAddress; // Display the server's IP Address
            senseUDPportBox.Text = senseUDPport.ToString();
            animodUDPportBox.Text = animodUDPport.ToString();
            // Customize the chart
            chart1.Series[0].ChartType = SeriesChartType.FastLine; 
            chart1.Legends[0].Enabled = false;
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
            chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            data.Clear();
            changeChartFormat = true;
        }

        void displayData(TextBox RotXZtextbox, TextBox RotYZtextbox, TextBox AccXtextbox, TextBox AccYtextbox, TextBox AccZtextbox) {
            RotXZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(6, 6)).ToString();
            RotYZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(16, 6)).ToString();
            AccXtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(25, 6)).ToString();
            AccYtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(34, 6)).ToString();
            AccZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(43, 6)).ToString();
        }

        void graphData(int lineSize, int max, int interval, int min, String title, CustomLabel label1, CustomLabel label2, TextBox textbox) {
            if (changeChartFormat) { // If the data type selecction changed
                // The graph format are changed accordingly
                chart1.Series[0].BorderWidth = lineSize;
                chart1.ChartAreas["ChartArea1"].AxisY.Maximum = max;
                chart1.ChartAreas["ChartArea1"].AxisY.Interval = interval;
                chart1.ChartAreas["ChartArea1"].AxisY.Minimum = min;
                chart1.ChartAreas["ChartArea1"].AxisY.Title = title;
                chart1.ChartAreas["ChartArea1"].AxisY.CustomLabels.Clear();
                chart1.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(label1);
                chart1.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(label2);
                changeChartFormat = false;
            }
            if(textbox.Text != "") {
                data.Enqueue(Convert.ToDouble(textbox.Text));
            }
        }

        void listenSense() {
            while (true) {
                byte[] inputData = senseUDPclient.Receive(ref senseUDPserverEP); // Receive the information
                if (senseServerTurnedOff) { // If the server was turned off and is now turned on
                    inputData = Encoding.ASCII.GetBytes(""); // Delete what was stored in the buffer when the server was off
                    senseServerTurnedOff = false;
                }
                else {
                    this.Invoke((MethodInvoker)delegate {
                        senseUDPmessageBox.Text = Encoding.ASCII.GetString(inputData);
                        if (senseUDPmessageBox.Text.Length == 50 && !senseUDPmessageBox.Text.Contains("NAN")) { // If a whole valid frame was received
                            switch (senseUDPmessageBox.Text.Substring(0,2)) { // Depending on what sensor sent the information, display it in the corresponding textboxes
                                case "AL":
                                    displayData(alRotXZtextbox, alRotYZtextbox, alAccXtextbox, alAccYtextbox, alAccZtextbox);
                                    break;
                                case "AR":
                                    displayData(arRotXZtextbox, arRotYZtextbox, arAccXtextbox, arAccYtextbox, arAccZtextbox);
                                    break;
                                case "FL":
                                    displayData(flRotXZtextbox, flRotYZtextbox, flAccXtextbox, flAccYtextbox, flAccZtextbox);
                                    break;
                                case "FR":
                                    displayData(frRotXZtextbox, frRotYZtextbox, frAccXtextbox, frAccYtextbox, frAccZtextbox);
                                    break;
                                case "CC":
                                    displayData(ccRotXZtextbox, ccRotYZtextbox, ccAccXtextbox, ccAccYtextbox, ccAccZtextbox);
                                    break;
                                default:
                                    break;
                            }
                            if(comboBox1.SelectedIndex != -1) { // If a type of data is selected to be graphed
                                if (data.Count == 10) { // If 10 data points are graphed
                                    data.Dequeue(); // Erase the oldest one
                                }
                                switch (comboBox1.SelectedIndex) { // Graph the corresponding data point
                                    // This code could be reduced by using an array of structures and using the selected index as the array index
                                    case 1: // Left Arm XZ Rotation
                                        graphData(5, 90, 30, -90, "Left Arm XZ Rotation", fRotLabel, bRotLabel, alRotXZtextbox);
                                        break;
                                    case 2: // Left Arm YZ Rotation
                                        graphData(5, 90, 30, -90, "Left Arm YZ Rotation", rRotLabel, lRotLabel, alRotYZtextbox);
                                        break;
                                    case 3: // Left Arm X Acceleration
                                        graphData(3, 15, 5, -15, "Left Arm X Acceleration", rAccLabel, lAccLabel, alAccXtextbox);
                                        break;
                                    case 4: // Left Arm Y Acceleration
                                        graphData(3, 15, 5, -15, "Left Arm Y Acceleration", fAccLabel, bAccLabel, alAccYtextbox);
                                        break;
                                    case 5: // Left Arm Z Acceleration
                                        graphData(3, 15, 5, -15, "Left Arm Z Acceleration", uAccLabel, dAccLabel, alAccZtextbox);
                                        break;
                                    case 6: // Right Arm XZ Rotation
                                        graphData(5, 90, 30, -90, "Right Arm XZ Rotation", fRotLabel, bRotLabel, arRotXZtextbox);
                                        break;
                                    case 7: // Right Arm YZ Rotation
                                        graphData(5, 90, 30, -90, "Right Arm YZ Rotation", rRotLabel, lRotLabel, arRotYZtextbox);
                                        break;
                                    case 8: // Right Arm X Acceleration
                                        graphData(3, 15, 5, -15, "Right Arm X Acceleration", rAccLabel, lAccLabel, arAccXtextbox);
                                        break;
                                    case 9: // Right Arm Y Acceleration
                                        graphData(3, 15, 5, -15, "Right Arm Y Acceleration", fAccLabel, bAccLabel, arAccYtextbox);
                                        break;
                                    case 10: // Right Arm Z Acceleration
                                        graphData(3, 15, 5, -15, "Right Arm Z Acceleration", uAccLabel, dAccLabel, arAccZtextbox);
                                        break;
                                    case 11: // Torso XZ Rotation
                                        graphData(5, 90, 30, -90, "Torso XZ Rotation", fRotLabel, bRotLabel, ccRotXZtextbox);
                                        break;
                                    case 12: // Torso YZ Rotation
                                        graphData(5, 90, 30, -90, "Torso YZ Rotation", rRotLabel, lRotLabel, ccRotYZtextbox);
                                        break;
                                    case 13: // Torso X Acceleration
                                        graphData(3, 15, 5, -15, "Torso X Acceleration", rAccLabel, lAccLabel, ccAccXtextbox);
                                        break;
                                    case 14: // Torso Y Acceleration
                                        graphData(3, 15, 5, -15, "Torso Y Acceleration", fAccLabel, bAccLabel, ccAccYtextbox);
                                        break;
                                    case 15: // Torso Z Acceleration
                                        graphData(3, 15, 5, -15, "Torso Z Acceleration", uAccLabel, dAccLabel, ccAccZtextbox);
                                        break;
                                    case 16: // Left Foot XZ Rotation
                                        graphData(5, 90, 30, -90, "Left Foot XZ Rotation", fRotLabel, bRotLabel, flRotXZtextbox);
                                        break;
                                    case 17: // Left Foot XY Rotation
                                        graphData(5, 90, 30, -90, "Left Foot YZ Rotation", rRotLabel, lRotLabel, flRotYZtextbox);
                                        break;
                                    case 18: // Left Foot X Acceleration
                                        graphData(3, 15, 5, -15, "Left Foot X Acceleration", rAccLabel, lAccLabel, flAccXtextbox);
                                        break;
                                    case 19: // Left Foot Y Acceleration
                                        graphData(3, 15, 5, -15, "Left Foot Y Acceleration", fAccLabel, bAccLabel, flAccYtextbox);
                                        break;
                                    case 20: // Left Foot Z Acceleration
                                        graphData(3, 15, 5, -15, "Left Foot Z Acceleration", uAccLabel, dAccLabel, flAccZtextbox);
                                        break;
                                    case 21: // Right Foot XZ Rotation
                                        graphData(5, 90, 30, -90, "Right Foot XZ Rotation", fRotLabel, bRotLabel, frRotXZtextbox);
                                        break;
                                    case 22: // Right Foot YZ Rotation
                                        graphData(5, 90, 30, -90, "Right Foot YZ Rotation", rRotLabel, lRotLabel, frRotYZtextbox);
                                        break;
                                    case 23: // Right Foot X Acceleration
                                        graphData(3, 15, 5, -15, "Right Foot X Acceleration", rAccLabel, lAccLabel, frAccXtextbox);
                                        break;
                                    case 24: // Right Foot Y Acceleration
                                        graphData(3, 15, 5, -15, "Right Foot Y Acceleration", fAccLabel, bAccLabel, frAccYtextbox);
                                        break;
                                    case 25: // Right Foot Z Acceleration
                                        graphData(3, 15, 5, -15, "Right Foot Z Acceleration", uAccLabel, dAccLabel, frAccZtextbox);
                                        break;
                                }
                                chart1.Series[0].Points.DataBindY(data); // Graph the data
                            }
                        }
                    });
                }
            }
        }

        private void alResetButton_Click(object sender, EventArgs e) {
            
        }

        private void alCalibrateButton_Click(object sender, EventArgs e) {
            
        }

        private void arResetButton_Click(object sender, EventArgs e) {
            senseTCPstream.Write(Encoding.ASCII.GetBytes("r"), 0, Encoding.ASCII.GetBytes("c").Length);
        }

        private void arCalibrateButton_Click(object sender, EventArgs e) {
            senseTCPstream.Write(Encoding.ASCII.GetBytes("c"), 0, Encoding.ASCII.GetBytes("c").Length);
        }

        private void ccResetButton_Click(object sender, EventArgs e) {
            
        }

        private void ccCalibrateButton_Click(object sender, EventArgs e) {
            
        }

        private void flResetButton_Click(object sender, EventArgs e) {
            
        }

        private void flCalibrateButton_Click(object sender, EventArgs e) {
            
        }

        private void frResetButton_Click(object sender, EventArgs e) {
            
        }

        private void frCalibrateButton_Click(object sender, EventArgs e) {
            
        }

        void sendAndLog(String value) {
            animodUDPserver.Send(Encoding.ASCII.GetBytes(value), Encoding.ASCII.GetBytes(value).Length, animodUDPserverEP);
            if (chat.Count == 10) {
                chat.Dequeue();
            }
            chat.Enqueue(serverAddress + " at " + DateTime.Now.ToString() + ": " + value);
        }

        void listenAnimod() {
            while (true) {
                byte[] inputData = animodUDPserver.Receive(ref animodUDPserverEP); // Receive the information
                if (animodServerTurnedOff) { // If the server was turned off and is now turned on
                    inputData = Encoding.ASCII.GetBytes(""); // Delete what was stored in the buffer when the server was off
                    animodServerTurnedOff = false;
                }
                else {
                    if(chat.Count == 10) {
                        chat.Dequeue();
                    }
                    chat.Enqueue(animodUDPserverEP.Address.ToString() + " at " + DateTime.Now.ToString() + ": " + Encoding.ASCII.GetString(inputData));
                    switch (Encoding.ASCII.GetString(inputData)) {
                        case "alxz":
                            sendAndLog(alRotXZtextbox.Text);
                            break;
                        case "alyz":
                            sendAndLog(alRotYZtextbox.Text);
                            break;
                        case "alx":
                            sendAndLog(alAccXtextbox.Text);
                            break;
                        case "aly":
                            sendAndLog(alAccYtextbox.Text);
                            break;
                        case "alz":
                            sendAndLog(alAccZtextbox.Text);
                            break;
                        case "arxz":
                            sendAndLog(arRotXZtextbox.Text);
                            break;
                        case "aryz":
                            sendAndLog(arRotYZtextbox.Text);
                            break;
                        case "arx":
                            sendAndLog(arAccXtextbox.Text);
                            break;
                        case "ary":
                            sendAndLog(arAccYtextbox.Text);
                            break;
                        case "arz":
                            sendAndLog(arAccZtextbox.Text);
                            break;
                        case "ccxz":
                            sendAndLog(ccRotXZtextbox.Text);
                            break;
                        case "ccyz":
                            sendAndLog(ccRotYZtextbox.Text);
                            break;
                        case "ccx":
                            sendAndLog(ccAccXtextbox.Text);
                            break;
                        case "ccy":
                            sendAndLog(ccAccYtextbox.Text);
                            break;
                        case "ccz":
                            sendAndLog(ccAccZtextbox.Text);
                            break;
                        case "flxz":
                            sendAndLog(flRotXZtextbox.Text);
                            break;
                        case "flyz":
                            sendAndLog(flRotYZtextbox.Text);
                            break;
                        case "flx":
                            sendAndLog(flAccXtextbox.Text);
                            break;
                        case "fly":
                            sendAndLog(flAccYtextbox.Text);
                            break;
                        case "flz":
                            sendAndLog(flAccZtextbox.Text);
                            break;
                        case "frxz":
                            sendAndLog(frRotXZtextbox.Text);
                            break;
                        case "fryz":
                            sendAndLog(frRotYZtextbox.Text);
                            break;
                        case "frx":
                            sendAndLog(frAccXtextbox.Text);
                            break;
                        case "fry":
                            sendAndLog(frAccYtextbox.Text);
                            break;
                        case "frz":
                            sendAndLog(arAccZtextbox.Text);
                            break;
                        default:
                            break;
                    }
                    this.Invoke((MethodInvoker)delegate {
                        animodUDPchat.Text = String.Join("\r\n\r\n", Array.ConvertAll(chat.ToArray(), i => i.ToString()));
                    });
                }
            }
        }

        private void senseUDPpowerButton_Click(object sender, EventArgs e) {
            if (!senseServerStarted) { // If the initial configuration hasn't been done
                senseUDPserverEP = new IPEndPoint(IPAddress.Any, senseUDPport);
                senseUDPclient = new UdpClient(senseUDPserverEP);
       
                // TCP
                /*senseTCPserverEP = new IPEndPoint(IPAddress.Any, senseTCPport);
                alSenseEP = new IPEndPoint(IPAddress.Parse("192.168.0.101"), senseTCPport);
                senseTCPclient = new TcpClient(senseTCPserverEP);
                senseTCPclient.Connect(alSenseEP); // Revisar si el puerto ya esta utilizado y ver si se puede desconectar
                senseTCPstream = senseTCPclient.GetStream();*/

                // Start the thread
                senseUDPserverThread = new Thread(() => listenSense());
                senseUDPserverThread.Start();
                senseUDPstatusIndicator.Value = 100;
                senseServerStatus = true;
                senseServerStarted = true;
                senseUDPmessageBox.Text = "";
            }
            else { // Once the initial configuration has been done
                if (senseServerStatus) { // If the server's turned off
                    senseUDPserverThread.Abort();
                    senseUDPstatusIndicator.Value = 0;
                    senseServerStatus = false;
                    senseServerTurnedOff = true;

                    // TCP
                    //senseTCPstream.Close();
                }
                else { // If the server's turned on
                    senseUDPserverThread = new Thread(() => listenSense());
                    senseUDPserverThread.Start();
                    senseUDPstatusIndicator.Value = 100;
                    senseServerStatus = true;
                    
                    // TCP
                    /*senseTCPclient.Connect(alSenseEP); // Error: Cannot access a disposed objefct
                    senseTCPstream = senseTCPclient.GetStream();*/
                    
                }
            }
        }

        private void animodUDPpowerButton_Click(object sender, EventArgs e) {
            if (!animodServerStarted) { // If the initial configuration hasn't been done
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
            else { // Once the initial configuration has been done
                if (animodServerStatus) { // If the server is turned off
                    animodUDPserverThread.Abort();
                    animodUDPstatusIndicator.Value = 0;
                    animodServerStatus = false;
                    animodServerTurnedOff = true;
                }
                else { // If the server is turned on
                    animodUDPserverThread = new Thread(() => listenAnimod());
                    animodUDPserverThread.Start();
                    animodUDPstatusIndicator.Value = 100;
                    animodServerStatus = true;
                }
            }
        }
    }
}
