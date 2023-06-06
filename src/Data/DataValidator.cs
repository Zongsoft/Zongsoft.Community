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
 * Copyright (C) 2015-2019 Zongsoft Corporation. All rights reserved.
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
using System.Collections.Generic;

using Zongsoft.Community.Models;
using Zongsoft.Data;
using Zongsoft.Data.Metadata;
using Zongsoft.Security;
using Zongsoft.Services;

namespace Zongsoft.Community.Data
{
	public class DataValidator : IDataValidator
	{
		#region 委托定义
		private delegate bool TryGetDelegate(IDataMutateContextBase context, out object value);
		#endregion

		#region 常量定义
		private const string SITE_ID = "SiteId";
		#endregion

		#region 静态字段
		private static readonly IDictionary<string, TryGetDelegate> _inserts;
		private static readonly IDictionary<string, TryGetDelegate> _updates;
		#endregion

		#region 静态构造
		static DataValidator()
		{
			_inserts = new Dictionary<string, TryGetDelegate>(StringComparer.OrdinalIgnoreCase)
			{
				{ "SiteId", TryGetSiteId },

				{ "CreatorId",  TryGetUserId },
				{ "CreatedTime", TryGetTimestamp },
				{ "Creation", TryGetTimestamp },
			};

			_updates = new Dictionary<string, TryGetDelegate>(StringComparer.OrdinalIgnoreCase)
			{
				{ "ModifierId",  TryGetUserId },
				{ "ModifiedTime", TryGetTimestamp },
				{ "Modification", TryGetTimestamp },
			};
		}
		#endregion

		#region 公共方法
		public ICondition Validate(IDataAccessContextBase context, ICondition criteria)
		{
			if(!ApplicationContext.Current.Principal.Identity.TryGetClaim(SITE_ID, out var value) || !uint.TryParse(value, out var siteId))
				return criteria;

			if(criteria == null)
				return Condition.Equal(SITE_ID, siteId);

			if(criteria.Matches(SITE_ID, matched => matched.Value = siteId) > 0)
				return criteria;

			if(criteria is ConditionCollection conditions && conditions.Combination == ConditionCombination.And)
			{
				conditions.Add(Condition.Equal(SITE_ID, siteId));
				return conditions;
			}

			return ConditionCollection.And(Condition.Equal(SITE_ID, siteId), criteria);
		}

		public bool OnInsert(IDataMutateContextBase context, IDataEntityProperty property, out object value)
		{
			if(_inserts.TryGetValue(property.Name, out var factory))
				return factory.Invoke(context, out value);

			value = null;
			return false;
		}

		public bool OnUpdate(IDataMutateContextBase context, IDataEntityProperty property, out object value)
		{
			if(_updates.TryGetValue(property.Name, out var factory))
				return factory.Invoke(context, out value);

			value = null;
			return false;
		}
		#endregion

		#region 私有方法
		private static bool TryGetUserId(IDataMutateContextBase context, out object value)
		{
			value = null;

			if(ApplicationContext.Current?.Principal is CredentialPrincipal principal && principal.Identity.IsAuthenticated)
				value = principal.Identity.GetIdentifier<uint>();

			return value != null;
		}

		private static bool TryGetSiteId(IDataMutateContextBase context, out object value)
		{
			if(ApplicationContext.Current?.Principal is CredentialPrincipal principal &&
			   principal.Identity.IsAuthenticated &&
			   principal.Identity.TryGetClaim<uint>(nameof(UserProfile.SiteId), out var siteId))
			{
					value = siteId;
					return true;
			}

			value = null;
			return false;
		}

		private static bool TryGetTimestamp(IDataMutateContextBase context, out object value)
		{
			value = DateTime.Now;
			return true;
		}
		#endregion
	}
}