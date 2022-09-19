﻿// <auto-generated />
using System;
using Library.Web.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Test.Web.Migrations
{
    [DbContext(typeof(LibDbContext))]
    [Migration("20211217082001_ModifyBorrowProp")]
    partial class ModifyBorrowProp
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("Test.Web.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Author")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Brief")
                        .HasColumnType("longtext");

                    b.Property<string>("Catalog")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Code")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<byte[]>("Cover")
                        .HasColumnType("blob");

                    b.Property<DateTime>("DateIn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DatePress")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Isbn")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<int>("Language")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Pages")
                        .HasColumnType("int");

                    b.Property<string>("Press")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(4,1)");

                    b.Property<string>("Status")
                        .HasMaxLength(2)
                        .HasColumnType("char(2)");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Test.Web.Entities.Borrow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("ContinueTimes")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOut")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateRetAct")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateRetPlan")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("IsReturned")
                        .HasColumnType("bit");

                    b.Property<string>("OperatorBorrow")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("OperatorReturn")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("OverDay")
                        .HasColumnType("int");

                    b.Property<decimal>("OverMoney")
                        .HasColumnType("decimal(6,2)");

                    b.Property<decimal>("PunishMoney")
                        .HasColumnType("decimal(6,2)");

                    b.Property<int?>("ReaderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ReaderId");

                    b.ToTable("Borrows");
                });

            modelBuilder.Entity("Test.Web.Entities.Reader", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("AdminRole")
                        .HasColumnType("int");

                    b.Property<int>("BorrowQty")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateReg")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Dept")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Email")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Password")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Phone")
                        .HasMaxLength(11)
                        .HasColumnType("char(11)");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("blob");

                    b.Property<string>("Sex")
                        .HasMaxLength(1)
                        .HasColumnType("char(1)");

                    b.Property<string>("Status")
                        .HasMaxLength(2)
                        .HasColumnType("char(2)");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Readers");
                });

            modelBuilder.Entity("Test.Web.Entities.ReaderType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("CanContinueTimes")
                        .HasColumnType("int");

                    b.Property<int>("CanLendDay")
                        .HasColumnType("int");

                    b.Property<int>("CanLendQty")
                        .HasColumnType("int");

                    b.Property<int>("DataValid")
                        .HasColumnType("int");

                    b.Property<decimal>("PunishRate")
                        .HasColumnType("decimal(3,2)");

                    b.Property<string>("TypeName")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("ReaderTypes");
                });

            modelBuilder.Entity("Test.Web.Entities.Borrow", b =>
                {
                    b.HasOne("Test.Web.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId");

                    b.HasOne("Test.Web.Entities.Reader", "Reader")
                        .WithMany()
                        .HasForeignKey("ReaderId");

                    b.Navigation("Book");

                    b.Navigation("Reader");
                });

            modelBuilder.Entity("Test.Web.Entities.Reader", b =>
                {
                    b.HasOne("Test.Web.Entities.ReaderType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.Navigation("Type");
                });
#pragma warning restore 612, 618
        }
    }
}
