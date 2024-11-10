using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Calculator
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string Calculate(string process)
        {
            char[] bracketsChars = { '(', ')' };
            string[] bracketsSpit = process.Split(bracketsChars);
            for (int i = 0; i < bracketsSpit.Count(); i++)
            {
                if (bracketsSpit[i] == "")
                    continue;
                bracketsSpit[i] = AddSubProcess(bracketsSpit[i]).ToString();
            }

            process = "";
            foreach (var item in bracketsSpit)
            {
                process += item;
            }

            string addResult = AddSubProcess(process);
            //نمایش نتیجه در لیبل 3
            return addResult;
        }

        private static string AddSubProcess(string process)
        {
            if (process.StartsWith("-") || process.StartsWith("+") ||
                process.StartsWith("/") || process.StartsWith("*") ||
                process.EndsWith("-") || process.EndsWith("+") ||
                process.EndsWith("/") || process.EndsWith("*"))
            {
                return process;
            }

            char[] addChars = { '+' };
            //جدا کردن متن نسبت به مثبت
            string[] adds = process.Split(addChars);
            double addResult = 0;
            //بررسی اعضای مولتی اددس
            foreach (var add in adds)
            {
                //جدا کردن متن نسبت به منفی
                List<string> subs = add.Split('-').ToList();
                for (int i = 0; i < subs.Count; i++)
                {
                    //بررسی ضرب و تقسیم هر یک از اععضای سابس
                    subs[i] = multiDivitionProcess(subs[i]).ToString();
                }
                double subResult = 0;
                if (subs.Count() > 0)
                {
                    //دادن مقدار عضو اول به ساب ریسات
                    //و حذف آن از لیست بخاطر اینکه مقدار اول از صفر کاسته نشود
                    subResult = Convert.ToDouble(subs[0]);
                    subs.Remove(subs[0]);
                    foreach (var sub in subs)
                    {
                        //تفریق از دیویشن ریسالت
                        subResult -= Convert.ToDouble(sub);
                    }
                }
                else
                {
                    //بررسی ضرب و تقسیم عضو
                    double multiDivitionResult2 = multiDivitionProcess(add);

                    subResult = Convert.ToDouble(multiDivitionResult2);
                }
                //جمع کردن تمامی نتیجه ها
                addResult += Convert.ToDouble(subResult);
            }

            return addResult.ToString();
        }

        private static double multiDivitionProcess(string process)
        {
            char[] multiDivitionChars = { '*' };
            //جدا کردن متن نسبت به ضرب
            string[] multiDivitions = process.Split(multiDivitionChars);
            double multiDivitionResult = 1;
            //بررسی اعضای مولتی دیویشنس
            foreach (var multiDivition in multiDivitions)
            {
                //جدا کردن متن نسبت به تقسیم
                List<string> divitions = multiDivition.Split('/').ToList();
                for (int i = 0; i < divitions.Count; i++)
                {
                    divitions[i] = BaseRootProcess(divitions[i]).ToString();
                }
                double divitionResult = 1;
                //بررسی وجود نتیحه در دیویشن
                if (divitions.Count() > 0)
                {
                    //دادن مقدار عضو اول به دیویشن ریسات
                    //و حذف آن از لیست بخاطر اینکه مقدار اول به یک تقسیم نشود نشود
                    divitionResult = Convert.ToDouble(divitions[0]);
                    divitions.Remove(divitions[0]);
                    foreach (var divition in divitions)
                    {
                        //تقسیم اعضا به دیویشن ریسالت
                        divitionResult /= Convert.ToDouble(divition);
                    }
                }
                else
                {
                    double divitionResult2 = BaseRootProcess(multiDivition);

                    divitionResult = Convert.ToDouble(divitionResult2);
                }
                //ضرب نتایج
                multiDivitionResult *= Convert.ToInt32(divitionResult);
            }
            //پس دادن مقدار مولتی دیویشن
            return multiDivitionResult;
        }
        private static double BaseRootProcess(string process)
        {
            char[] baseChars = { '^' };
            List<string> baseSplit = process.Split(baseChars).ToList();
            for (int i = 0; i < baseSplit.Count(); i++)
            {
                if (baseSplit[i].Contains('√'))
                {
                    baseSplit[i] = Math.Sqrt(Convert.ToDouble(baseSplit[i].Substring(1))).ToString();
                }
            }
            baseSplit.Reverse();
            double rootResult = Convert.ToDouble(baseSplit[0]);
            baseSplit.Remove(baseSplit[0]);
            for (int i = 0; i < baseSplit.Count(); i++)
            {
                rootResult = Math.Pow(Convert.ToDouble(baseSplit[i]), rootResult);
            }
            return rootResult;
        }
    }
}
