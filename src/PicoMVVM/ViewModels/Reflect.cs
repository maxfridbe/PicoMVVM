using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace PicoMVVM.ViewModels
{
	public static class Reflect
	{
		public static PropertyInfo PropertyOf<TDeclaring, T>(Expression<Func<TDeclaring, T>> expression)
		{
			var memberExpr = expression.Body as MemberExpression;
			if (memberExpr == null)
				throw new ArgumentException("Expression \"" + expression + "\" is not a valid member expression.");
			var property = memberExpr.Member as PropertyInfo;
			if (property == null)
				throw new ArgumentException("Expression \"" + expression + "\" does not reference a property.");
			return property;
		}
	}
}
