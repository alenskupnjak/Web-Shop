using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace Movies.data.Migrations
{
	/// <inheritdoc />
	public partial class AdiminUser : Migration
	{
		const string ADMIN_USER_GUID = "4ca4f702-7bd9-4329-809b-a9113dda9e88";
		const string ADMIN_ROLE_GUID = "3b103e86-d790-4ce9-abf1-a9294e75f5ab";
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			var hasher = new PasswordHasher<IdentityUser>();
			var passwordhash = hasher.HashPassword(null, "Admin123!");

			StringBuilder sb = new StringBuilder();

			sb.AppendLine("INSERT INTO AspNetUsers (" +
				"Id, " +
				"UserName, " +
				"NormalizedUserName, " +
				"Email, " +
				"NormalizedEmail, " +
				"EmailConfirmed, " +
				"PasswordHash, " +
				"SecurityStamp, " +
				"PhoneNumber, " +
				"PhoneNumberConfirmed, " +
				"TwoFactorEnabled, " +
				"LockoutEnabled, " +
				"AccessFailedCount, " +
				"Address, " +
				"City, " +
				"Country, " +
				"FirstName, " +
				"LastName, " +
				"PostalCode)");

			sb.AppendLine("VALUES(");
			sb.AppendLine($"'{ADMIN_USER_GUID}',");
			sb.AppendLine($"'admin@admin.com',");
			sb.AppendLine($"'ADMIN@ADMIN.COM',");
			sb.AppendLine($"'admin@admin.com',");
			sb.AppendLine($"'ADMIN@ADMIN.COM',");
			sb.AppendLine($"0,");
			sb.AppendLine($"'{passwordhash}',");
			sb.AppendLine($"'Admin',");
			sb.AppendLine($"'+3859812345678',");
			sb.AppendLine($"1,");
			sb.AppendLine($"0,");
			sb.AppendLine($"0,");
			sb.AppendLine($"0,");
			sb.AppendLine($"'Trg Bana jelacica',");
			sb.AppendLine($"'Zagreb',");
			sb.AppendLine($"'Croatia',");
			sb.AppendLine($"'Marko',");
			sb.AppendLine($"'Markovic',");
			sb.AppendLine($"'10000'");
			sb.AppendLine(")");

			migrationBuilder.Sql(sb.ToString());
			migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{ADMIN_ROLE_GUID}', 'Admin', 'ADMIN')");
			migrationBuilder.Sql($"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('{ADMIN_USER_GUID}', '{ADMIN_ROLE_GUID}')");

		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql($"DELETE FROM AspNetUserRoles WHERE UserId = '{ADMIN_USER_GUID}' AND RoleId='{ADMIN_ROLE_GUID}'");
			migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id = '{ADMIN_ROLE_GUID}'");
			migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id = '{ADMIN_USER_GUID}'");
		}
	}
}
