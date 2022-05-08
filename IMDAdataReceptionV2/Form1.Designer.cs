
namespace IMDAdataReceptionV2 {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.alYZchart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.alXZchart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label2 = new System.Windows.Forms.Label();
            this.senseUDPstatusIndicator = new System.Windows.Forms.ProgressBar();
            this.senseUDPpowerButton = new System.Windows.Forms.Button();
            this.senseUDPportBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.senseUDPmessageBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.animodUDPstatusIndicator = new System.Windows.Forms.ProgressBar();
            this.animodUDPportBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.animodUDPchat = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.animodUDPpowerButton = new System.Windows.Forms.Button();
            this.serverAddressBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.debugTextBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.alXchart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.alYchart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.alZchart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.alYZchart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alXZchart)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alXchart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alYchart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alZchart)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.alZchart);
            this.groupBox2.Controls.Add(this.alYchart);
            this.groupBox2.Controls.Add(this.alXchart);
            this.groupBox2.Controls.Add(this.alYZchart);
            this.groupBox2.Controls.Add(this.alXZchart);
            this.groupBox2.Location = new System.Drawing.Point(16, 51);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(504, 481);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Left Arm";
            // 
            // alYZchart
            // 
            chartArea4.Name = "ChartArea1";
            this.alYZchart.ChartAreas.Add(chartArea4);
            this.alYZchart.Location = new System.Drawing.Point(12, 254);
            this.alYZchart.Margin = new System.Windows.Forms.Padding(4);
            this.alYZchart.Name = "alYZchart";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Name = "Series1";
            this.alYZchart.Series.Add(series4);
            this.alYZchart.Size = new System.Drawing.Size(238, 219);
            this.alYZchart.TabIndex = 11;
            this.alYZchart.Text = "chart2";
            // 
            // alXZchart
            // 
            chartArea5.Name = "ChartArea1";
            this.alXZchart.ChartAreas.Add(chartArea5);
            this.alXZchart.Location = new System.Drawing.Point(12, 23);
            this.alXZchart.Margin = new System.Windows.Forms.Padding(4);
            this.alXZchart.Name = "alXZchart";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Name = "Series1";
            this.alXZchart.Series.Add(series5);
            this.alXZchart.Size = new System.Drawing.Size(238, 219);
            this.alXZchart.TabIndex = 10;
            this.alXZchart.Text = "chart1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(710, 546);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Status";
            // 
            // senseUDPstatusIndicator
            // 
            this.senseUDPstatusIndicator.Location = new System.Drawing.Point(768, 540);
            this.senseUDPstatusIndicator.Margin = new System.Windows.Forms.Padding(4);
            this.senseUDPstatusIndicator.Name = "senseUDPstatusIndicator";
            this.senseUDPstatusIndicator.Size = new System.Drawing.Size(133, 28);
            this.senseUDPstatusIndicator.TabIndex = 15;
            // 
            // senseUDPpowerButton
            // 
            this.senseUDPpowerButton.Location = new System.Drawing.Point(1369, 10);
            this.senseUDPpowerButton.Margin = new System.Windows.Forms.Padding(4);
            this.senseUDPpowerButton.Name = "senseUDPpowerButton";
            this.senseUDPpowerButton.Size = new System.Drawing.Size(211, 28);
            this.senseUDPpowerButton.TabIndex = 14;
            this.senseUDPpowerButton.Text = "Turn ON / OFF";
            this.senseUDPpowerButton.UseVisualStyleBackColor = true;
            this.senseUDPpowerButton.Click += new System.EventHandler(this.senseUDPpowerButton_Click);
            // 
            // senseUDPportBox
            // 
            this.senseUDPportBox.Location = new System.Drawing.Point(56, 13);
            this.senseUDPportBox.Margin = new System.Windows.Forms.Padding(4);
            this.senseUDPportBox.Name = "senseUDPportBox";
            this.senseUDPportBox.Size = new System.Drawing.Size(132, 22);
            this.senseUDPportBox.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(501, 16);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Received data";
            // 
            // senseUDPmessageBox
            // 
            this.senseUDPmessageBox.Enabled = false;
            this.senseUDPmessageBox.Location = new System.Drawing.Point(608, 13);
            this.senseUDPmessageBox.Margin = new System.Windows.Forms.Padding(4);
            this.senseUDPmessageBox.Multiline = true;
            this.senseUDPmessageBox.Name = "senseUDPmessageBox";
            this.senseUDPmessageBox.ReadOnly = true;
            this.senseUDPmessageBox.Size = new System.Drawing.Size(487, 27);
            this.senseUDPmessageBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.animodUDPstatusIndicator);
            this.groupBox1.Controls.Add(this.animodUDPportBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.animodUDPchat);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.animodUDPpowerButton);
            this.groupBox1.Location = new System.Drawing.Point(547, 51);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(504, 481);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "UDP Animod";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(306, 448);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "Status";
            // 
            // animodUDPstatusIndicator
            // 
            this.animodUDPstatusIndicator.Location = new System.Drawing.Point(364, 442);
            this.animodUDPstatusIndicator.Margin = new System.Windows.Forms.Padding(4);
            this.animodUDPstatusIndicator.Name = "animodUDPstatusIndicator";
            this.animodUDPstatusIndicator.Size = new System.Drawing.Size(133, 28);
            this.animodUDPstatusIndicator.TabIndex = 11;
            // 
            // animodUDPportBox
            // 
            this.animodUDPportBox.Location = new System.Drawing.Point(11, 53);
            this.animodUDPportBox.Margin = new System.Windows.Forms.Padding(4);
            this.animodUDPportBox.Name = "animodUDPportBox";
            this.animodUDPportBox.Size = new System.Drawing.Size(132, 22);
            this.animodUDPportBox.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Message history";
            // 
            // animodUDPchat
            // 
            this.animodUDPchat.Enabled = false;
            this.animodUDPchat.Location = new System.Drawing.Point(9, 105);
            this.animodUDPchat.Margin = new System.Windows.Forms.Padding(4);
            this.animodUDPchat.Multiline = true;
            this.animodUDPchat.Name = "animodUDPchat";
            this.animodUDPchat.ReadOnly = true;
            this.animodUDPchat.Size = new System.Drawing.Size(487, 329);
            this.animodUDPchat.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 32);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Port";
            // 
            // animodUDPpowerButton
            // 
            this.animodUDPpowerButton.Location = new System.Drawing.Point(9, 442);
            this.animodUDPpowerButton.Margin = new System.Windows.Forms.Padding(4);
            this.animodUDPpowerButton.Name = "animodUDPpowerButton";
            this.animodUDPpowerButton.Size = new System.Drawing.Size(211, 28);
            this.animodUDPpowerButton.TabIndex = 3;
            this.animodUDPpowerButton.Text = "Turn ON / OFF";
            this.animodUDPpowerButton.UseVisualStyleBackColor = true;
            this.animodUDPpowerButton.Click += new System.EventHandler(this.animodUDPpowerButton_Click);
            // 
            // serverAddressBox
            // 
            this.serverAddressBox.Enabled = false;
            this.serverAddressBox.Location = new System.Drawing.Point(1371, 540);
            this.serverAddressBox.Margin = new System.Windows.Forms.Padding(4);
            this.serverAddressBox.Name = "serverAddressBox";
            this.serverAddressBox.Size = new System.Drawing.Size(209, 22);
            this.serverAddressBox.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1287, 543);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "IP Address";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 540);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 17);
            this.label8.TabIndex = 12;
            this.label8.Text = "Debug";
            // 
            // debugTextBox
            // 
            this.debugTextBox.Enabled = false;
            this.debugTextBox.Location = new System.Drawing.Point(75, 540);
            this.debugTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.debugTextBox.Name = "debugTextBox";
            this.debugTextBox.Size = new System.Drawing.Size(209, 22);
            this.debugTextBox.TabIndex = 13;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chart1);
            this.groupBox3.Controls.Add(this.chart2);
            this.groupBox3.Location = new System.Drawing.Point(1076, 51);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(504, 481);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Right Arm";
            // 
            // chart1
            // 
            chartArea6.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea6);
            this.chart1.Location = new System.Drawing.Point(10, 297);
            this.chart1.Margin = new System.Windows.Forms.Padding(4);
            this.chart1.Name = "chart1";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Name = "Series1";
            this.chart1.Series.Add(series6);
            this.chart1.Size = new System.Drawing.Size(488, 137);
            this.chart1.TabIndex = 11;
            this.chart1.Text = "chart2";
            // 
            // chart2
            // 
            chartArea7.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea7);
            this.chart2.Location = new System.Drawing.Point(10, 154);
            this.chart2.Margin = new System.Windows.Forms.Padding(4);
            this.chart2.Name = "chart2";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Name = "Series1";
            this.chart2.Series.Add(series7);
            this.chart2.Size = new System.Drawing.Size(488, 137);
            this.chart2.TabIndex = 10;
            this.chart2.Text = "chart1";
            // 
            // alXchart
            // 
            chartArea3.Name = "ChartArea1";
            this.alXchart.ChartAreas.Add(chartArea3);
            this.alXchart.Location = new System.Drawing.Point(258, 23);
            this.alXchart.Margin = new System.Windows.Forms.Padding(4);
            this.alXchart.Name = "alXchart";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Name = "Series1";
            this.alXchart.Series.Add(series3);
            this.alXchart.Size = new System.Drawing.Size(238, 134);
            this.alXchart.TabIndex = 12;
            this.alXchart.Text = "chart1";
            // 
            // alYchart
            // 
            chartArea2.Name = "ChartArea1";
            this.alYchart.ChartAreas.Add(chartArea2);
            this.alYchart.Location = new System.Drawing.Point(258, 180);
            this.alYchart.Margin = new System.Windows.Forms.Padding(4);
            this.alYchart.Name = "alYchart";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Name = "Series1";
            this.alYchart.Series.Add(series2);
            this.alYchart.Size = new System.Drawing.Size(238, 134);
            this.alYchart.TabIndex = 13;
            this.alYchart.Text = "chart1";
            // 
            // alZchart
            // 
            chartArea1.Name = "ChartArea1";
            this.alZchart.ChartAreas.Add(chartArea1);
            this.alZchart.Location = new System.Drawing.Point(258, 336);
            this.alZchart.Margin = new System.Windows.Forms.Padding(4);
            this.alZchart.Name = "alZchart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "Series1";
            this.alZchart.Series.Add(series1);
            this.alZchart.Size = new System.Drawing.Size(238, 134);
            this.alZchart.TabIndex = 14;
            this.alZchart.Text = "chart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1602, 574);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.senseUDPpowerButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.senseUDPportBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.debugTextBox);
            this.Controls.Add(this.senseUDPmessageBox);
            this.Controls.Add(this.senseUDPstatusIndicator);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.serverAddressBox);
            this.Controls.Add(this.label6);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "ComLinK";
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.alYZchart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alXZchart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alXchart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alYchart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alZchart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox senseUDPmessageBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart alXZchart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox animodUDPchat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button animodUDPpowerButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox animodUDPportBox;
        private System.Windows.Forms.TextBox serverAddressBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar animodUDPstatusIndicator;
        private System.Windows.Forms.DataVisualization.Charting.Chart alYZchart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar senseUDPstatusIndicator;
        private System.Windows.Forms.Button senseUDPpowerButton;
        private System.Windows.Forms.TextBox senseUDPportBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox debugTextBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.DataVisualization.Charting.Chart alZchart;
        private System.Windows.Forms.DataVisualization.Charting.Chart alYchart;
        private System.Windows.Forms.DataVisualization.Charting.Chart alXchart;
    }
}

