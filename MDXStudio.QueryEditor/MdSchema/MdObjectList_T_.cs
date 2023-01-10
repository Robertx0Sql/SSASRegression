using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace MDXStudio.MdSchema
{
	internal class MdObjectList<T> : IEnumerable<T>, IEnumerable
	where T : IMdObject
	{
		private List<T> mList;

		private SortedList<string, T> mUniqueNameList;

		private SortedList<string, T> mNameList;

		public int Count
		{
			get
			{
				return this.mList.Count;
			}
		}

		public T this[int pIndex]
		{
			get
			{
				return this.mList[pIndex];
			}
		}

		public T this[string pUniqueName]
		{
			get
			{
				return this.mUniqueNameList[pUniqueName];
			}
		}

		public MdObjectList()
		{
			this.mList = new List<T>();
			this.mUniqueNameList = new SortedList<string, T>();
			this.mNameList = new SortedList<string, T>();
		}

		public void Add(T pMdObject)
		{
			this.mList.Add(pMdObject);
			this.mUniqueNameList.Add(pMdObject.UniqueName, pMdObject);
			this.mNameList.Add(pMdObject.UniqueName, pMdObject);
		}

		public T GetByName(string pName)
		{
			return this.mNameList[pName];
		}

		public IEnumerator<T> GetEnumerator()
		{
			return this.mList.GetEnumerator();
		}

		public List<string> GetUNameList()
		{
			return new List<string>(this.mUniqueNameList.Keys);
		}

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.mList.GetEnumerator();
		}

		public bool TryGetValue(string pUniqueName, out T oValue)
		{
			return this.mUniqueNameList.TryGetValue(pUniqueName, out oValue);
		}
	}
}