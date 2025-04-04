using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PattySlaps.Data
{
    // Database Context for Entity Framework Core
    public class PattySlapsDbContext : DbContext
    {
        public PattySlapsDbContext(DbContextOptions<PattySlapsDbContext> options) : base(options)
        {
        }
        public DbSet<Kiosk> Kiosks { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<HireRequest> HireRequests { get; set; }
        public DbSet<ShiftSchedule> ShiftSchedules { get; set; }
        public DbSet<InventoryRecord> InventoryRecords { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<HRMS> HRMS { get; set; }
        public DbSet<SummaryReport> SummaryReports { get; set; }
        public DbSet<WasteRecord> WasteRecords { get; set; }
        public DbSet<ReceptionQCChecklist> ReceptionQCChecklists { get; set; }
        public DbSet<EndOfShiftReport> EndOfShiftReports { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<HREmployee> HREmployees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3307;database=PattySlapsDB;user=root;password=patty2025$slaps;", new MySqlServerVersion(new Version(8, 0, 23)));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderID, oi.ItemID });

            modelBuilder.Entity<ShiftScheduleEmployee>()
                .HasKey(se => new { se.ScheduleID, se.EmployeeID });

            modelBuilder.Entity<HRMS>().HasNoKey();  
        }

    }

    // Generic Repository Class for CRUD Operations
    public class Repository<T> where T : class
    {
        private readonly PattySlapsDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(PattySlapsDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            if (_dbSet == null)
            {
                Console.WriteLine($"Repository: _dbSet<{typeof(T).Name}> is NULL!");
                return new List<T>();  // Return empty list to avoid crashes
            }

            var result = _dbSet.ToList();
            Console.WriteLine($"Repository: Found {result.Count} records in {typeof(T).Name}");
            return result;
        }
    }
}
