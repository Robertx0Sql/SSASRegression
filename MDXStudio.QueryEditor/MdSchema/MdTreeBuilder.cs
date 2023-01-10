using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace MDXStudio.MdSchema
{
	internal class MdTreeBuilder
	{
		private const string cChildrenPlaceholder = "__CHILDREN_PLACEHOLDER_";

		private const string cChildrenPlaceholderHierarchy = "__CHILDREN_PLACEHOLDER_HIER__";

		private const string cChildrenPlaceholderLevel = "__CHILDREN_PLACEHOLDER_LVL__";

		private const string cChildrenPlaceholderMembers = "__CHILDREN_PLACEHOLDER_MEMB__";

		private AdomdConnection mConn;

		private bool mShowHiddenObjects;

		private int mMaxMemberCount = 100;

		private eServerVersion mServerVersion;

		public AdomdConnection Conn
		{
			get
			{
				return this.mConn;
			}
			set
			{
				this.mConn = value;
				switch (this.GetOlapServerVersion().Major)
				{
					case 8:
					{
						this.mServerVersion = eServerVersion.Shiloh;
						return;
					}
					case 9:
					{
						this.mServerVersion = eServerVersion.Yukon;
						return;
					}
					case 10:
					{
						this.mServerVersion = eServerVersion.Katmai;
						return;
					}
				}
				this.mServerVersion = eServerVersion.Unknown;
			}
		}

		public int MaxMemberCount
		{
			get
			{
				return this.mMaxMemberCount;
			}
			set
			{
				this.mMaxMemberCount = value;
			}
		}

		public bool ShowHiddenObjects
		{
			get
			{
				return this.mShowHiddenObjects;
			}
			set
			{
				this.mShowHiddenObjects = value;
			}
		}

		public MdTreeBuilder()
		{
		}

		private void AddDimensionsNodes(TreeNode pCubeNode)
		{
			AdomdRestrictionCollection adomdRestrictionCollection = new AdomdRestrictionCollection();
			adomdRestrictionCollection.Add("CUBE_NAME", ((MdCube)pCubeNode.Tag).Name);
			if (this.mServerVersion >= eServerVersion.Yukon)
			{
				adomdRestrictionCollection.Add("DIMENSION_VISIBILITY", 3);
			}
			DataTable schemaDataTable = this.GetSchemaDataTable(AdomdSchemaGuid.Dimensions, adomdRestrictionCollection);
			DataView dataViews = new DataView(schemaDataTable, null, "DIMENSION_CAPTION", DataViewRowState.CurrentRows);
			foreach (DataRow row in dataViews.ToTable().Rows)
			{
				MdDimension mdDimension = new MdDimension(row);
				if (mdDimension.DimensionType == eDimensionType.Measure)
				{
					continue;
				}
				string str = "Dimension.ico";
				if (!mdDimension.IsVisible)
				{
					str = string.Concat(str, "_BW");
				}
				TreeNode treeNode = pCubeNode.Nodes.Add(mdDimension.UniqueName, mdDimension.Caption, str, str);
				treeNode.Tag = mdDimension;
				this.AddDummyNode(treeNode, "__CHILDREN_PLACEHOLDER_HIER__");
			}
		}

		private void AddDummyNode(TreeNode pNode, string pPlaceholder)
		{
			pNode.Nodes.Add(pPlaceholder);
		}

		private void AddHierarchiesNodes(TreeNode pDimensionNode)
		{
			string str;
			TreeNode treeNode;
			DataView dataViews;
			AdomdRestrictionCollection adomdRestrictionCollection = new AdomdRestrictionCollection();
			MdDimension tag = (MdDimension)pDimensionNode.Tag;
			adomdRestrictionCollection.Add("CUBE_NAME", tag.CubeName);
			adomdRestrictionCollection.Add("DIMENSION_UNIQUE_NAME", tag.UniqueName);
			if (this.mServerVersion >= eServerVersion.Yukon)
			{
				adomdRestrictionCollection.Add("HIERARCHY_VISIBILITY", 3);
			}
			DataTable schemaDataTable = this.GetSchemaDataTable(AdomdSchemaGuid.Hierarchies, adomdRestrictionCollection);
			dataViews = (this.mServerVersion >= eServerVersion.Yukon ? new DataView(schemaDataTable, null, "HIERARCHY_DISPLAY_FOLDER, HIERARCHY_CAPTION", DataViewRowState.CurrentRows) : new DataView(schemaDataTable, null, "HIERARCHY_CAPTION", DataViewRowState.CurrentRows));
			foreach (DataRow row in dataViews.ToTable().Rows)
			{
				MdHierarchy mdHierarchy = new MdHierarchy(row);
				switch (mdHierarchy.Origin)
				{
					case eHierarchyOrigin.UserDefined:
					{
						str = "Hierarchy.ico";
						break;
					}
					case eHierarchyOrigin.SystemEnabled:
					{
						str = "AttributeHierarchy.ico";
						break;
					}
					case eHierarchyOrigin.ParentChild:
					{
						str = "ParentChild.ico";
						break;
					}
					case eHierarchyOrigin.SystemInternal:
					case eHierarchyOrigin.UserDefinedSystemInternal:
					{
						str = "AttributeHierarchy.ico";
						break;
					}
					case eHierarchyOrigin.Key:
					{
						str = "AttributeKey.ico";
						break;
					}
					default:
					{
						goto case eHierarchyOrigin.UserDefinedSystemInternal;
					}
				}
				if (!mdHierarchy.IsVisible)
				{
					str = string.Concat(str, "_BW");
				}
				if (string.IsNullOrEmpty(mdHierarchy.DisplayFolder))
				{
					treeNode = pDimensionNode.Nodes.Add(mdHierarchy.UniqueName, mdHierarchy.Caption, str, str);
				}
				else
				{
					string displayFolder = mdHierarchy.DisplayFolder;
					TreeNode[] treeNodeArray = pDimensionNode.Nodes.Find(displayFolder, false);
					TreeNode treeNode1 = null;
					if ((int)treeNodeArray.Length != 0)
					{
						treeNode1 = treeNodeArray[0];
					}
					else
					{
						treeNode1 = pDimensionNode.Nodes.Add(displayFolder, displayFolder);
						treeNode1.ImageKey = "FolderClosed.ico";
						treeNode1.SelectedImageKey = "FolderOpen.ico";
					}
					treeNode = treeNode1.Nodes.Add(mdHierarchy.UniqueName, mdHierarchy.Caption, str, str);
				}
				treeNode.Tag = mdHierarchy;
				this.AddDummyNode(treeNode, "__CHILDREN_PLACEHOLDER_LVL__");
			}
		}

		private void AddKpisNodes(TreeNode pCubeNode)
		{
			TreeNode treeNode;
			TreeNode treeNode1 = pCubeNode.Nodes.Add("Kpis", "Kpis", "Kpi.ico", "Kpi.ico");
			AdomdRestrictionCollection adomdRestrictionCollection = new AdomdRestrictionCollection();
			adomdRestrictionCollection.Add("CUBE_NAME", ((MdCube)pCubeNode.Tag).Name);
			DataTable schemaDataTable = this.GetSchemaDataTable(AdomdSchemaGuid.Kpis, adomdRestrictionCollection);
			DataView dataViews = new DataView(schemaDataTable, null, "KPI_CAPTION", DataViewRowState.CurrentRows);
			foreach (DataRow row in dataViews.ToTable().Rows)
			{
				MdKpi mdKpi = new MdKpi(row);
				string str = "Kpi.ico";
				string displayFolder = mdKpi.DisplayFolder;
				if (string.IsNullOrEmpty(displayFolder))
				{
					displayFolder = mdKpi.MeasureGroupName;
				}
				if (string.IsNullOrEmpty(displayFolder))
				{
					treeNode = treeNode1.Nodes.Add(mdKpi.UniqueName, mdKpi.Caption, str, str);
					treeNode.Tag = mdKpi;
				}
				else
				{
					TreeNode[] treeNodeArray = this.BuildDisplayFolderNode(treeNode1, displayFolder);
					for (int i = 0; i < (int)treeNodeArray.Length; i++)
					{
						TreeNode treeNode2 = treeNodeArray[i];
						treeNode = treeNode2.Nodes.Add(mdKpi.UniqueName, mdKpi.Caption, str, str);
						treeNode.Tag = mdKpi;
					}
				}
			}
		}

		private void AddLelvelsNodes(TreeNode pHierarchyNode)
		{
			MdHierarchy tag = (MdHierarchy)pHierarchyNode.Tag;
			AdomdRestrictionCollection adomdRestrictionCollection = new AdomdRestrictionCollection();
			adomdRestrictionCollection.Add("CUBE_NAME", tag.CubeName);
			adomdRestrictionCollection.Add("DIMENSION_UNIQUE_NAME", tag.DimensionUniqueName);
			adomdRestrictionCollection.Add("HIERARCHY_UNIQUE_NAME", tag.UniqueName);
			if (this.mServerVersion >= eServerVersion.Yukon)
			{
				adomdRestrictionCollection.Add("LEVEL_VISIBILITY", 3);
			}
			DataTable schemaDataTable = this.GetSchemaDataTable(AdomdSchemaGuid.Levels, adomdRestrictionCollection);
			foreach (DataRow row in schemaDataTable.Rows)
			{
				MdLevel mdLevel = new MdLevel(row);
				int number = mdLevel.Number + 1;
				string str = string.Concat("Level", number.ToString(), ".ico");
				if (!mdLevel.IsVisible)
				{
					str = string.Concat(str, "_BW");
				}
				TreeNode treeNode = pHierarchyNode.Nodes.Add(mdLevel.UniqueName, mdLevel.Caption, str, str);
				treeNode.Tag = mdLevel;
				this.AddDummyNode(treeNode, "__CHILDREN_PLACEHOLDER_MEMB__");
			}
		}

		private void AddMeasuresNodes(TreeNode pCubeNode)
		{
			TreeNode treeNode;
			DataView dataViews;
			string str;
			TreeNode treeNode1 = pCubeNode.Nodes.Add("Measures", "Measures", "Measure.ico", "Measure.ico");
			AdomdRestrictionCollection adomdRestrictionCollection = new AdomdRestrictionCollection();
			adomdRestrictionCollection.Add("CUBE_NAME", ((MdCube)pCubeNode.Tag).Name);
			if (this.mServerVersion >= eServerVersion.Yukon)
			{
				adomdRestrictionCollection.Add("MEASURE_VISIBILITY", 3);
			}
			DataTable schemaDataTable = this.GetSchemaDataTable(AdomdSchemaGuid.Measures, adomdRestrictionCollection);
			dataViews = (this.mServerVersion >= eServerVersion.Yukon ? new DataView(schemaDataTable, null, "MEASUREGROUP_NAME, MEASURE_CAPTION", DataViewRowState.CurrentRows) : new DataView(schemaDataTable, null, "MEASURE_CAPTION", DataViewRowState.CurrentRows));
			foreach (DataRow row in dataViews.ToTable().Rows)
			{
				MdMeasure mdMeasure = new MdMeasure(row);
				str = (string.IsNullOrEmpty(mdMeasure.Expression) ? "Measure.ico" : "MeasureCalculated.ico");
				string str1 = str;
				if (!mdMeasure.IsVisible)
				{
					str1 = string.Concat(str1, "_BW");
				}
				string displayFolder = mdMeasure.DisplayFolder;
				if (string.IsNullOrEmpty(displayFolder))
				{
					displayFolder = mdMeasure.MeasureGroupName;
				}
				if (string.IsNullOrEmpty(displayFolder))
				{
					treeNode = treeNode1.Nodes.Add(mdMeasure.UniqueName, mdMeasure.Caption, str1, str1);
					treeNode.Tag = mdMeasure;
				}
				else
				{
					TreeNode[] treeNodeArray = this.BuildDisplayFolderNode(treeNode1, displayFolder);
					for (int i = 0; i < (int)treeNodeArray.Length; i++)
					{
						TreeNode treeNode2 = treeNodeArray[i];
						treeNode = treeNode2.Nodes.Add(mdMeasure.UniqueName, mdMeasure.Caption, str1, str1);
						treeNode.Tag = mdMeasure;
					}
				}
			}
		}

		private void AddMembersNodes(TreeNode pNode)
		{
			string str;
			string cubeName;
			if (!(pNode.Tag is MdLevel))
			{
				if (!(pNode.Tag is MdMember))
				{
					return;
				}
				MdMember tag = (MdMember)pNode.Tag;
				str = string.Format("{0}.Children", tag.UniqueName);
				cubeName = tag.CubeName;
			}
			else
			{
				MdLevel mdLevel = (MdLevel)pNode.Tag;
				str = string.Format("{0}.Members", mdLevel.UniqueName);
				cubeName = mdLevel.CubeName;
			}
			string str1 = string.Format("SELECT HEAD({0}, {1}) \r\nPROPERTIES MEMBER_KEY, PARENT_UNIQUE_NAME, PARENT_LEVEL, DIMENSION_UNIQUE_NAME, HIERARCHY_UNIQUE_NAME, CUBE_NAME on 0, \r\n{{}} on 1 \r\nFROM [{2}]", str, this.mMaxMemberCount, cubeName);
			CellSet cellSet = (new AdomdCommand(str1, this.mConn)).ExecuteCellSet();
			PositionCollection positions = cellSet.Axes[0].Positions;
			PositionCollection.Enumerator enumerator = positions.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Position current = enumerator.Current;
				MdMember mdMember = MdMember.CreateFromMember(current.Members[0]);
				TreeNode treeNode = pNode.Nodes.Add(mdMember.UniqueName, mdMember.Name, "Member.ico", "Member.ico");
				treeNode.Tag = mdMember;
				if (!mdMember.HasChildren)
				{
					continue;
				}
				this.AddDummyNode(treeNode, "__CHILDREN_PLACEHOLDER_MEMB__");
			}
			if (positions.Count >= this.mMaxMemberCount)
			{
				pNode.Nodes.Add("...", "...", "Member.ico", "Member.ico");
			}
		}

		private void AddSetsNodes(TreeNode pCubeNode)
		{
			TreeNode treeNode;
			TreeNode treeNode1 = pCubeNode.Nodes.Add("NamedSets", "Named Sets", "NamedSet.ico", "NamedSet.ico");
			AdomdRestrictionCollection adomdRestrictionCollection = new AdomdRestrictionCollection();
			adomdRestrictionCollection.Add("CUBE_NAME", ((MdCube)pCubeNode.Tag).Name);
			DataTable schemaDataTable = this.GetSchemaDataTable(AdomdSchemaGuid.Sets, adomdRestrictionCollection);
			DataView dataViews = new DataView(schemaDataTable, null, "SET_CAPTION", DataViewRowState.CurrentRows);
			foreach (DataRow row in dataViews.ToTable().Rows)
			{
				MdSet mdSet = new MdSet(row);
				string str = "NamedSet.ico";
				string displayFolder = mdSet.DisplayFolder;
				if (string.IsNullOrEmpty(displayFolder))
				{
					treeNode = treeNode1.Nodes.Add(mdSet.UniqueName, mdSet.Caption, str, str);
					treeNode.Tag = mdSet;
				}
				else
				{
					TreeNode[] treeNodeArray = this.BuildDisplayFolderNode(treeNode1, displayFolder);
					for (int i = 0; i < (int)treeNodeArray.Length; i++)
					{
						TreeNode treeNode2 = treeNodeArray[i];
						treeNode = treeNode2.Nodes.Add(mdSet.UniqueName, mdSet.Caption, str, str);
						treeNode.Tag = mdSet;
					}
				}
			}
		}

		private TreeNode[] BuildDisplayFolderNode(TreeNode lParentNode, string lDisplayFolder)
		{
			char[] chrArray = new char[] { ';' };
			string[] strArrays = lDisplayFolder.Split(chrArray);
			TreeNode[] treeNodeArray = new TreeNode[(int)strArrays.Length];
			int num = 0;
			string[] strArrays1 = strArrays;
			for (int i = 0; i < (int)strArrays1.Length; i++)
			{
				string str = strArrays1[i];
				char[] chrArray1 = new char[] { '\\' };
				string[] strArrays2 = str.Split(chrArray1);
				TreeNode treeNode = lParentNode;
				string[] strArrays3 = strArrays2;
				for (int j = 0; j < (int)strArrays3.Length; j++)
				{
					string str1 = strArrays3[j];
					TreeNode[] treeNodeArray1 = treeNode.Nodes.Find(str1, false);
					if ((int)treeNodeArray1.Length != 0)
					{
						treeNode = treeNodeArray1[0];
					}
					else
					{
						treeNode = treeNode.Nodes.Add(str1, str1);
						treeNode.ImageKey = "FolderClosed.ico";
						treeNode.SelectedImageKey = "FolderOpen.ico";
					}
				}
				int num1 = num;
				num = num1 + 1;
				treeNodeArray[num1] = treeNode;
			}
			return treeNodeArray;
		}

		public void ExpandNode(TreeNode pNode)
		{
			if (pNode.Nodes.Count == 0)
			{
				return;
			}
			string text = pNode.Nodes[0].Text;
			if (!text.StartsWith("__CHILDREN_PLACEHOLDER_", StringComparison.Ordinal))
			{
				return;
			}
			pNode.Nodes.Clear();
			string str = text;
			string str1 = str;
			if (str != null)
			{
				if (str1 == "__CHILDREN_PLACEHOLDER_HIER__")
				{
					this.AddHierarchiesNodes(pNode);
					return;
				}
				if (str1 == "__CHILDREN_PLACEHOLDER_LVL__")
				{
					this.AddLelvelsNodes(pNode);
					return;
				}
				if (str1 != "__CHILDREN_PLACEHOLDER_MEMB__")
				{
					return;
				}
				this.AddMembersNodes(pNode);
			}
		}

		private Version GetOlapServerVersion()
		{
			AdomdRestrictionCollection adomdRestrictionCollection = new AdomdRestrictionCollection();
			adomdRestrictionCollection.Add(new AdomdRestriction("PropertyName", "DBMSVersion"));
			DataSet schemaDataSet = this.mConn.GetSchemaDataSet("DISCOVER_PROPERTIES", adomdRestrictionCollection);
			string item = (string)schemaDataSet.Tables[0].Rows[0]["Value"];
			return new Version(item);
		}

		private DataTable GetSchemaDataTable(Guid pSchemaGuid, AdomdRestrictionCollection pRestrictions)
		{
			if (pRestrictions == null)
			{
				pRestrictions = new AdomdRestrictionCollection();
			}
			if (this.mShowHiddenObjects && this.mServerVersion >= eServerVersion.Yukon)
			{
				pRestrictions.Add("CUBE_SOURCE", 0);
			}
			DataSet schemaDataSet = this.mConn.GetSchemaDataSet(AdomdSchema.sSchemaList[pSchemaGuid].Id, pRestrictions, true);
			return schemaDataSet.Tables[0];
		}

		public void RebuidTree(TreeView pTreeView, string pCubeName)
		{
			TreeNodeCollection nodes = pTreeView.Nodes;
			nodes.Clear();
			if (this.mConn == null || this.mConn.State != ConnectionState.Open)
			{
				return;
			}
			AdomdRestrictionCollection adomdRestrictionCollection = new AdomdRestrictionCollection();
			adomdRestrictionCollection.Add("CUBE_NAME", pCubeName);
			DataTable schemaDataTable = this.GetSchemaDataTable(AdomdSchemaGuid.Cubes, adomdRestrictionCollection);
			MdCube mdCube = new MdCube(schemaDataTable.Rows[0]);
			TreeNode treeNode = nodes.Add(string.Concat("[", mdCube.UniqueName, "]"), mdCube.Caption, "Cube.ico");
			treeNode.Tag = mdCube;
			this.AddMeasuresNodes(treeNode);
			this.AddSetsNodes(treeNode);
			this.AddKpisNodes(treeNode);
			this.AddDimensionsNodes(treeNode);
			treeNode.Expand();
		}
	}
}