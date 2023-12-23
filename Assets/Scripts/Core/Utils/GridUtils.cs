using System.Collections.Generic;
using System.Numerics;

namespace Core.Utils
{
    /// <summary>
    /// Provides utility methods for working with 2D grids.
    /// </summary>
    public static class GridUtils
    {
        private static readonly List<object> temp = new();
        
        /// <summary>
        /// Shifts the cells of a 2D grid in a specified direction.
        /// </summary>
        /// <typeparam name="T">The type of elements in the grid.</typeparam>
        /// <param name="grid">The 2D grid to be shifted.</param>
        /// <param name="direction">The direction vector specifying the shift.</param>
        public static void ShiftCells<T>(T[,] grid, Vector2 direction)
        {
            var rows = grid.GetLength(0);
            var cols = grid.GetLength(1);

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    var newI = i + (int)direction.Y;
                    var newJ = j + (int)direction.X;

                    if (IsWithinGridBounds(newI, newJ, rows, cols))
                    {
                        grid[newI, newJ] = grid[i, j];
                    }
                }
            }
        }

        /// <summary>
        /// Shifts the elements of a flat list in a specified direction using index calculations.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The flat list to be shifted.</param>
        /// <param name="width">The width of the virtual 2D grid.</param>
        /// <param name="direction">The direction vector specifying the shift.</param>
        /// <param name="wrappedCells">The container to mark cells that were been wrapped during shifting</param>
        public static void ShiftCells<T>(List<T> list, int width, Vector2 direction, List<T> wrappedCells = null)
        {

            // Calculation of the total number of elements in the matrix
            var totalElements = width * width;

            // copy the original matrix into temp
            list.ForEach(element => temp.Add(element));

            // Loop through each element in the matrix
            for (var i = 0; i < totalElements; i++) 
            {
                // Calculate the original row and column index of the current element
                var originalIndex = i;

                var shiftedRowIndex = originalIndex / width + (int)direction.Y; 
                var shiftedColIndex = originalIndex % width + (int)direction.X; 

                // Calculate the shifted row and column index based on the specified shifts
                var wrappedRowIndex = (shiftedRowIndex + width) % width;
                var wrappedColIndex = (shiftedColIndex + width) %  width;

                if (wrappedColIndex != shiftedColIndex || wrappedRowIndex != shiftedRowIndex)
                {
                    wrappedCells?.Add((T)temp[originalIndex]);
                }

                // Calculate the new index after shifting
                var newIndex = wrappedRowIndex * width + wrappedColIndex; 

                // Update the original matrix with the element from the temporary copy
                list[newIndex] = (T)temp[originalIndex]; 
            }
            temp.Clear();
        }


        /// <summary>
        /// Checks if the specified index is within the bounds of the list.
        /// </summary>
        /// <param name="index">The index to check.</param>
        /// <param name="count">The total number of elements in the list.</param>
        /// <returns>True if the index is within the bounds; otherwise, false.</returns>
        public static bool IsWithinListBounds(int index, int count)
        {
            return index < count;
        }

        /// <summary>
        /// Checks if the specified coordinates are within the bounds of the grid.
        /// </summary>
        /// <param name="i">The row index.</param>
        /// <param name="j">The column index.</param>
        /// <param name="rows">The total number of rows in the grid.</param>
        /// <param name="cols">The total number of columns in the grid.</param>
        /// <returns>True if the coordinates are within the bounds; otherwise, false.</returns>
        public static bool IsWithinGridBounds(int i, int j, int rows, int cols)
        {
            return i >= 0 && i < rows && j >= 0 && j < cols;
        }
    }
}