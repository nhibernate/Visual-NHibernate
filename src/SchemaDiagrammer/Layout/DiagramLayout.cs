using System;
using System.Collections.Generic;
using System.Windows;
using GraphSharp;
using GraphSharp.Algorithms.Layout;
using GraphSharp.Algorithms.Layout.Simple.FDP;
using GraphSharp.Algorithms.Layout.Simple.Hierarchical;
using QuickGraph;
using SchemaDiagrammer.View.Shapes;

namespace SchemaDiagrammer.Layout
{
	public class DiagramLayout
	{
		private Dictionary<int, DiagramShape> idTable;

		private LayoutAlgorithmBase<DiagramShape, IEdge<DiagramShape>, IBidirectionalGraph<DiagramShape, IEdge<DiagramShape>>>
			layoutAlgorithm;

		public void CalculateLayout(IEnumerable<DiagramShape> tablesToLayout, IEnumerable<Connection> connectionsToLayout, double width, double height)
		{
			var graph = new BidirectionalGraph<DiagramShape, IEdge<DiagramShape>>();

			idTable = new Dictionary<int, DiagramShape>();
			var sizeDictionary = new Dictionary<DiagramShape, Size>();

			int i = 0;
			foreach (var table in tablesToLayout)
			{
				idTable[i] = table;
				sizeDictionary.Add(table, new Size(table.ActualWidth + 100, table.ActualHeight + 100));

				graph.AddVertex(table);
				i++;
			}

			foreach(var conn in connectionsToLayout)
			{
				graph.AddEdge(new Edge<DiagramShape>(conn.Source, conn.Target));
			}

			//CreateAndRunFruchtermanReingoldLayout(graph, width, height);

			if (graph.EdgeCount == 0 || graph.VertexCount == 1)
			{
				// Sugiyama doesn't work when there are no edges. Use Kamada-Kawaii instead
				CreateAndRunKamadaKawaiiLayout(graph, width, height);
			}
			else
			{
				CreateAndRunEfficentSugiyamaLayout(sizeDictionary, graph);
			}

			return;
		}

		private void CreateAndRunFruchtermanReingoldLayout(BidirectionalGraph<DiagramShape, IEdge<DiagramShape>> graph, double width, double height)
		{
			var positionDictionary = new Dictionary<DiagramShape, Point>();

			foreach (var vertex in graph.Vertices)
			{
				positionDictionary.Add(vertex, vertex.Location);
			}
			var parameters = new BoundedFRLayoutParameters(){Width = width, Height = height, RepulsiveMultiplier = 2};
			layoutAlgorithm =
				new FRLayoutAlgorithm<DiagramShape, IEdge<DiagramShape>, IBidirectionalGraph<DiagramShape, IEdge<DiagramShape>>>(
					graph, positionDictionary, parameters);

			layoutAlgorithm.Compute();

		}

		private void CreateAndRunEfficentSugiyamaLayout(Dictionary<DiagramShape, Size> sizeDictionary, BidirectionalGraph<DiagramShape, IEdge<DiagramShape>> graph)
		{
			layoutAlgorithm =
				new EfficientSugiyamaLayoutAlgorithm<DiagramShape, IEdge<DiagramShape>, IBidirectionalGraph<DiagramShape, IEdge<DiagramShape>>>(
					graph, new EfficientSugiyamaLayoutParameters(), sizeDictionary);

			layoutAlgorithm.Compute();
		}

		private void CreateAndRunSugiyamaLayout(Dictionary<DiagramShape, Size> sizeDictionary, BidirectionalGraph<DiagramShape, IEdge<DiagramShape>> graph, SugiyamaLayoutParameters parameters)
		{
			layoutAlgorithm =
				new SugiyamaLayoutAlgorithm<DiagramShape, IEdge<DiagramShape>, IBidirectionalGraph<DiagramShape, IEdge<DiagramShape>>>(
					graph, sizeDictionary, parameters, e => EdgeTypes.Hierarchical);

			layoutAlgorithm.Compute();
		}

		private void CreateAndRunKamadaKawaiiLayout(BidirectionalGraph<DiagramShape, IEdge<DiagramShape>> graph, double width, double height)
		{
			layoutAlgorithm =
				new KKLayoutAlgorithm<DiagramShape, IEdge<DiagramShape>, IBidirectionalGraph<DiagramShape, IEdge<DiagramShape>>>(
					graph, new KKLayoutParameters { Width = width, Height = height });

			layoutAlgorithm.Compute();
		}

		public void LayoutGraph()
		{
			if(layoutAlgorithm == null)
			{
				throw new Exception("Layout must be calulated before it can be applied.");
			}

			foreach(var vertexPosition in layoutAlgorithm.VertexPositions)
			{
				var shape = vertexPosition.Key;
				if(double.IsNaN(vertexPosition.Value.X) && double.IsNaN(vertexPosition.Value.Y))
				{
					shape.Location = new Point(0, 0);
				}
				else
				{
					shape.Location = vertexPosition.Value;	
				}			
			}

			return;
		}
	}
}
