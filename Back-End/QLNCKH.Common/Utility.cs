using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QLNCKH.Common
{
    public class Utility
    {

        public static string ReadTemplateSendEmailExpire()
        {
            string path = @"C:\Users\Admin\Desktop\UET.QLNCKH.VTT\Project\VuThanhThien-KLTN-QLNCKH\Back-End\QLNCKH.API\Template\template-email-expire.html";
            var sText = File.ReadAllText(path);

            return sText;
        }

        public static string FormSendEmailExpire(string file, string[] entity)
        {
            var formated = string.Format(file, entity);

            return formated;
        }
    }
}
