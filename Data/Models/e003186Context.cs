using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Models
{
    public partial class e003186Context : DbContext
    {
        public e003186Context()
        {
        }

        public e003186Context(DbContextOptions<e003186Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Adresa> Adresa { get; set; }
        public virtual DbSet<Albo> Albo { get; set; }
        public virtual DbSet<AlboCene> AlboCene { get; set; }
        public virtual DbSet<AlboGrupe> AlboGrupe { get; set; }
        public virtual DbSet<Artikal> Artikal { get; set; }
        public virtual DbSet<ArtikalDetalj> ArtikalDetalj { get; set; }
        public virtual DbSet<ArtikalSlike> ArtikalSlike { get; set; }
        public virtual DbSet<ArtikalStanje> ArtikalStanje { get; set; }
        public virtual DbSet<ArtikalSvojstva> ArtikalSvojstva { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartItem> CartItem { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<GrupeArtikala> GrupeArtikala { get; set; }
        public virtual DbSet<ImportGroup> ImportGroup { get; set; }
        public virtual DbSet<Lacuna> Lacuna { get; set; }
        public virtual DbSet<Lacuna2> Lacuna2 { get; set; }
        public virtual DbSet<Lacuna3> Lacuna3 { get; set; }
        public virtual DbSet<Lacuna4> Lacuna4 { get; set; }
        public virtual DbSet<LacunaGrupe> LacunaGrupe { get; set; }
        public virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<ModelColor> ModelColor { get; set; }
        public virtual DbSet<ModelColorSize> ModelColorSize { get; set; }
        public virtual DbSet<ModelDetail> ModelDetail { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<PromosolutionsToken> PromosolutionsToken { get; set; }
        public virtual DbSet<Publik> Publik { get; set; }
        public virtual DbSet<PublikGrupe> PublikGrupe { get; set; }
        public virtual DbSet<SavaCoop> SavaCoop { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Zemlje> Zemlje { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=81.95.105.76;Database=e003186;User Id=e003186a;Password=xjMIJTHLbawGpus9;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adresa>(entity =>
            {
                entity.Property(e => e.AdresaPl)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.AdresaPr)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.GradPl)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.GradPr)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.PbrojPl)
                    .HasColumnName("PBRojPl")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.PbrojPr)
                    .HasColumnName("PBrojPr")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Telefon1Pl)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Telefon1Pr)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Telefon2Pl)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Telefon2Pr)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.UserId).HasColumnName("User_Id");
            });

            modelBuilder.Entity<Albo>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Br)
                    .HasColumnName("Br#")
                    .HasMaxLength(255);

                entity.Property(e => e.Br2)
                    .HasColumnName("Br# 2")
                    .HasMaxLength(255);

                entity.Property(e => e.KomercijalniBroj)
                    .HasColumnName("Komercijalni broj")
                    .HasMaxLength(255);

                entity.Property(e => e.KomercijalniNaziv)
                    .HasColumnName("Komercijalni naziv")
                    .HasMaxLength(255);

                entity.Property(e => e.KomercijalniStatus)
                    .HasColumnName("Komercijalni status")
                    .HasMaxLength(255);

                entity.Property(e => e.NazivBoje)
                    .HasColumnName("Naziv boje")
                    .HasMaxLength(255);

                entity.Property(e => e.NazivVeličine)
                    .HasColumnName("Naziv veličine")
                    .HasMaxLength(255);

                entity.Property(e => e.Opis).HasMaxLength(255);

                entity.Property(e => e.Opis2)
                    .HasColumnName("Opis 2")
                    .HasMaxLength(255);

                entity.Property(e => e.OsnovnaJedinicaMere)
                    .HasColumnName("Osnovna jedinica mere")
                    .HasMaxLength(255);

                entity.Property(e => e.Proizvođač).HasMaxLength(255);

                entity.Property(e => e.ŠifraBoja)
                    .HasColumnName("Šifra boja ")
                    .HasMaxLength(255);

                entity.Property(e => e.ŠifraDržaveRegionaPorekla)
                    .HasColumnName("Šifra države/regiona porekla")
                    .HasMaxLength(255);

                entity.Property(e => e.ŠifraVeličina)
                    .HasColumnName("Šifra veličina")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<AlboCene>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ALBO_Cene");

                entity.Property(e => e.FinalnaCena).HasColumnName("Finalna_Cena");

                entity.Property(e => e.FinalnaCenaPdv).HasColumnName("Finalna_Cena_PDV");

                entity.Property(e => e.Mpcena).HasColumnName("MPCena");
            });

            modelBuilder.Entity<AlboGrupe>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Albo_Grupe");
            });

            modelBuilder.Entity<Artikal>(entity =>
            {
                entity.Property(e => e.Barkod)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Brand)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Color)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.FinalnaCena).HasColumnName("Finalna_Cena");

                entity.Property(e => e.FinalnaCenaPdv).HasColumnName("Finalna_Cena_PDV");

                entity.Property(e => e.Jm)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ModelId).HasColumnName("Model_Id");

                entity.Property(e => e.Mpcena)
                    .HasColumnName("MPCena")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Naziv)
                    .HasMaxLength(300)
                    .IsFixedLength();

                entity.Property(e => e.Pdv).HasColumnName("PDV");

                entity.Property(e => e.Sifra)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Size)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Slika)
                    .HasMaxLength(300)
                    .IsFixedLength();

                entity.Property(e => e.SlikaSource)
                    .HasColumnName("Slika_Source")
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Source)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SourceId)
                    .HasColumnName("Source_Id")
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.SourceSifra)
                    .HasColumnName("Source_Sifra")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Temp)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Vpcena)
                    .HasColumnName("VPCena")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WebshopNaziv)
                    .HasColumnName("Webshop_Naziv")
                    .HasMaxLength(300)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ArtikalDetalj>(entity =>
            {
                entity.ToTable("Artikal_Detalj");

                entity.Property(e => e.ArtikalId).HasColumnName("Artikal_Id");

                entity.Property(e => e.Deklaracija).IsUnicode(false);

                entity.Property(e => e.Opis).IsUnicode(false);

                entity.Property(e => e.Opis2).IsUnicode(false);

                entity.Property(e => e.OpisSeo)
                    .HasColumnName("OpisSEO")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.WebshopNaziv)
                    .HasColumnName("Webshop_Naziv")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.Artikal)
                    .WithMany(p => p.ArtikalDetalj)
                    .HasForeignKey(d => d.ArtikalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Artikal_Detalj_Artikal");
            });

            modelBuilder.Entity<ArtikalSlike>(entity =>
            {
                entity.ToTable("Artikal_Slike");

                entity.Property(e => e.ArtikalId).HasColumnName("Artikal_Id");

                entity.Property(e => e.Slika)
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Artikal)
                    .WithMany(p => p.ArtikalSlike)
                    .HasForeignKey(d => d.ArtikalId)
                    .HasConstraintName("FK_Artikal_Slike_Artikal");
            });

            modelBuilder.Entity<ArtikalStanje>(entity =>
            {
                entity.ToTable("Artikal_Stanje");

                entity.Property(e => e.ArtikalId).HasColumnName("Artikal_Id");

                entity.Property(e => e.Stanje).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<ArtikalSvojstva>(entity =>
            {
                entity.ToTable("Artikal_Svojstva");

                entity.Property(e => e.ArtikalId).HasColumnName("Artikal_Id");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vrednost)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Artikal)
                    .WithMany(p => p.ArtikalSvojstva)
                    .HasForeignKey(d => d.ArtikalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Artikal_Svojstva_Artikal");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.Image)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SourceId)
                    .HasColumnName("Source_Id")
                    .HasMaxLength(20)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.ExpDate)
                    .HasColumnName("exp_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsFixedLength();

                entity.Property(e => e.UserId).HasColumnName("User_Id");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.ToTable("Cart_Item");

                entity.Property(e => e.ArtikalId).HasColumnName("Artikal_Id");

                entity.Property(e => e.CartId).HasColumnName("Cart_Id");

                entity.HasOne(d => d.Artikal)
                    .WithMany(p => p.CartItem)
                    .HasForeignKey(d => d.ArtikalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_Item_Artikal");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartItem)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_Item_Cart");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.HtmlColor)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Naziv)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SourceId)
                    .HasColumnName("Source_Id")
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<GrupeArtikala>(entity =>
            {
                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ParentId).HasColumnName("Parent_Id");

                entity.Property(e => e.ParentSourceId)
                    .HasColumnName("Parent_Source_Id")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Slika)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Source)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.SourceId)
                    .HasColumnName("Source_Id")
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.WebshopNaziv)
                    .HasColumnName("Webshop_Naziv")
                    .HasMaxLength(300)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ImportGroup>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Grupa1).HasMaxLength(255);

                entity.Property(e => e.Grupa2).HasMaxLength(255);
            });

            modelBuilder.Entity<Lacuna>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Brand).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.FinalnaCena).HasColumnName("Finalna_Cena");

                entity.Property(e => e.FinalnaCenaBezPdv).HasColumnName("Finalna_Cena_Bez_PDV");

                entity.Property(e => e.Grupa).HasMaxLength(255);

                entity.Property(e => e.Mpcena).HasColumnName("MPCena");

                entity.Property(e => e.Naziv).HasMaxLength(255);

                entity.Property(e => e.ObrisiIzBaze).HasColumnName("Obrisi_Iz_Baze");

                entity.Property(e => e.Pdv).HasColumnName("PDV");
            });

            modelBuilder.Entity<Lacuna2>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Brand).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.FinalnaCena).HasColumnName("Finalna_Cena");

                entity.Property(e => e.FinalnaCenaBezPdv).HasColumnName("Finalna_Cena_Bez_PDV");

                entity.Property(e => e.Grupa).HasMaxLength(255);

                entity.Property(e => e.Mpcena).HasColumnName("MPCena");

                entity.Property(e => e.Naziv).HasMaxLength(255);

                entity.Property(e => e.ObrisiIzBaze).HasColumnName("Obrisi_Iz_Baze");

                entity.Property(e => e.Pdv).HasColumnName("PDV");
            });

            modelBuilder.Entity<Lacuna3>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Brand).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.FinalnaCena).HasColumnName("Finalna_Cena");

                entity.Property(e => e.FinalnaCenaBezPdv).HasColumnName("Finalna_Cena_Bez_PDV");

                entity.Property(e => e.Grupa).HasMaxLength(255);

                entity.Property(e => e.Mpcena).HasColumnName("MPCena");

                entity.Property(e => e.Naziv).HasMaxLength(255);

                entity.Property(e => e.ObrisiIzBaze).HasColumnName("Obrisi_Iz_Baze");

                entity.Property(e => e.Pdv).HasColumnName("PDV");
            });

            modelBuilder.Entity<Lacuna4>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Brand).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.FinalnaCena).HasColumnName("Finalna_Cena");

                entity.Property(e => e.FinalnaCenaBezPdv).HasColumnName("Finalna_Cena_Bez_PDV");

                entity.Property(e => e.Grupa).HasMaxLength(255);

                entity.Property(e => e.Mpcena).HasColumnName("MPCena");

                entity.Property(e => e.Naziv).HasMaxLength(255);

                entity.Property(e => e.ObrisiIzBaze).HasColumnName("Obrisi_Iz_Baze");

                entity.Property(e => e.Pdv).HasColumnName("PDV");
            });

            modelBuilder.Entity<LacunaGrupe>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LACUNA_Grupe");
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.Property(e => e.Brand)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Description2).IsUnicode(false);

                entity.Property(e => e.FinalnaCena).HasColumnName("Finalna_Cena");

                entity.Property(e => e.FinalnaCenaPdv).HasColumnName("Finalna_Cena_PDV");

                entity.Property(e => e.Mpcena)
                    .HasColumnName("MPCena")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.Property(e => e.Pdv).HasColumnName("PDV");

                entity.Property(e => e.Slika)
                    .HasMaxLength(300)
                    .IsFixedLength();

                entity.Property(e => e.SlikaSource)
                    .HasColumnName("Slika_Source")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Source)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SourceId)
                    .HasColumnName("Source_Id")
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Tag)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Video)
                    .HasMaxLength(300)
                    .IsFixedLength();

                entity.Property(e => e.Vpcena)
                    .HasColumnName("VPCena")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WebshopNaziv)
                    .HasColumnName("Webshop_Naziv")
                    .HasMaxLength(300)
                    .IsFixedLength();

                entity.Property(e => e.ZemljaPorekla)
                    .HasMaxLength(3)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ModelColor>(entity =>
            {
                entity.ToTable("Model_Color");

                entity.Property(e => e.Color)
                    .HasMaxLength(40)
                    .IsFixedLength();

                entity.Property(e => e.HtmlColor)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ModelId).HasColumnName("Model_Id");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.ModelColor)
                    .HasForeignKey(d => d.ModelId)
                    .HasConstraintName("FK_Model_Color_Model_Color");
            });

            modelBuilder.Entity<ModelColorSize>(entity =>
            {
                entity.ToTable("Model_Color_Size");

                entity.Property(e => e.ArtikalId).HasColumnName("Artikal_Id");

                entity.Property(e => e.ModelColorId).HasColumnName("Model_Color_Id");

                entity.Property(e => e.Size)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.HasOne(d => d.Artikal)
                    .WithMany(p => p.ModelColorSize)
                    .HasForeignKey(d => d.ArtikalId)
                    .HasConstraintName("FK_Model_Color_Size_Artikal");

                entity.HasOne(d => d.ModelColor)
                    .WithMany(p => p.ModelColorSize)
                    .HasForeignKey(d => d.ModelColorId)
                    .HasConstraintName("FK_Model_Color_Size_Model_Color");
            });

            modelBuilder.Entity<ModelDetail>(entity =>
            {
                entity.ToTable("Model_Detail");

                entity.Property(e => e.Deklaracija).IsUnicode(false);

                entity.Property(e => e.ModelId).HasColumnName("Model_Id");

                entity.Property(e => e.Opis).IsUnicode(false);

                entity.Property(e => e.Opis2).IsUnicode(false);

                entity.Property(e => e.OpisDetalj)
                    .HasColumnName("Opis_Detalj")
                    .IsUnicode(false);

                entity.Property(e => e.OpisSeo)
                    .HasColumnName("OpisSEO")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.WebshopNaziv)
                    .HasColumnName("Webshop_Naziv")
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Datum).HasColumnType("date");

                entity.Property(e => e.Napomena).IsUnicode(false);

                entity.Property(e => e.Referenca)
                    .HasMaxLength(14)
                    .IsFixedLength();

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UserId).HasColumnName("User_Id");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("Order_Item");

                entity.Property(e => e.ArtikalId).HasColumnName("Artikal_Id");

                entity.Property(e => e.CartItemId).HasColumnName("CartItem_Id");

                entity.Property(e => e.NazivArtikla)
                    .HasColumnName("Naziv_Artikla")
                    .HasMaxLength(300)
                    .IsFixedLength();

                entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            });

            modelBuilder.Entity<PromosolutionsToken>(entity =>
            {
                entity.ToTable("Promosolutions_Token");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccessToken)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Expires).HasColumnType("datetime");

                entity.Property(e => e.Issued).HasColumnType("datetime");

                entity.Property(e => e.TokenType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Publik>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.FinalnaCena).HasColumnName("Finalna_Cena");

                entity.Property(e => e.FinalnaCenaBezPdv).HasColumnName("Finalna_Cena_Bez_PDV");

                entity.Property(e => e.Grupa).HasMaxLength(255);

                entity.Property(e => e.Mpcena)
                    .HasColumnName("MPCena")
                    .HasMaxLength(255);

                entity.Property(e => e.Naziv).HasMaxLength(255);

                entity.Property(e => e.ObrisiIzBaze)
                    .HasColumnName("Obrisi_Iz_Baze")
                    .HasMaxLength(255);

                entity.Property(e => e.Pdv).HasColumnName("PDV");

                entity.Property(e => e.ZaSveArtikle)
                    .HasColumnName("Za_Sve_Artikle")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<PublikGrupe>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Publik_Grupe");
            });

            modelBuilder.Entity<SavaCoop>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Barkod).HasMaxLength(255);

                entity.Property(e => e.Boja).HasMaxLength(255);

                entity.Property(e => e.Brand).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Description2).HasMaxLength(255);

                entity.Property(e => e.FinalnaCena)
                    .HasColumnName("Finalna_Cena")
                    .HasMaxLength(255);

                entity.Property(e => e.FinalnaCenaPdv)
                    .HasColumnName("Finalna_Cena_PDV")
                    .HasMaxLength(255);

                entity.Property(e => e.Grupa3).HasMaxLength(255);

                entity.Property(e => e.Mpcena)
                    .HasColumnName("MPCena")
                    .HasMaxLength(255);

                entity.Property(e => e.Naziv).HasMaxLength(255);

                entity.Property(e => e.NazivModel)
                    .HasColumnName("Naziv_Model")
                    .HasMaxLength(255);

                entity.Property(e => e.Pdv)
                    .HasColumnName("PDV")
                    .HasMaxLength(255);

                entity.Property(e => e.Popust).HasMaxLength(255);

                entity.Property(e => e.SlikaSource)
                    .HasColumnName("Slika_Source")
                    .HasMaxLength(255);

                entity.Property(e => e.Source).HasMaxLength(255);

                entity.Property(e => e.SourceId)
                    .HasColumnName("Source_Id")
                    .HasMaxLength(255);

                entity.Property(e => e.Tag).HasMaxLength(255);

                entity.Property(e => e.Tip).HasMaxLength(255);

                entity.Property(e => e.Velicina).HasMaxLength(255);

                entity.Property(e => e.Vpcena).HasColumnName("VPCena");

                entity.Property(e => e.WebshopNaziv)
                    .HasColumnName("Webshop_Naziv")
                    .HasMaxLength(255);

                entity.Property(e => e.ZemljaPorekla).HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CalculusId)
                    .HasColumnName("Calculus_Id")
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Guid)
                    .HasColumnName("GUID")
                    .HasMaxLength(36)
                    .IsFixedLength();

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(16)
                    .IsFixedLength();

                entity.Property(e => e.VremeVazenjaLinka).HasColumnType("datetime");
            });

            modelBuilder.Entity<Zemlje>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .HasMaxLength(3)
                    .IsFixedLength();

                entity.Property(e => e.Naziv)
                    .HasMaxLength(99)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
