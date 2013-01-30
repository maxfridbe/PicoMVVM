using System;

namespace PicoMVVM.Infrastructure.Options
{
	public abstract class Option<T> : IEquatable<Option<T>>
	{
		public abstract bool IsSome();

		public abstract bool IsNone();

		public abstract T Value { get; set; }

		#region Implementation of IEquatable<T>

		public abstract bool Equals(Option<T> other);

		public override bool Equals(object obj)
		{
			var opt = obj as Option<T>;

			return opt != null && Equals(opt);
		}

		#endregion

	}

	public static class Option
	{
		public static Option<T> Some<T>(T val)
		{
			return new Some<T>(val);
		}
		public static Option<T> None<T>()
		{
			return new None<T>();
		}
	}
}