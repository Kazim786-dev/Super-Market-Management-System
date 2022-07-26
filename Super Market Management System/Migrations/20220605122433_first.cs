using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Super_Market_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadOffice = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Cashier",
                columns: table => new
                {
                    cashierid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cashier", x => x.cashierid);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    supplierid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.supplierid);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    orderid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_generated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    customerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    totalPrice = table.Column<int>(type: "int", nullable: false),
                    cashierid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.orderid);
                    table.ForeignKey(
                        name: "FK_orders_Cashier_cashierid",
                        column: x => x.cashierid,
                        principalTable: "Cashier",
                        principalColumn: "cashierid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    ProdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProdPrice = table.Column<int>(type: "int", nullable: false),
                    ProdQuantity = table.Column<int>(type: "int", nullable: false),
                    Cat_Id = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.ProdId);
                    table.ForeignKey(
                        name: "FK_products_Categories_Cat_Id",
                        column: x => x.Cat_Id,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_products_brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orderproduct",
                columns: table => new
                {
                    orderid = table.Column<int>(type: "int", nullable: false),
                    ProdId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderproduct", x => new { x.orderid, x.ProdId });
                    table.ForeignKey(
                        name: "FK_orderproduct_orders_orderid",
                        column: x => x.orderid,
                        principalTable: "orders",
                        principalColumn: "orderid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orderproduct_products_ProdId",
                        column: x => x.ProdId,
                        principalTable: "products",
                        principalColumn: "ProdId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderproduct_ProdId",
                table: "orderproduct",
                column: "ProdId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_cashierid",
                table: "orders",
                column: "cashierid");

            migrationBuilder.CreateIndex(
                name: "IX_products_BrandId",
                table: "products",
                column: "BrandId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_Cat_Id",
                table: "products",
                column: "Cat_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderproduct");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "Cashier");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "brands");
        }
    }
}
