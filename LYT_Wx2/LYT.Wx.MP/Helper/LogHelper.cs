using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYT.Wx.MP.Helper
{
   public class LogHelper
    {
        public static void ExLog(Exception ex,string action,string content="")
        {
            try
            {
                string fname = DateTime.Now.ToString("yyyyMM") + ".log";
                using (TextWriter tw = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/logfiles/" + fname), true))
                {
                    tw.WriteLine("--------------------------------" + DateTime.Now);
                    tw.WriteLine(action);
                    tw.WriteLine("------");
                    tw.WriteLine(content);
                    tw.WriteLine("ExMessage:" + ex.Message);
                    tw.Flush();
                    tw.Close();
                }
            }
            catch (Exception)
            {

               
            }
           
        }
        public static void Log(string action, string content = "")
        {
            try
            {
                string fname = DateTime.Now.ToString("yyyyMM") + ".log";
                using (TextWriter tw = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/logfiles/" + fname), true))
                {
                    tw.WriteLine("--------------------------------" + DateTime.Now);
                    tw.WriteLine(action);
                    tw.WriteLine("------");
                    tw.WriteLine(content);
                    tw.Flush();
                    tw.Close();
                }
            }
            catch (Exception)
            {

               
            }
           
        }
    }
}
