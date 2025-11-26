using LORModingBase.CustomExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LORModingBase.DM
{
    /// <summary>
    /// Game static datas management
    /// </summary>
    partial class GameInfos
    {
        /// <summary>
        /// Static XmlData dictionary
        /// </summary>
        public static Dictionary<string, XmlData> staticInfos = new Dictionary<string, XmlData>();
        /// <summary>
        /// Localized XmlData dictionary
        /// </summary>
        public static Dictionary<string, XmlData> localizeInfos = new Dictionary<string, XmlData>();
        /// <summary>
        /// Stoary XmlData dictionary
        /// </summary>
        public static Dictionary<string, XmlData> storyInfos = new Dictionary<string, XmlData>();

        /// <summary>
        /// Load all static & localize & story datas
        /// </summary>
        public static void LoadAllDatas()
        {
            LoadForGivenDirectoryRoot(DM.Config.GAME_RESOURCE_PATHS.RESOURCE_ROOT_LOCALIZE, localizeInfos);
            LoadForGivenDirectoryRoot(DM.Config.GAME_RESOURCE_PATHS.RESOURCE_ROOT_STATIC, staticInfos);

            #region Load story dictionary
            storyInfos.Clear();
            storyInfos["EffectInfo"] = new XmlData(DM.Config.GAME_RESOURCE_PATHS.RESOURCE_ROOT_STORY_EFFECT_INFO);
            storyInfos["Localize"] = new XmlData(Directory.GetFiles(DM.Config.GAME_RESOURCE_PATHS.RESOURCE_ROOT_STORY_LOCALIZE)
                .FindAll_Contains(DM.Config.config.localizeOption.ToUpper()));
            #endregion
        }

        /// <summary>
        /// Load all datas for given directory
        /// </summary>
        public static void LoadForGivenDirectoryRoot(
            string directoryRootPath,
            Dictionary<string, XmlData> XmlDataDic)
        {
            XmlDataDic.Clear();

            if (!Directory.Exists(directoryRootPath))
                return;

            Directory.GetDirectories(directoryRootPath).ForEachSafe((string dicPath) =>
            {
                string DIC_KEY = dicPath.Split('\\').Last();

                // 🔹 取这个子目录下的所有 xml / txt 文件（递归）
                var xmlFiles = Directory
                    .GetFiles(dicPath, "*", SearchOption.AllDirectories)
                    .Where(p =>
                        p.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) ||
                        p.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                // 🔹 没有 xml / txt 的目录就跳过
                if (xmlFiles == null || xmlFiles.Count == 0)
                    return;

                try
                {
                    // 使用 XmlData(List<string>) 构造函数
                    XmlDataDic[DIC_KEY] = new XmlData(xmlFiles);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"[LoadForGivenDirectoryRoot] 忽略目录 {dicPath}: {ex.Message}");
                    // 这个目录数据坏了就算了，不要让整个初始化失败
                }
            });
        }
    }
}



        