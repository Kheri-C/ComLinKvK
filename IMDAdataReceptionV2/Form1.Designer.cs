
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.senseUDPstatusIndicator = new System.Windows.Forms.ProgressBar();
            this.senseUDPpowerButton = new System.Windows.Forms.Button();
            this.senseUDPportBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.YZchart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label4 = new System.Windows.Forms.Label();
            this.XZchart = new System.Windows.Forms.DataVisualization.Charting.Chart();
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
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YZchart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XZchart)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.senseUDPstatusIndicator);
            this.groupBox2.Controls.Add(this.senseUDPpowerButton);
            this.groupBox2.Controls.Add(this.senseUDPportBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.YZchart);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.XZchart);
            this.groupBox2.Controls.Add(this.senseUDPmessageBox);
            this.groupBox2.Location = new System.Drawing.Point(16, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(504, 481);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "UDP Sense";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(307, 447);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Status";
            // 
            // senseUDPstatusIndicator
            // 
            this.senseUDPstatusIndicator.Location = new System.Drawing.Point(365, 441);
            this.senseUDPstatusIndicator.Margin = new System.Windows.Forms.Padding(4);
            this.senseUDPstatusIndicator.Name = "senseUDPstatusIndicator";
            this.senseUDPstatusIndicator.Size = new System.Drawing.Size(133, 28);
            this.senseUDPstatusIndicator.TabIndex = 15;
            // 
            // senseUDPpowerButton
            // 
            this.senseUDPpowerButton.Location = new System.Drawing.Point(10, 441);
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
            this.senseUDPportBox.Location = new System.Drawing.Point(10, 53);
            this.senseUDPportBox.Margin = new System.Windows.Forms.Padding(4);
            this.senseUDPportBox.Name = "senseUDPportBox";
            this.senseUDPportBox.Size = new System.Drawing.Size(132, 22);
            this.senseUDPportBox.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Port";
            // 
            // YZchart
            // 
            chartArea1.Name = "ChartArea1";
            this.YZchart.ChartAreas.Add(chartArea1);
            this.YZchart.Location = new System.Drawing.Point(10, 297);
            this.YZchart.Margin = new System.Windows.Forms.Padding(4);
            this.YZchart.Name = "YZchart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "Series1";
            this.YZchart.Series.Add(series1);
            this.YZchart.Size = new System.Drawing.Size(488, 137);
            this.YZchart.TabIndex = 11;
            this.YZchart.Text = "chart2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 84);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Received data";
            // 
            // XZchart
            // 
            chartArea2.Name = "ChartArea1";
            this.XZchart.ChartAreas.Add(chartArea2);
            this.XZchart.Location = new System.Drawing.Point(10, 154);
            this.XZchart.Margin = new System.Windows.Forms.Padding(4);
            this.XZchart.Name = "XZchart";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Name = "Series1";
            this.XZchart.Series.Add(series2);
            this.XZchart.Size = new System.Drawing.Size(488, 137);
            this.XZchart.TabIndex = 10;
            this.XZchart.Text = "chart1";
            // 
            // senseUDPmessageBox
            // 
            this.senseUDPmessageBox.Enabled = false;
            this.senseUDPmessageBox.Location = new System.Drawing.Point(10, 104);
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
            this.groupBox1.Location = new System.Drawing.Point(547, 15);
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
            this.serverAddressBox.Location = new System.Drawing.Point(845, 504);
            this.serverAddressBox.Margin = new System.Windows.Forms.Padding(4);
            this.serverAddressBox.Name = "serverAddressBox";
            this.serverAddressBox.Size = new System.Drawing.Size(209, 22);
            this.serverAddressBox.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(761, 507);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "IP Address";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 507);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 17);
            this.label8.TabIndex = 12;
            this.label8.Text = "Debug";
            // 
            // debugTextBox
            // 
            this.debugTextBox.Enabled = false;
            this.debugTextBox.Location = new System.Drawing.Point(80, 507);
            this.debugTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.debugTextBox.Name = "debugTextBox";
            this.debugTextBox.Size = new System.Drawing.Size(209, 22);
            this.debugTextBox.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 538);
            this.Controls.Add(this.debugTextBox);
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
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YZchart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XZchart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox senseUDPmessageBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart XZchart;
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
        private System.Windows.Forms.DataVisualization.Charting.Chart YZchart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar senseUDPstatusIndicator;
        private System.Windows.Forms.Button senseUDPpowerButton;
        private System.Windows.Forms.TextBox senseUDPportBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox debugTextBox;
    }
}

