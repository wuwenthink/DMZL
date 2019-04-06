using System.IO;

namespace Common
{
    /// <summary>
    /// 文件操作助手
    /// </summary>
    public static class DirectoryHelper
    {
        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="sourceFolder">待复制的文件夹</param>
        /// <param name="destFolder">复制到的文件夹</param>
        public static void CopyFolder ( string sourceFolder,string destFolder )
        {
            if ( !Directory.Exists(destFolder) )
            {
                Directory.CreateDirectory(destFolder);
            }
            string[] files = Directory.GetFiles(sourceFolder);
            foreach ( string file in files )
            {
                string name = Path.GetFileName(file);

                string dest = Path.Combine(destFolder,name);

                File.Copy(file,dest);
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach ( string folder in folders )
            {
                string name = Path.GetFileName(folder);

                string dest = Path.Combine(destFolder,name);

                CopyFolder(folder,dest);
            }
        }


        /// <summary>    
        /// C# 删除文件夹       
        /// </summary>    
        /// <param name="dir">删除的文件夹，全路径格式</param>    
        public static void DeleteFolder (  string dir )
        {
            // 循环文件夹里面的内容    
            foreach ( string f in Directory.GetFileSystemEntries(dir) )
            {
                // 如果是文件存在    
                if ( File.Exists(f) )
                {
                    FileInfo fi = new FileInfo(f);
                    if ( fi.Attributes.ToString().IndexOf("Readonly") != 1 )
                    {
                        fi.Attributes = FileAttributes.Normal;
                    }
                    // 直接删除其中的文件    
                    File.Delete(f);
                }
                else
                {
                    // 如果是文件夹存在    
                    // 递归删除子文件夹    
                    DeleteFolder(f);
                }
            }
            // 删除已空文件夹    
            Directory.Delete(dir);
        }
    }
}
