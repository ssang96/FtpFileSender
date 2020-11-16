namespace FtpFileSender
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlLogin = new DevExpress.XtraEditors.PanelControl();
            this.pnlSiteManage = new DevExpress.XtraEditors.PanelControl();
            this.pnlCurrentStatus = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSiteManage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCurrentStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLogin
            // 
            this.pnlLogin.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogin.Location = new System.Drawing.Point(0, 0);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(884, 220);
            this.pnlLogin.TabIndex = 0;
            // 
            // pnlSiteManage
            // 
            this.pnlSiteManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSiteManage.Location = new System.Drawing.Point(0, 220);
            this.pnlSiteManage.Name = "pnlSiteManage";
            this.pnlSiteManage.Size = new System.Drawing.Size(884, 181);
            this.pnlSiteManage.TabIndex = 1;
            // 
            // pnlCurrentStatus
            // 
            this.pnlCurrentStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCurrentStatus.Location = new System.Drawing.Point(0, 401);
            this.pnlCurrentStatus.Name = "pnlCurrentStatus";
            this.pnlCurrentStatus.Size = new System.Drawing.Size(884, 320);
            this.pnlCurrentStatus.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(884, 721);
            this.Controls.Add(this.pnlCurrentStatus);
            this.Controls.Add(this.pnlSiteManage);
            this.Controls.Add(this.pnlLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FTP FILE SENDER";
            ((System.ComponentModel.ISupportInitialize)(this.pnlLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSiteManage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCurrentStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlLogin;
        private DevExpress.XtraEditors.PanelControl pnlSiteManage;
        private DevExpress.XtraEditors.PanelControl pnlCurrentStatus;
    }
}

