namespace PicoMVVM.Infrastructure.Options
{
	public class None<T> : Option<T>
	{
		#region Overrides of Option<T>

		public override bool IsSome()
		{
			return false;
		}

		public override bool IsNone()
		{
			return true;
		}

		public override bool Equals(Option<T> other)
		{
			return other != null
			       && other.IsNone();
		}

		public override T Value { get; set; }
		#endregion

		public override int GetHashCode()
		{
			return 0;
		}
	}
}