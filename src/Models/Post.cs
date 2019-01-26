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
using System.Collections.Generic;

namespace Zongsoft.Community.Models
{
	/// <summary>
	/// 表示帖子的业务实体类。
	/// </summary>
	public class Post : Zongsoft.Common.ModelBase
	{
		#region 成员字段
		private ulong _postId;
		private uint _siteId;
		private ulong _threadId;
		private bool _disabled;
		private bool _isApproved;
		private bool _isLocked;
		private bool _isValued;
		private uint _totalUpvotes;
		private uint _totalDownvotes;
		private uint _creatorId;
		private DateTime _createdTime;
		#endregion

		#region 构造函数
		public Post()
		{
			this.CreatedTime = DateTime.Now;
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取或设置帖子编号，主键。
		/// </summary>
		public ulong PostId
		{
			get
			{
				return _postId;
			}
			set
			{
				this.SetPropertyValue(() => this.PostId, ref _postId, value);
			}
		}

		/// <summary>
		/// 获取或设置帖子所属的站点编号。
		/// </summary>
		public uint SiteId
		{
			get
			{
				return _siteId;
			}
			set
			{
				this.SetPropertyValue(() => this.SiteId, ref _siteId, value);
			}
		}

		/// <summary>
		/// 获取或设置帖子所属的主题编号。
		/// </summary>
		public ulong ThreadId
		{
			get
			{
				return _threadId;
			}
			set
			{
				this.SetPropertyValue(() => this.ThreadId, ref _threadId, value);
			}
		}

		/// <summary>
		/// 获取或设置帖子所属的主题对象。
		/// </summary>
		public Thread Thread
		{
			get
			{
				return this.GetPropertyValue(() => this.Thread);
			}
			set
			{
				this.SetPropertyValue(() => this.Thread, value);
			}
		}

		/// <summary>
		/// 获取或设置帖子的内容。
		/// </summary>
		public string Content
		{
			get
			{
				return this.GetPropertyValue(() => this.Content);
			}
			set
			{
				this.SetPropertyValue(() => this.Content, value);
			}
		}

		/// <summary>
		/// 获取或设置帖子的内容类型。
		/// </summary>
		public string ContentType
		{
			get
			{
				return this.GetPropertyValue(() => this.ContentType);
			}
			set
			{
				this.SetPropertyValue(() => this.ContentType, value);
			}
		}

		/// <summary>
		/// 获取或设置一个值，表示是否禁用。
		/// </summary>
		public bool Disabled
		{
			get
			{
				return _disabled;
			}
			set
			{
				this.SetPropertyValue(() => this.Disabled, ref _disabled, value);
			}
		}

		/// <summary>
		/// 获取或设置一个值，表示帖子是否被审核通过。
		/// </summary>
		public bool IsApproved
		{
			get
			{
				return _isApproved;
			}
			set
			{
				this.SetPropertyValue(() => this.IsApproved, ref _isApproved, value);
			}
		}

		/// <summary>
		/// 获取或设置一个值，表示帖子是否被锁定，如果锁定则不允许回复。
		/// </summary>
		public bool IsLocked
		{
			get
			{
				return _isLocked;
			}
			set
			{
				this.SetPropertyValue(() => this.IsLocked, ref _isLocked, value);
			}
		}

		/// <summary>
		/// 获取或设置一个值，表示帖子是否为精华帖。
		/// </summary>
		public bool IsValued
		{
			get
			{
				return _isValued;
			}
			set
			{
				this.SetPropertyValue(() => this.IsValued, ref _isValued, value);
			}
		}

		/// <summary>
		/// 获取或设置帖子的累计点赞总数。
		/// </summary>
		public uint TotalUpvotes
		{
			get
			{
				return _totalUpvotes;
			}
			set
			{
				this.SetPropertyValue(() => this.TotalUpvotes, ref _totalUpvotes, value);
			}
		}

		/// <summary>
		/// 获取或设置帖子的累计被踩总数。
		/// </summary>
		public uint TotalDownvotes
		{
			get
			{
				return _totalDownvotes;
			}
			set
			{
				this.SetPropertyValue(() => this.TotalDownvotes, ref _totalDownvotes, value);
			}
		}

		/// <summary>
		/// 获取或设置访问者的位置信息。
		/// </summary>
		public string VisitorAddress
		{
			get
			{
				return this.GetPropertyValue(() => this.VisitorAddress);
			}
			set
			{
				this.SetPropertyValue(() => this.VisitorAddress, value);
			}
		}

		/// <summary>
		/// 获取或设置访问者的描述信息。
		/// </summary>
		public string VisitorDescription
		{
			get
			{
				return this.GetPropertyValue(() => this.VisitorDescription);
			}
			set
			{
				this.SetPropertyValue(() => this.VisitorDescription, value);
			}
		}

		/// <summary>
		/// 获取或设置帖子作者编号。
		/// </summary>
		public uint CreatorId
		{
			get
			{
				return _creatorId;
			}
			set
			{
				this.SetPropertyValue(() => this.CreatorId, ref _creatorId, value);
			}
		}

		/// <summary>
		/// 获取或设置帖子的创建时间。
		/// </summary>
		public DateTime CreatedTime
		{
			get
			{
				return _createdTime;
			}
			set
			{
				this.SetPropertyValue(() => this.CreatedTime, ref _createdTime, value);
			}
		}

		/// <summary>
		/// 获取或设置帖子作者对应的用户对象。
		/// </summary>
		public IUserProfile Creator
		{
			get
			{
				return this.GetPropertyValue(() => this.Creator);
			}
			set
			{
				this.SetPropertyValue(() => this.Creator, value);
			}
		}

		/// <summary>
		/// 获取或设置帖子的回复集。
		/// </summary>
		public IEnumerable<PostComment> Comments
		{
			get
			{
				return this.GetPropertyValue(() => this.Comments);
			}
			set
			{
				this.SetPropertyValue(() => this.Comments, value);
			}
		}

		/// <summary>
		/// 获取或设置帖子的附件集。
		/// </summary>
		public IEnumerable<PostAttachment> Attachments
		{
			get
			{
				return this.GetPropertyValue(() => this.Attachments);
			}
			set
			{
				this.SetPropertyValue(() => this.Attachments, value);
			}
		}

		/// <summary>
		/// 获取或设置帖子的投票（点赞和被踩）记录集。
		/// </summary>
		public IEnumerable<PostVoting> Votes
		{
			get
			{
				return this.GetPropertyValue(() => this.Votes);
			}
			set
			{
				this.SetPropertyValue(() => this.Votes, value);
			}
		}

		/// <summary>
		/// 获取帖子的点赞记录集。
		/// </summary>
		public IEnumerable<PostVoting> Upvotes
		{
			get
			{
				return this.Votes.Where(p => p.Value > 0);
			}
		}

		/// <summary>
		/// 获取帖子的被踩记录集。
		/// </summary>
		public IEnumerable<PostVoting> Downvotes
		{
			get
			{
				return this.Votes.Where(p => p.Value < 0);
			}
		}
		#endregion

		#region 嵌套子类
		/// <summary>
		/// 表示帖子回复的业务实体类。
		/// </summary>
		public class PostComment
		{
			#region 成员字段
			private ulong _postId;
			private ushort _serialId;
			private ushort _sourceId;
			private string _content;
			private string _contentType;
			private string _visitorAddress;
			private string _visitorDescription;
			private uint _creatorId;
			private IUserProfile _creator;
			private DateTime _createdTime;
			#endregion

			#region 构造函数
			public PostComment()
			{
			}
			#endregion

			#region 公共属性
			/// <summary>
			/// 获取或设置帖子编号。
			/// </summary>
			public ulong PostId
			{
				get
				{
					return _postId;
				}
				set
				{
					_postId = value;
				}
			}

			/// <summary>
			/// 获取或设置回复序号。
			/// </summary>
			public ushort SerialId
			{
				get
				{
					return _serialId;
				}
				set
				{
					_serialId = value;
				}
			}

			/// <summary>
			/// 获取或设置回复关联的源回复序号。
			/// </summary>
			public ushort SourceId
			{
				get
				{
					return _sourceId;
				}
				set
				{
					_sourceId = value;
				}
			}

			/// <summary>
			/// 获取或设置回复的内容。
			/// </summary>
			public string Content
			{
				get
				{
					return _content;
				}
				set
				{
					_content = value;
				}
			}

			/// <summary>
			/// 获取或设置回复的内容类型。
			/// </summary>
			public string ContentType
			{
				get
				{
					return _contentType;
				}
				set
				{
					_contentType = value;
				}
			}

			/// <summary>
			/// 获取或设置访问者地址。
			/// </summary>
			public string VisitorAddress
			{
				get
				{
					return _visitorAddress;
				}
				set
				{
					_visitorAddress = value;
				}
			}

			/// <summary>
			/// 获取或设置访问者描述信息。
			/// </summary>
			public string VisitorDescription
			{
				get
				{
					return _visitorDescription;
				}
				set
				{
					_visitorDescription = value;
				}
			}

			/// <summary>
			/// 获取或设置回复人编号。
			/// </summary>
			public uint CreatorId
			{
				get
				{
					return _creatorId;
				}
				set
				{
					_creatorId = value;
				}
			}

			/// <summary>
			/// 获取或设置回复人对象。
			/// </summary>
			public IUserProfile Creator
			{
				get
				{
					return _creator;
				}
				set
				{
					_creator = value;
				}
			}

			/// <summary>
			/// 获取或设置回复时间。
			/// </summary>
			public DateTime CreatedTime
			{
				get
				{
					return _createdTime;
				}
				set
				{
					_createdTime = value;
				}
			}
			#endregion
		}

		/// <summary>
		/// 表示帖子投票的业务实体类。
		/// </summary>
		public class PostVoting
		{
			#region 成员字段
			private ulong _postId;
			private uint _userId;
			private IUserProfile _user;
			private string _userName;
			private string _userAvatar;
			private sbyte _value;
			private DateTime _timestamp;
			#endregion

			#region 构造函数
			public PostVoting()
			{
				_timestamp = DateTime.Now;
			}

			public PostVoting(ulong postId, uint userId, sbyte value)
			{
				_postId = postId;
				_userId = userId;
				_value = value;
				_timestamp = DateTime.Now;
			}
			#endregion

			#region 公共属性
			/// <summary>
			/// 获取或设置投票关联的帖子编号。
			/// </summary>
			public ulong PostId
			{
				get
				{
					return _postId;
				}
				set
				{
					_postId = value;
				}
			}

			/// <summary>
			/// 获取或设置投票的用户编号。
			/// </summary>
			public uint UserId
			{
				get
				{
					return _userId;
				}
				set
				{
					_userId = value;
				}
			}

			/// <summary>
			/// 获取或设置投票的用户对象。
			/// </summary>
			public IUserProfile User
			{
				get
				{
					return _user;
				}
				set
				{
					_user = value;
				}
			}

			/// <summary>
			/// 获取或设置投票的用户名称。
			/// </summary>
			public string UserName
			{
				get
				{
					return _userName;
				}
				set
				{
					_userName = value;
				}
			}

			/// <summary>
			/// 获取或设置投票的用户头像。
			/// </summary>
			public string UserAvatar
			{
				get
				{
					return _userAvatar;
				}
				set
				{
					_userAvatar = value;
				}
			}

			/// <summary>
			/// 获取或设置投票值（正数为赞，负数为踩）。
			/// </summary>
			public sbyte Value
			{
				get
				{
					return _value;
				}
				set
				{
					_value = value;
				}
			}

			/// <summary>
			/// 获取或设置投票的时间。
			/// </summary>
			public DateTime Timestamp
			{
				get
				{
					return _timestamp;
				}
				set
				{
					_timestamp = value;
				}
			}
			#endregion
		}

		/// <summary>
		/// 表示帖子附件的业务实体类。
		/// </summary>
		public class PostAttachment
		{
			#region 成员字段
			private ulong _postId;
			private ulong _fileId;
			private IFile _file;
			#endregion

			#region 构造函数
			public PostAttachment()
			{
			}

			public PostAttachment(ulong postId, ulong fileId)
			{
				_postId = postId;
				_fileId = fileId;
			}
			#endregion

			#region 公共属性
			/// <summary>
			/// 获取或设置帖子编号。
			/// </summary>
			public ulong PostId
			{
				get
				{
					return _postId;
				}
				set
				{
					_postId = value;
				}
			}

			/// <summary>
			/// 获取或设置帖子的文件编号。
			/// </summary>
			public ulong FileId
			{
				get
				{
					return _fileId;
				}
				set
				{
					_fileId = value;
				}
			}

			/// <summary>
			/// 获取或设置帖子的文件对象。
			/// </summary>
			public IFile File
			{
				get
				{
					return _file;
				}
				set
				{
					_file = value;
				}
			}
			#endregion
		}
		#endregion
	}
}
