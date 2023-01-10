using System;
using System.Data;

namespace MDXStudio.MdSchema
{
	internal class MdCube : IMdObject
	{
		private string mCubeName;

		private string mCubeCaption;

		private string mDescription;

		private bool mIsDimensionCube;

		private bool mIsLinkable;

		private bool mIsWriteEnabled;

		private DateTime mLastSchemaUpdate;

		private DateTime mLastDataUpdate;

		private string mBaseCubeName;

		private bool mIsVitual;

		private MdObjectList<MdDimension> mDimensionList;

		private MdObjectList<MdMeasure> mMeasureList;

		private MdObjectList<MdMeasureGroup> mMeasureGroupList;

		private MdObjectList<MdSet> mSetList;

		private MdObjectList<MdKpi> mKpiList;

		public string BaseCubeName
		{
			get
			{
				return this.mBaseCubeName;
			}
		}

		public string Caption
		{
			get
			{
				return this.mCubeCaption;
			}
		}

		public string Description
		{
			get
			{
				return this.mDescription;
			}
		}

		public MdObjectList<MdDimension> Dimensions
		{
			get
			{
				return this.mDimensionList;
			}
		}

		public bool IsDimensionCube
		{
			get
			{
				return this.mIsDimensionCube;
			}
		}

		public bool IsLinkable
		{
			get
			{
				return this.mIsLinkable;
			}
		}

		public bool IsPespective
		{
			get
			{
				return !string.IsNullOrEmpty(this.mBaseCubeName);
			}
		}

		public bool IsVirtual
		{
			get
			{
				return this.mIsVitual;
			}
		}

		public bool IsWriteEnabled
		{
			get
			{
				return this.mIsWriteEnabled;
			}
		}

		public MdObjectList<MdKpi> Kpis
		{
			get
			{
				return this.mKpiList;
			}
		}

		public DateTime LastDataUpdate
		{
			get
			{
				return this.mLastDataUpdate;
			}
		}

		public DateTime LastSchemaUpdate
		{
			get
			{
				return this.mLastSchemaUpdate;
			}
		}

		public MdObjectList<MdMeasureGroup> MeasureGroups
		{
			get
			{
				return this.mMeasureGroupList;
			}
		}

		public MdObjectList<MdMeasure> Measures
		{
			get
			{
				return this.mMeasureList;
			}
		}

		public string Name
		{
			get
			{
				return this.mCubeName;
			}
		}

		public MdObjectList<MdSet> Sets
		{
			get
			{
				return this.mSetList;
			}
		}

		public string UniqueName
		{
			get
			{
				return this.mCubeName;
			}
		}

		public MdCube(string pName, string pCaption, string pDescription, bool pIsDimensionCube, bool pIsLinkable, bool pIsWriteEnabled, DateTime pLastSchemaUpdate, DateTime pLastDataUpdate, string pBaseCubeName)
		{
			this.mCubeName = pName;
			this.mCubeCaption = pCaption;
			this.mDescription = pDescription;
			this.mIsDimensionCube = pIsDimensionCube;
			this.mIsLinkable = pIsLinkable;
			this.mIsWriteEnabled = pIsWriteEnabled;
			this.mLastSchemaUpdate = pLastSchemaUpdate;
			this.mLastDataUpdate = pLastDataUpdate;
			this.mBaseCubeName = pBaseCubeName;
			this.mDimensionList = new MdObjectList<MdDimension>();
			this.mMeasureList = new MdObjectList<MdMeasure>();
			this.mMeasureGroupList = new MdObjectList<MdMeasureGroup>();
			this.mSetList = new MdObjectList<MdSet>();
			this.mKpiList = new MdObjectList<MdKpi>();
		}

		public MdCube(DataRow pRow)
		{
			string str;
			string str1;
			bool flag;
			string empty;
			this.mCubeName = (string)pRow["CUBE_NAME"];
			str = (pRow.Table.Columns.IndexOf("CUBE_CAPTION") >= 0 ? (string)pRow["CUBE_CAPTION"] : this.mCubeName);
			this.mCubeCaption = str;
			str1 = (pRow["DESCRIPTION"] is DBNull ? string.Empty : (string)pRow["DESCRIPTION"]);
			this.mDescription = str1;
			flag = (pRow.Table.Columns.IndexOf("CUBE_SOURCE") >= 0 ? (ushort)pRow["CUBE_SOURCE"] == 2 : false);
			this.mIsDimensionCube = flag;
			this.mIsLinkable = (bool)pRow["IS_LINKABLE"];
			this.mIsVitual = pRow["CUBE_TYPE"].ToString().ToUpper() == "VIRTUAL CUBE";
			this.mIsWriteEnabled = (bool)pRow["IS_WRITE_ENABLED"];
			this.mLastSchemaUpdate = (DateTime)pRow["LAST_SCHEMA_UPDATE"];
			this.mLastDataUpdate = (DateTime)pRow["LAST_DATA_UPDATE"];
			if (pRow.Table.Columns.IndexOf("BASE_CUBE_NAME") >= 0)
			{
				empty = (pRow["BASE_CUBE_NAME"] is DBNull ? string.Empty : (string)pRow["BASE_CUBE_NAME"]);
			}
			else
			{
				empty = string.Empty;
			}
			this.mBaseCubeName = empty;
			this.mDimensionList = new MdObjectList<MdDimension>();
			this.mMeasureList = new MdObjectList<MdMeasure>();
			this.mMeasureGroupList = new MdObjectList<MdMeasureGroup>();
			this.mSetList = new MdObjectList<MdSet>();
			this.mKpiList = new MdObjectList<MdKpi>();
		}

		public override string ToString()
		{
			return this.mCubeCaption;
		}
	}
}