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

        // Serial variables
        List<int> xValues = new List<int>();
        List<double> yValues = new List<double>();
        List<double> yValues2 = new List<double>();
        int n = 1;
        // UDP variables
        int port;
        String[] chatHistoryArray = new String[10];
        String chatHistory, hostAddress;
        Thread serverThread;
        IPEndPoint serverEP;
        UdpClient udpServer;
        bool status, started = false, turnedOff;

        public Form1() {
            InitializeComponent();
            // Change char appearance
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
            chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
            chart1.Series[0].BorderWidth = 5;
            chart2.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gainsboro;
            chart2.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gainsboro;
            chart2.Series[0].BorderWidth = 5;
            String[] ports = SerialPort.GetPortNames(); // Get availabre Serial ports
            comboBox1.Items.AddRange(ports); // Display the ports
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(OnDataReceived);
            hostAddress = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString(); // Get server's IP Address
            serverAddress.Text = hostAddress; // Display the server's IP Address
        }

        private void button3_Click(object sender, EventArgs e) { // Open Serial port
            try {
                if(comboBox1.Text == "" || comboBox2.Text == "") {
                    textBox2.Text = "Please select port settings";
                }
                else {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                    serialPort1.Open();
                    serialOpenButton.Enabled = false;
                    serialCloseButton.Enabled = true;
                }
            }
            catch (UnauthorizedAccessException) {
                textBox2.Text = "Unauthorized Access";
            }
        }

        private void button4_Click(object sender, EventArgs e) { // Close Serial port
            serialPort1.Close();
            serialCloseButton.Enabled = false;
            serialOpenButton.Enabled = true;
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e) {
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
        }

        void Listen() {
            while (true) {
                byte[] inputData = udpServer.Receive(ref serverEP); // Receive the informations
                if (turnedOff) { // If the server was turned off and is now turned on
                    inputData = Encoding.ASCII.GetBytes(""); // Delete what was stored in the buffer when the server was off
                    turnedOff = false;
                }
                else {
                    for (int i = 9; i >= 1; i--) {
                        chatHistoryArray[i] = chatHistoryArray[i - 1]; // Move the history up one position 
                    }
                    chatHistoryArray[0] = serverEP.Address.ToString() + " at " + DateTime.Now.ToString() + ": " + Encoding.ASCII.GetString(inputData); // Insert the received information in the history
                    if (Encoding.ASCII.GetString(inputData) == "r") { // If a request is received
                        // Send data
                        byte[] outputData = Encoding.ASCII.GetBytes(yValues[9].ToString());
                        udpServer.Send(outputData, Encoding.ASCII.GetString(outputData).Length, serverEP);
                        for (int i = 7; i >= 1; i--) {
                            chatHistoryArray[i] = chatHistoryArray[i - 1]; // Move the history up one position
                        }
                        chatHistoryArray[0] = hostAddress + " at " + DateTime.Now.ToString() + ": " + Encoding.ASCII.GetString(outputData); // Insert the sent data in the history
                    }
                    chatHistory = "";
                    for (int i = 9; i >= 0; i--) {
                        chatHistory += chatHistoryArray[i] + "\r\n\r\n"; // Append the history in a single String
                    }
                    this.Invoke((MethodInvoker)delegate {
                        chat.Text = chatHistory; // Update the history
                    });
                }
            }
        }

        private void udpOnButton_Click(object sender, EventArgs e) {
            if (!started) { // If the initial configuration hasn't been done
                if (int.TryParse(portInput.Text, out port)) {
                    serverEP = new IPEndPoint(IPAddress.Any, port);
                    udpServer = new UdpClient(serverEP);
                    // Start the thread
                    serverThread = new Thread(() => Listen());
                    serverThread.Start();
                    statusIndicator.Value = 100;
                    status = true;
                    started = true;
                    chat.Text = "";
                }
                else {
                    chat.Text = "Please input a correct port";
                }
            }
            else { // Once the initial configuration has been done
                if (status) { // If the server's turned off
                    serverThread.Abort();
                    statusIndicator.Value = 0;
                    status = false;
                    turnedOff = true;
                }
                else { // If the server's turned on
                    serverThread = new Thread(() => Listen());
                    serverThread.Start();
                    statusIndicator.Value = 100;
                    status = true;
                }
            }
        }
    }
}
