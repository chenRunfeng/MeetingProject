﻿using Meeting.BLL;
using Meeting.Entity;
using Meeting.Interface;
using Meeting.Pc.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Meeting.Pc.View
{
    public partial class FrmRecord : Form
    {
        private string _meetingId = "";
        IMeetingInterface imeeting = new MeetingService();

        public FrmRecord(string meetingId)
        {
            InitializeComponent();
            _meetingId = meetingId;
            Initial(meetingId);
        }

        private void pxHome_Click(object sender, EventArgs e)
        {
            //FrmMain main = new FrmMain();
            //main.Show();
            //Hide();
        }

        private void peRerurn_Click(object sender, EventArgs e)
        {
            FrmMeetingInfo info = new FrmMeetingInfo(_meetingId);
            info.Show();
            Helper.KillProcess();
            Hide();
        }



        private void FrmRecord_Load(object sender, EventArgs e)
        {
          

            //先下载word
            string path = System.Environment.CurrentDirectory;
            string url = Helper.DownloadFile(_directory,path);

            Helper.KillProcess();
            var word = new CtrlWinWord()
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                Location = new System.Drawing.Point(0, 0),
                Name = "文档",
                TabIndex = 1
            };
            //加载word的完整路径  修改此处
            word.LoadDocument(url);
            //word.LoadDocument(phath + @"\1.docx");
            word.Show();
            panelEx3.Controls.Add(word);
        }

        private void pxSave_Click(object sender, EventArgs e)
        {
            //会议记录保存上传


        }


        private string _directory = "";   //这次会议  上传材料的根目录
        private void Initial(string meetingId)
        {
            mMeeting model = imeeting.GetMeetingModel(Convert.ToInt32(meetingId));
            label4.Text = model.MeetingName;
            label5.Text = model.StartDate;
            label6.Text = model.EendDate;
            label11.Text = model.MeetingAddress;
            label14.Text = model.MeetingHost;
            label22.Text = model.SecretaryName;
            label12.Text = model.PeopleName;
            _directory = model.Directory;
        }
    }
}
