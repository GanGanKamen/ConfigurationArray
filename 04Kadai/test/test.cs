using System;
using FK_CLI;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            fk_Material.InitDefault();

            // ウィンドウ生成
            var window = new fk_AppWindow();
            window.Size = new fk_Dimension(600, 600);
            window.BGColor = new fk_Color(1, 1, 1);
            window.Open();

            var posArray = new fk_Vector[100];
            var point = new fk_Point();
            for (int i = 0; i < 100; i++)
            {
                posArray[i] = new fk_Vector(0,0,0);
                point.PushVertex(posArray[i]);
            }

            var pointModel = new fk_Model();
            pointModel.Shape = point;
            pointModel.PointColor = new fk_Color(0.0, 0.0, 0.0);
            pointModel.PointSize = 2.0;
            window.Entry(pointModel);

            var line = new fk_Line();
            var lineModel = new fk_Model();
            lineModel.Shape = line;
            lineModel.LineColor = new fk_Color(0.0, 0.0, 1.0);
            window.Entry(lineModel);

            posArray[1] = new fk_Vector(0, 10, 0);
            posArray[1].Normalize();
            line.PushLine(posArray[0], posArray[1]);

            var count = 0;
            for(int i = 0; i < 10; i++)
            {
                count += 1;
            }
            Console.WriteLine(count);

            while (window.Update())
            {
                if (window.GetMouseStatus(fk_MouseButton.M1, fk_Switch.DOWN, true))
                {
                    Console.WriteLine("ZeroOne");
                }
            }
        }
    }
}