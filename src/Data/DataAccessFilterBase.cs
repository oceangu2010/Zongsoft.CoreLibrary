﻿/*
 * Authors:
 *   钟峰(Popeye Zhong) <zongsoft@gmail.com>
 *
 * Copyright (C) 2010-2017 Zongsoft Corporation <http://www.zongsoft.com>
 *
 * This file is part of Zongsoft.CoreLibrary.
 *
 * Zongsoft.CoreLibrary is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * Zongsoft.CoreLibrary is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * Lesser General Public License for more details.
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with Zongsoft.CoreLibrary; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA
 */

using System;
using System.Linq;
using System.Collections.Generic;

namespace Zongsoft.Data
{
	public abstract class DataAccessFilterBase : IDataAccessFilter, Zongsoft.Services.IPredication<DataAccessFilterContext>
	{
		#region 成员字段
		private string[] _names;
		private DataAccessMethod[] _methods;
		#endregion

		#region 构造函数
		protected DataAccessFilterBase(params string[] names)
		{
			_names = names;
		}

		protected DataAccessFilterBase(DataAccessMethod method, params string[] names)
		{
			_methods = new DataAccessMethod[] { method };
			_names = names;
		}

		protected DataAccessFilterBase(IEnumerable<DataAccessMethod> methods, params string[] names)
		{
			if(methods != null)
				_methods = methods.ToArray();

			_names = names;
		}
		#endregion

		#region 保护属性
		protected virtual Zongsoft.Security.CredentialPrincipal Principal
		{
			get
			{
				return Zongsoft.ComponentModel.ApplicationContextBase.Current.Principal as Zongsoft.Security.CredentialPrincipal;
			}
		}
		#endregion

		#region 抽象方法
		public abstract void OnExecuted(DataAccessFilterContext context);
		public abstract void OnExecuting(DataAccessFilterContext context);
		#endregion

		#region 断言方法
		public virtual bool Predicate(DataAccessFilterContext context)
		{
			var result = true;

			if(result && (_methods != null && _methods.Length > 0))
				result &= _methods.Contains(context.Method);

			if(result && (_names != null && _names.Length > 0))
				result &= _names.Contains(context.Name, StringComparer.OrdinalIgnoreCase);

			return result;
		}

		bool Zongsoft.Services.IPredication.Predicate(object parameter)
		{
			var context = parameter as DataAccessFilterContext;

			if(context == null)
				return false;
			else
				return this.Predicate(context);
		}
		#endregion
	}
}
