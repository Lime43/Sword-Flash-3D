/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// Shatter Toolkit
// Copyright 2015 Gustav Olsson

namespace ShatterToolkit
{
    public class Triangle
    {
        public int vertex0, vertex1, vertex2;
        public Point point0, point1, point2;
        public Edge edge0, edge1, edge2;
        
        public Triangle(int vertex0, int vertex1, int vertex2, Point point0, Point point1, Point point2, Edge edge0, Edge edge1, Edge edge2)
        {
            this.vertex0 = vertex0;
            this.vertex1 = vertex1;
            this.vertex2 = vertex2;
            this.point0 = point0;
            this.point1 = point1;
            this.point2 = point2;
            this.edge0 = edge0;
            this.edge1 = edge1;
            this.edge2 = edge2;
        }
    }
}
