using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    /// <summary>
	/// Allows an object to encode itself
	/// </summary>
	public interface IEncodeData
	{
		void Encode(DataEncoder encoder);
	}
}
