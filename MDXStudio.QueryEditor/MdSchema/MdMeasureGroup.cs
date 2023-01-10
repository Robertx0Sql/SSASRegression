using System;
using System.Collections.Generic;
using System.Data;

namespace MDXStudio.MdSchema
{
	internal class MdMeasureGroup : IMdObject
	{
		private string mCubeName;

		private string mMeasureGroupName;

		private string mMeasureGroupCaption;

		private string mDescription;

		private bool mIsWriteEnabled;

		private List<MdMeasureGroupDimension> mMeasureGroupDimensionList;

		public string Caption
		{
			get
			{
				return this.mMeasureGroupCaption;
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

		public List<MdMeasureGroupDimension> Dimensions
		{
			get
			{
				return this.mMeasureGroupDimensionList;
			}
		}

		public bool IsWriteEnabled
		{
			get
			{
				return this.mIsWriteEnabled;
			}
		}

		public string Name
		{
			get
			{
				return this.mMeasureGroupName;
			}
		}

		public string UniqueName
		{
			get
			{
				return this.mMeasureGroupName;
			}
		}

		public MdMeasureGroup(string pCubeName, string pName, string pCaption, string pDescription, bool pIsWriteEnabled)
		{
			this.mCubeName = pCubeName;
			this.mMeasureGroupName = pName;
			this.mMeasureGroupCaption = pCaption;
			this.mDescription = pDescription;
			this.mIsWriteEnabled = pIsWriteEnabled;
			this.mMeasureGroupDimensionList = new List<MdMeasureGroupDimension>();
		}

		public MdMeasureGroup(DataRow pRow)
		{
			this.mCubeName = (string)pRow["CUBE_NAME"];
			this.mMeasureGroupName = (string)pRow["MEASUREGROUP_NAME"];
			this.mMeasureGroupCaption = (string)pRow["MEASUREGROUP_CAPTION"];
			this.mDescription = (string)pRow["DESCRIPTION"];
			this.mIsWriteEnabled = (bool)pRow["IS_WRITE_ENABLED"];
			this.mMeasureGroupDimensionList = new List<MdMeasureGroupDimension>();
		}

		public override string ToString()
		{
			return this.mMeasureGroupCaption;
		}
	}
}