using System.Data;

namespace PicoMVVM.Infrastructure.Options
{
	public class Some<T> : Option<T>
	{
		public Some(T value)
		{
			if (value == null)
				throw new NoNullAllowedException("Null value passed in, did you mean None()");
			Value = value;
		}

		public override T Value { get; set; }

		#region Overrides of Option<T>

		public override bool IsSome()
		{
			return true;
		}

		public override bool IsNone()
		{
			return false;
		}

		public override bool Equals(Option<T> other)
		{
			return other != null
			       && other.IsSome()
			       && ((other as Some<T>).Value.Equals((this).Value));
		}

		#endregion

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}
	}
}