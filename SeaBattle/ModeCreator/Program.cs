using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeCreator
{
    class Program
    {
        class MyField : ModeDLL.ShotMapCreator
        {
            public MyField(string fileToSave)
                : base(fileToSave)
            {

            }

            public override void GetShotMap()
            {
                for (int i = 99; i > -1; --i)
                    _shotMap.Add(i);
                base.WriteFieldToFile();
            }

        }
        static void Main(string[] args)
        {
            MyField f = new MyField("endToStart.txt");
            f.GetShotMap();
        }
    }
}
