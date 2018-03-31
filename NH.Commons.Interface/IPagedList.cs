﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Commons.Interface
{
    public interface IPagedList
    {
        /// <summary>
        /// Total number of subsets within the superset.
        /// </summary>
        /// <value>
        /// Total number of subsets within the superset.
        /// </value>
        int PageCount { get; }

        /// <summary>
        /// Total number of objects contained within the superset.
        /// </summary>
        /// <value>
        /// Total number of objects contained within the superset.
        /// </value>
        int TotalItemCount { get; }

        /// <summary>
        /// One-based index of this subset within the superset.
        /// </summary>
        /// <value>
        /// One-based index of this subset within the superset.
        /// </value>
        int CurrentPageIndex { get; }

        /// <summary>
        /// Maximum size any individual subset.
        /// </summary>
        /// <value>
        /// Maximum size any individual subset.
        /// </value>
        int PageSize { get; }

        /// <summary>
        /// Returns true if this is NOT the first subset within the superset.
        /// </summary>
        /// <value>
        /// Returns true if this is NOT the first subset within the superset.
        /// </value>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Returns true if this is NOT the last subset within the superset.
        /// </summary>
        /// <value>
        /// Returns true if this is NOT the last subset within the superset.
        /// </value>
        bool HasNextPage { get; }

        /// <summary>
        /// Returns true if this is the first subset within the superset.
        /// </summary>
        /// <value>
        /// Returns true if this is the first subset within the superset.
        /// </value>
        bool IsFirstPage { get; }

        /// <summary>
        /// Returns true if this is the last subset within the superset.
        /// </summary>
        /// <value>
        /// Returns true if this is the last subset within the superset.
        /// </value>
        bool IsLastPage { get; }

        /// <summary>
        /// One-based index of the first item in the paged subset.
        /// </summary>
        /// <value>
        /// One-based index of the first item in the paged subset.
        /// </value>
        int FirstItemOnPage { get; }

        /// <summary>
        /// One-based index of the last item in the paged subset.
        /// </summary>
        /// <value>
        /// One-based index of the last item in the paged subset.
        /// </value>
        int LastItemOnPage { get; }
    }

    public interface IPagedList<out T> : IPagedList, IEnumerable<T>, IEnumerable
    {
        int Count { get; }

        T this[int index] { get; }

        IPagedList GetMetaData();
    }
}
