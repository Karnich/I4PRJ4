﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Model
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class Tag
	{
		public virtual string Text
		{
			get;
			set;
		}

		public virtual int TagId
		{
			get;
			set;
		}

		public virtual IEnumerable<QuizTagRelation> QuizTagRelation
		{
			get;
			set;
		}

	}
}

