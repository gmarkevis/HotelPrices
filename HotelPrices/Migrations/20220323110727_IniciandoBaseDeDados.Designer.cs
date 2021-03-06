// <auto-generated />
using System;
using HotelPrices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelPrices.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20220323110727_IniciandoBaseDeDados")]
    partial class IniciandoBaseDeDados
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HotelPrices.Models.HotelQuarto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOut")
                        .HasColumnType("datetime2");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumeroAdultos")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("HotelQuarto");
                });

            modelBuilder.Entity("HotelPrices.Models.HotelQuartoTarifa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Condicao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HotelQuartoId")
                        .HasColumnType("int");

                    b.Property<string>("ValorMedio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ValorTotal")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HotelQuartoId");

                    b.ToTable("HotelQuartoTarifa");
                });

            modelBuilder.Entity("HotelPrices.Models.HotelQuartoTarifa", b =>
                {
                    b.HasOne("HotelPrices.Models.HotelQuarto", null)
                        .WithMany("Tarifas")
                        .HasForeignKey("HotelQuartoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
