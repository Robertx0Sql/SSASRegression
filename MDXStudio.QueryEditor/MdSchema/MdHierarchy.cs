using System;
using System.Data;

namespace MDXStudio.MdSchema
{
	internal class MdHierarchy : IMdObject
	{
		private string mCubeName;

		private string mDimensionUniqueName;

		private string mHierarchyUniqueName;

		private string mHierarchyName;

		private string mHierarchyCaption;

		private string mDescription;

		private string mAllMember;

		private string mDefaultMember;

		private string mDisplayFolder;

		private int mHierarchyOrdinal;

		private int mHierarchyCardinality;

		private bool mIsWriteEnabled;

		private bool mIsVisible;

		private eDimensionType mDimensionType;

		private eHierarchyStructure mStructure;

		private eHierarchyOrigin mHierarchyOrigin;

		private eInstanceSelection mInstanceSelection;

		private int mGroupingBehaviour;

		private string mUniqueCaption;

		private MdObjectList<MdLevel> mLevelList;

		public string AllMember
		{
			get
			{
				return this.mAllMember;
			}
		}

		public string Caption
		{
			get
			{
				return this.mHierarchyCaption;
			}
		}

		public int Cardinality
		{
			get
			{
				return this.mHierarchyCardinality;
			}
		}

		public string CubeName
		{
			get
			{
				return this.mCubeName;
			}
		}

		public string DefaultMember
		{
			get
			{
				return this.mDefaultMember;
			}
		}

		public string Description
		{
			get
			{
				return this.mDescription;
			}
		}

		public string DimensionUniqueName
		{
			get
			{
				return this.mDimensionUniqueName;
			}
		}

		public string DisplayFolder
		{
			get
			{
				return this.mDisplayFolder;
			}
		}

		public int GroupingBehaviour
		{
			get
			{
				return this.mGroupingBehaviour;
			}
		}

		public eInstanceSelection InstanceSelection
		{
			get
			{
				return this.mInstanceSelection;
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

		public MdObjectList<MdLevel> Levels
		{
			get
			{
				return this.mLevelList;
			}
		}

		public string Name
		{
			get
			{
				return this.mHierarchyName;
			}
		}

		public int Ordinal
		{
			get
			{
				return this.mHierarchyOrdinal;
			}
		}

		public eHierarchyOrigin Origin
		{
			get
			{
				return this.mHierarchyOrigin;
			}
		}

		public eHierarchyStructure Structure
		{
			get
			{
				return this.mStructure;
			}
		}

		public eDimensionType Type
		{
			get
			{
				return this.mDimensionType;
			}
		}

		public string UniqueCaption
		{
			get
			{
				return this.mUniqueCaption;
			}
		}

		public string UniqueName
		{
			get
			{
				return this.mHierarchyUniqueName;
			}
		}

		public MdHierarchy(string pCubeName, string pDimensionUniqueName, string pUniqueName, string pName, string pCaption, string pDescription, string pAllMember, string pDefaultMember, string pDisplayFolder, int pOrdinal, int pCardinality, bool pIsWriteEnabled, bool pIsVisible, eDimensionType pType, eHierarchyStructure pStructure, eHierarchyOrigin pOrigin, eInstanceSelection pInstanceSelection, int pGroupingBehaviour)
		{
			this.mCubeName = pCubeName;
			this.mDimensionUniqueName = pDimensionUniqueName;
			this.mHierarchyUniqueName = pUniqueName;
			this.mHierarchyName = pName;
			this.mHierarchyCaption = pCaption;
			this.mDescription = pDescription;
			this.mAllMember = pAllMember;
			this.mDefaultMember = pDefaultMember;
			this.mDisplayFolder = pDisplayFolder;
			this.mHierarchyOrdinal = pOrdinal;
			this.mHierarchyCardinality = pCardinality;
			this.mIsWriteEnabled = pIsWriteEnabled;
			this.mIsVisible = pIsVisible;
			this.mDimensionType = pType;
			this.mStructure = pStructure;
			this.mHierarchyOrigin = pOrigin;
			this.mInstanceSelection = pInstanceSelection;
			this.mGroupingBehaviour = pGroupingBehaviour;
			this.mLevelList = new MdObjectList<MdLevel>();
		}

		public MdHierarchy(DataRow pRow)
		{
			string str;
			string str1;
			string str2;
			string str3;
			bool flag;
			eHierarchyOrigin item;
			eInstanceSelection _eInstanceSelection;
			int num;
			this.mCubeName = (string)pRow["CUBE_NAME"];
			this.mDimensionUniqueName = (string)pRow["DIMENSION_UNIQUE_NAME"];
			this.mHierarchyUniqueName = (string)pRow["HIERARCHY_UNIQUE_NAME"];
			str = (pRow["HIERARCHY_NAME"] is DBNull ? string.Empty : (string)pRow["HIERARCHY_NAME"]);
			this.mHierarchyName = str;
			this.mHierarchyCaption = (string)pRow["HIERARCHY_CAPTION"];
			str1 = (pRow["DESCRIPTION"] is DBNull ? string.Empty : (string)pRow["DESCRIPTION"]);
			this.mDescription = str1;
			str2 = (pRow["ALL_MEMBER"] is DBNull ? string.Empty : (string)pRow["ALL_MEMBER"]);
			this.mAllMember = str2;
			this.mDefaultMember = (string)pRow["DEFAULT_MEMBER"];
			str3 = (pRow.Table.Columns.IndexOf("HIERARCHY_DISPLAY_FOLDER") < 0 || pRow["HIERARCHY_DISPLAY_FOLDER"] is DBNull ? string.Empty : (string)pRow["HIERARCHY_DISPLAY_FOLDER"]);
			this.mDisplayFolder = str3;
			this.mHierarchyOrdinal = (int)pRow["HIERARCHY_ORDINAL"];
			this.mHierarchyCardinality = (int)pRow["HIERARCHY_CARDINALITY"];
			this.mIsWriteEnabled = (bool)pRow["IS_READWRITE"];
			flag = (pRow.Table.Columns.IndexOf("HIERARCHY_IS_VISIBLE") < 0 ? true : (bool)pRow["HIERARCHY_IS_VISIBLE"]);
			this.mIsVisible = flag;
			this.mDimensionType = (eDimensionType)((short)pRow["DIMENSION_TYPE"]);
			this.mStructure = (eHierarchyStructure)((short)pRow["STRUCTURE"]);
			if (pRow.Table.Columns.IndexOf("HIERARCHY_ORIGIN") < 0)
			{
				item = eHierarchyOrigin.UserDefined;
			}
			else
			{
				item = (eHierarchyOrigin)((ushort)pRow["HIERARCHY_ORIGIN"]);
			}
			this.mHierarchyOrigin = item;
			if (pRow.Table.Columns.IndexOf("INSTANCE_SELECTION") < 0 || pRow["INSTANCE_SELECTION"] is DBNull)
			{
				_eInstanceSelection = eInstanceSelection.None;
			}
			else
			{
				_eInstanceSelection = (eInstanceSelection)((ushort)pRow["INSTANCE_SELECTION"]);
			}
			this.mInstanceSelection = _eInstanceSelection;
			num = (pRow.Table.Columns.IndexOf("GROUPING_BEHAVIOR") < 0 ? 0 : (int)((ushort)pRow["GROUPING_BEHAVIOR"]));
			this.mGroupingBehaviour = num;
			this.mLevelList = new MdObjectList<MdLevel>();
		}

		public void SetCaption(string pCaption)
		{
			this.mHierarchyCaption = pCaption;
		}

		public void SetUniqueCaption(string pString)
		{
			this.mUniqueCaption = pString;
		}

		public override string ToString()
		{
			return this.mUniqueCaption;
		}
	}
}