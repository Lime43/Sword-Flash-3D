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
    /// <summary>
    /// Interface for triangulators.
    /// The outlines to be triangulated should be specified in the
    /// constructor of an implementing class and should include:
        /// A list of points in space. 1 point occupies 1 array element.
        /// A list of edges, integer indices referencing 2 points per edge. 1 edge occupies 2 subsequent array elements.
    /// </summary>
    public interface ITriangulator
    {
        /// <summary>
        /// Triangulates the outlines specified in the constructor.
        /// </summary>
        /// <param name="newEdges">
        /// The new edges needed to correctly triangulate the outlines.
        /// Specified in the same format as the input edges.
        /// The newEdges should be appended to the input edges for the
        /// newTriangleEdges to be valid.
        /// </param>
        /// <param name="newTriangles">
        /// The new triangles.
        /// A triangle occupies 3 array elements, each of which corresponds to
        /// a point index.
        /// Triangles have clockwise winding order.
        /// For example, newTriangles[11 * 3 + 2] equals the 12th triangle's 3rd point.
        /// </param>
        /// <param name="newTriangleEdges">
        /// The corresponding edges of the new triangles.
        /// A triangle occupies 3 array elements, each of which corresponds to
        /// an edge index.
        /// References the edges by their index, not the where they lie in the array.
        /// For example, edges[newTriangleEdges[5 * 3 + 1] * 2 + 0] equals the
        /// 1th point of the 6th triangle's 2nd edge.
        /// </param>
        void Fill(out int[] newEdges, out int[] newTriangles, out int[] newTriangleEdges);
    }
}
