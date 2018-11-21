// ======================================================================================
// 文 件 名 称：Business_Warehouse.cs 
// 版 本 编 号：v1.1.1
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-06 21:48:53
// 最 后 修 改：xic
// 更 新 时 间：2017-11-13
// ======================================================================================
// 功 能 描 述：
// ======================================================================================

public class Business_Warehouse
{

    public Business_Warehouse (int _type, string _name, int _lv, int _building, int _Vol, string _volValue, int _dailyLoss, int _rentingPrice, int _buyPrice, int _maintainPrice, int _robberPercent, string _robberValue, int _firePercent, string _fireValue, int _dirtyPercent)
    {
        type = _type;
        name = _name;
        lv = _lv;
        building = _building;
        Vol = _Vol;
        volValue = _volValue;
        dailyLoss = _dailyLoss;
        rentingPrice = _rentingPrice;
        buyPrice = _buyPrice;
        maintainPrice = _maintainPrice;
        robberPercent = _robberPercent;
        robberValue = _robberValue;
        firePercent = _firePercent;
        fireValue = _fireValue;
        dirtyPercent = _dirtyPercent;
    }

    public Business_Warehouse (int _type)
    {
        var warehosue = SelectDao.GetDao ().SelectBusiness_Warehouse (_type);
        type = warehosue.type;
        name = warehosue.name;
        lv = warehosue.lv;
        building = warehosue.building;
        Vol = warehosue.Vol;
        volValue = warehosue.volValue;
        dailyLoss = warehosue.dailyLoss;
        rentingPrice = warehosue.rentingPrice;
        buyPrice = warehosue.buyPrice;
        maintainPrice = warehosue.maintainPrice;
        robberPercent = warehosue.robberPercent;
        robberValue = warehosue.robberValue;
        firePercent = warehosue.firePercent;
        fireValue = warehosue.fireValue;
        dirtyPercent = warehosue.dirtyPercent;
    }


    /// <summary>
    /// ID
    /// </summary>
    public int type { get; private set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string name { get; private set; }
    /// <summary>
    /// 等级
    /// </summary>
    public int lv { get; private set; }
    /// <summary>
    /// 建筑ID
    /// </summary>
    public int building { get; private set; }
    /// <summary>
    /// 基础容量
    /// </summary>
    public int Vol { get; private set; }
    /// <summary>
    /// 容量升级参数
    /// </summary>
    public string volValue { get; private set; }
    /// <summary>
    /// 每日损耗
    /// </summary>
    public int dailyLoss { get; private set; }
    /// <summary>
    /// 租赁价格
    /// </summary>
    public int rentingPrice { get; private set; }
    /// <summary>
    /// 购买价格
    /// </summary>
    public int buyPrice { get; private set; }
    /// <summary>
    /// 每月维护费用
    /// </summary>
    public int maintainPrice { get; private set; }
    /// <summary>
    /// 盗贼几率
    /// </summary>
    public int robberPercent { get; private set; }
    /// <summary>
    /// 防盗升级参数
    /// </summary>
    public string robberValue { get; private set; }
    /// <summary>
    /// 火灾几率
    /// </summary>
    public int firePercent { get; private set; }
    /// <summary>
    /// 防火升级参数
    /// </summary>
    public string fireValue { get; private set; }
    /// <summary>
    /// 脏乱几率
    /// </summary>
    public int dirtyPercent { get; private set; }



}
