﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Randy.GraphicsLibrary;
using System.Runtime.InteropServices;
using System.IO;

namespace PetvetPOS_Inventory_System
{
    public partial class Titlebar : MyUserControl
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]

        public static extern bool ReleaseCapture();
        private Menu defaultMenu = Menu.Login;
        private Bitmap defaultIcon = Properties.Resources.shopIcon;
        private string title;
        private Bitmap imageIcon;
 
        public Titlebar()
        {
            InitializeComponent();
        }

        public Titlebar(Panel panel, MasterController masterController):base(masterController, panel, true)
        {
            InitializeComponent();
            this.masterController = masterController;
            this.masterController.setTitleBar = this;
            this.masterController.ContentChange += new EventHandler<ContentArgs>(masterController_ContentChange);

            this.masterController.EmployeeLogin += masterController_EmployeeLogin;
            this.masterController.EmployeeLogout += masterController_EmployeeLogout;
            masterController.getFrmMain.Resize += getFrmMain_Resize;
           
            TabStop = false;
            whenNotLogin();
        }

        void getFrmMain_Resize(object sender, EventArgs e)
        {
            panelTitle.Invalidate();
        }

        void masterController_EmployeeLogout(object sender, EventArgs e)
        {
            whenNotLogin();
        }

        void masterController_EmployeeLogin(object sender, EmployeeArgs e)
        {
            whenLogin();
        }

        void whenNotLogin()
        {
            button1.Visible = false;
            button3.Visible = false;
            setImage = defaultIcon;
        }

        void whenLogin()
        {
            button1.Visible = true;
            button3.Visible = true;
        }

        void masterController_ContentChange(object sender, ContentArgs e)
        {
            if (e.currentContent is IContentPage)
            {
              IContentPage foo =  e.currentContent as IContentPage;
              updateTitle(foo.accessMenuName, foo.accessImage);
            }
        }

        public void setDefault(){

            updateTitle(defaultMenu, defaultIcon);
        }

        public string setTitle 
        {
            get
            {
                return title;
            }
            set
            {
                title = value.ToUpper();
                panelTitle.Invalidate();
            }
        }

        public Bitmap setImage
        {
            set
            {
                imageIcon = Renderer.resizeImage(value, panelImage.Width, panelImage.Height);
                panelImage.Invalidate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            masterController.closeForm();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            masterController.mimimizeForm();
        }

        public void updateTitle(Menu menuname, Bitmap icon)
        {
            string titlename = "";

            switch (menuname){
                case Menu.POS:
                    titlename = "Point of Sale";
                    break;
                case Menu.AccountSettings:
                    titlename = "Account Settings";
                    break;
                case Menu.AuditTrail:
                    titlename = "Audit Trail";
                    break;
                case Menu.LoginTrail:
                    titlename = "Login Trail";
                    break;
                case Menu.Login:
                    titlename = "EXOGEN";
                    break;
                case Menu.Home:
                    titlename = "Dashboard";
                    break;
                case Menu.Sales:
                    titlename = "Sales Report";
                    break;
                case Menu.User:
                    titlename = "User Management";
                    break;
                case Menu.CustomerInformation:
                    titlename = "Customer Information";
                    break;
                default:
                    titlename = menuname.ToString();
                    break;
            }

            setTitle = titlename;
            if (icon != null)
                setImage = icon;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            masterController.toogleMaximize();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            masterController.mimimizeForm();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            masterController.toogleMaximize();
            panelTitle.Invalidate();
            panelImage.Invalidate();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            masterController.closeForm();
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
     //       button3.BackgroundImage = Properties.Resources.max_;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
    //        button3.BackgroundImage = Properties.Resources.max_ini_;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (Font arialRoundedMt = new Font("Arial Rounded MT", 48, FontStyle.Bold)){
                SizeF stringSize = g.MeasureString(title, arialRoundedMt);
                g.DrawString(title, arialRoundedMt, Brushes.White, new PointF((panelTitle.Width - stringSize.Width)/2, 5));
            }
        }

        private void panelImage_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImageUnscaled(imageIcon, new Point(0, 0));
        }

   
        private void mouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left){
                masterController.moveForm();
            }
        }

        private void panelTitle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            masterController.toogleMaximize();
        }

    }
}