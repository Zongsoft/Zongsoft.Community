﻿/*
 * Authors:
 *   钟峰(Popeye Zhong) <zongsoft@qq.com>
 * 
 * Copyright (C) 2015-2017 Zongsoft Corporation. All rights reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Linq;
using System.Collections;

using Zongsoft.Data;
using Zongsoft.Security.Membership;

namespace Zongsoft.Community.Data
{
	public class UserFilter : DataAccessFilterBase
	{
		#region 常量定义
		private const string DELETED_RESULTS = "deleted:users";
		#endregion

		#region 构造函数
		public UserFilter() : base(new DataAccessMethod[] { DataAccessMethod.Delete }, typeof(Models.IUserProfile), typeof(User))
		{
		}
		#endregion

		#region 重写方法
		protected override void OnDeleting(DataDeleteContextBase context)
		{
			if(string.Equals(context.Name, context.DataAccess.Naming.Get<Models.IUserProfile>(), StringComparison.OrdinalIgnoreCase))
				context.States[DELETED_RESULTS] = context.DataAccess.Select<Models.IUserProfile>(context.Name, context.Condition, Paging.Disable).ToArray();
		}

		protected override void OnDeleted(DataDeleteContextBase context)
		{
			if(context.States.TryGetValue(DELETED_RESULTS, out var items))
				this.OnDeleted(items as IEnumerable);
		}
		#endregion

		#region 私有方法
		private void OnDeleted(IEnumerable items)
		{
			if(items == null)
				return;

			foreach(var item in items)
			{
				var profile = item as Models.IUserProfile;

				if(profile != null && !string.IsNullOrWhiteSpace(profile.PhotoPath))
					Zongsoft.IO.FileSystem.File.DeleteAsync(profile.PhotoPath);
			}
		}
		#endregion
	}
}
