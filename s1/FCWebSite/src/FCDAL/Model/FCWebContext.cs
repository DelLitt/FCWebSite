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

            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(e => e.Author).HasMaxLength(256);

                entity.Property(e => e.ContentHTML).IsRequired();

                entity.Property(e => e.DateChanged).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDisplayed).HasColumnType("datetime");

                entity.Property(e => e.Header)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Lead).HasMaxLength(1024);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.URLKey)
                    .IsRequired()
                    .HasMaxLength(256);
            });

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

            modelBuilder.Entity<Publication>(entity =>
            {
                entity.Property(e => e.DateChanged).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateDisplayed).HasColumnType("datetime");

                entity.Property(e => e.Header)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Image).HasMaxLength(512);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.URLKey)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.article).WithMany(p => p.Publication).HasForeignKey(d => d.articleId);

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

        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<ImageGallery> ImageGallery { get; set; }
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