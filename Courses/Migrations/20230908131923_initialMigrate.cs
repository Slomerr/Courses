using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cources.Migrations
{
    /// <inheritdoc />
    public partial class initialMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    course_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    course_name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_courses", x => x.course_id);
                });

            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new
                {
                    teacher_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    email = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_teachers", x => x.teacher_id);
                });

            migrationBuilder.CreateTable(
                name: "organizations",
                columns: table => new
                {
                    organization_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    inn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    teacher_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizations", x => x.organization_id);
                    table.ForeignKey(
                        name: "fk_organizations_teachers_teacher_id",
                        column: x => x.teacher_id,
                        principalTable: "teachers",
                        principalColumn: "teacher_id");
                });

            migrationBuilder.CreateTable(
                name: "study_groups",
                columns: table => new
                {
                    study_group_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    teacher_id = table.Column<int>(type: "int", nullable: false),
                    course_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_study_groups", x => x.study_group_id);
                    table.ForeignKey(
                        name: "fk_study_groups_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_study_groups_teachers_teacher_id",
                        column: x => x.teacher_id,
                        principalTable: "teachers",
                        principalColumn: "teacher_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    employee_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    organization_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employees", x => x.employee_id);
                    table.ForeignKey(
                        name: "fk_employees_organizations_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organizations",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee_study_group",
                columns: table => new
                {
                    employees_employee_id = table.Column<int>(type: "int", nullable: false),
                    study_groups_study_group_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employee_study_group", x => new { x.employees_employee_id, x.study_groups_study_group_id });
                    table.ForeignKey(
                        name: "fk_employee_study_group_employees_employees_employee_id",
                        column: x => x.employees_employee_id,
                        principalTable: "employees",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_employee_study_group_study_groups_study_groups_study_group_id",
                        column: x => x.study_groups_study_group_id,
                        principalTable: "study_groups",
                        principalColumn: "study_group_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_employee_study_group_study_groups_study_group_id",
                table: "employee_study_group",
                column: "study_groups_study_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_organization_id",
                table: "employees",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "ix_organizations_teacher_id",
                table: "organizations",
                column: "teacher_id");

            migrationBuilder.CreateIndex(
                name: "ix_study_groups_course_id",
                table: "study_groups",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_study_groups_teacher_id",
                table: "study_groups",
                column: "teacher_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employee_study_group");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "study_groups");

            migrationBuilder.DropTable(
                name: "organizations");

            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.DropTable(
                name: "teachers");
        }
    }
}
