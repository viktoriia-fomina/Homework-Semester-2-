﻿using System;

namespace UniqueList
{
    /// <summary>
    /// UniqueList is a linear container of data elemenents.
    /// UniqueList does not contain duplicate elements.
    /// </summary>
    public class UniqueList : List<int>
    {
        /// <summary>
        /// Creates an object of the UniqueList class.
        /// </summary>
        public UniqueList() : base() { }

        /// <summary>
        /// Pushes an element into position.
        /// </summary>
        /// <param name="position">Index by which element is going to be add.</param>
        /// <param name="data">Element to add.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when position is invalid.</exception>
        /// <exception cref="DuplicateElementException">Thrown when someone tries to add element that is already 
        /// in the list.</exception>
        public override void PushToPosition(int position, int data)
        {
            if (Exists(data))
            {
                throw new DuplicateElementException($"Element with the data {data} is already in the list\n");
            }

            base.PushToPosition(position, data);
        }

        /// <summary>
        /// Changes the value by position.
        /// </summary>
        public override void ChangeByPosition(int position, int data)
        {
            if (GetValueByPosition(position) != data)
            {
                if (!Exists(data))
                {
                    base.ChangeByPosition(position, data);
                }
            }
        }
    }
}
