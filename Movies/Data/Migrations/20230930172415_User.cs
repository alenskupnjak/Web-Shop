using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace Movies.data.Migrations
{
	/// <inheritdoc />
	public partial class User : Migration
	{
		const string USER_GUID = "f84447cf-8273-4f9e-bd70-37ec60e6a683";
		const string ROLE_GUID = "5741d4f6-6e97-4fd8-9f70-623653a81e14";
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			var hasher = new PasswordHasher<IdentityUser>();
			var passwordhash = hasher.HashPassword(null, "User123!");

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
			sb.AppendLine($"'{USER_GUID}',");
			sb.AppendLine($"'user@user.com',");
			sb.AppendLine($"'USER@USER.COM',");
			sb.AppendLine($"'user@user.com',");
			sb.AppendLine($"'USER@USER.COM',");
			sb.AppendLine($"0,");
			sb.AppendLine($"'{passwordhash}',");
			sb.AppendLine($"'User',");
			sb.AppendLine($"'+385981234888',");
			sb.AppendLine($"1,");
			sb.AppendLine($"0,");
			sb.AppendLine($"0,");
			sb.AppendLine($"0,");
			sb.AppendLine($"'Trg Bana jelacica',");
			sb.AppendLine($"'Dubrovnik',");
			sb.AppendLine($"'Croatia',");
			sb.AppendLine($"'Ivan',");
			sb.AppendLine($"'Ivkovic',");
			sb.AppendLine($"'10000'");
			sb.AppendLine(")");

			migrationBuilder.Sql(sb.ToString());
			migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('{ROLE_GUID}', 'User', 'USER')");
			migrationBuilder.Sql($"INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES ('{USER_GUID}', '{ROLE_GUID}')");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql($"DELETE FROM AspNetUserRoles WHERE UserId = '{USER_GUID}' AND RoleId='{ROLE_GUID}'");
			migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id = '{ROLE_GUID}'");
			migrationBuilder.Sql($"DELETE FROM AspNetUsers WHERE Id = '{USER_GUID}'");
		}
	}
}
