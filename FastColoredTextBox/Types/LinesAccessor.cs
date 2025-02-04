﻿using System;
using System.Collections.Generic;

namespace FastColoredTextBoxNS.Types {
	public class LinesAccessor : IList<string> {
		readonly IList<Line> ts;

		public LinesAccessor(IList<Line> ts) => this.ts = ts;

		public int IndexOf(string item) {
			for (int i = 0; i < ts.Count; i++)
				if (ts[i].Text == item)
					return i;

			return -1;
		}

		public void Insert(int index, string item) => throw new NotImplementedException();
		public void RemoveAt(int index) => throw new NotImplementedException();

		public string this[int index] {
			get {
				return ts[index].Text;
			}
			set {
				throw new NotImplementedException();
			}
		}

		public void Add(string item) => throw new NotImplementedException();
		public void Clear() => throw new NotImplementedException();

		public bool Contains(string item) {
			for (int i = 0; i < ts.Count; i++)
				if (ts[i].Text == item)
					return true;

			return false;
		}

		public void CopyTo(string[] array, int arrayIndex) {
			for (int i = 0; i < ts.Count; i++)
				array[i + arrayIndex] = ts[i].Text;
		}

		public int Count => ts.Count;
		public bool IsReadOnly => true;
		public bool Remove(string item) => throw new NotImplementedException();

		public IEnumerator<string> GetEnumerator() {
			for (int i = 0; i < ts.Count; i++)
				yield return ts[i].Text;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}
