﻿// <auto-generated />
using System;
using MedicSystem.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MedicSystem.Infrastucture.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240219184304_PacienteFix")]
    partial class PacienteFix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CitaResultadoDeLaboratorio", b =>
                {
                    b.Property<int>("CitasId")
                        .HasColumnType("int");

                    b.Property<int>("ResultadosDeLaboratorioId")
                        .HasColumnType("int");

                    b.HasKey("CitasId", "ResultadosDeLaboratorioId");

                    b.HasIndex("ResultadosDeLaboratorioId");

                    b.ToTable("CitasResultadosDeLaboratorio", (string)null);
                });

            modelBuilder.Entity("MedicSystem.Core.Domain.Entities.Cita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Causa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstadoCita")
                        .HasColumnType("int");

                    b.Property<string>("Fecha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hora")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MedicoId")
                        .HasColumnType("int");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedicoId");

                    b.HasIndex("PacienteId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Citas", (string)null);
                });

            modelBuilder.Entity("MedicSystem.Core.Domain.Entities.Medico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Medicos", (string)null);
                });

            modelBuilder.Entity("MedicSystem.Core.Domain.Entities.Paciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Alergias")
                        .HasColumnType("bit");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("FechaNacimiento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Fumador")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Pacientes", (string)null);
                });

            modelBuilder.Entity("MedicSystem.Core.Domain.Entities.PruebaDeLaboratorio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int?>("ResultadoDeLaboratorioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ResultadoDeLaboratorioId");

                    b.ToTable("PruebasDeLaboratorio", (string)null);
                });

            modelBuilder.Entity("MedicSystem.Core.Domain.Entities.ResultadoDeLaboratorio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstadoResultado")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.Property<int>("PruebaDeLaboratorioId")
                        .HasColumnType("int");

                    b.Property<string>("Resultado")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId");

                    b.HasIndex("PruebaDeLaboratorioId");

                    b.ToTable("ResultadosDeLaboratorio", (string)null);
                });

            modelBuilder.Entity("MedicSystem.Core.Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoUsuario")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Usuarios", (string)null);
                });

            modelBuilder.Entity("MedicoPaciente", b =>
                {
                    b.Property<int>("MedicosId")
                        .HasColumnType("int");

                    b.Property<int>("PacientesId")
                        .HasColumnType("int");

                    b.HasKey("MedicosId", "PacientesId");

                    b.HasIndex("PacientesId");

                    b.ToTable("MedicosPacientes", (string)null);
                });

            modelBuilder.Entity("CitaResultadoDeLaboratorio", b =>
                {
                    b.HasOne("MedicSystem.Core.Domain.Entities.Cita", null)
                        .WithMany()
                        .HasForeignKey("CitasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicSystem.Core.Domain.Entities.ResultadoDeLaboratorio", null)
                        .WithMany()
                        .HasForeignKey("ResultadosDeLaboratorioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MedicSystem.Core.Domain.Entities.Cita", b =>
                {
                    b.HasOne("MedicSystem.Core.Domain.Entities.Medico", "Medico")
                        .WithMany("Citas")
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MedicSystem.Core.Domain.Entities.Paciente", "Paciente")
                        .WithMany("Citas")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicSystem.Core.Domain.Entities.Usuario", "Usuario")
                        .WithMany("Citas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medico");

                    b.Navigation("Paciente");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MedicSystem.Core.Domain.Entities.Medico", b =>
                {
                    b.HasOne("MedicSystem.Core.Domain.Entities.Usuario", "Usuario")
                        .WithMany("Medicos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MedicSystem.Core.Domain.Entities.Paciente", b =>
                {
                    b.HasOne("MedicSystem.Core.Domain.Entities.Usuario", "Usuario")
                        .WithMany("Pacientes")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MedicSystem.Core.Domain.Entities.PruebaDeLaboratorio", b =>
                {
                    b.HasOne("MedicSystem.Core.Domain.Entities.ResultadoDeLaboratorio", null)
                        .WithMany("PruebasDeLaboratorio")
                        .HasForeignKey("ResultadoDeLaboratorioId");
                });

            modelBuilder.Entity("MedicSystem.Core.Domain.Entities.ResultadoDeLaboratorio", b =>
                {
                    b.HasOne("MedicSystem.Core.Domain.Entities.Paciente", "Paciente")
                        .WithMany("ResultadosDeLaboratorio")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MedicSystem.Core.Domain.Entities.PruebaDeLaboratorio", "PruebaDeLaboratorio")
                        .WithMany("ResultadosDeLaboratorio")
                        .HasForeignKey("PruebaDeLaboratorioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");

                    b.Navigation("PruebaDeLaboratorio");
                });

            modelBuilder.Entity("MedicoPaciente", b =>
                {
                    b.HasOne("MedicSystem.Core.Domain.Entities.Medico", null)
                        .WithMany()
                        .HasForeignKey("MedicosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicSystem.Core.Domain.Entities.Paciente", null)
                        .WithMany()
                        .HasForeignKey("PacientesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MedicSystem.Core.Domain.Entities.Medico", b =>
                {
                    b.Navigation("Citas");
                });

            modelBuilder.Entity("MedicSystem.Core.Domain.Entities.Paciente", b =>
                {
                    b.Navigation("Citas");

                    b.Navigation("ResultadosDeLaboratorio");
                });

            modelBuilder.Entity("MedicSystem.Core.Domain.Entities.PruebaDeLaboratorio", b =>
                {
                    b.Navigation("ResultadosDeLaboratorio");
                });

            modelBuilder.Entity("MedicSystem.Core.Domain.Entities.ResultadoDeLaboratorio", b =>
                {
                    b.Navigation("PruebasDeLaboratorio");
                });

            modelBuilder.Entity("MedicSystem.Core.Domain.Entities.Usuario", b =>
                {
                    b.Navigation("Citas");

                    b.Navigation("Medicos");

                    b.Navigation("Pacientes");
                });
#pragma warning restore 612, 618
        }
    }
}
