using System;
using FK_CLI;
using System.Collections.Generic;

namespace _4_2Kadai
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
                posArray[i] = new fk_Vector(fk_Math.DRand(-25.0, 25.0),
                                            fk_Math.DRand(-25.0, 25.0), 0.0);
                point.PushVertex(posArray[i]);
            }

            var pointModel = new fk_Model();
            pointModel.Shape = point;
            pointModel.PointColor = new fk_Color(0.0, 0.0, 0.0);
            pointModel.PointSize = 2.0;
            window.Entry(pointModel);
            pointModel.ElementMode = fk_ElementMode.ELEMENT;

            var line = new fk_Line();
            var lineModel = new fk_Model();
            lineModel.Shape = line;
            lineModel.LineColor = new fk_Color(0.0, 0.0, 1.0);
            window.Entry(lineModel);

            var posList = new List<fk_Vector>();
            posList.AddRange(posArray);
            var startPos = new fk_Vector();
            double posX = -9999;
            foreach (fk_Vector pos in posList)
            {
                if (pos.x > posX)
                {
                    posX = pos.x;
                    startPos = pos;
                }
            }

            var startPos1 = new fk_Vector();
            double y = -999;
            foreach (fk_Vector pos in posArray)
            {
                var vec1 = pos - startPos; vec1.Normalize();
                var vec2 = new fk_Vector(0, 1, 0); vec2.Normalize();
                double naiseki = vec1 * vec2;
                if (naiseki >= y && pos != startPos)
                {
                    y = naiseki;
                    startPos1 = pos;
                }
            }
            var newPosList = new List<fk_Vector>();
            newPosList.Add(startPos);
            newPosList.Add(startPos1);
            //posList.Remove(startPos);
            posList.Remove(startPos1);

            line.PushLine(startPos, startPos1);

            var startVec = startPos1 - startPos;
            var vecList = new List<fk_Vector>();
            vecList.Add(startVec);

            for (int i = 2; i < posArray.Length; i++)
            {
                var nextPos = new fk_Vector();
                double naiseki0 = -9999;
                foreach (fk_Vector pos in posList)
                {
                    var vec1 = pos - newPosList[i - 1]; vec1.Normalize();
                    var vec2 = vecList[i - 2]; vec2.Normalize();
                    double naiseki = vec1 * vec2;
                    if (naiseki > naiseki0)
                    {
                        naiseki0 = naiseki;
                        nextPos = pos;
                    }
                }
                posList.Remove(nextPos);
                newPosList.Add(nextPos);
                line.PushLine(newPosList[i - 1], nextPos);
                var vec = nextPos - newPosList[i - 1];
                vecList.Add(vec);
                if (nextPos == startPos) break;
            }

            var clickPos = new fk_Vector();
            var winPos = new fk_Vector();

            var plane = new fk_Plane();
            plane.SetPosNormal(clickPos, new fk_Vector(0.0, 0.0, 1.0));

            Console.WriteLine(startPos);
            Console.WriteLine(vecList.Count);
            
            int pointID = 99;

            while (window.Update())
            {               
                if (window.GetMouseStatus(fk_MouseButton.M1,fk_Switch.DOWN, true))
                {
                    pointID += 1;
                    winPos = window.MousePosition;
                    window.GetProjectPosition(winPos.x, winPos.y, plane, clickPos);
                    clickPos = new fk_Vector(clickPos.x, clickPos.y, 0);
                    var count = 0;
                    for(int i = 0; i < vecList.Count; i++)
                    {
                        var vx1 = newPosList[i + 1].x - newPosList[i].x;
                        var vy1 = newPosList[i + 1].y - newPosList[i].y;
                        var vx2 = clickPos.x - newPosList[i].x;
                        var vy2 = clickPos.y - newPosList[i].y;
                        var ans = vx1 * vy2 - vy1 * vx2;
                        Console.WriteLine(ans);
                        if (ans < 0)
                        {
                            count += 1;
                        }
                    }
                    Console.WriteLine(count);
                    if(count > 0)
                    {
                        point.PushVertex(clickPos);
                        point.SetColor(pointID, new fk_Color(1, 0, 0));
                    }
                    else
                    {
                        point.PushVertex(clickPos);
                        point.SetColor(pointID, new fk_Color(0, 1, 0));
                    }
                }
            }
        }
    }
}