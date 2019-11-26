using System;
using FK_CLI;
using System.Collections.Generic;

namespace _4_3Kadai
{
    class Program
    {
        static public double squareX = 2;
        static void Main(string[] args)
        {
            fk_Material.InitDefault();

            // ウィンドウ生成
            var window = new fk_AppWindow();
            window.Size = new fk_Dimension(600, 600);
            window.BGColor = new fk_Color(1, 1, 1);
            window.Open();

            var triArray = new fk_Vector[3];
            var triPoint = new fk_Point();
            for (int i = 0; i < 3; i++)
            {
                triArray[i] = new fk_Vector(fk_Math.DRand(-25.0, 25.0),
                                            fk_Math.DRand(-25.0, 25.0), 0.0);
                triPoint.PushVertex(triArray[i]);
            }
            var triModel = new fk_Model();
            triModel.Shape = triPoint;
            triModel.PointColor = new fk_Color(0.0, 0.0, 0.0);
            triModel.PointSize = 2.0;
            window.Entry(triModel);

            var triLine = new fk_Line();
            var lineModel = new fk_Model();
            lineModel.Shape = triLine;
            lineModel.LineColor = new fk_Color(0.0, 0.0, 1.0);
            window.Entry(lineModel);
            for (int i = 0; i < 2; i++)
            {
                triLine.PushLine(triArray[i], triArray[i + 1]);
            }
            triLine.PushLine(triArray[2], triArray[0]);

            var confiLine = new fk_Line();
            var lineModel1 = new fk_Model();
            lineModel1.Shape = confiLine;
            lineModel1.LineColor = new fk_Color(0.0, 1.0, 0.0);
            window.Entry(lineModel1);

            var squareList = new List<fk_Vector>();
            for (int i = 0; i < triArray.Length; i++)
            {
                var squarePoint0 = new fk_Vector(triArray[i].x + squareX * 2, triArray[i].y + squareX * 2, 0);
                squareList.Add(squarePoint0);
                var squarePoint1 = new fk_Vector(triArray[i].x - squareX * 2, triArray[i].y + squareX * 2, 0);
                squareList.Add(squarePoint1);
                var squarePoint2 = new fk_Vector(triArray[i].x - squareX * 2, triArray[i].y - squareX * 2, 0);
                squareList.Add(squarePoint2);
                var squarePoint3 = new fk_Vector(triArray[i].x + squareX * 2, triArray[i].y - squareX * 2, 0);
                squareList.Add(squarePoint3);
            }

            var allPosList = new List<fk_Vector>();
            allPosList.AddRange(squareList);
            allPosList.AddRange(triArray);

            var startPos = new fk_Vector();
            double posX = -9999;
            foreach (fk_Vector pos in allPosList)
            {
                if (pos.x > posX)
                {
                    posX = pos.x;
                    startPos = pos;
                }
            }
            var startPos1 = new fk_Vector();
            double y = -9999;
            foreach (fk_Vector pos in allPosList)
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
            var confiPosList = new List<fk_Vector>();
            confiPosList.Add(startPos);
            confiPosList.Add(startPos1);

            var startVec = startPos1 - startPos;
            var vecList = new List<fk_Vector>();
            vecList.Add(startVec);

            confiLine.PushLine(startPos, startPos1);
            for (int i = 2; i < allPosList.Count; i++)
            {
                var nextPos = new fk_Vector();
                double naiseki0 = -9999;
                foreach (fk_Vector pos in allPosList)
                {
                    var vec1 = pos - confiPosList[i - 1]; vec1.Normalize();
                    var vec2 = vecList[i - 2]; vec2.Normalize();
                    double naiseki = vec1 * vec2;
                    if (naiseki > naiseki0)
                    {
                        naiseki0 = naiseki;
                        nextPos = pos;
                    }
                }
                allPosList.Remove(nextPos);
                confiPosList.Add(nextPos);
                confiLine.PushLine(confiPosList[i - 1], nextPos);
                var vec = nextPos - confiPosList[i - 1];
                vecList.Add(vec);
                if (nextPos == startPos) break;
            }

            while (window.Update())
            {

            }
        }
    }
}