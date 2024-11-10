using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Calculator
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        //[System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json, UseHttpGet = true)]
        public static string Calculate(string process)
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
        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    string process = TextBox2.Text;

        //    char[] bracketsChars = { '(', ')' };
        //    string[] bracketsSpit = process.Split(bracketsChars);
        //    for (int i = 0; i < bracketsSpit.Count(); i++)
        //    {
        //        if (bracketsSpit[i] == "")
        //            continue;
        //        bracketsSpit[i] = AddSubProcess(bracketsSpit[i]).ToString();
        //    }

        //    process = "";
        //    foreach (var item in bracketsSpit)
        //    {
        //        process += item;
        //    }

        //    string addResult = AddSubProcess(process);
        //    //نمایش نتیجه در لیبل 3
        //    Label2.Text = addResult;
        //}

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

        //protected void number1Button_Click(object sender, EventArgs e)
        //{
        //    //نوشتن عدد مربوط در تکست باکس دو
        //    TextBox2.Text += number1Button.Text.Trim();
        //}

        //protected void number2Button_Click(object sender, EventArgs e)
        //{
        //    TextBox2.Text += number2Button.Text.Trim();
        //}

        //protected void number3Button_Click(object sender, EventArgs e)
        //{
        //    TextBox2.Text += number3Button.Text.Trim();
        //}

        //protected void addButton_Click(object sender, EventArgs e)
        //{
        //    TextBox2.Text += addButton.Text.Trim();
        //}

        //protected void sub_Click(object sender, EventArgs e)
        //{
        //    TextBox2.Text += sub.Text.Trim();
        //}

        //protected void number4Button_Click(object sender, EventArgs e)
        //{
        //    TextBox2.Text += number4Button.Text.Trim();
        //}

        //protected void number5Button_Click(object sender, EventArgs e)
        //{
        //    TextBox2.Text += number5Button.Text.Trim();
        //}

        //protected void number6Button_Click(object sender, EventArgs e)
        //{
        //    TextBox2.Text += number6Button.Text.Trim();
        //}

        //protected void number7Button_Click(object sender, EventArgs e)
        //{
        //    TextBox2.Text += number7Button.Text.Trim();
        //}

        //protected void number8Button_Click(object sender, EventArgs e)
        //{
        //    TextBox2.Text += number8Button.Text.Trim();
        //}

        //protected void number9Button_Click(object sender, EventArgs e)
        //{
        //    TextBox2.Text += number9Button.Text.Trim();
        //}

        //protected void number0Button_Click(object sender, EventArgs e)
        //{
        //    TextBox2.Text += number0Button.Text.Trim();
        //}

        //protected void multiButton_Click(object sender, EventArgs e)
        //{
        //    TextBox2.Text += multiButton.Text.Trim();
        //}

        //protected void divitionButton0_Click(object sender, EventArgs e)
        //{
        //    TextBox2.Text += divitionButton0.Text.Trim();
        //}

        //protected void dotButton_Click(object sender, EventArgs e)
        //{
        //    TextBox2.Text += dotButton.Text.Trim();
        //}

        //protected void EqualButton1_Click(object sender, EventArgs e)
        //{
        //    TextBox2.Text += EqualButton.Text.Trim();
        //}

        //protected void backButton2_Click(object sender, EventArgs e)
        //{
        //    TextBox2.Text = TextBox2.Text.Substring(0, TextBox2.Text.Length - 1);
        //}

        //protected void backButton4_Click(object sender, EventArgs e)
        //{
        //    TextBox2.Text = "";
        //    Label2.Text = "";
        //}

        //protected void bracket1_Click(object sender, EventArgs e)
        //{

        //    TextBox2.Text += bracket1.Text.Trim();
        //}

        //protected void bracket2_Click(object sender, EventArgs e)
        //{

        //    TextBox2.Text += bracket2.Text.Trim();
        //}

        //protected void BaseButton_Click(object sender, EventArgs e)
        //{

        //    TextBox2.Text += BaseButton.Text.Trim();
        //}

        //protected void RootButton_Click(object sender, EventArgs e)
        //{

        //    TextBox2.Text += RootButton.Text.Trim();
        //}
    }
}