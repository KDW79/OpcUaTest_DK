namespace OpcUaTest_DK
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnConnect = new Button();
            btnRead = new Button();
            label1 = new Label();
            lblReadingValue = new Label();
            txtIpPort = new TextBox();
            txtNodeId = new TextBox();
            btnDisconnect = new Button();
            treeViewNS = new TreeView();
            lblReqHandle = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(610, 40);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(115, 23);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnRead
            // 
            btnRead.Location = new Point(610, 77);
            btnRead.Name = "btnRead";
            btnRead.Size = new Size(115, 23);
            btnRead.TabIndex = 1;
            btnRead.Text = "Read";
            btnRead.UseVisualStyleBackColor = true;
            btnRead.Click += btnRead_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(39, 117);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 2;
            label1.Text = "Result : ";
            // 
            // lblReadingValue
            // 
            lblReadingValue.AutoSize = true;
            lblReadingValue.Location = new Point(84, 117);
            lblReadingValue.Name = "lblReadingValue";
            lblReadingValue.Size = new Size(35, 15);
            lblReadingValue.TabIndex = 3;
            lblReadingValue.Text = "value";
            lblReadingValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtIpPort
            // 
            txtIpPort.Location = new Point(38, 40);
            txtIpPort.Name = "txtIpPort";
            txtIpPort.Size = new Size(566, 23);
            txtIpPort.TabIndex = 4;
            txtIpPort.Text = "192.168.0.1:4840";
            // 
            // txtNodeId
            // 
            txtNodeId.Location = new Point(38, 77);
            txtNodeId.Name = "txtNodeId";
            txtNodeId.Size = new Size(566, 23);
            txtNodeId.TabIndex = 5;
            txtNodeId.Text = "ns=3;s=\"TEST_DB_OPC_UA\".\"Value_0to10\"";
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new Point(610, 113);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(115, 23);
            btnDisconnect.TabIndex = 6;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // treeViewNS
            // 
            treeViewNS.Location = new Point(40, 165);
            treeViewNS.Name = "treeViewNS";
            treeViewNS.Size = new Size(685, 333);
            treeViewNS.TabIndex = 7;
            // 
            // lblReqHandle
            // 
            lblReqHandle.AutoSize = true;
            lblReqHandle.Location = new Point(332, 117);
            lblReqHandle.Name = "lblReqHandle";
            lblReqHandle.Size = new Size(35, 15);
            lblReqHandle.TabIndex = 9;
            lblReqHandle.Text = "value";
            lblReqHandle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(232, 117);
            label3.Name = "label3";
            label3.Size = new Size(94, 15);
            label3.TabIndex = 8;
            label3.Text = "RequestHandle :";
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(746, 510);
            Controls.Add(lblReqHandle);
            Controls.Add(label3);
            Controls.Add(treeViewNS);
            Controls.Add(btnDisconnect);
            Controls.Add(txtNodeId);
            Controls.Add(txtIpPort);
            Controls.Add(lblReadingValue);
            Controls.Add(label1);
            Controls.Add(btnRead);
            Controls.Add(btnConnect);
            Name = "frmMain";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnConnect;
        private Button btnRead;
        private Label label1;
        private Label lblReadingValue;
        private TextBox txtIpPort;
        private TextBox txtNodeId;
        private Button btnDisconnect;
        private TreeView treeViewNS;
        private Label lblReqHandle;
        private Label label3;
    }
}