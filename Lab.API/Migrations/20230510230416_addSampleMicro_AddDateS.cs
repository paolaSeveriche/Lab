using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab.API.Migrations
{
    /// <inheritdoc />
    public partial class addSampleMicro_AddDateS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataSample",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectorDate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AdmissionDate = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Hours = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameR = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    temperature = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RecentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Smell = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Aspect = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSample", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataSample_Id",
                table: "DataSample",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataSample");
        }
    }
}
