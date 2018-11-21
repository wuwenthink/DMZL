// ======================================================================================
// 文 件 名 称：Interaction_Npc.cs
// 版 本 编 号：v1.1.0
// 原 创 作 者：wuwenthink
// 创 建 时 间：2017-11-28 17:40:35
// 最 后 修 改：wuwenthink
// 更 新 时 间：2017-12-4
// ======================================================================================
// 功 能 描 述：Npc交互
// ======================================================================================

using System.Collections.Generic;

public class Interaction_Npc
{
    public int id { private set; get; }
    public string content { private set; get; }
    public string need { private set; get; }
    public List<string> npc_condition { private set; get; }
    public List<string> player_condition { private set; get; }

    public Interaction_Npc (int _id, string _content, string _need, string _npc, string _player)
    {
        id = _id;
        content = _content;
        need = _need;
        if (!_npc.Equals ("0"))
        {
            npc_condition = new List<string> ();
            string [] reg = _npc.Split (';');
            foreach (string str in reg)
            {
                npc_condition.Add (str);
            }
        }
        else
            npc_condition = null;
        if (!_player.Equals ("0"))
        {
            player_condition = new List<string> ();
            string [] reg = _player.Split (';');
            foreach (string str in reg)
            {
                player_condition.Add (str);
            }
        }
        else
            player_condition = null;
    }
}