using MDXParser;
using Microsoft.AnalysisServices.AdomdClient;
using System;

namespace MDXStudio
{
    internal class AdomdClientCubeInfo : MDXParser.CubeInfo
    {
        private string m_Cube;

        private AdomdConnection m_Cn;

        internal AdomdClientCubeInfo(string cubename, AdomdConnection cn)
        {
            this.m_Cube = cubename;
            this.m_Cn = cn;
        }

        public override MDXDataType DetermineType(string id)
        {
            MDXDataType mDXDataType;
            if (this.m_Cn == null || this.m_Cube == null)
            {
                return base.DetermineType(id);
            }
            DimensionCollection.Enumerator enumerator = this.m_Cn.Cubes[this.m_Cube].Dimensions.GetEnumerator();
        Label0:
            while (enumerator.MoveNext())
            {
                Dimension current = enumerator.Current;
                HierarchyCollection.Enumerator enumerator1 = current.Hierarchies.GetEnumerator();
                while (true)
                {
                    if (enumerator1.MoveNext())
                    {
                        Hierarchy hierarchy = enumerator1.Current;
                        LevelCollection.Enumerator enumerator2 = hierarchy.Levels.GetEnumerator();
                        while (enumerator2.MoveNext())
                        {
                            if (!id.Equals(enumerator2.Current.UniqueName, StringComparison.InvariantCultureIgnoreCase))
                            {
                                continue;
                            }
                            mDXDataType = MDXDataType.Level;
                            return mDXDataType;
                        }
                        if (id.Equals(hierarchy.UniqueName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            mDXDataType = MDXDataType.Hierarchy;
                            break;
                        }
                    }
                    else
                    {
                        if (!id.Equals(current.UniqueName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            goto Label0;
                        }
                        mDXDataType = MDXDataType.Dimension;
                        break;
                    }
                }
                return mDXDataType;
            }
            return base.DetermineType(id);
        }
    }
}