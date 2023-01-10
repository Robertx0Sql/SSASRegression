using System;
using System.Data;

namespace MDXStudio.MdSchema
{
	internal class MdSet : IMdObject
	{
		private string mCubeName;

		private string mSetUniqueName;

		private string mSetName;

		private string mSetCaption;

		private string mDescription;

		private int mScope;

		private string mExpression;

		private string mDimensions;

		private string mDisplayFolder;

		public string Caption
		{
			get
			{
				return this.mSetCaption;
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

		public string Dimensions
		{
			get
			{
				return this.mDimensions;
			}
		}

		public string DisplayFolder
		{
			get
			{
				return this.mDisplayFolder;
			}
		}

		public string Expression
		{
			get
			{
				return this.mExpression;
			}
		}

		public string Name
		{
			get
			{
				return this.mSetName;
			}
		}

		public int Scope
		{
			get
			{
				return this.mScope;
			}
		}

		public string UniqueName
		{
			get
			{
				return this.mSetUniqueName;
			}
		}

		public MdSet(DataRow pRow)
		{
			string str;
			string str1;
			this.mCubeName = (string)pRow["CUBE_NAME"];
			this.mSetName = (string)pRow["SET_NAME"];
			this.mSetUniqueName = string.Concat("[", this.mSetName, "]");
			this.mSetCaption = (string)pRow["SET_CAPTION"];
			str = (pRow["DESCRIPTION"] is DBNull ? string.Empty : (string)pRow["DESCRIPTION"]);
			this.mDescription = str;
			this.mScope = (int)pRow["SCOPE"];
			this.mExpression = pRow["EXPRESSION"] as string;
			this.mDimensions = (string)pRow["DIMENSIONS"];
			str1 = (pRow.Table.Columns.IndexOf("SET_DISPLAY_FOLDER") < 0 || pRow["SET_DISPLAY_FOLDER"] is DBNull ? string.Empty : (string)pRow["SET_DISPLAY_FOLDER"]);
			this.mDisplayFolder = str1;
		}

		public override string ToString()
		{
			return this.mSetCaption;
		}
	}
}