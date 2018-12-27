// ======================================================================================
// 文 件 名 称：Business_Broker.cs 
// 版 本 编 号：v1.1.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-06 21:23:34
// 最 后 修 改：xic
// 更 新 时 间：2017-11-18
// ======================================================================================
// 功 能 描 述：牙行
// ======================================================================================

public class Business_Broker
{

    public Business_Broker (int _baseId, string _name, int _level, int _type_num, int _discount, string _gift, string _introduce, int _time_max, int _goods_max, int _fixedTime_min, int _fixedPrice_min, int _reducePrice, int _partnership_money, int _partnership_goods, int _refresh_num, int _ElitePercent)
    {
        baseId = _baseId;
        name = _name;
        level = _level;
        type_num = _type_num;
        discount = _discount;
        gift = _gift;
        introduce = _introduce;
        time_max = _time_max;
        goods_max = _goods_max;
        fixedTime_min = _fixedTime_min;
        fixedPrice_min = _fixedPrice_min;
        reducePrice = _reducePrice;
        partnership_money = _partnership_money;
        partnership_goods = _partnership_goods;
        refresh_num = _refresh_num;
        ElitePercent = _ElitePercent;
    }

    public Business_Broker (int _baseId)
    {
        var broker = SelectDao.GetDao ().SelectBusiness_Broker (_baseId);

        baseId = broker.baseId;
        name = broker.name;
        level = broker.level;
        type_num = broker.type_num;
        discount = broker.discount;
        gift = broker.gift;
        introduce = broker.introduce;
        time_max = broker.time_max;
        goods_max = broker.goods_max;
        fixedTime_min = broker.fixedTime_min;
        fixedPrice_min = broker.fixedPrice_min;
        reducePrice = broker.reducePrice;
        partnership_money = broker.partnership_money;
        partnership_goods = broker.partnership_goods;
        refresh_num = broker.refresh_num;
        ElitePercent = broker.ElitePercent;
    }



    /// <summary>
    /// ID
    /// </summary>
    public int baseId { get; private set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string name { get; private set; }
    /// <summary>
    /// 等级
    /// </summary>
    public int level { get; private set; }
    /// <summary>
    /// 商品类型数量
    /// </summary>
    public int type_num { get; private set; }
    /// <summary>
    /// 折扣基础额度
    /// </summary>
    public int discount { get; private set; }
    /// <summary>
    /// 赠礼价值范围
    /// </summary>
    public string gift { get; private set; }
    /// <summary>
    /// 介绍人脉类型
    /// </summary>
    public string introduce { get; private set; }
    /// <summary>
    /// 供货时长上限
    /// </summary>
    public int time_max { get; private set; }
    /// <summary>
    /// 可选货物数量
    /// </summary>
    public int goods_max { get; private set; }
    /// <summary>
    /// 定期定金下限
    /// </summary>
    public int fixedTime_min { get; private set; }
    /// <summary>
    /// 定额定金下限
    /// </summary>
    public int fixedPrice_min { get; private set; }
    /// <summary>
    /// 基础优惠价格
    /// </summary>
    public int reducePrice { get; private set; }
    /// <summary>
    /// 资金入伙限额
    /// </summary>
    public int partnership_money { get; private set; }
    /// <summary>
    /// 货物入伙限额
    /// </summary>
    public int partnership_goods { get; private set; }
    /// <summary>
    /// 招募人员刷新数量
    /// </summary>
    public int refresh_num { get; private set; }
    /// <summary>
    /// 招募人员精英率
    /// </summary>
    public int ElitePercent { get; private set; }



}
