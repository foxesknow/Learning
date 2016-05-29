using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending
{
	/// <summary>
	/// Holds information on the outcome of a vending operation
	/// </summary>
	public sealed class VendingResult
	{
		/// <summary>
		/// A shared instance for failure, as the instances are all the same.
		/// This will save memory allocations in the backtracking machine where failure will be common as we recurse
		/// </summary>
		private static readonly VendingResult FailedResultInstance = new VendingResult(false, Enumerable.Empty<Change>());

		private readonly bool m_Success;
		private readonly IEnumerable<Change> m_Change;

		/// <summary>
		/// Initializes the instance
		/// </summary>
		/// <param name="success">true if successful, otherwise false</param>
		/// <param name="change">On success the change for the user, on failure an empty sequence</param>
		private VendingResult(bool success, IEnumerable<Change> change)
		{
			if(change == null) throw new ArgumentNullException("change");
			if(!success && change.Any()) throw new ArgumentException("you cannot supply change on failure", "change");

			m_Success = success;
			m_Change = change;
		}

		/// <summary>
		/// True if the vending was able to supply the correct change, otherwise false
		/// </summary>
		public bool Success
		{
			get{return m_Success;}
		}

		/// <summary>
		/// True if the vending failed to supply the correct change, otherwise false
		/// </summary>
		public bool Failed
		{
			get{return !m_Success;}
		}

		/// <summary>
		/// On success returns the change given, if any.
		/// On failure returns an empty sequence
		/// </summary>
		public IEnumerable<Change> Change
		{
			get{return m_Change;}
		}

		/// <summary>
		/// Renders the results as a string
		/// </summary>
		/// <returns>A string</returns>
		public override string ToString()
		{
			if(m_Success)
			{
				return string.Format("Success, change = {0}", string.Join(", ", m_Change));
			}
			else
			{
				return "Failed";
			}
		}

		/// <summary>
		/// A "named constructor" to create a successful result
		/// </summary>
		/// <param name="change">The change to be returned</param>
		/// <returns>A result instance that represents a successful vend</returns>
		public static VendingResult CreateSuccess(IEnumerable<Change> change)
		{
			return new VendingResult(true, change);
		}

		/// <summary>
		/// A "named constructor" to create a failed result
		/// </summary>
		/// <returns>A result instance that represents a failed vend</returns>
		public static VendingResult CreateFailed()
		{
			return FailedResultInstance;
		}
	}
}
