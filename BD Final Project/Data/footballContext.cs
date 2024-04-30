using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BD_Final_Project.Models;

namespace BD_Final_Project.Data
{
    public partial class footballContext : DbContext
    {
        public footballContext()
        {
        }

        public footballContext(DbContextOptions<footballContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Championnat> Championnats { get; set; } = null!;
        public virtual DbSet<Changelog> Changelogs { get; set; } = null!;
        public virtual DbSet<Changelog1> Changelogs1 { get; set; } = null!;
        public virtual DbSet<ChangementClub> ChangementClubs { get; set; } = null!;
        public virtual DbSet<Equipe> Equipes { get; set; } = null!;
        public virtual DbSet<Joueur> Joueurs { get; set; } = null!;
        public virtual DbSet<Match> Matches { get; set; } = null!;
        public virtual DbSet<Palmare> Palmares { get; set; } = null!;
        public virtual DbSet<Saison> Saisons { get; set; } = null!;
        public virtual DbSet<Stade> Stades { get; set; } = null!;
        public virtual DbSet<Trophee> Trophees { get; set; } = null!;
        public virtual DbSet<VwGestionDesEquipe> VwGestionDesEquipes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=football");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Changelog>(entity =>
            {
                entity.Property(e => e.InstalledOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Changelog1>(entity =>
            {
                entity.Property(e => e.InstalledOn).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Equipe>(entity =>
            {
                entity.HasOne(d => d.Championnat)
                    .WithMany(p => p.Equipes)
                    .HasForeignKey(d => d.ChampionnatId)
                    .HasConstraintName("FK_Equipe_ChampionnatID");
            });

            modelBuilder.Entity<Joueur>(entity =>
            {
                entity.HasOne(d => d.Equipe)
                    .WithMany(p => p.Joueurs)
                    .HasForeignKey(d => d.EquipeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Joueur_EquipeID");
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.HasOne(d => d.EquipeDomicileNavigation)
                    .WithMany(p => p.MatchEquipeDomicileNavigations)
                    .HasForeignKey(d => d.EquipeDomicile)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Match_EquipeDomicile");

                entity.HasOne(d => d.EquipeExterieureNavigation)
                    .WithMany(p => p.MatchEquipeExterieureNavigations)
                    .HasForeignKey(d => d.EquipeExterieure)
                    .HasConstraintName("FK_Match_EquipeExterieur");

                entity.HasOne(d => d.EquipeGagnanteNavigation)
                    .WithMany(p => p.MatchEquipeGagnanteNavigations)
                    .HasForeignKey(d => d.EquipeGagnante)
                    .HasConstraintName("FK_Match_EquipeGagnant");

                entity.HasOne(d => d.Saison)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.SaisonId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Match_SaisonID");

                entity.HasOne(d => d.Stade)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.StadeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Match_StadeID");
            });

            modelBuilder.Entity<Palmare>(entity =>
            {
                entity.HasKey(e => e.PalmaresId)
                    .HasName("PK_Palmares_PalmaresID");

                entity.HasOne(d => d.Joueur)
                    .WithMany(p => p.Palmares)
                    .HasForeignKey(d => d.JoueurId)
                    .HasConstraintName("FK_Palmares_JoueurID");

                entity.HasOne(d => d.Trophee)
                    .WithMany(p => p.Palmares)
                    .HasForeignKey(d => d.TropheeId)
                    .HasConstraintName("FK_Palmares_TropheeID");
            });

            modelBuilder.Entity<Saison>(entity =>
            {
                entity.HasOne(d => d.Championnat)
                    .WithMany(p => p.Saisons)
                    .HasForeignKey(d => d.ChampionnatId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Saison_ChampionnatID");
            });

            modelBuilder.Entity<Stade>(entity =>
            {
                entity.HasOne(d => d.Equipe)
                    .WithMany(p => p.Stades)
                    .HasForeignKey(d => d.EquipeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Stade_EquipeID");
            });

            modelBuilder.Entity<VwGestionDesEquipe>(entity =>
            {
                entity.ToView("vw_GestionDesEquipes", "Equipes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
