using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Simple.Controls
{
	public interface ITextFinder
	{
		bool FindNextText(object startNode, string textToFind, bool matchCase);
		bool FindNextText2(object? startNode, string textToFind, bool matchCase, CancellationToken cancellationToken, IProgress<int> progress);

		object? FocusedNode { get; }
	}
}
