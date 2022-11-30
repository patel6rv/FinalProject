using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Title", "Author", "Description", "PublishYear" },
                values: new object[] { 1, "Harry Potter", "J.K. Rowling", "Magic School", 1993 });

            /*
            migrationBuilder.InsertData(
                table: "CollegeStudents",
                columns: new[] { "Id", "FullName", "Birthdate", "CollegeProgram", "YearInProgram" },
                values: new object[] { 1, "Walter White", new DateTime(1958,9,7), "IT", "Freshman" });
            */

            migrationBuilder.InsertData(
                table: "CollegeStudents",
                columns: new[] { "Id", "FullName", "Birthdate", "CollegeProgram", "YearInProgram" },
                values: new object[,]{
                    { 1, "Elliott Phillips", new DateTime(2003,1,15), "IT", "Sophmore" },
                    { 2, "Ravi Patel", new DateTime(2002, 10, 12), "IT", "Sophmore" }
                });

            migrationBuilder.InsertData(
                table: "OlympicAthletes",
                columns: new[] { "Id", "AthleteName", "Country", "Sport", "MedalsWon" },
                values: new object[] { 1, "Usain Bolt", "Jamaica", "Track & Field", 8 });

            migrationBuilder.InsertData(
                table: "ProgrammingLanguages",
                columns: new[] { "Id", "Name", "Typed", "Execution", "Paradigm" },
                values: new object[] { 1, "Java", "Static", "Compiled", "Object Oriented" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CollegeStudents",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OlympicAthletes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
