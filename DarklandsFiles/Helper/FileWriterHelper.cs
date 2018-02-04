using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DarklandsFiles.Class;

namespace DarklandsFiles.Helper
{
    class FileWriterHelper
    {
        public static void SaveGame(string newFileName, DarklandInfoController controller)
        {
            if (controller.FileData == null) return;

            //write the characters color
            SaveCharactersColor(controller);

            //write the money
            ShortHelper.WriteUInt(controller.FileData, 112, controller.Florings);
            ShortHelper.WriteUInt(controller.FileData, 114, controller.Groschen);
            ShortHelper.WriteUInt(controller.FileData, 116, controller.Pfenniges);

            //write the places data
            WritePlaces(controller);

            CreateBackup(newFileName);

            File.WriteAllBytes(newFileName, controller.FileData.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        private static void CreateBackup(string newFileName)
        {
            //rename old to backup
            var backup = newFileName + ".bak";
            try
            {
                if (File.Exists(backup)) File.Delete(backup);
            }
            catch
            {
            }
            try
            {
                if (File.Exists(newFileName)) File.Move(newFileName, backup);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void WritePlaces(DarklandInfoController controller)
        {
            if (controller.Places == null) return;
            
            foreach (var place in controller.Places)
            {
                place.SaveCity(controller.FileData);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void SaveCharactersColor(DarklandInfoController controller)
        {
            if (controller.CharactersColors.Count != 4) return;

            for (int i = 0; i < 4; i++)
            {
                var dataIndex = DarkCharacterColor.StartPoint + (DarkCharacterColor.Size*i);
                var color = controller.CharactersColors[i];
                for (int bIndex = 0; bIndex < color.Data.Count; bIndex++)
                {
                    var b = color.Data[bIndex];
                    controller.FileData[dataIndex + bIndex] = b;
                }
            }
        }
    }
}
