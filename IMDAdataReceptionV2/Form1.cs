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
        CustomLabel bAccLabel = new CustomLabel(13, 15, "B", 1, LabelMarkStyle.None);
        CustomLabel fAccLabel = new CustomLabel(-15, -13, "F", 1, LabelMarkStyle.None);
        CustomLabel dAccLabel = new CustomLabel(13, 15, "D", 1, LabelMarkStyle.None);
        CustomLabel uAccLabel = new CustomLabel(-15, -13, "U", 1, LabelMarkStyle.None);
        bool changeChartFormat = false;
        int senseUDPport;
        Thread senseUDPserverThread;
        IPEndPoint senseUDPserverEP;
        UdpClient senseUDPserver;
        bool senseServerStatus, senseServerStarted = false, senseServerTurnedOff;
        Queue<double> data = new Queue<double>(10);

        // UDP animod variables
        int animodUDPport;
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
            chart1.Series[0].ChartType = SeriesChartType.FastLine;
            chart1.Legends[0].Enabled = false;
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
            chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
        }

        void setChartFormat(int lineSize, int max, int interval, int min, String title, CustomLabel label1, CustomLabel label2) {
            chart1.Series[0].BorderWidth = lineSize;
            chart1.ChartAreas["ChartArea1"].AxisY.Maximum = max;
            chart1.ChartAreas["ChartArea1"].AxisY.Interval = interval;
            chart1.ChartAreas["ChartArea1"].AxisY.Minimum = min;
            chart1.ChartAreas["ChartArea1"].AxisY.Title = title;
            chart1.ChartAreas["ChartArea1"].AxisY.CustomLabels.Clear();
            chart1.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(label1);
            chart1.ChartAreas["ChartArea1"].AxisY.CustomLabels.Add(label2);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            data.Clear();
            changeChartFormat = true;
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
                            switch (senseUDPmessageBox.Text.Substring(0,2)) {
                                case "AL":
                                    alRotXZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(6, 6)).ToString();
                                    alRotYZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(16, 6)).ToString();
                                    alAccXtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(25, 6)).ToString();
                                    alAccYtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(34, 6)).ToString();
                                    alAccZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(43, 6)).ToString();
                                    break;
                                case "AR":
                                    arRotXZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(6, 6)).ToString();
                                    arRotYZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(16, 6)).ToString();
                                    arAccXtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(25, 6)).ToString();
                                    arAccYtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(34, 6)).ToString();
                                    arAccZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(43, 6)).ToString();
                                    break;
                                case "FL":
                                    flRotXZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(6, 6)).ToString();
                                    flRotYZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(16, 6)).ToString();
                                    flAccXtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(25, 6)).ToString();
                                    flAccYtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(34, 6)).ToString();
                                    flAccZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(43, 6)).ToString();
                                    break;
                                case "FR":
                                    frRotXZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(6, 6)).ToString();
                                    frRotYZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(16, 6)).ToString();
                                    frAccXtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(25, 6)).ToString();
                                    frAccYtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(34, 6)).ToString();
                                    frAccZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(43, 6)).ToString();
                                    break;
                                case "CC":
                                    ccRotXZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(6, 6)).ToString();
                                    ccRotYZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(16, 6)).ToString();
                                    ccAccXtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(25, 6)).ToString();
                                    ccAccYtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(34, 6)).ToString();
                                    ccAccZtextbox.Text = Convert.ToDouble(senseUDPmessageBox.Text.Substring(43, 6)).ToString();
                                    break;
                                default:
                                    break;
                            }
                            if(comboBox1.SelectedIndex != -1) {
                                if (data.Count == 10) {
                                    data.Dequeue();
                                }
                                switch (comboBox1.SelectedItem.ToString()) {
                                    case "Left Arm XZ Rotation":
                                        if (changeChartFormat) {
                                            setChartFormat(5, 90, 30, -90, "Left Arm XZ Rotation", fRotLabel, bRotLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(alRotXZtextbox.Text));
                                        break;
                                    case "Left Arm YZ Rotation":
                                        if (changeChartFormat) {
                                            setChartFormat(5, 90, 30, -90, "Left Arm YZ Rotation", rRotLabel, lRotLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(alRotYZtextbox.Text));
                                        break;
                                    case "Left Arm X Acceleration":
                                        if (changeChartFormat) {
                                            setChartFormat(3, 15, 5, -15, "Left Arm X Acceleration", rAccLabel, lAccLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(alAccXtextbox.Text));
                                        break;
                                    case "Left Arm Y Acceleration":
                                        if (changeChartFormat) {
                                            setChartFormat(3, 15, 5, -15, "Left Arm Y Acceleration", fAccLabel, bAccLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(alAccYtextbox.Text));
                                        break;
                                    case "Left Arm Z Acceleration":
                                        if (changeChartFormat) {
                                            setChartFormat(3, 15, 5, -15, "Left Arm Z Acceleration", uAccLabel, dAccLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(alAccZtextbox.Text));
                                        break;
                                    case "Right Arm XZ Rotation":
                                        if (changeChartFormat) {
                                            setChartFormat(5, 90, 30, -90, "Right Arm XZ Rotation", fRotLabel, bRotLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(arRotXZtextbox.Text));
                                        break;
                                    case "Right Arm YZ Rotation":
                                        if (changeChartFormat) {
                                            setChartFormat(5, 90, 30, -90, "Right Arm YZ Rotation", rRotLabel, lRotLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(arRotYZtextbox.Text));
                                        break;
                                    case "Right Arm X Acceleration":
                                        if (changeChartFormat) {
                                            setChartFormat(3, 15, 5, -15, "Right Arm X Acceleration", rAccLabel, lAccLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(arAccXtextbox.Text));
                                        break;
                                    case "Right Arm Y Acceleration":
                                        if (changeChartFormat) {
                                            setChartFormat(3, 15, 5, -15, "Right Arm Y Acceleration", fAccLabel, bAccLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(arAccYtextbox.Text));
                                        break;
                                    case "Right Arm Z Acceleration":
                                        if (changeChartFormat) {
                                            setChartFormat(3, 15, 5, -15, "Right Arm Z Acceleration", uAccLabel, dAccLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(arAccZtextbox.Text));
                                        break;
                                    case "Torso XZ Rotation":
                                        if (changeChartFormat) {
                                            setChartFormat(5, 90, 30, -90, "Torso XZ Rotation", fRotLabel, bRotLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(ccRotXZtextbox.Text));
                                        break;
                                    case "Torso YZ Rotation":
                                        if (changeChartFormat) {
                                            setChartFormat(5, 90, 30, -90, "Torso YZ Rotation", rRotLabel, lRotLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(ccRotYZtextbox.Text));
                                        break;
                                    case "Torso X Acceleration":
                                        if (changeChartFormat) {
                                            setChartFormat(3, 15, 5, -15, "Torso X Acceleration", rAccLabel, lAccLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(ccAccXtextbox.Text));
                                        break;
                                    case "Torso Y Acceleration":
                                        if (changeChartFormat) {
                                            setChartFormat(3, 15, 5, -15, "Torso Y Acceleration", fAccLabel, bAccLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(ccAccYtextbox.Text));
                                        break;
                                    case "Torso Z Acceleration":
                                        if (changeChartFormat) {
                                            setChartFormat(3, 15, 5, -15, "Torso Z Acceleration", uAccLabel, dAccLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(ccAccZtextbox.Text));
                                        break;
                                    case "Left Foot XZ Rotation":
                                        if (changeChartFormat) {
                                            setChartFormat(5, 90, 30, -90, "Left Foot XZ Rotation", fRotLabel, bRotLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(flRotXZtextbox.Text));
                                        break;
                                    case "Left Foot YZ Rotation":
                                        if (changeChartFormat) {
                                            setChartFormat(5, 90, 30, -90, "Left Foot YZ Rotation", rRotLabel, lRotLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(flRotYZtextbox.Text));
                                        break;
                                    case "Left Foot X Acceleration":
                                        if (changeChartFormat) {
                                            setChartFormat(3, 15, 5, -15, "Left Foot X Acceleration", rAccLabel, lAccLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(flAccXtextbox.Text));
                                        break;
                                    case "Left Foot Y Acceleration":
                                        if (changeChartFormat) {
                                            setChartFormat(3, 15, 5, -15, "Left Foot Y Acceleration", fAccLabel, bAccLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(flAccYtextbox.Text));
                                        break;
                                    case "Left Foot Z Acceleration":
                                        if (changeChartFormat) {
                                            setChartFormat(3, 15, 5, -15, "Left Foot Z Acceleration", uAccLabel, dAccLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(flAccZtextbox.Text));
                                        break;
                                    case "Right Foot XZ Rotation":
                                        if (changeChartFormat) {
                                            setChartFormat(5, 90, 30, -90, "Right Foot XZ Rotation", fRotLabel, bRotLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(frRotXZtextbox.Text));
                                        break;
                                    case "Right Foot YZ Rotation":
                                        if (changeChartFormat) {
                                            setChartFormat(5, 90, 30, -90, "Right Foot YZ Rotation", rRotLabel, lRotLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(frRotYZtextbox.Text));
                                        break;
                                    case "Right Foot X Acceleration":
                                        if (changeChartFormat) {
                                            setChartFormat(3, 15, 5, -15, "Right Foot X Acceleration", rAccLabel, lAccLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(frAccXtextbox.Text));
                                        break;
                                    case "Right Foot Y Acceleration":
                                        if (changeChartFormat) {
                                            setChartFormat(3, 15, 5, -15, "Right Foot Y Acceleration", fAccLabel, bAccLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(frAccYtextbox.Text));
                                        break;
                                    case "Right Foot Z Acceleration":
                                        if (changeChartFormat) {
                                            setChartFormat(3, 15, 5, -15, "Right Foot Z Acceleration", uAccLabel, dAccLabel);
                                            changeChartFormat = false;
                                        }
                                        data.Enqueue(Convert.ToDouble(frAccZtextbox.Text));
                                        break;
                                }
                                chart1.Series[0].Points.DataBindY(data);
                            }
                        }
                    });
                }
            }
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
