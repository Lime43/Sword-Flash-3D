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
using UnityEngine;

namespace ShatterToolkit
{
    public static class Tools
    {
        public static bool IsPointInsideTriangle(ref Vector3 point, ref Vector3 triangle0, ref Vector3 triangle1, ref Vector3 triangle2)
        {
            Vector3 normal = Vector3.Cross(triangle1 - triangle0, triangle2 - triangle0);
            
            return IsPointInsideTriangle(ref point, ref triangle0, ref triangle1, ref triangle2, ref normal);
        }
        
        public static bool IsPointInsideTriangle(ref Vector3 point, ref Vector3 triangle0, ref Vector3 triangle1, ref Vector3 triangle2, ref Vector3 triangleNormal)
        {
            // Discard zero-size triangles; slower but more logical than considering the triangle edges as outside
            if (Vector3.Cross(triangle1 - triangle0, triangle2 - triangle0) == Vector3.zero)
            {
                return false;
            }
            
            Vector3 pointTo0 = triangle0 - point;
            Vector3 pointTo1 = triangle1 - point;
            Vector3 pointTo2 = triangle2 - point;
            
            if (Vector3.Dot(Vector3.Cross(pointTo0, pointTo1), triangleNormal) < 0.0f ||
                Vector3.Dot(Vector3.Cross(pointTo1, pointTo2), triangleNormal) < 0.0f ||
                Vector3.Dot(Vector3.Cross(pointTo2, pointTo0), triangleNormal) < 0.0f)
            {
                return false;
            }
            
            return true;
        }
    }
}
