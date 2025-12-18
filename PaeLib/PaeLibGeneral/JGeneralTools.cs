using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace PaeLibGeneral
{
    /// <summary>
    /// 常用工具函式 (全域函式)
    /// </summary>
    public class JGeneralTools
    {
        private static double dw = 1920;    //設計環境的螢幕解析度 W
        private static double dh = 1080;    //設計環境的螢幕解析度 H
        private static double sw = Convert.ToDouble(SystemInformation.PrimaryMonitorSize.Width);
        private static double sh = Convert.ToDouble(SystemInformation.PrimaryMonitorSize.Height);
        private static double sizeX = sw / dw;
        private static double sizeY = sh / dh;

        /// <summary>
        /// 取得目前根目錄的完整路徑
        /// </summary>
        /// <returns>目前根目錄的完整路徑</returns>
        public static string GetCurrentDir()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// 深複製
        /// </summary>
        /// <typeparam name="T">要複製的類別</typeparam>
        /// <param name="RealObject">要複製的物件</param>
        /// <returns>回傳 RealObject 的副本</returns>
        public static T DeepCopy<T>(T RealObject)
        {
            using (Stream stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, RealObject);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)serializer.Deserialize(stream);
            }
        }

        /// <summary>
        /// 取得指定 Control 元件 form 內的所有 Control 元件
        /// </summary>
        /// <param name="form">指定的 Control 元件</param>
        /// <returns>回傳 Control 元件 form 內的所有 Control 元件</returns>
        public static IEnumerable<Control> GetControls(Control form)
        {
            foreach (Control childControl in form.Controls)
            {   // Recurse child controls.
                foreach (Control grandChild in GetControls(childControl))
                {
                    yield return grandChild;
                }
                yield return childControl;
            }
        }

        /// <summary>
        /// 依據目前螢幕解析度，自動調整 Control 元件上的字型大小
        /// </summary>
        /// <param name="form"></param>
        public static void chResolution(Control form)
        {
            foreach (Control p in GetControls(form))
            {
                p.Top = Convert.ToInt32(Convert.ToDouble(p.Top) * sizeX);
                p.Left = Convert.ToInt32(Convert.ToDouble(p.Left) * sizeY);
                p.Width = Convert.ToInt32(Convert.ToDouble(p.Width) * sizeX);
                p.Height = Convert.ToInt32(Convert.ToDouble(p.Height) * sizeY);
                if (p is Button || p is Label || p is TextBox || p is ComboBox)
                {
                    p.Font = new Font(p.Font.FontFamily, p.Font.SizeInPoints * Convert.ToSingle(sizeX) * Convert.ToSingle(sizeY));
                }
            }
        }

        /// <summary>
        /// 使用 XPathDocument，可直接傳入計算式後計算出結果
        /// </summary>
        /// <param name="sExpression">數值運算式</param>
        /// <returns>數值運算結果</returns>
        /// <example>
        /// <code>
        /// double ret = JGeneralTools.EvaluateUsingXPathDocument("100+35-6");  //ret = 129
        /// </code>
        /// </example>
        public static double EvaluateUsingXPathDocument(string sExpression)
        {
            try
            {
                var xsltExpression = string.Format("number({0})",
                        new Regex(@"([\+\-\*])").Replace(sExpression, " ${1} ")
                                                .Replace("/", " div ")
                                                .Replace("%", " mod "));

                return (double)new XPathDocument(new StringReader("<r/>")).CreateNavigator().Evaluate(xsltExpression);
            }
            catch (Exception e)
            {
                //...
            }
            return 0;
        }
    }
}