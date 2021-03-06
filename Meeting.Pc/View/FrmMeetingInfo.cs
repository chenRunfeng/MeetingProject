﻿using Meeting.BLL;
using Meeting.Common;
using Meeting.Entity;
using Meeting.Interface;
using Meeting.Pc.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Meeting.Pc.View
{
    public partial class FrmMeetingInfo : Form
    {

        IMeetingInterface imeeting = new MeetingService();
        private string _meetingId = "";
        public FrmMeetingInfo(string meetingId)
        {
            InitializeComponent();
            _meetingId = meetingId;
            Initial(meetingId);
        }

        private void pxHome_Click(object sender, EventArgs e)
        {
            FrmMain main = new FrmMain(Helper.NickName);
            main.Show();
            Hide();
        }

        #region  窗体随鼠标移动
        private int _currentX;
        private int _currentY;
        private bool _canMove = false;
        private void FrmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_canMove)
            {
                Location = new Point(MousePosition.X - _currentX, MousePosition.Y - _currentY);
            }
        }

        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            _currentX = e.X;
            _currentY = e.Y;
            _canMove = true;
        }

        private void FrmMain_MouseUp(object sender, MouseEventArgs e)
        {
            _canMove = false;
        }
        #endregion

        private void panelEx7_Click(object sender, EventArgs e)
        {
            //查看材料
            FrmResources resources = new FrmResources(_meetingId);
            resources.Show();
            Hide();
        }

        private void panelEx3_Click(object sender, EventArgs e)
        {
            //会议记录
            string url = Helper.DownloadFile(_meetingId, Consts.PcUrlPath,_meetingId+".docx");

            FrmSign sign = new FrmSign(_meetingId,url,2);
            sign.Show();
            Hide();

        }

        private int _issueid = 0;
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
            label24.Text = model.IssueList.IssueName;
            label25.Text = model.IssueList.RepostUser;
            label26.Text = model.IssueList.DepartName;
            _directory = model.Directory;

            _issueid = model.IssueList.Id;
        }
    }
}
