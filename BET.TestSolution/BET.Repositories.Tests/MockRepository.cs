using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Moq;

namespace BET.Repositories.Tests
{
	public static class MockRepository
	{
		
		public static DbSet<T> GetQueryableMockDbSet<T>(params T[] sourceList) where T : class
		{
			var queryable = sourceList.AsQueryable();
			var dbSet = new Mock<DbSet<T>>();
			dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
			dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
			dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
			dbSet.As<IDbAsyncEnumerable<T>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<T>(sourceList.ToList().GetEnumerator()));
			dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<T>(sourceList.ToList().AsQueryable().Provider));

			return dbSet.Object;
		}

		internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
		{

			private readonly IEnumerator<T> _inner;

			public TestDbAsyncEnumerator(IEnumerator<T> inner) { _inner = inner; }
			public void Dispose() { _inner.Dispose(); }
			public Task<bool> MoveNextAsync(CancellationToken cancellationToken) { return Task.FromResult(_inner.MoveNext()); }
			public T Current { get { return _inner.Current; } }
			object IDbAsyncEnumerator.Current { get { return Current; } }
		}

		internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>
		{
			public TestDbAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable) { }
			public TestDbAsyncEnumerable(Expression expression) : base(expression) { }
			public IDbAsyncEnumerator<T> GetAsyncEnumerator() { return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator()); }
			IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator() { return GetAsyncEnumerator(); }
			public IQueryProvider Provider { get { return new TestDbAsyncQueryProvider<T>(this); } }
		}

		internal class TestDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
		{

			private readonly IQueryProvider _inner;

			internal TestDbAsyncQueryProvider(IQueryProvider inner) { _inner = inner; }
			public IQueryable CreateQuery(Expression expression) { return new TestDbAsyncEnumerable<TEntity>(expression); }
			public IQueryable<TElement> CreateQuery<TElement>(Expression expression) { return new TestDbAsyncEnumerable<TElement>(expression); }
			public object Execute(Expression expression) { return _inner.Execute(expression); }
			public TResult Execute<TResult>(Expression expression) { return _inner.Execute<TResult>(expression); }
			public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken) { return Task.FromResult(Execute(expression)); }
			public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken) { return Task.FromResult(Execute<TResult>(expression)); }
		}
	}
}
