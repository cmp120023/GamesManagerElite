namespace GamesManagerElite
{
    partial class Form_GameManagerElite
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label_userLogin = new Label();
            Label_password = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label3 = new Label();
            comboBox1 = new ComboBox();
            label_League = new Label();
            SuspendLayout();
            // 
            // label_userLogin
            // 
            label_userLogin.AutoSize = true;
            label_userLogin.ForeColor = SystemColors.Control;
            label_userLogin.Location = new Point(252, 173);
            label_userLogin.Name = "label_userLogin";
            label_userLogin.Size = new Size(69, 15);
            label_userLogin.TabIndex = 0;
            label_userLogin.Text = "User Login: ";
            // 
            // Label_password
            // 
            Label_password.AutoSize = true;
            Label_password.ForeColor = SystemColors.ControlLight;
            Label_password.Location = new Point(258, 216);
            Label_password.Name = "Label_password";
            Label_password.Size = new Size(63, 15);
            Label_password.TabIndex = 1;
            Label_password.Text = "Password: ";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(327, 165);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(170, 23);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(327, 208);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(170, 23);
            textBox2.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Rage Italic", 34F, FontStyle.Italic | FontStyle.Underline);
            label3.ForeColor = Color.YellowGreen;
            label3.Location = new Point(143, 88);
            label3.Name = "label3";
            label3.Size = new Size(372, 58);
            label3.TabIndex = 4;
            label3.Text = "Game Manager Elite";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "APA", "BCA", "UPA (Coming Soon!)", "TAP (Coming Soon!)" });
            comboBox1.Location = new Point(327, 250);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(170, 23);
            comboBox1.TabIndex = 5;
            // 
            // label_League
            // 
            label_League.AutoSize = true;
            label_League.ForeColor = SystemColors.ControlLight;
            label_League.Location = new Point(270, 258);
            label_League.Name = "label_League";
            label_League.Size = new Size(51, 15);
            label_League.TabIndex = 6;
            label_League.Text = "League: ";
            // 
            // Form_GameManagerElite
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(800, 450);
            Controls.Add(label_League);
            Controls.Add(comboBox1);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(Label_password);
            Controls.Add(label_userLogin);
            Name = "Form_GameManagerElite";
            Text = "Game Manager Elite";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_userLogin;
        private Label Label_password;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label3;
        private ComboBox comboBox1;
        private Label label_League;
    }
}
