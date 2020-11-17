namespace FtpFileSender.VIEW
{
    partial class LoggerUserControl
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvCurrentStatus = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvCurrentStatus
            // 
            this.lvCurrentStatus.BackColor = System.Drawing.SystemColors.Info;
            this.lvCurrentStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvCurrentStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCurrentStatus.GridLines = true;
            this.lvCurrentStatus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvCurrentStatus.HideSelection = false;
            this.lvCurrentStatus.Location = new System.Drawing.Point(0, 0);
            this.lvCurrentStatus.Name = "lvCurrentStatus";
            this.lvCurrentStatus.Size = new System.Drawing.Size(884, 320);
            this.lvCurrentStatus.TabIndex = 0;
            this.lvCurrentStatus.UseCompatibleStateImageBehavior = false;
            this.lvCurrentStatus.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date";
            this.columnHeader1.Width = 121;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "이벤트";
            this.columnHeader2.Width = 734;
            // 
            // LoggerUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvCurrentStatus);
            this.Name = "LoggerUserControl";
            this.Size = new System.Drawing.Size(884, 320);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvCurrentStatus;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}
