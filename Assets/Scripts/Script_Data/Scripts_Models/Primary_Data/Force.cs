// ====================================================================================== 
// 文件名         ：    Force.cs                                                         
// 版本号         ：    v1.0.0.0                                                  
// 作者           ：    wuwenthink                                                          
// 创建日期       ：    2017-8-19                                                                  
// 最后修改日期   ：    2017-8-19                                                            
// ====================================================================================== 
// 功能描述       ：    势力                                                                  
// ====================================================================================== 

/// <summary>
/// 势力
/// </summary>
public class Force
{
    /// <summary>
    /// 势力id
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// 势力名称
    /// </summary>
    public string name { get; private set; }
    /// <summary>
    /// 势力类型
    /// </summary>
    public string type { get; private set; }
    /// <summary>
    /// 势力图标
    /// </summary>
    public string icon { get; private set; }
    /// <summary>
    /// 势力旗帜上文字
    /// </summary>
    public string flag { get; private set; }
    /// <summary>
    /// 势力首都
    /// </summary>
    public string capital { get; private set; }
    /// <summary>
    /// 势力疆域说明
    /// </summary>
    public string geography { get; private set; }
    /// <summary>
    /// 势力历史信息
    /// </summary>
    public string history { get; private set; }
    /// <summary>
    /// 势力旗帜颜色
    /// </summary>
    public UnityEngine.Color color { get; private set; }   

    public Force(int _id, string _name, string _type, string _icon, string _flag, string _capital, string _geography, string _history, int _colorR, int _colorG, int _colorB, int _colorA)
    {
        id = _id;
        name = _name;
        type = _type;
        icon = _icon;
        flag = _flag;
        capital = _capital;
        geography = _geography;
        history = _history;
        color = new UnityEngine.Color(_colorR, _colorG, _colorB, _colorA);
    }
}