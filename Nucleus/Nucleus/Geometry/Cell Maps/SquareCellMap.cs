﻿using Nucleus.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Geometry
{
    /// <summary>
    /// A data structure representing a map of regular square cells arranged in a orthogonal grid
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class SquareCellMap<T> : RegularCellMap<T>
    {
        #region Fields

        /// <summary>
        /// The backing array of cells
        /// </summary>
        private T[] _Cells;

        #endregion

        #region Properties

        /// <summary>
        /// Get the number of cells in this map
        /// </summary>
        public override int CellCount
        {
            get { return _Cells.Length; }
        }

        /// <summary>
        /// Private backing field for Origin property
        /// </summary>
        private Vector _Origin = Vector.Zero;

        /// <summary>
        /// The origin of the map
        /// </summary>
        public Vector Origin { get { return _Origin; } }

        /// <summary>
        /// Private backing field for SizeX property
        /// </summary>
        private int _SizeX;

        /// <summary>
        /// The number of cells in the X-direction
        /// </summary>
        public int SizeX
        {
            get { return _SizeX; }
        }

        /// <summary>
        /// Private backing field for SizeY property
        /// </summary>
        private int _SizeY;

        /// <summary>
        /// The number of cells in the Y-direction
        /// </summary>
        public int SizeY
        {
            get { return _SizeY; }
        }

        /// <summary>
        /// Private backing field for CellSize property
        /// </summary>
        private double _CellSize = 1.0;

        /// <summary>
        /// The edge dimension of each cell
        /// </summary>
        public double CellSize
        {
            get { return _CellSize; }
        }

        /// <summary>
        /// Get or set the contents of the cell at the specified cell index
        /// </summary>
        /// <param name="cellIndex">The 1-dimensional cell index</param>
        /// <returns></returns>
        public override T this[int cellIndex]
        {
            get { return _Cells[cellIndex]; }
            set {_Cells[cellIndex] = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialise a new Square cell map with the specified dimensions
        /// </summary>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        /// <param name="cellSize"></param>
        public SquareCellMap(int sizeX, int sizeY, double cellSize = 1.0)
        {
            _SizeX = sizeX;
            _SizeY = sizeY;
            _CellSize = cellSize;
            _Cells = new T[sizeX * sizeY]; 
        }

        /// <summary>
        /// Initialise a new square cell map at the given origin with the specified dimensions
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        /// <param name="cellSize"></param>
        public SquareCellMap(Vector origin, int sizeX, int sizeY, double cellSize = 1.0) : this(sizeX, sizeY, cellSize)
        {
            _Origin = origin;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Does a cell exist at the specified index?
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <returns></returns>
        public override bool Exists(int cellIndex)
        {
            return (cellIndex >= 0 && cellIndex < _Cells.Length);
        }

        /// <summary>
        /// Get the index of the cell at the specified location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public override int IndexAt(Vector location)
        {
            return IndexAt(location.X, location.Y);
        }

        /// <summary>
        /// Get the index of the cell at the specified x and y coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int IndexAt(double x, double y)
        {
            return IndexAt((int)((x - _Origin.X) / CellSize), (int)((y - _Origin.Y) / CellSize));
        }

        /// <summary>
        /// Get the index of the cell at the specified i and j indices
        /// (aligned to the x and y axes respectively)
        /// </summary>
        /// <param name="columnIndex">The column index</param>
        /// <param name="rowIndex">The row index</param>
        /// <returns></returns>
        public int IndexAt(int columnIndex, int rowIndex)
        {
            if (columnIndex >= 0 && rowIndex >= 0 && columnIndex < SizeX && rowIndex < SizeY)
                return columnIndex + rowIndex * SizeX;
            else return -1;
        }

        /// <summary>
        /// Get the column index of the cell with the specified cell index
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <returns></returns>
        public int ColumnIndex(int cellIndex)
        {
            return cellIndex % SizeX;
        }

        /// <summary>
        /// Get the row index of the cell with the specified cell index
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <returns></returns>
        public int RowIndex(int cellIndex)
        {
            return cellIndex / SizeX;
        }

        /// <summary>
        /// Get the maximum number of possible adjacent cells for the specified
        /// cellIndex.  Note that this is only the maximum possible number and
        /// does not guarantee that all adjacencyIndices up to this number will
        /// return a cell that exists - you should check for this.
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <returns></returns>
        public override int AdjacencyCount(int cellIndex)
        {
            if (Exists(cellIndex)) return 4;
            else return 0;
        }

        /// <summary>
        /// Get the cell index of the specified adjacent cell to the specified cell
        /// </summary>
        /// <param name="cellIndex">The index of the starting cell</param>
        /// <param name="adjacencyIndex">The adjacency index of the cell to retrieve</param>
        /// <returns></returns>
        public override int AdjacentCellIndex(int cellIndex, int adjacencyIndex)
        {
            int i = ColumnIndex(cellIndex);
            int j = ColumnIndex(cellIndex);

            switch (adjacencyIndex)
            {
                case 0:
                    return IndexAt(i + 1, j);
                case 1:
                    return IndexAt(i, j - 1);
                case 2:
                    return IndexAt(i - 1, j);
                case 3:
                    return IndexAt(i, j + 1);
            }

            return -1;
        }

        /// <summary>
        /// Get the cell index of the cell adjacent to the cell with the specified
        /// index in the specified direction.
        /// </summary>
        /// <param name="cellIndex">The index of the starting cell</param>
        /// <param name="direction">The direction of the cell to retrieve</param>
        /// <returns></returns>
        public override int AdjacentCellIndex(int cellIndex, Vector direction)
        {
            int i = ColumnIndex(cellIndex);
            int j = RowIndex(cellIndex);

            if (direction.X.Abs() > direction.Y.Abs()) //X-dominant
                return IndexAt(i + direction.X.Sign(), j);
            else //Y-dominant
                return IndexAt(i, j + direction.Y.Sign());
        }

        /// <summary>
        /// Get the position of the specified cell
        /// </summary>
        /// <param name="cellIndex">The index of the cell to determine the position for</param>
        /// <returns></returns>
        public override Vector CellPosition(int cellIndex)
        {
            return new Vector( ColumnIndex(cellIndex) + 0.5 * CellSize + Origin.X, RowIndex(cellIndex) + 0.5 * CellSize + Origin.Y);
        }

        /// <summary>
        /// Get the number of vertices posessed by the cell at the specifed index
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <returns></returns>
        public override int VertexCount(int cellIndex)
        {
            if (Exists(cellIndex)) return 4;
            else return 0;
        }

        /// <summary>
        /// Get the position of the specifed vertex of the specified cell
        /// </summary>
        /// <param name="cellIndex"></param>
        /// <param name="vertexIndex"></param>
        /// <returns></returns>
        public override Vector CellVertex(int cellIndex, int vertexIndex)
        {
            Vector cP = CellPosition(cellIndex);
            switch(vertexIndex)
            {
                case 0:
                    return cP + new Vector(CellSize / 2, CellSize / 2);
                case 1:
                    return cP + new Vector(-CellSize / 2, CellSize / 2);
                case 2:
                    return cP + new Vector(-CellSize / 2, -CellSize / 2);
                case 3:
                    return cP + new Vector(CellSize / 2, -CellSize / 2);
            }
            return Vector.Unset;
        }

        #endregion
    }
}
