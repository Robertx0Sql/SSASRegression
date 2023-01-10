using System;
using System.Data;

namespace MDXStudio.MdSchema
{
	internal class MdDimension : IMdObject
	{
		private string mCubeName;

		private string mDimensionUniqueName;

		private string mDimensionName;

		private string mDimensionCaption;

		private string mDescription;

		private string mMasterName;

		private eDimensionType mDimensionType;

		private int mDimensionOrdinal;

		private int mKeyAttributeCardinality;

		private bool mIsWriteEnabled;

		private bool mIsVisible;

		private MdObjectList<MdHierarchy> mHierarchyList;

		public string Caption
		{
			get
			{
				return this.mDimensionCaption;
			}
		}

		public int Cardinality
		{
			get
			{
				return this.mKeyAttributeCardinality;
			}
		}

		public string CubeName
		{
			get
			{
				return this.mCubeName;
			}
		}

		public string Description
		{
			get
			{
				return this.mDescription;
			}
		}

		public eDimensionType DimensionType
		{
			get
			{
				return this.mDimensionType;
			}
		}

		public MdObjectList<MdHierarchy> Hierarchies
		{
			get
			{
				return this.mHierarchyList;
			}
		}

		public bool IsVisible
		{
			get
			{
				return this.mIsVisible;
			}
		}

		public bool IsWriteEnabled
		{
			get
			{
				return this.mIsWriteEnabled;
			}
		}

		public string MasterName
		{
			get
			{
				return this.mMasterName;
			}
		}

		public string Name
		{
			get
			{
				return this.mDimensionName;
			}
		}

		public int Ordinal
		{
			get
			{
				return this.mDimensionOrdinal;
			}
		}

		public string UniqueName
		{
			get
			{
				return this.mDimensionUniqueName;
			}
		}

		public MdDimension(string pCubeName, string pUniqueName, string pName, string pCaption, string pDescription, string pMasterName, eDimensionType pType, int pOrdinal, int pCardinality, bool pIsWriteEnabled, bool pIsVisible)
		{
			this.mCubeName = pCubeName;
			this.mDimensionUniqueName = pUniqueName;
			this.mDimensionName = pName;
			this.mDimensionCaption = pCaption;
			this.mDescription = pDescription;
			this.mMasterName = pMasterName;
			this.mDimensionType = pType;
			this.mDimensionOrdinal = pOrdinal;
			this.mKeyAttributeCardinality = pCardinality;
			this.mIsWriteEnabled = pIsWriteEnabled;
			this.mIsVisible = pIsVisible;
			this.mHierarchyList = new MdObjectList<MdHierarchy>();
		}

		public MdDimension(DataRow pRow)
		{
			string str;
			string str1;
			this.mCubeName = (string)pRow["CUBE_NAME"];
			this.mDimensionUniqueName = (string)pRow["DIMENSION_UNIQUE_NAME"];
			this.mDimensionName = (string)pRow["DIMENSION_NAME"];
			this.mDimensionCaption = (string)pRow["DIMENSION_CAPTION"];
			str = (pRow["DESCRIPTION"] is DBNull ? string.Empty : (string)pRow["DESCRIPTION"]);
			this.mDescription = str;
			str1 = (pRow.Table.Columns.IndexOf("DIMENSION_MASTER_NAME") < 0 ? string.Empty : (string)pRow["DIMENSION_MASTER_NAME"]);
			this.mMasterName = str1;
			this.mDimensionType = (eDimensionType)((short)pRow["DIMENSION_TYPE"]);
			this.mDimensionOrdinal = (int)pRow["DIMENSION_ORDINAL"];
			this.mKeyAttributeCardinality = (int)pRow["DIMENSION_CARDINALITY"];
			this.mIsWriteEnabled = (bool)pRow["IS_READWRITE"];
			this.mIsVisible = (bool)pRow["DIMENSION_IS_VISIBLE"];
			this.mHierarchyList = new MdObjectList<MdHierarchy>();
		}

		public override string ToString()
		{
			return this.mDimensionCaption;
		}
	}
}