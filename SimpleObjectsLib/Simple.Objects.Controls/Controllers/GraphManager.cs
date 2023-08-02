using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Objects.Controls
{
	public class GraphManager
	{
		private static Dictionary<IGraphController, bool> commitChangeOnFocusedNodeChangeByGraphController = new Dictionary<IGraphController, bool>();
		private static bool isSetCommitChangeOnFocusedNodeChange = false;
		private static Dictionary<IGraphController, bool> commitChangeOnDeleteRequestByGraphController = new Dictionary<IGraphController, bool>();
		private static bool isSetCommitChangeOnDeleteRequest = false;

		static GraphManager()
		{
			GraphControllers = new List<IGraphController>();
		}

		public static void SetCommitChangeOnFocusedNodeChangeForAll(bool commitChangeOnChangeFocus)
		{
			CommitChangeOnFocusedNodeChange = commitChangeOnChangeFocus;
			isSetCommitChangeOnFocusedNodeChange = true;
			commitChangeOnFocusedNodeChangeByGraphController.Clear();
			
			foreach (IGraphController controller in GraphControllers)
			{
				commitChangeOnFocusedNodeChangeByGraphController.Add(controller, controller.CommitChangeOnFocusedNodeChange);
				controller.CommitChangeOnFocusedNodeChange = commitChangeOnChangeFocus;
			}
		}

		public static void RestoreCommitChangeOnFocusedNodeChangeFromAll()
		{
			if (isSetCommitChangeOnFocusedNodeChange)
			{
				CommitChangeOnFocusedNodeChange = !CommitChangeOnFocusedNodeChange;
				isSetCommitChangeOnFocusedNodeChange = false;
			}

			foreach (var item in commitChangeOnFocusedNodeChangeByGraphController)
			{
				IGraphController controller = item.Key;
				bool commitChangeOnChangeFocus = item.Value;

				controller.CommitChangeOnFocusedNodeChange = commitChangeOnChangeFocus;
			}
		}

		public static void SetCommitChangeOnDeleteRequestForAll(bool commitChangeOnDeleteRequest)
		{
			CommitChangeOnDeleteRequest = commitChangeOnDeleteRequest;
			isSetCommitChangeOnDeleteRequest = true;
			commitChangeOnDeleteRequestByGraphController.Clear();

			foreach (IGraphController controller in GraphControllers)
			{
				commitChangeOnDeleteRequestByGraphController.Add(controller, controller.CommitChangeOnDeleteRequest);
				controller.CommitChangeOnDeleteRequest = commitChangeOnDeleteRequest;
			}
		}

		public static void RestoreCommitChangeOnDeleteRequestFromAll()
		{
			if (isSetCommitChangeOnDeleteRequest)
			{
				CommitChangeOnDeleteRequest = !CommitChangeOnDeleteRequest;
				isSetCommitChangeOnDeleteRequest = false;
			}

			foreach (var item in commitChangeOnDeleteRequestByGraphController)
			{
				IGraphController controller = item.Key;
				bool commitChangeOnDeleteRequest = item.Value;

				controller.CommitChangeOnDeleteRequest = commitChangeOnDeleteRequest;
			}
		}

		public static bool CommitChangeOnFocusedNodeChange { get; private set; }
		public static bool CommitChangeOnDeleteRequest { get; private set; }

		public static List<IGraphController> GraphControllers { get; private set; }

		public static void SubscribeMe(IGraphController graphController)
		{
			GraphControllers.Add(graphController);
		}

		public static void UnsubscribeMe(IGraphController graphController)
		{
			GraphControllers.Remove(graphController);
		}
	}
}
