using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DarklandsFiles.Class
{
    /// <summary>
    /// so far looks like a 554 bytes of data
    /// </summary>
    public class DarkCharacter
    {
        public DarkCharacter(List<byte> data, int index)
        {
            try
            {
                //read full name if fails this its not valid
                FullName = ListHelper.Read(data, index + 37, 25);
            }
            catch (Exception)
            {
                return;
            }
            FullName = ListHelper.Read(data, index + 37, 25);
            NickName = ListHelper.Read(data, index + 37 + 25, 11);
        }

        public const int CharacterLength = 554;
        public readonly string FullName;
        public readonly string NickName;

        public static bool IsValid(DarkCharacter character)
        {
            return character.FullName != null;
        }

        public override string ToString()
        {
            return NickName + ", " + FullName;
        }
    }
    /// <summary>
    /// the colors used in combat, this is 24 bytes
    /// </summary>
    public class DarkCharacterColor
    {
        public DarkCharacterColor(List<byte> data, int index)
        {
            DataIndex = index;
            for (int i = index; i < index + Size; i++)
            {
                Data.Add(data[i]);
            }
        }


        public const int StartPoint = 273;
        public const int Size = 24;

        public readonly List<byte> Data = new List<byte>();
        public readonly int DataIndex  ;
    }
}
