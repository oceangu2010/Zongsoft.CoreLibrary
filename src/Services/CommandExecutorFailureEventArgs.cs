﻿/*
 * Authors:
 *   钟峰(Popeye Zhong) <zongsoft@gmail.com>
 *
 * Copyright (C) 2016 Zongsoft Corporation <http://www.zongsoft.com>
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

namespace Zongsoft.Services
{
	[Serializable]
	public class CommandExecutorFailureEventArgs : Zongsoft.Diagnostics.FailureEventArgs
	{
		#region 成员字段
		private CommandExecutorContext _context;
		#endregion

		#region 构造函数
		public CommandExecutorFailureEventArgs(CommandExecutorContext context, Exception exception) : base(exception, false)
		{
			if(context == null)
				throw new ArgumentNullException(nameof(context));

			_context = context;
		}

		public CommandExecutorFailureEventArgs(CommandExecutorContext context, Exception exception, bool handled) : base(exception, handled)
		{
			if(context == null)
				throw new ArgumentNullException(nameof(context));

			_context = context;
		}
		#endregion

		#region 公共属性
		public CommandExecutorContext Context
		{
			get
			{
				return _context;
			}
		}
		#endregion
	}
}