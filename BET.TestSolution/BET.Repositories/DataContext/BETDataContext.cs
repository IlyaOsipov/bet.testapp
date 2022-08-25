using BET.Infrastructure.Models;
using BET.Repositories.Migrations;
using System.Data.Entity;

namespace BET.Repositories.DataContext
{
	public class BETDataContext : DbContext
	{
		private const string connectionStringName = "BETDataDB";

		private bool isDisposed = false;

		#region Constructors

		static BETDataContext()
		{
			Database.SetInitializer<BETDataContext>(new MigrateDatabaseToLatestVersion<BETDataContext, Configuration>());
		}

		public BETDataContext()
			: base(connectionStringName)
		{
		}

		public BETDataContext(string connString)
			: base(connString)
		{
		}

		#endregion

		#region Properties

		public bool IsDisposed
		{
			get
			{
				return isDisposed;
			}
		}

		public DbSet<User> Users { get; set; }

		public DbSet<Product> Products { get; set; }

		public DbSet<Cart> Carts { get; set; }

		public DbSet<CartItem> CartItems { get; set; }

		#endregion

		protected override void Dispose(bool disposing)
		{
			if (!disposing || isDisposed)
			{
				return;
			}

			base.Dispose(disposing);

			isDisposed = true;
		}
	}
}
