using System;
using System.Data;

namespace MDXStudio.MdSchema
{
	internal class MdMeasureGroupDimension
	{
		private string mCubeName;

		private string mMeasureGroupName;

		private eRelationCardinality mMeasureGroupCardinality;

		private string mDimensionUniqueName;

		private eRelationCardinality mDimensionCardinality;

		private bool mIsDimensionVisible;

		private bool mIsFactDimension;

		private string mDimensionGranularity;

		public string CubeName
		{
			get
			{
				return this.mCubeName;
			}
		}

		public eRelationCardinality DimensionCardinality
		{
			get
			{
				return this.mDimensionCardinality;
			}
		}

		public string DimensionGranularity
		{
			get
			{
				return this.mDimensionGranularity;
			}
		}

		public string DimensionUniqueName
		{
			get
			{
				return this.mDimensionUniqueName;
			}
		}

		public bool IsDimensionVisible
		{
			get
			{
				return this.mIsDimensionVisible;
			}
		}

		public bool IsFactDimension
		{
			get
			{
				return this.mIsFactDimension;
			}
		}

		public eRelationCardinality MeasureGroupCardinality
		{
			get
			{
				return this.mMeasureGroupCardinality;
			}
		}

		public string MeasureGroupName
		{
			get
			{
				return this.mMeasureGroupName;
			}
		}

		public MdMeasureGroupDimension(string pCubeName, string pMeasureGroupName, eRelationCardinality pMeasureGroupCardinality, string pDimensionUniqueName, eRelationCardinality pDimensionCradinality, bool pIsDimensionVisible, bool pIsFactDimension, string pDimensionGranularity)
		{
			this.mCubeName = pCubeName;
			this.mMeasureGroupName = pMeasureGroupName;
			this.mMeasureGroupCardinality = pMeasureGroupCardinality;
			this.mDimensionUniqueName = pDimensionUniqueName;
			this.mDimensionCardinality = pDimensionCradinality;
			this.mIsDimensionVisible = pIsDimensionVisible;
			this.mIsFactDimension = pIsFactDimension;
			this.mDimensionGranularity = pDimensionGranularity;
		}

		public MdMeasureGroupDimension(DataRow pRow)
		{
			eRelationCardinality _eRelationCardinality;
			eRelationCardinality _eRelationCardinality1;
			this.mCubeName = (string)pRow["CUBE_NAME"];
			this.mMeasureGroupName = (string)pRow["MEASUREGROUP_NAME"];
			_eRelationCardinality = ((string)pRow["MEASUREGROUP_CARDINALITY"] == "MANY" ? eRelationCardinality.Many : eRelationCardinality.One);
			this.mMeasureGroupCardinality = _eRelationCardinality;
			this.mDimensionUniqueName = (string)pRow["DIMENSION_UNIQUE_NAME"];
			_eRelationCardinality1 = ((string)pRow["DIMENSION_CARDINALITY"] == "MANY" ? eRelationCardinality.Many : eRelationCardinality.One);
			this.mDimensionCardinality = _eRelationCardinality1;
			this.mIsDimensionVisible = (bool)pRow["DIMENSION_IS_VISIBLE"];
			this.mIsFactDimension = (bool)pRow["DIMENSION_IS_FACT_DIMENSION"];
			this.mDimensionGranularity = (string)pRow["DIMENSION_GRANULARITY"];
		}

		public override string ToString()
		{
			return string.Concat(this.mMeasureGroupName, " - ", this.mDimensionUniqueName);
		}
	}
}