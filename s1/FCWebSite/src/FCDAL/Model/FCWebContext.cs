using FCCore.Model;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace FCDAL.Model
{
    public partial class FCWebContext : ApplicationDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=.\sqlexpress;Initial Catalog=FCWeb;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.GameDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(264);

                entity.Property(e => e.Referees).HasMaxLength(512);

                entity.HasOne(d => d.away).WithMany(p => p.Game).HasForeignKey(d => d.awayId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.home).WithMany(p => p.GameNavigation).HasForeignKey(d => d.homeId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.round).WithMany(p => p.Game).HasForeignKey(d => d.roundId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ImageGallery>(entity =>
            {
                entity.Property(e => e.Author).HasMaxLength(256);

                entity.Property(e => e.DateChanged).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDisplayed).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1024);

                entity.Property(e => e.Header)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.URLKey)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.Image).HasMaxLength(1000);

                entity.Property(e => e.NameFirst)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.NameLast)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.NameMiddle).HasMaxLength(64);

                entity.Property(e => e.NameNick).HasMaxLength(64);

                entity.HasOne(d => d.PersonNavigation).WithOne(p => p.InversePersonNavigation).HasForeignKey<Person>(d => d.Id).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.personStatus).WithMany(p => p.Person).HasForeignKey(d => d.personStatusId);

                entity.HasOne(d => d.role).WithMany(p => p.Person).HasForeignKey(d => d.roleId);

                entity.HasOne(d => d.team).WithMany(p => p.Person).HasForeignKey(d => d.teamId);
            });

            modelBuilder.Entity<PersonCareer>(entity =>
            {
                entity.Property(e => e.DateFinish).HasColumnType("datetime");

                entity.Property(e => e.DateStart).HasColumnType("datetime");

                entity.HasOne(d => d.person).WithMany(p => p.PersonCareer).HasForeignKey(d => d.personId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.team).WithMany(p => p.PersonCareer).HasForeignKey(d => d.teamId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PersonCareerTourney>(entity =>
            {
                entity.HasOne(d => d.personCareer).WithMany(p => p.PersonCareerTourney).HasForeignKey(d => d.personCareerId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.tourney).WithMany(p => p.PersonCareerTourney).HasForeignKey(d => d.tourneyId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PersonRole>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.NameFull)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.personRoleGroup).WithMany(p => p.PersonRole).HasForeignKey(d => d.personRoleGroupId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PersonRoleGroup>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.NameFull)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<PersonStatistics>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.PersonStatisticsNavigation).WithOne(p => p.InversePersonStatisticsNavigation).HasForeignKey<PersonStatistics>(d => d.Id).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.person).WithMany(p => p.PersonStatistics).HasForeignKey(d => d.personId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.team).WithMany(p => p.PersonStatistics).HasForeignKey(d => d.teamId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.tourney).WithMany(p => p.PersonStatistics).HasForeignKey(d => d.tourneyId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PersonStatus>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.NameFull)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Publication>(entity =>
            {
                entity.Property(e => e.DateChanged).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDisplayed).HasColumnType("datetime");

                entity.Property(e => e.Header)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Image).HasMaxLength(512);

                entity.Property(e => e.Lead).HasMaxLength(1024);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.URLKey)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.imageGallery).WithMany(p => p.Publication).HasForeignKey(d => d.imageGalleryId);

                entity.HasOne(d => d.video).WithMany(p => p.Publication).HasForeignKey(d => d.videoId);
            });

            modelBuilder.Entity<Round>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.NameFull)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.TeamList).HasMaxLength(1000);

                entity.HasOne(d => d.roundFormat).WithMany(p => p.Round).HasForeignKey(d => d.roundFormatId);

                entity.HasOne(d => d.tourney).WithMany(p => p.Round).HasForeignKey(d => d.tourneyId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RoundFormat>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.NameFull)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<SettingsVisibility>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TableRecord>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Team).WithOne(p => p.TableRecord).HasForeignKey<TableRecord>(d => d.Id).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.tourney).WithMany(p => p.TableRecord).HasForeignKey(d => d.tourneyId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(256);

                entity.Property(e => e.Email).HasMaxLength(128);

                entity.Property(e => e.Image).HasMaxLength(512);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.NameFull)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.NamePre)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.WebSite).HasMaxLength(128);
            });

            modelBuilder.Entity<Tourney>(entity =>
            {
                entity.Property(e => e.DateEnd).HasColumnType("datetime");

                entity.Property(e => e.DateStart).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.NameFull)
                    .IsRequired()
                    .HasMaxLength(1024);
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.Property(e => e.Author).HasMaxLength(256);

                entity.Property(e => e.CodeHTML)
                    .IsRequired()
                    .HasMaxLength(2048);

                entity.Property(e => e.DateChanged).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDisplayed).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1024);

                entity.Property(e => e.ExternalId).HasMaxLength(256);

                entity.Property(e => e.Header)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.URLKey)
                    .IsRequired()
                    .HasMaxLength(256);
            });
        }

        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<ImageGallery> ImageGallery { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonCareer> PersonCareer { get; set; }
        public virtual DbSet<PersonCareerTourney> PersonCareerTourney { get; set; }
        public virtual DbSet<PersonRole> PersonRole { get; set; }
        public virtual DbSet<PersonRoleGroup> PersonRoleGroup { get; set; }
        public virtual DbSet<PersonStatistics> PersonStatistics { get; set; }
        public virtual DbSet<PersonStatus> PersonStatus { get; set; }
        public virtual DbSet<Publication> Publication { get; set; }
        public virtual DbSet<Round> Round { get; set; }
        public virtual DbSet<RoundFormat> RoundFormat { get; set; }
        public virtual DbSet<SettingsVisibility> SettingsVisibility { get; set; }
        public virtual DbSet<TableRecord> TableRecord { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<Tourney> Tourney { get; set; }
        public virtual DbSet<Video> Video { get; set; }
    }
}