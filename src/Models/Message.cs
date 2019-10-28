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
using System.ComponentModel;
using System.Collections.Generic;

using Zongsoft.Data;

namespace Zongsoft.Community.Models
{
	/// <summary>
	/// 表示消息的业务实体类。
	/// </summary>
	public abstract class Message
	{
		#region 公共属性
		public abstract ulong MessageId
		{
			get; set;
		}

		public abstract uint SiteId
		{
			get; set;
		}

		public abstract string Subject
		{
			get; set;
		}

		public abstract string Content
		{
			get; set;
		}

		public abstract string ContentType
		{
			get; set;
		}

		public abstract string MessageType
		{
			get; set;
		}

		public abstract string Referer
		{
			get; set;
		}

		[TypeConverter(typeof(TagsConverter))]
		public abstract string[] Tags
		{
			get; set;
		}

		public abstract uint? CreatorId
		{
			get; set;
		}

		public abstract UserProfile Creator
		{
			get; set;
		}

		public abstract DateTime CreatedTime
		{
			get; set;
		}
		#endregion

		#region 关联属性
		public abstract IEnumerable<MessageUser> Users
		{
			get; set;
		}
		#endregion

		#region 嵌套结构
		/// <summary>
		/// 表示消息接受人的业务实体类。
		/// </summary>
		public struct MessageUser
		{
			#region 构造函数
			public MessageUser(ulong messageId, uint userId, bool isRead = false)
			{
				this.MessageId = messageId;
				this.UserId = userId;
				this.IsRead = isRead;

				this.User = null;
				this.Message = null;
			}
			#endregion

			public ulong MessageId
			{
				get;
				set;
			}

			public Message Message
			{
				get;
				set;
			}

			public uint UserId
			{
				get;
				set;
			}

			public UserProfile User
			{
				get;
				set;
			}

			public bool IsRead
			{
				get;
				set;
			}
		}
		#endregion
	}

	/// <summary>
	/// 表示消息查询条件的实体类。
	/// </summary>
	public abstract class MessageConditional : ConditionalBase
	{
		#region 公共属性
		[Conditional(ConditionOperator.Like)]
		public abstract string Subject
		{
			get; set;
		}

		public abstract string MessageType
		{
			get; set;
		}

		public abstract string Referer
		{
			get; set;
		}

		public abstract uint? CreatorId
		{
			get; set;
		}

		public abstract Range<DateTime>? CreatedTime
		{
			get; set;
		}
		#endregion
	}
}
