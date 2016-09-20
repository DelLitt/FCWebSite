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

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.NameFull)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.country).WithMany(p => p.City).HasForeignKey(d => d.countryId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameFull)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.NameFull)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.eventGroup).WithMany(p => p.Event).HasForeignKey(d => d.eventGroupId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EventGroup>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.NameFull)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.GameDate).HasColumnType("datetime");

                entity.Property(e => e.Referees).HasMaxLength(512);

                entity.HasOne(d => d.away).WithMany(p => p.Game).HasForeignKey(d => d.awayId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.home).WithMany(p => p.GameNavigation).HasForeignKey(d => d.homeId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.imageGallery).WithMany(p => p.Game).HasForeignKey(d => d.imageGalleryId);

                entity.HasOne(d => d.round).WithMany(p => p.Game).HasForeignKey(d => d.roundId).OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.stadium).WithMany(p => p.Game).HasForeignKey(d => d.stadiumId);

                entity.HasOne(d => d.video).WithMany(p => p.Game).HasForeignKey(d => d.videoId);
            });

            modelBuilder.Entity<GameFormat>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.NameFull)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<ImageGallery>(entity =>
            {
                entity.Property(e => e.Author).HasMaxLength(256);

                entity.Property(e => e.DateChanged).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDisplayed).HasColumnType("datetime");

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

                entity.HasOne(d => d.city).WithMany(p => p.Person).HasForeignKey(d => d.cityId);

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

            modelBuilder.Entity<ProtocolRecord>(entity =>
            {
                entity.HasOne(d => d._event).WithMany(p => p.ProtocolRecord).HasForeignKey(d => d.eventId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.game).WithMany(p => p.ProtocolRecord).HasForeignKey(d => d.gameId).OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.person).WithMany(p => p.ProtocolRecord).HasForeignKey(d => d.personId);

                entity.HasOne(d => d.team).WithMany(p => p.ProtocolRecord).HasForeignKey(d => d.teamId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Publication>(entity =>
            {
                entity.Property(e => e.Author).HasMaxLength(256);

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

            modelBuilder.Entity<Stadium>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Image).HasMaxLength(1000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.NameFull)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.city).WithMany(p => p.Stadium).HasForeignKey(d => d.cityId);
            });

            modelBuilder.Entity<TableRecord>(entity =>
            {
                // entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Team).WithMany(p => p.TableRecord).HasForeignKey(d => d.teamId).OnDelete(DeleteBehavior.Restrict);

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

                entity.HasOne(d => d.city).WithMany(p => p.Team).HasForeignKey(d => d.cityId);

                entity.HasOne(d => d.mainTourney).WithMany(p => p.Team).HasForeignKey(d => d.mainTourneyId);

                entity.HasOne(d => d.stadium).WithMany(p => p.Team).HasForeignKey(d => d.stadiumId);

                entity.HasOne(d => d.teamType).WithMany(p => p.Team).HasForeignKey(d => d.teamTypeId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TeamType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.NameFull)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Totalizator>(entity =>
            {
                entity.HasOne(d => d.game).WithMany(p => p.Totalizator).HasForeignKey(d => d.gameId).OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.voteType).IsRequired();

                entity.Property(e => e.UserIP)
                    .IsRequired()
                    .HasMaxLength(50);
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

                entity.HasOne(d => d.city).WithMany(p => p.Tourney).HasForeignKey(d => d.cityId);

                entity.HasOne(d => d.tourneyType).WithMany(p => p.Tourney).HasForeignKey(d => d.tourneyTypeId);
            });

            modelBuilder.Entity<TourneyType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.NameFull)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

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

                entity.HasOne(d => d.VideoNavigation).WithOne(p => p.InverseVideoNavigation).HasForeignKey<Video>(d => d.Id).OnDelete(DeleteBehavior.Restrict);
            });
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventGroup> EventGroup { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<GameFormat> GameFormat { get; set; }
        public virtual DbSet<ImageGallery> ImageGallery { get; set; }
        public virtual DbSet<Period> Period { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonCareer> PersonCareer { get; set; }
        public virtual DbSet<PersonCareerTourney> PersonCareerTourney { get; set; }
        public virtual DbSet<PersonRole> PersonRole { get; set; }
        public virtual DbSet<PersonRoleGroup> PersonRoleGroup { get; set; }
        public virtual DbSet<PersonStatistics> PersonStatistics { get; set; }
        public virtual DbSet<PersonStatus> PersonStatus { get; set; }
        public virtual DbSet<ProtocolRecord> ProtocolRecord { get; set; }
        public virtual DbSet<Publication> Publication { get; set; }
        public virtual DbSet<Round> Round { get; set; }
        public virtual DbSet<RoundFormat> RoundFormat { get; set; }
        public virtual DbSet<SettingsVisibility> SettingsVisibility { get; set; }
        public virtual DbSet<Stadium> Stadium { get; set; }
        public virtual DbSet<TableRecord> TableRecord { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<TeamType> TeamType { get; set; }
        public virtual DbSet<Totalizator> Totalizator { get; set; }
        public virtual DbSet<Tourney> Tourney { get; set; }
        public virtual DbSet<TourneyType> TourneyType { get; set; }
        public virtual DbSet<Video> Video { get; set; }
    }
}