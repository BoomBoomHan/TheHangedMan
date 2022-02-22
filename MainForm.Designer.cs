using System;

partial class MainForm
{
	/// <summary>
	/// 必需的设计器变量。
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// 清理所有正在使用的资源。
	/// </summary>
	/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows 窗体设计器生成的代码

	/// <summary>
	/// 设计器支持所需的方法 - 不要修改
	/// 使用代码编辑器修改此方法的内容。
	/// </summary>
	private void InitializeComponent()
	{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.label1 = new System.Windows.Forms.Label();
			this.AddGameButton = new System.Windows.Forms.Button();
			this.GameName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.Path = new System.Windows.Forms.TextBox();
			this.SelectGame = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.GamesListView = new System.Windows.Forms.ListView();
			this.LV_Order = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.LV_GameName0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.LV_GamePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.RecoverButton = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(150, 613);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(1000, 39);
			this.label1.TabIndex = 0;
			this.label1.Text = "倒吊人，您戒除拖延症的好帮手。";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// AddGameButton
			// 
			this.AddGameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.AddGameButton.Font = new System.Drawing.Font("微软雅黑 Light", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.AddGameButton.Location = new System.Drawing.Point(16, 285);
			this.AddGameButton.Name = "AddGameButton";
			this.AddGameButton.Size = new System.Drawing.Size(300, 100);
			this.AddGameButton.TabIndex = 1;
			this.AddGameButton.Text = "添加游戏";
			this.AddGameButton.UseVisualStyleBackColor = true;
			this.AddGameButton.Click += new System.EventHandler(this.OnTestClick);
			// 
			// GameName
			// 
			this.GameName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.GameName.Location = new System.Drawing.Point(103, 126);
			this.GameName.Name = "GameName";
			this.GameName.Size = new System.Drawing.Size(189, 27);
			this.GameName.TabIndex = 2;
			this.GameName.Text = "新游戏";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(1707, 112);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(0, 15);
			this.label2.TabIndex = 3;
			this.label2.Click += new System.EventHandler(this.label2_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(12, 126);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "游戏名";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.Location = new System.Drawing.Point(12, 61);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(78, 23);
			this.label4.TabIndex = 6;
			this.label4.Text = "游戏路径";
			// 
			// Path
			// 
			this.Path.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Path.Location = new System.Drawing.Point(103, 60);
			this.Path.Name = "Path";
			this.Path.ReadOnly = true;
			this.Path.Size = new System.Drawing.Size(694, 27);
			this.Path.TabIndex = 5;
			this.Path.TextChanged += new System.EventHandler(this.Path_TextChanged);
			// 
			// SelectGame
			// 
			this.SelectGame.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.SelectGame.Location = new System.Drawing.Point(825, 56);
			this.SelectGame.Name = "SelectGame";
			this.SelectGame.Size = new System.Drawing.Size(66, 35);
			this.SelectGame.TabIndex = 7;
			this.SelectGame.Text = "浏览";
			this.SelectGame.UseVisualStyleBackColor = true;
			this.SelectGame.Click += new System.EventHandler(this.SelectGame_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Monaco", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(2, 9);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(549, 19);
			this.label5.TabIndex = 8;
			this.label5.Text = "@2022 Rebirth Studio X Chuangming Studio Version Alpha 0.1.1";
			// 
			// GamesListView
			// 
			this.GamesListView.AutoArrange = false;
			this.GamesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LV_Order,
            this.LV_GameName0,
            this.LV_GamePath});
			this.GamesListView.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.GamesListView.HideSelection = false;
			this.GamesListView.Location = new System.Drawing.Point(519, 181);
			this.GamesListView.Name = "GamesListView";
			this.GamesListView.Size = new System.Drawing.Size(751, 352);
			this.GamesListView.TabIndex = 9;
			this.GamesListView.UseCompatibleStateImageBehavior = false;
			this.GamesListView.View = System.Windows.Forms.View.Details;
			this.GamesListView.SelectedIndexChanged += new System.EventHandler(this.GamesListView_SelectedIndexChanged);
			// 
			// LV_Order
			// 
			this.LV_Order.Text = "序号";
			this.LV_Order.Width = 45;
			// 
			// LV_GameName0
			// 
			this.LV_GameName0.Text = "游戏名";
			this.LV_GameName0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.LV_GameName0.Width = 100;
			// 
			// LV_GamePath
			// 
			this.LV_GamePath.Text = "游戏路径";
			this.LV_GamePath.Width = 600;
			// 
			// RecoverButton
			// 
			this.RecoverButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.RecoverButton.Enabled = false;
			this.RecoverButton.Font = new System.Drawing.Font("微软雅黑 Light", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.RecoverButton.Location = new System.Drawing.Point(16, 434);
			this.RecoverButton.Name = "RecoverButton";
			this.RecoverButton.Size = new System.Drawing.Size(300, 100);
			this.RecoverButton.TabIndex = 10;
			this.RecoverButton.Text = "恢复游戏";
			this.RecoverButton.UseVisualStyleBackColor = true;
			this.RecoverButton.Click += new System.EventHandler(this.RecoverButton_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label6.Location = new System.Drawing.Point(516, 145);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(632, 27);
			this.label6.TabIndex = 11;
			this.label6.Text = "所有被你添加的游戏都会显示在这里，点击左边的序号可以恢复游戏。";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1282, 673);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.RecoverButton);
			this.Controls.Add(this.GamesListView);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.SelectGame);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.Path);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.GameName);
			this.Controls.Add(this.AddGameButton);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "The Hanged Man";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Button AddGameButton;
	private System.Windows.Forms.TextBox GameName;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.Label label4;
	private System.Windows.Forms.TextBox Path;
	private System.Windows.Forms.Button SelectGame;
	private System.Windows.Forms.Label label5;
	private System.Windows.Forms.ListView GamesListView;
	private System.Windows.Forms.ColumnHeader LV_Order;
	private System.Windows.Forms.ColumnHeader LV_GameName0;
	private System.Windows.Forms.ColumnHeader LV_GamePath;
	private System.Windows.Forms.Button RecoverButton;
	private System.Windows.Forms.Label label6;
}