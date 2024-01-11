using Microsoft.EntityFrameworkCore;

namespace Task20_consumewebapioftask11_.Models
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Customer> Customer1 { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Register> Register { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State{ get; set; }
        //public DbSet<Register> Login { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC075576AFB6");

                entity.ToTable("Customer");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                //entity.HasOne(d => d.Gender).WithMany(p => p.Customers)
                //    .HasForeignKey(d => d.GenderId)
                //    .HasConstraintName("FK__Customer__Gender__4E88ABD4");

                //entity.HasOne(d => d.Product).WithMany(p => p.Customer)
                //    .HasForeignKey(d => d.ProductId)
                //    .HasConstraintName("FK__Customer__Produc__4D94879B");
            });
            modelBuilder.Entity<Register>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Register__3214EC073F6AA02D");

                entity.ToTable("Register");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.SelectedCountry)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                //entity.Property(e => e.PhoneNumber)
                //    .HasMaxLength(50)
                //    .IsUnicode(false);
                //entity.Property(e => e.SelectedState)
                //    .HasMaxLength(50)
                //    .IsUnicode(false);
            });

            //protected override void OnModelCreating(ModelBuilder modelBuilder)
            //{
            //    modelBuilder.Entity<Product>()
            //        .HasOne(x => x.customer).WithMany(y => y.Product).HasForeignKey(z => z.Id);
            //}
            //public async Task<string> GetProductNameByIdAsync(int customerId)
            //{
            //    var productName = await Customer
            //        .Where(c => c.Id == customerId)
            //        .Select(c => c.Product.ProductName)
            //        .FirstOrDefaultAsync();

            //    return productName;
            //}
        }
    }
}
