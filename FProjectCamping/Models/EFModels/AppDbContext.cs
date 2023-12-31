using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FProjectCamping.Models.EFModels
{
	public partial class AppDbContext : DbContext
	{
		public AppDbContext()
			: base("name=AppDbContext")
		{
		}

		public virtual DbSet<CartItem> CartItems { get; set; }
		public virtual DbSet<Cart> Carts { get; set; }
		public virtual DbSet<Member> Members { get; set; }
		public virtual DbSet<News> News { get; set; }
		public virtual DbSet<NewsImage> NewsImages { get; set; }
		public virtual DbSet<OrderItem> OrderItems { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<PaymentType> PaymentTypes { get; set; }
		public virtual DbSet<Photo> Photos { get; set; }
		public virtual DbSet<Reservation> Reservations { get; set; }
		public virtual DbSet<Room> Rooms { get; set; }
		public virtual DbSet<RoomService> RoomServices { get; set; }
		public virtual DbSet<RoomType> RoomTypes { get; set; }
		public virtual DbSet<Service> Services { get; set; }
		public virtual DbSet<User> Users { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Member>()
				.Property(e => e.PhoneNum)
				.IsUnicode(false);

			modelBuilder.Entity<Member>()
				.Property(e => e.ConfirmCode)
				.IsUnicode(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<News>()
				.HasMany(e => e.NewsImages)
				.WithRequired(e => e.News)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<OrderItem>()
				.HasMany(e => e.Reservations)
				.WithRequired(e => e.OrderItem)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Order>()
				.Property(e => e.OrderNumber)
				.IsUnicode(false);

			modelBuilder.Entity<Order>()
				.Property(e => e.PhoneNum)
				.IsUnicode(false);

			modelBuilder.Entity<Order>()
				.Property(e => e.Email)
				.IsUnicode(false);

			modelBuilder.Entity<Order>()
				.Property(e => e.Coupon)
				.IsUnicode(false);

			modelBuilder.Entity<Order>()
				.HasMany(e => e.OrderItems)
				.WithRequired(e => e.Order)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<PaymentType>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.PaymentType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Room>()
				.HasMany(e => e.CartItems)
				.WithRequired(e => e.Room)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Room>()
				.HasMany(e => e.OrderItems)
				.WithRequired(e => e.Room)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Room>()
				.HasMany(e => e.Photos)
				.WithRequired(e => e.Room)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Room>()
				.HasMany(e => e.Reservations)
				.WithRequired(e => e.Room)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Room>()
				.HasMany(e => e.RoomServices)
				.WithRequired(e => e.Room)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<RoomType>()
				.HasMany(e => e.Photos)
				.WithRequired(e => e.RoomType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<RoomType>()
				.HasMany(e => e.Rooms)
				.WithRequired(e => e.RoomType)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Service>()
				.HasMany(e => e.RoomServices)
				.WithRequired(e => e.Service)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.Property(e => e.Password)
				.IsUnicode(false);
		}
	}
}
