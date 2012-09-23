//  * **************************************************************************
//  * Copyright (c) McCreary, Veselka, Bragg & Allen, P.C.
//  * This source code is subject to terms and conditions of the MIT License.
//  * A copy of the license can be found in the License.txt file
//  * at the root of this distribution. 
//  * By using this source code in any fashion, you are agreeing to be bound by 
//  * the terms of the MIT License.
//  * You must not remove this notice from this software.
//  * **************************************************************************

using System;
using System.Linq;

using QIFGet.MvbaCore.CodeQuery;

namespace QIFGet.MvbaCore.NamedConstants
{
	public static class NamedConstantExtensions
	{
		public static T DefaultValue<T>() where T : NamedConstant<T>
		{
			var fields = typeof(T).GetFields().ThatAreStatic();
			var defaultField = fields.WithAttributeOfType<DefaultKeyAttribute>().FirstOrDefault();
			if (defaultField == null)
			{
				return null;
			}
			return (T)defaultField.GetValue(null);
		}

		public static T OrDefault<T>(this T value) where T : NamedConstant<T>
		{
			if (value != null)
			{
				return value;
			}
			var defaultValue = DefaultValue<T>();
			if (defaultValue == null)
			{
				throw new ArgumentException("No default value defined for Named Constant type " + typeof(T));
			}
			return defaultValue;
		}
	}
}