// ======================================================================================
// 文件名         ：    Produce_Runtime.cs
// 版本号         ：    v1.2.0
// 作者           ：    wuwenthink
// 创建日期       ：    2017-9-1
// 最后修改日期   ：    2017-9-29 21:55:43
// ======================================================================================
// 功能描述       ：    游戏运行时记录的配方数据
// ======================================================================================

/// <summary>
/// 配方的运行时数据
/// </summary>
public class Produce_Runtime
{
    public int Id { private set; get; }
    public MakeRecipe produce { private set; get; }
    public bool IsUnlocked;

    public Produce_Runtime (MakeRecipe _produce, bool _isUnlocked = false)
    {
        Id = _produce.id;
        produce = _produce;
        IsUnlocked = _isUnlocked;
    }
}