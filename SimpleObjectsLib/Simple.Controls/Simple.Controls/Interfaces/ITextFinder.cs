using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Controls
{
	public interface ITextFinder
	{
		bool FindNextText(object startNode, string textToFind, bool matchCase);
		object? FocusedNode { get; }
	}
}
