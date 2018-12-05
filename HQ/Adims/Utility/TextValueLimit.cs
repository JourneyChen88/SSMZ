using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using adims_MODEL;

namespace adims_Utility
{
    public class TextValueLimit
    {
        public static int TimeToX(DateTime dt, List<ZoomRegion> list)
        {
            try
            {
                int xRe = 0;
                long x1 = Convert.ToInt64(dt.ToString("yyyyMMddHHmm"));
                int r = 0;
                long x_Start = 0;
                long x_End = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    x_Start = Convert.ToInt64(list[i].AStartTime.ToString("yyyyMMddHHmm"));
                    x_End = Convert.ToInt64(list[i].EndTime.ToString("yyyyMMddHHmm"));
                    if (x1 > x_Start && x1 <= x_End)
                    {
                        r = i;
                        break;
                    }
                    else
                        xRe += list[i].RegionSize;
                }
                if (r == 0)
                {
                    int add = TimeDiffMinute(list[0].AStartTime, dt);
                    xRe = add * 15 / list[0].Interval;
                }
                else
                {
                    int add = TimeDiffMinute(list[r].AStartTime, dt);
                    xRe += add * 15 / list[r].Interval;
                }
                return xRe;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());

                return 0;
            }


        }
        public static int TimeToXprint(DateTime dt, List<ZoomRegion> list)
        {
            try
            {
                int xRe = 0;
                long x1 = Convert.ToInt64(dt.ToString("yyyyMMddHHmm"));
                int r = 0;
                long x_Start = 0;
                long x_End = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    x_Start = Convert.ToInt64(list[i].AStartTime.ToString("yyyyMMddHHmm"));
                    x_End = Convert.ToInt64(list[i].EndTime.ToString("yyyyMMddHHmm"));
                    if (x1 > x_Start && x1 <= x_End)
                    {
                        r = i;
                        break;
                    }
                    else
                        xRe += list[i].RegionSize;
                }
                if (r == 0)
                {
                    int add = TimeDiffMinute(list[0].AStartTime, dt);
                    xRe = add * 10 / list[0].Interval;
                }
                else
                {
                    int add = TimeDiffMinute(list[r].AStartTime, dt);
                    xRe += add * 10 / list[r].Interval;
                }
                return xRe;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());

                return 0;
            }


        }
        public static int XtoMinute(int x, List<ZoomRegion> list)
        {
            try
            {
                int minute = 0;
                int x_End = 0;
                int r = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    x_End += list[i].RegionSize;
                    if (x <= x_End)
                    {
                        r = i;
                        x_End = x_End - list[i].RegionSize;
                        break;
                    }
                }
                if (r == 0)
                {
                    minute = x * list[0].Interval / 15;
                }
                else
                {
                    int addTime = TimeDiffMinute(list[0].AStartTime, list[r - 1].EndTime);

                    minute = addTime + ((x - x_End) * list[r].Interval / 15);
                }
                return minute;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                return 0;
            }
        }
        public static int XtoMinutePrint(int x, List<ZoomRegion> list)
        {
            try
            {
                int minute = 0;
                int x_End = 0;
                int r = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    x_End += list[i].RegionSize;
                    if (x <= x_End)
                    {
                        r = i;
                        x_End = x_End - list[i].RegionSize;
                        break;
                    }
                }
                if (r == 0)
                {
                    minute = x * list[0].Interval / 10;
                }
                else
                {
                    int addTime = TimeDiffMinute(list[0].AStartTime, list[r - 1].EndTime);

                    minute = addTime + ((x - x_End) * list[r].Interval / 10);
                }
                return minute;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                return 0;
            }
        }

        public static int TimeDiffMinute(DateTime AStartTime, DateTime EndTime)
        {
            try
            {
                TimeSpan ts = EndTime - AStartTime;
                int addMinute = ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes;
                return addMinute;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                return 0;
            }
        }

        public static int GetRegionSize(ZoomRegion zr)
        {
            TimeSpan ts = zr.EndTime - zr.AStartTime;
            int x = (ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes) * 15 / zr.Interval;
            return x;
        }
        public static int GetRegionSizePrint(ZoomRegion zr)
        {
            TimeSpan ts = zr.EndTime - zr.AStartTime;
            int x = (ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes) * 10 / zr.Interval;
            return x;
        }
        public static bool PingHost(string Address, int TimeOut = 2000)
        {
            using (System.Net.NetworkInformation.Ping PingSender = new System.Net.NetworkInformation.Ping())
            {
                PingOptions Options = new PingOptions();
                Options.DontFragment = true;
                string Data = "test";
                byte[] DataBuffer = Encoding.ASCII.GetBytes(Data);
                PingReply Reply = PingSender.Send(Address, TimeOut, DataBuffer, Options);
                if (Reply.Status == IPStatus.Success)
                    return true;
                return false;
            }
        }
        public static void Text_Value_Limit(object sender, KeyPressEventArgs e)
        {

            if (!(((e.KeyChar >= '0') && (e.KeyChar <= '9')) || e.KeyChar <= 31))
            {
                if (e.KeyChar == '.')
                {
                    if (((TextBox)sender).Text.Trim().IndexOf('.') > -1)
                        e.Handled = true;
                }
                else
                    e.Handled = true;
            }
            else
            {
                if (e.KeyChar <= 31)
                {
                    e.Handled = false;
                }
                else if (((TextBox)sender).Text.Trim().IndexOf('.') > -1)
                {
                    if (((TextBox)sender).Text.Trim().Substring(((TextBox)sender).Text.Trim().IndexOf('.') + 1).Length >= 4)
                        e.Handled = true;
                }
            }

        }
        public static void Int_Limit(object sender, KeyPressEventArgs e)
        {
            if ((((e.KeyChar >= '0') && (e.KeyChar <= '9')) || e.KeyChar <= 31))
            {               
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

        }
        /// <summary>
        /// 判断日期格式是否有效
        /// </summary>
        /// <param name="objString"></param>
        /// <returns></returns>
        public static bool IsDate(String objString)
        {
            Regex myReg = new Regex(@"\b(?<year>\d{2,4})-(?<month>\d{1,2})-(?<day>\d{1,2})\b");
            return myReg.IsMatch(objString);
        }
    }
}
