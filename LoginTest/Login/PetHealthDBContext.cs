namespace Login
{
    public class PetHealthDBContext : DbContext
{
    public PetHealthDBContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingService> BookingService { get; set; }   
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Record> Records { get; set; }
    public DbSet<RecordDetail> RecordDetails { get; set; }
    public DbSet<Barn> Barns { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseModel).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var propertyMethodInfo = typeof(Microsoft.EntityFrameworkCore.EF).GetMethod("Property").MakeGenericMethod(typeof(DateTime?));
                var deletedAtProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("DeletedAt"));
                var nullCheckExpression = Expression.Equal(deletedAtProperty, Expression.Constant(null));

                var lambda = Expression.Lambda(nullCheckExpression, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
        modelBuilder.ApplyConfiguration(new CustomerConfig());
        modelBuilder.ApplyConfiguration(new PetConfig());
        modelBuilder.ApplyConfiguration(new StaffConfig());
        modelBuilder.ApplyConfiguration(new RecordDetailConfig());
        modelBuilder.ApplyConfiguration(new DoctorConfig());
        modelBuilder.ApplyConfiguration(new BarnConfig());
        modelBuilder.ApplyConfiguration(new RecordConfig());
        modelBuilder.ApplyConfiguration(new BookingConfig());
        modelBuilder.ApplyConfiguration(new ServiceConfig());
        modelBuilder.ApplyConfiguration(new BookingServiceConfig());
        modelBuilder.ApplyConfiguration(new UserConfig());
        modelBuilder.ApplyConfiguration(new RoleConfig());
        modelBuilder.ApplyConfiguration(new UserRoleConfig());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}