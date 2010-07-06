using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;


namespace FluentNHibernate.Utils
{
	public static class ReflectionHelper
	{
		public static bool IsMethodExpression< TModel >( Expression< Func< TModel, object > > expression )
		{
			return IsMethodExpression< TModel, object >( expression );
		}


		public static bool IsMethodExpression< TModel, TResult >( Expression< Func< TModel, TResult > > expression )
		{
			return expression.Body is MethodCallExpression;
		}


		public static bool IsPropertyExpression< TModel >( Expression< Func< TModel, object > > expression )
		{
			return GetMemberExpression( expression, false ) != null;
		}


		public static PropertyInfo GetProperty< TModel >( Expression< Func< TModel, object > > expression )
		{
			bool isExpressionOfDynamicComponent = expression.ToString().Contains( "get_Item" );

			if ( isExpressionOfDynamicComponent )
				return GetDynamicComponentProperty( expression );

			MemberExpression memberExpression = GetMemberExpression( expression );

			return (PropertyInfo)memberExpression.Member;
		}


		static PropertyInfo GetDynamicComponentProperty< TModel, T >( Expression< Func< TModel, T > > expression )
		{
			Type desiredConversionType = null;
			MethodCallExpression methodCallExpression = null;
			Expression nextOperand = expression.Body;

			while ( nextOperand != null )
			{
				if ( nextOperand.NodeType == ExpressionType.Call )
				{
					methodCallExpression = nextOperand as MethodCallExpression;
					desiredConversionType = desiredConversionType ?? methodCallExpression.Method.ReturnType;
					break;
				}

				if ( nextOperand.NodeType != ExpressionType.Convert )
					throw new ArgumentException( "Expression not supported", "expression" );

				var unaryExpression = (UnaryExpression)nextOperand;
				desiredConversionType = unaryExpression.Type;
				nextOperand = unaryExpression.Operand;
			}

			var constExpression = methodCallExpression.Arguments[0] as ConstantExpression;

			return new DummyPropertyInfo( (string)constExpression.Value, desiredConversionType );
		}


		public static PropertyInfo GetProperty< TModel, T >( Expression< Func< TModel, T > > expression )
		{
			bool isExpressionOfDynamicComponent = expression.ToString().Contains( "get_Item" );

			if ( isExpressionOfDynamicComponent )
				return GetDynamicComponentProperty( expression );

			MemberExpression memberExpression = GetMemberExpression( expression );

			return (PropertyInfo)memberExpression.Member;
		}


		static MemberExpression GetMemberExpression< TModel, T >( Expression< Func< TModel, T > > expression )
		{
			return GetMemberExpression( expression, true );
		}


		static MemberExpression GetMemberExpression< TModel, T >( Expression< Func< TModel, T > > expression, bool enforceCheck )
		{
			MemberExpression memberExpression = null;
			if ( expression.Body.NodeType == ExpressionType.Convert )
			{
				var body = (UnaryExpression)expression.Body;
				memberExpression = body.Operand as MemberExpression;
			}
			else if ( expression.Body.NodeType == ExpressionType.MemberAccess )
				memberExpression = expression.Body as MemberExpression;

			if ( enforceCheck && memberExpression == null )
				throw new ArgumentException( "Not a member access", "expression" );

			return memberExpression;
		}


		public static Accessor GetAccessor< MODEL >( Expression< Func< MODEL, object > > expression )
		{
			MemberExpression memberExpression = GetMemberExpression( expression );

			return getAccessor( memberExpression );
		}


		static Accessor getAccessor( MemberExpression memberExpression )
		{
			var list = new List< PropertyInfo >();

			while ( memberExpression != null )
			{
				list.Add( (PropertyInfo)memberExpression.Member );
				memberExpression = memberExpression.Expression as MemberExpression;
			}

			if ( list.Count == 1 )
				return new SingleProperty( list[0] );

			list.Reverse();
			return new PropertyChain( list.ToArray() );
		}


		public static Accessor GetAccessor< MODEL, T >( Expression< Func< MODEL, T > > expression )
		{
			MemberExpression memberExpression = GetMemberExpression( expression );

			return getAccessor( memberExpression );
		}


		public static MethodInfo GetMethod< T >( Expression< Func< T, object > > expression )
		{
			var methodCall = (MethodCallExpression)expression.Body;
			return methodCall.Method;
		}


		public static MethodInfo GetMethod< T, TResult >( Expression< Func< T, TResult > > expression )
		{
			var methodCall = (MethodCallExpression)expression.Body;
			return methodCall.Method;
		}


		public static MethodInfo GetMethod< T, U, V >( Expression< Func< T, U, V > > expression )
		{
			var methodCall = (MethodCallExpression)expression.Body;
			return methodCall.Method;
		}


		public static MethodInfo GetMethod( Expression< Func< object > > expression )
		{
			var methodCall = (MethodCallExpression)expression.Body;
			return methodCall.Method;
		}
	}
}