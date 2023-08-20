﻿/*
 *   _____                                ______
 *  /_   /  ____  ____  ____  _________  / __/ /_
 *    / /  / __ \/ __ \/ __ \/ ___/ __ \/ /_/ __/
 *   / /__/ /_/ / / / / /_/ /\_ \/ /_/ / __/ /_
 *  /____/\____/_/ /_/\__  /____/\____/_/  \__/
 *                   /____/
 *
 * Authors:
 *   钟峰(Popeye Zhong) <zongsoft@qq.com>
 * 
 * Copyright (C) 2015-2023 Zongsoft Corporation. All rights reserved.
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
using System.Security.Claims;

using Zongsoft.Security;

namespace Zongsoft.Community.Security
{
	public class UserIdentityTransformer : IClaimsIdentityTransformer
	{
		#region 单例字段
		public static readonly UserIdentityTransformer Instance = new();
		#endregion

		#region 私有构造
		private UserIdentityTransformer() { }
		#endregion

		#region 公共方法
		public bool CanTransform(ClaimsIdentity identity) => string.Equals(identity.AuthenticationType, UserIdentity.Scheme, StringComparison.OrdinalIgnoreCase);
		public object Transform(ClaimsIdentity identity) => identity.AsModel<UserIdentity>(this.OnTransform);
		#endregion

		#region 虚拟方法
		protected virtual bool OnTransform(UserIdentity user, Claim claim)
		{
			switch(claim.Type)
			{
				case nameof(UserIdentity.SiteId):
					user.SiteId = (claim.Value != null && uint.TryParse(claim.Value, out var siteId)) ? siteId : 0;
					return true;
				case nameof(UserIdentity.Gender):
					user.Gender = (claim.Value != null && Enum.TryParse<Models.Gender>(claim.Value, out var gender)) ? gender : Models.Gender.None;
					return true;
				case nameof(UserIdentity.Avatar):
					user.Avatar = claim.Value;
					return true;
				case nameof(UserIdentity.Grade):
					user.Grade = (claim.Value != null && byte.TryParse(claim.Value, out var grade)) ? grade : (byte)0;
					return true;
				case nameof(UserIdentity.TotalPosts):
					user.TotalPosts = (claim.Value != null && uint.TryParse(claim.Value, out var totalPosts)) ? totalPosts : 0;
					return true;
				case nameof(UserIdentity.TotalThreads):
					user.TotalThreads = (claim.Value != null && uint.TryParse(claim.Value, out var totalThreads)) ? totalThreads : 0;
					return true;
				default:
					return false;
			}
		}
		#endregion
	}
}