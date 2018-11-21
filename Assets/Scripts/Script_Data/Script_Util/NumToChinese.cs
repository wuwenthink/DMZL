// ======================================================================================
// 文 件 名 称：NumToChinese.cs 
// 版 本 编 号：v1.0.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-26 20:32:10
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-11-26 20:32:10
// ======================================================================================
// 功 能 描 述：
// ======================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class NumToChinese  {

    private static NumToChinese _instance;

    public static NumToChinese GetInstance
    {
        get
        {
            return _instance;
        }
    }

    // 转换数字 
    private char ToNum(char x)
    {
        string strChnNames = "零一二三四五六七八九";
        string strNumNames = "0123456789";
        return strChnNames[strNumNames.IndexOf(x)];
    }

    // 转换万以下整数 
    private string ChangeInt(string x)
    {
        string[] strArrayLevelNames = new string[4] { "", "十", "百", "千" };
        string ret = "";
        int i;
        for (i = x.Length - 1; i >= 0; i--)
            if (x[i] == '0')
                ret = ToNum(x[i]) + ret;
            else
                ret = ToNum(x[i]) + strArrayLevelNames[x.Length - 1 - i] + ret;
        while ((i = ret.IndexOf("零零")) != -1)
            ret = ret.Remove(i, 1);
        if (ret[ret.Length - 1] == '零' && ret.Length > 1)
            ret = ret.Remove(ret.Length - 1, 1);
        if (ret.Length >= 2 && ret.Substring(0, 2) == "一十")
            ret = ret.Remove(0, 1);
        return ret;
    }

    // 转换整数 
    private string ToInt(string x)
    {
        int len = x.Length;
        string ret, temp;
        if (len <= 4)
            ret = ChangeInt(x);
        else if (len <= 8)
        {
            ret = ChangeInt(x.Substring(0, len - 4)) + "万";
            temp = ChangeInt(x.Substring(len - 4, 4));
            if (temp.IndexOf("千") == -1 && temp != "")
                ret += "零" + temp;
            else
                ret += temp;
        }
        else
        {
            ret = ChangeInt(x.Substring(0, len - 8)) + "亿";
            temp = ChangeInt(x.Substring(len - 8, 4));
            if (temp.IndexOf("千") == -1 && temp != "")
                ret += "零" + temp;
            else
                ret += temp;
            ret += "万";
            temp = ChangeInt(x.Substring(len - 4, 4));
            if (temp.IndexOf("千") == -1 && temp != "")
                ret += "零" + temp;
            else
                ret += temp;
        }
        int i;
        if ((i = ret.IndexOf("零万")) != -1)
            ret = ret.Remove(i + 1, 1);
        while ((i = ret.IndexOf("零零")) != -1)
            ret = ret.Remove(i, 1);
        if (ret[ret.Length - 1] == '零' && ret.Length > 1)
            ret = ret.Remove(ret.Length - 1, 1);
        return ret;
    }

    private string ToDecimal(string x)
    {
        string ret = "";
        for (int i = 0; i < x.Length; i++)
            ret += ToNum(x[i]);
        return ret;
    }

    public string NumToChn(string x)
    {
        if (x.Length == 0)
            return "";
        string ret = "";
        if (x[0] == '-')
        {
            ret = "负";
            x = x.Remove(0, 1);
        }
        if (x[0].ToString() == ".")
            x = "0" + x;
        if (x[x.Length - 1].ToString() == ".")
            x = x.Remove(x.Length - 1, 1);
        if (x.IndexOf(".") > -1)
            ret += ToInt(x.Substring(0, x.IndexOf("."))) + "点" + ToDecimal(x.Substring(x.IndexOf(".") + 1));
        else
            ret += ToInt(x);
        return ret;
    }
    /// <summary>
    /// 转换人民币大小金额
    /// </summary>
    /// <param name="num">金额</param>
    /// <returns>返回大写形式</returns>
    public string CmycurD(decimal num)
    {
        string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字
        string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字
        string str3 = "";    //从原num值中取出的值
        string str4 = "";    //数字的字符串形式
        string str5 = "";  //人民币大写金额形式
        int i;    //循环变量
        int j;    //num的值乘以100的字符串长度
        string ch1 = "";    //数字的汉语读法
        string ch2 = "";    //数字位的汉字读法
        int nzero = 0;  //用来计算连续的零值是几个
        int temp;            //从原num值中取出的值

        num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数
        str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式
        j = str4.Length;      //找出最高位
        if (j > 15) { return "溢出"; }
        str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分

        //循环取出每一位需要转换的值
        for (i = 0; i < j; i++)
        {
            str3 = str4.Substring(i, 1);          //取出需转换的某一位的值
            temp = Convert.ToInt32(str3);      //转换为数字
            if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
            {
                //当所取位数不为元、万、亿、万亿上的数字时
                if (str3 == "0")
                {
                    ch1 = "";
                    ch2 = "";
                    nzero = nzero + 1;
                }
                else
                {
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        ch1 = str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                }
            }
            else
            {
                //该位是万亿，亿，万，元位等关键位
                if (str3 != "0" && nzero != 0)
                {
                    ch1 = "零" + str1.Substring(temp * 1, 1);
                    ch2 = str2.Substring(i, 1);
                    nzero = 0;
                }
                else
                {
                    if (str3 != "0" && nzero == 0)
                    {
                        ch1 = str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 == "0" && nzero >= 3)
                        {
                            ch1 = "";
                            ch2 = "";
                            nzero = nzero + 1;
                        }
                        else
                        {
                            if (j >= 11)
                            {
                                ch1 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                ch1 = "";
                                ch2 = str2.Substring(i, 1);
                                nzero = nzero + 1;
                            }
                        }
                    }
                }
            }
            if (i == (j - 11) || i == (j - 3))
            {
                //如果该位是亿位或元位，则必须写上
                ch2 = str2.Substring(i, 1);
            }
            str5 = str5 + ch1 + ch2;

            if (i == j - 1 && str3 == "0")
            {
                //最后一位（分）为0时，加上“整”
                str5 = str5 + '整';
            }
        }
        if (num == 0)
        {
            str5 = "零元整";
        }
        return str5;
    }

    /**/
    /// <summary>
    /// 一个重载，将字符串先转换成数字在调用CmycurD(decimal num)
    /// </summary>
    /// <param name="num">用户输入的金额，字符串形式未转成decimal</param>
    /// <returns></returns>
    public string CmycurD(string numstr)
    {
        try
        {
            decimal num = Convert.ToDecimal(numstr);
            return CmycurD(num);
        }
        catch
        {
            return "非数字形式！";
        }
    }
}
