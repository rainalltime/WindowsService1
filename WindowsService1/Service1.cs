﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Timers;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        private Timer time = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
#if DEBUG
            if (!Debugger.IsAttached)
                Debugger.Launch();
            Debugger.Break();
#endif
            WriteLog("服务启动，时间：" + DateTime.Now.ToString("HH:mm:ss") + "\r\n");
            time.Elapsed += new ElapsedEventHandler(MethodEvent);
            time.Interval = 60 * 1000;//时间间隔为2秒钟
            time.Start();
        }

        protected override void OnStop()
        {
#if DEBUG
            if (!Debugger.IsAttached)
                Debugger.Launch();
            Debugger.Break();
#endif
            WriteLog("服务停止，时间：" + DateTime.Now.ToString("HH:mm:ss") + "\r\n");
        }

        protected override void OnPause()
        {
#if DEBUG
            if (!Debugger.IsAttached)
                Debugger.Launch();
            Debugger.Break();
#endif
            WriteLog("服务暂停，时间：" + DateTime.Now.ToString("HH:mm:ss") + "\r\n");
            base.OnPause();
        }

        protected override void OnContinue()
        {
#if DEBUG
            if (!Debugger.IsAttached)
                Debugger.Launch();
            Debugger.Break();
#endif
            WriteLog("服务恢复，时间：" + DateTime.Now.ToString("HH:mm:ss") + "\r\n");
            base.OnContinue();
        }

        protected override void OnShutdown()
        {
            WriteLog("计算机关闭，时间：" + DateTime.Now.ToString("HH:mm:ss") + "\r\n");
            base.OnShutdown();
        }

        private void MethodEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            time.Enabled = false;
            string result = string.Empty;
            try
            {
                //.........
                result = "执行成功，时间：" + DateTime.Now.ToString("HH:mm:ss") + "\r\n";
            }
            catch (Exception ex)
            {
                result = "执行失败，原因：" + ex.Message + "\r\n";
            }
            finally
            {
                WriteLog(result);
                time.Enabled = true;
            }
        }
        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="logInfo"></param>
        private void WriteLog(string logInfo)
        {
            try
            {
                string logDirectory = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }
                string filePath = logDirectory + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                File.AppendAllText(filePath, logInfo);
            }
            catch
            {

            }
        }
    }
}
